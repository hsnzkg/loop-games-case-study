    using System;
    using Project.ThirdParty.ScratchCard.Scripts.Core;
    using Project.ThirdParty.ScratchCard.Scripts.Core.InputData;
    using Project.ThirdParty.ScratchCard.Scripts.Core.ScratchData;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UnityEngine.Serialization;

    namespace Project.ThirdParty.ScratchCard.Scripts
    {
	    /// <summary>
	    /// Creates and configures RenderTexture, gets data from the InputController, then renders the quads into RenderTexture
	    /// </summary>
	    public class ScratchCard : MonoBehaviour
	    {
		    #region Events

		    public event Action<ScratchCard> OnInitialized;
		    public event Action<RenderTexture> OnRenderTextureInitialized;
		    public event Action<Vector2, float> OnScratchHole;
		    public event Action<Vector2, float> OnScratchHoleSucceed;
		    public event Action<Vector2, float, Vector2, float> OnScratchLine;
		    public event Action<Vector2, float, Vector2, float> OnScratchLineSucceed;

		    #endregion
		    
		    [FormerlySerializedAs("Surface")] public Transform SurfaceTransform;
		    [FormerlySerializedAs("ScratchSurface")] public Material SurfaceMaterial;
		    [FormerlySerializedAs("Eraser")] public Material BrushMaterial;
		    [Min(0.001f)] public float BrushSize = 1f;
		    public Quality RenderTextureQuality = Quality.High;

            public RenderTexture GetRenderTexture()
            {
                return m_renderTexture;
            }

            private void SetRenderTexture(RenderTexture value)
            {
                m_renderTexture = value;
            }

            public RenderTargetIdentifier GetRenderTarget()
            {
                return m_renderTarget;
            }

            private void SetRenderTarget(RenderTargetIdentifier value)
            {
                m_renderTarget = value;
            }

            [SerializeField] private ScratchMode mode = ScratchMode.Erase;

            public ScratchMode GetMode()
            {
                return mode;
            }

            public void SetMode(ScratchMode value)
            {
                mode = value;
                if (BrushMaterial != null)
                {
                    int blendOp = mode == ScratchMode.Erase ? (int)BlendOp.Add : (int)BlendOp.ReverseSubtract;
                    BrushMaterial.SetInt(Constants.BrushShader.BlendOpShaderParam, blendOp);
                }
            }

            public bool GetIsScratched()
            {
                if (cardRenderer != null)
                {
                    return cardRenderer.IsScratched;
                }

                return false;
            }

            public void SetIsScratched(bool value)
            {
                cardRenderer.IsScratched = value;
            }

            public bool GetIsScratching()
            {
                return GetInput().GetIsScratching();
            }

            public bool GetInitialized()
            {
                return initialized;
            }

            public BaseData GetScratchData()
            {
                return m_scratchData;
            }

            private void SetScratchData(BaseData value)
            {
                m_scratchData = value;
            }

            public ScratchCardInput GetInput()
            {
                return m_input;
            }

            private void SetInput(ScratchCardInput value)
            {
                m_input = value;
            }

            private ScratchCardRenderer cardRenderer;
		    private bool initialized;
            private RenderTexture m_renderTexture;
            private RenderTargetIdentifier m_renderTarget;
            private BaseData m_scratchData;
            private ScratchCardInput m_input;

            #region MonoBehaviour Methods

		    private void Start()
		    {
                if (initialized)
                {
                    return;
                }

			    Init();
		    }

		    private void OnDisable()
		    {
                if (!initialized)
                {
                    return;
                }
			    
			    GetInput().ResetData();
		    }

		    private void OnDestroy()
		    {
			    UnsubscribeFromEvents();
			    ReleaseRenderTexture();
			    cardRenderer?.Release();
		    }

		    private void Update()
		    {
			    if (!GetInput().TryUpdate())
			    {
				    cardRenderer.IsScratched = false;
			    }
		    }

		    #endregion

		    #region Initializaion

		    public void Init()
		    {
			    if (GetScratchData() == null)
			    {
				    Debug.LogError("ScratchData is null!");
				    enabled = false;
				    return;
			    }
			    
			    UnsubscribeFromEvents();
                ScratchCardInput input = new ScratchCardInput(GetIsScratched);
			    SetInput(input);
			    SubscribeToEvents();
			    cardRenderer?.Release();
			    cardRenderer = new ScratchCardRenderer(this);
			    ReleaseRenderTexture();
			    CreateRenderTexture();
			    cardRenderer.FillRenderTextureWithColor(Color.clear);
			    initialized = true;
			    OnInitialized?.Invoke(this);
		    }

		    public void SetRenderType(ScratchCardRenderType renderType, Camera mainCamera)
		    {
			    if (renderType == ScratchCardRenderType.MeshRenderer)
			    {
				    SetScratchData(new MeshRendererData(SurfaceTransform, mainCamera));
			    }
			    else if (renderType == ScratchCardRenderType.SpriteRenderer)
			    {
				    SetScratchData(new SpriteRendererData(SurfaceTransform, mainCamera));
			    }
			    else
			    {
				    SetScratchData(new ImageData(SurfaceTransform, mainCamera));
			    }
		    }

		    private void SubscribeToEvents()
		    {
			    UnsubscribeFromEvents();
			    GetInput().OnScratch += GetScratchData().GetScratchPosition;
			    GetInput().OnScratchHole += TryScratchHole;
			    GetInput().OnScratchLine += TryScratchLine;
		    }
		    
		    private void UnsubscribeFromEvents()
		    {
                if (GetInput() == null)
                {
                    return;
                }
			    
			    GetInput().OnScratch -= GetScratchData().GetScratchPosition;
			    GetInput().OnScratchHole -= TryScratchHole;
			    GetInput().OnScratchLine -= TryScratchLine;
		    }

		    /// <summary>
		    /// Creates RenderTexture
		    /// </summary>
		    private void CreateRenderTexture()
		    {
			    float qualityRatio = (float)RenderTextureQuality;
			    Vector2 renderTextureSize = new Vector2(GetScratchData().GetTextureSize().x / qualityRatio, GetScratchData().GetTextureSize().y / qualityRatio);
			    RenderTextureFormat renderTextureFormat = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8) ? RenderTextureFormat.R8 : RenderTextureFormat.ARGB32;
			    SetRenderTexture(new RenderTexture((int)renderTextureSize.x, (int)renderTextureSize.y, 0, renderTextureFormat));
			    SurfaceMaterial.SetTexture(Constants.MaskShader.MaskTexture, GetRenderTexture());
			    SetRenderTarget(new RenderTargetIdentifier(GetRenderTexture()));
			    OnRenderTextureInitialized?.Invoke(GetRenderTexture());
		    }
		    
		    private void ReleaseRenderTexture()
		    {
			    if (GetRenderTexture() != null && GetRenderTexture().IsCreated())
			    {
				    GetRenderTexture().Release();
				    Destroy(GetRenderTexture());
				    SetRenderTexture(null);
			    }
		    }

		    #endregion

		    #region Scratching

		    private void OnScratchStart()
		    {
			    cardRenderer.IsScratched = false;
		    }

		    private void TryScratchHole(Vector2 position, float pressure)
		    {
			    cardRenderer.ScratchHole(position, pressure);
			    Vector2 localPosition = GetScratchData().GetLocalPosition(position);
			    OnScratchHole?.Invoke(localPosition, pressure);
			    if (GetIsScratched())
			    {
				    OnScratchHoleSucceed?.Invoke(localPosition, pressure);
			    }
		    }

		    private void TryScratchLine(Vector2 startPosition, float startPressure, Vector2 endPosition, float endPressure)
		    {
			    cardRenderer.ScratchLine(startPosition, endPosition, startPressure, endPressure);
			    Vector2 startLocalPosition = GetScratchData().GetLocalPosition(startPosition);
			    Vector2 endLocalPosition = GetScratchData().GetLocalPosition(endPosition);
			    OnScratchLine?.Invoke(startLocalPosition, startPressure, endLocalPosition, endPressure);
			    if (GetIsScratched())
			    {
				    OnScratchLineSucceed?.Invoke(startLocalPosition, startPressure, endLocalPosition, endPressure);
			    }
		    }

		    #endregion
		    
		    #region Public Methods


		    public void Fill(bool setIsScratched = true)
		    {
			    cardRenderer.FillRenderTextureWithColor(Color.white);
			    if (setIsScratched)
			    {
				    SetIsScratched(true);
			    }
		    }

		    public void FillInstantly()
		    {
			    Fill();
		    }
            
		    public void Clear(bool setIsScratched = true)
		    {
			    cardRenderer.FillRenderTextureWithColor(Color.clear);
			    if (setIsScratched)
			    {
				    SetIsScratched(true);
			    }
		    }

		    public void ClearInstantly()
		    {
			    Clear();
		    }
            
		    public void ResetRenderTexture()
		    {
			    ReleaseRenderTexture();
			    CreateRenderTexture();
			    cardRenderer.FillRenderTextureWithColor(Color.clear);
			    SetIsScratched(true);
		    }
            
		    public void ScratchHole(Vector2 position, float pressure = 1f)
		    {
			    cardRenderer.ScratchHole(position, pressure);
			    Vector2 localPosition = GetScratchData().GetLocalPosition(position);
			    OnScratchHole?.Invoke(localPosition, pressure);
			    if (GetIsScratched())
			    {
				    OnScratchHoleSucceed?.Invoke(localPosition, pressure);
			    }
		    }


		    public void ScratchLine(Vector2 startPosition, Vector2 endPosition, float startPressure = 1f, float endPressure = 1f)
		    {
			    cardRenderer.ScratchLine(startPosition, endPosition, startPressure, endPressure);
			    Vector2 startLocalPosition = GetScratchData().GetLocalPosition(startPosition);
			    Vector2 endLocalPosition = GetScratchData().GetLocalPosition(endPosition);
			    OnScratchLine?.Invoke(startLocalPosition, startPressure, endLocalPosition, endPressure);
			    if (GetIsScratched())
			    {
				    OnScratchLineSucceed?.Invoke(startLocalPosition, startPressure, endLocalPosition, endPressure);	
			    }
		    }
            
		    public Texture2D GetScratchTexture()
		    {
			    RenderTexture previousRenderTexture = RenderTexture.active;
			    Texture2D texture2D = new Texture2D(GetRenderTexture().width, GetRenderTexture().height, TextureFormat.ARGB32, false);
			    RenderTexture.active = GetRenderTexture();
			    texture2D.ReadPixels(new Rect(0, 0, texture2D.width, texture2D.height), 0, 0, false);
			    texture2D.Apply();
			    RenderTexture.active = previousRenderTexture;
			    return texture2D;
		    }

		    #endregion
	    }
    }