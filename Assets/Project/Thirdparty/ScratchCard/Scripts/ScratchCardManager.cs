using System;
using System.Collections.Generic;
using Project.Thirdparty.ScratchCard.Scripts.Core;
using Project.Thirdparty.ScratchCard.Scripts.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Thirdparty.ScratchCard.Scripts
{
	/// <summary>
	/// ScratchCardManager component manages ScratchCard and EraseProgress components
	/// </summary>
	[ExecuteInEditMode]
	public class ScratchCardManager : MonoBehaviour
	{
		#region General

		public ScratchCard Card;
		public EraseProgress Progress;
		public ScratchCardRenderType RenderType;
		[Obsolete("This field is obsolete, use MeshRendererCard property instead", true)] public GameObject MeshCard;
		[Obsolete("This field is obsolete, use SpriteRendererCard property instead", true)] public GameObject SpriteCard;
		[Obsolete("This field is obsolete, use CanvasRendererCard property instead", true)] public GameObject ImageCard;

		[SerializeField] private MeshRenderer meshRendererCard;
        
        public MeshRenderer GetMeshRendererCard()
        {
            return meshRendererCard;
        }

        public void SetMeshRendererCard(MeshRenderer value)
        {
            meshRendererCard = value;
        }
		
		[SerializeField] private SpriteRenderer spriteRendererCard;
		public SpriteRenderer SpriteRendererCard
		{
			get => spriteRendererCard;
			set => spriteRendererCard = value;
		}
		
		[SerializeField] private Image canvasRendererCard;
		public Image CanvasRendererCard
		{
			get => canvasRendererCard;
			set => canvasRendererCard = value;
		}

		[FormerlySerializedAs("ScratchSurfaceSpriteHasAlpha")] [SerializeField] private bool scratchSurfaceSpriteHasAlpha = true;
        public bool GetScratchSurfaceSpriteHasAlpha()
        {
            return scratchSurfaceSpriteHasAlpha;
        }

        public void SetScratchSurfaceSpriteHasAlpha(bool value)
        {
            scratchSurfaceSpriteHasAlpha = value;
            if (Progress != null)
            {
                Progress.SetSampleSourceTexture(scratchSurfaceSpriteHasAlpha);
            }
        }

		#endregion

		#region ScratchCard Parameters

		[FormerlySerializedAs("Mode")] [SerializeField] private ScratchMode mode;
        public ScratchMode GetMode()
        {
            return mode;
        }

        public void SetMode(ScratchMode value)
        {
            mode = value;
            if (Card != null)
            {
                Card.SetMode(mode);
            }
        }

		[FormerlySerializedAs("MainCamera")] [SerializeField] private Camera mainCamera;
		public Camera MainCamera
		{
			get => mainCamera;
			set
			{
				mainCamera = value;
				if (Card != null && Card.GetScratchData() != null)
				{
					Card.GetScratchData().Camera = mainCamera;
				}
			}
		}

		[FormerlySerializedAs("ScratchSurfaceSprite")] [SerializeField] private Sprite scratchSurfaceSprite;
		public Sprite ScratchSurfaceSprite
		{
			get => scratchSurfaceSprite;
			set
			{
				scratchSurfaceSprite = value;
				if (Card != null)
				{
					if (Application.isPlaying)
					{
						if (initialized)
						{
							UpdateCardSprite(scratchSurfaceSprite);
							Card.SetRenderType(RenderType, mainCamera);
							Card.Init();
							if (Progress != null)
							{
								Progress.ResetProgress();
								Progress.UpdateProgress();
							}
						}
					}
					else
					{
						InitSurfaceMaterial();
					}
				}
			}
		}
		
		[SerializeField] private ProgressAccuracy progressAccuracy;
		public ProgressAccuracy ProgressAccuracy
		{
			get => progressAccuracy;
			set
			{
				progressAccuracy = value;
				if (Progress != null && Progress.GetProgressMaterial() != null)
				{
					Progress.SetProgressAccuracy(progressAccuracy);
				}
			}
		}

		[FormerlySerializedAs("EraseTexture")] [SerializeField] private Texture brushTexture;
		public Texture BrushTexture
		{
			get => brushTexture;
			set
			{
				brushTexture = value;
				if (Card != null && Card.BrushMaterial != null)
				{
					Card.BrushMaterial.mainTexture = brushTexture;
				}
			}
		}
		
		[FormerlySerializedAs("EraseTextureScale")] [SerializeField] private float brushSize = 1f;
        public void SetBrushSize(float size)
        {
            brushSize = size;
            if (Card != null && Card.Initialized)
            {
                Card.BrushSize = brushSize;
            }
        }

        public float GetBrushSize()
        {
            return brushSize;
        }
		
		[SerializeField] private float brushOpacity = 1f;
        
        public float GetBrushOpacity()
        {
            return brushOpacity;
        }

        public void SetBrushOpacity(float value)
        {
            brushOpacity = value;
            if (Card != null && Card.BrushMaterial != null)
            {
                var c = Card.BrushMaterial.color;
                Card.BrushMaterial.color = new Color(c.r, c.g, c.b, brushOpacity);
            }
        }

		#endregion

		#region Input
		
		[FormerlySerializedAs("InputEnabled")] [SerializeField] private bool inputEnabled = true;
		public bool InputEnabled
		{
			get => inputEnabled;
			set
			{
				inputEnabled = value;
				Card.enabled = inputEnabled;
			}
		}

		[SerializeField] private bool usePressure;
		public bool UsePressure
		{
			get => usePressure;
			set
			{
				usePressure = value;
				if (Card != null && Card.Initialized)
				{
					Card.GetInput().UsePressure = usePressure;
				}
			}
		}
		
		[SerializeField] private bool checkCanvasRaycasts = true;
		public bool CheckCanvasRaycasts
		{
			get => checkCanvasRaycasts;
			set
			{
				checkCanvasRaycasts = value;
				if (Card != null && Card.Initialized)
				{
					Card.GetInput().CheckCanvasRaycasts = checkCanvasRaycasts;
					if (checkCanvasRaycasts)
					{
						Card.GetInput().InitRaycastsController(Card.SurfaceTransform.gameObject, canvasesForRaycastsBlocking);
					}
				}
			}
		}

		[SerializeField] private Canvas[] canvasesForRaycastsBlocking;
		public Canvas[] CanvasesForRaycastsBlocking
		{
			get => canvasesForRaycastsBlocking;
			set
			{
				canvasesForRaycastsBlocking = value;
				if (Card != null && Card.Initialized)
				{
					Card.GetInput().InitRaycastsController(Card.SurfaceTransform.gameObject, canvasesForRaycastsBlocking);
				}
			}
		}
		
		#endregion

		#region Shaders
		
		[FormerlySerializedAs("MaskShader")] [SerializeField] private Shader maskShader;
		[FormerlySerializedAs("BrushShader")] [SerializeField] private Shader brushShader;
		[FormerlySerializedAs("MaskProgressShader")] [SerializeField] private Shader maskProgressShader;
		
		#endregion

		private Material surfaceMaterial;
		private Texture2D scratchTexture;
		private Color[] spritePixels;
		private Sprite scratchSprite;
		private MigrationHelper migrationHelper;
		private bool initialized;

		private void Awake()
		{
			if (!Application.isPlaying)
			{
				migrationHelper = new MigrationHelper();
				migrationHelper.StartMigrate(this);
				InitSurfaceMaterial();
			}
		}

		private void Start()
		{
			if (!Application.isPlaying)
			{
				migrationHelper.FinishMigrate();
				migrationHelper = null;
				return;
			}

			if (initialized)
				return;
			
			Init();
		}

		private void OnDestroy()
		{
			if (surfaceMaterial != null)
			{
				if (Application.isPlaying)
				{
					Destroy(surfaceMaterial);
				}
				else
				{
					DestroyImmediate(surfaceMaterial);
				}
				surfaceMaterial = null;
			}
			
			if (!Application.isPlaying)
				return;

			if (Card != null)
			{
				Card.OnInitialized -= OnCardInitialized;
				Card.OnRenderTextureInitialized -= OnCardRenderTextureInitialized;
			}
			ReleaseTexture();
		}

		public void Init()
		{
			if (Card == null)
			{
				Debug.LogError("ScratchCard field is not assigned!");
				return;
			}
			
			if (mainCamera == null)
			{
				mainCamera = mainCamera != null ? mainCamera : Camera.main;
			}

			InitSurfaceMaterial();
			InitBrushMaterial();
			InitProgressMaterial();
			if (TrySelectCard(RenderType))
			{
				Card.BrushSize = GetBrushSize();
				Card.SetMode(mode);
				Card.SetRenderType(RenderType, mainCamera);
				Card.OnInitialized -= OnCardInitialized;
				Card.OnInitialized += OnCardInitialized;
				Card.OnRenderTextureInitialized -= OnCardRenderTextureInitialized;
				Card.OnRenderTextureInitialized += OnCardRenderTextureInitialized;
				Card.Init();
			}
			else
			{
				Card.enabled = false;
			}
			
			if (Card.GetMode() == ScratchMode.Restore)
			{
				Card.Fill(false);
			}
			
			initialized = true;
		}
		
		private void OnCardInitialized(ScratchCard scratchCard)
		{
			scratchCard.GetInput().UsePressure = usePressure;
			scratchCard.GetInput().CheckCanvasRaycasts = checkCanvasRaycasts;
			if (checkCanvasRaycasts)
			{
				scratchCard.GetInput().InitRaycastsController(scratchCard.SurfaceTransform.gameObject, canvasesForRaycastsBlocking);
			}
		}

		private void OnCardRenderTextureInitialized(RenderTexture renderTexture)
		{
			if (Progress != null && Progress.GetProgressMaterial() != null)
			{
				Progress.GetProgressMaterial().mainTexture = renderTexture;
			}
		}
		
		private void ReleaseTexture()
		{
			if (scratchTexture != null)
			{
				Destroy(scratchTexture);
				scratchTexture = null;
			}

			if (scratchSprite != null)
			{
				Destroy(scratchSprite);
				scratchSprite = null;
			}
		}

		public void InitSurfaceMaterial()
		{
			if (Card != null && Card.SurfaceMaterial == null)
			{
				var scratchSurfaceMaterial = new Material(maskShader);
				Card.SurfaceMaterial = scratchSurfaceMaterial;
				surfaceMaterial = scratchSurfaceMaterial;
			}
			UpdateCardSprite(scratchSurfaceSprite);
		}

		private void UpdateCardSprite(Sprite sprite)
		{
			ReleaseTexture();
			var scratchSurfaceMaterial = Card.SurfaceMaterial;
			var isPartOfAtlas = sprite != null && (sprite.texture.width != sprite.rect.size.x || sprite.texture.height != sprite.rect.size.y);
			if (Application.isPlaying)
			{
				if (isPartOfAtlas || scratchSurfaceSpriteHasAlpha)
				{
					if (sprite.texture.isReadable)
					{
						if (sprite.packed)
						{
							spritePixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.rect.width, (int)sprite.rect.height);
						}
						else
						{
							spritePixels = sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
						}
					}
					else
					{
						Debug.LogError("Texture is not readable, please set Read/Write flag in texture settings.");
					}
				}

				if (isPartOfAtlas)
				{
					scratchTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
					scratchTexture.SetPixels(spritePixels);
					scratchTexture.Apply();
					
					if (scratchSurfaceMaterial != null)
					{
						scratchSurfaceMaterial.mainTexture = scratchTexture;
					}

					if (RenderType == ScratchCardRenderType.SpriteRenderer || RenderType == ScratchCardRenderType.CanvasRenderer)
					{
						var croppedRect = new Rect(0, 0, scratchTexture.width, scratchTexture.height);
						var pivot = scratchSurfaceSprite.pivot / croppedRect.size;
						scratchSprite = Sprite.Create(scratchTexture, croppedRect, pivot, Constants.General.PixelsPerUnit);
						sprite = scratchSprite;
					}
				}
				else if (scratchSurfaceMaterial != null && scratchSurfaceSprite != null)
				{
					scratchSurfaceMaterial.mainTexture = scratchSurfaceSprite.texture;
				}
				UpdateProgressMaterial();
			}
			else if (Card.SurfaceMaterial != null && scratchSurfaceSprite != null)
			{
				Card.SurfaceMaterial.mainTexture = scratchSurfaceSprite.texture;
			}

			UpdateCardOffset();

			if (RenderType == ScratchCardRenderType.MeshRenderer && meshRendererCard != null)
			{
				if (Card.SurfaceMaterial != null)
				{
					meshRendererCard.sharedMaterial = Card.SurfaceMaterial;
				}
				if (Card.SurfaceMaterial != null && scratchSurfaceSprite != null)
				{
					Card.SurfaceMaterial.mainTexture = scratchSurfaceSprite.texture;
				}
			}
			
			if (RenderType == ScratchCardRenderType.SpriteRenderer && SpriteRendererCard != null)
			{
				if (Card.SurfaceMaterial != null)
				{
					SpriteRendererCard.sharedMaterial = Card.SurfaceMaterial;
				}
				if (sprite != null)
				{
					SpriteRendererCard.sprite = sprite;
				}
			}
			
			if (RenderType == ScratchCardRenderType.CanvasRenderer && CanvasRendererCard != null)
			{
				if (Card.SurfaceMaterial != null)
				{
					CanvasRendererCard.material = Card.SurfaceMaterial;
				}
				if (sprite != null)
				{
					CanvasRendererCard.sprite = sprite;
				}
			}
		}

		private void UpdateCardOffset()
		{
			if (Card.SurfaceMaterial != null)
			{
				if (RenderType == ScratchCardRenderType.MeshRenderer && scratchSurfaceSprite != null)
				{
					var offset = new Vector4(
						scratchSurfaceSprite.textureRect.min.x / scratchSurfaceSprite.texture.width,
						scratchSurfaceSprite.textureRect.min.y / scratchSurfaceSprite.texture.height,
						scratchSurfaceSprite.textureRect.width / scratchSurfaceSprite.texture.width,
						scratchSurfaceSprite.textureRect.height / scratchSurfaceSprite.texture.height);
					Card.SurfaceMaterial.SetVector(Constants.MaskShader.Offset, offset);
				}
				else
				{
					Card.SurfaceMaterial.SetVector(Constants.MaskShader.Offset, new Vector4(0, 0, 1, 1));
				}
			}
		}

		private void InitBrushMaterial()
		{
			if (Card.BrushMaterial == null)
			{
				Card.BrushMaterial = new Material(brushShader);
			}
			Card.BrushMaterial.mainTexture = brushTexture;
			Card.BrushMaterial.color = new Color(1, 1, 1, brushOpacity);
		}

		private void InitProgressMaterial()
		{
			if (Progress == null)
				return;
			
			if (Progress.GetProgressMaterial() == null)
			{
				var progressMaterial = new Material(maskProgressShader);
				Progress.SetProgressMaterial(progressMaterial);
				Progress.SetSampleSourceTexture(scratchSurfaceSpriteHasAlpha);
			}
            Progress.SetProgressAccuracy(progressAccuracy);
			SetProgressSourceTexture();
		}

		private void SetProgressSourceTexture()
		{
			if (scratchSurfaceSpriteHasAlpha)
			{
				if (scratchTexture != null)
				{
					Progress.GetProgressMaterial().SetTexture(Constants.ProgressShader.SourceTexture, scratchTexture);
				}
				else if (scratchSurfaceSprite != null)
				{
					Progress.GetProgressMaterial().SetTexture(Constants.ProgressShader.SourceTexture, scratchSurfaceSprite.texture);
				}
			}
		}

		private void UpdateProgressMaterial()
		{
			if (Progress != null)
			{
				if (Progress.GetProgressMaterial() != null)
				{
					SetProgressSourceTexture();
				}

				if (Application.isPlaying && spritePixels != null)
				{
					Progress.SetSpritePixels(spritePixels);
					spritePixels = null;
				}
			}
		}
		
		#region Public Methods

		/// <summary>
		/// Fills RenderTexture with white color (100% scratched surface)
		/// </summary>
		public void FillScratchCard()
		{
			Card.Fill(false);
			if (Progress != null)
			{
				Progress.UpdateProgress();
			}
		}

		/// <summary>
		/// Fills ScratchCard RenderTexture with clear color (0% scratched surface)
		/// </summary>
		public void ClearScratchCard()
		{
			Card.Clear(false);
			if (Progress != null)
			{
				Progress.UpdateProgress();
			}
		}

		public bool TrySelectCard(ScratchCardRenderType renderType)
		{
			RenderType = renderType;
			var cards = new Dictionary<ScratchCardRenderType, Component>
			{
				{ ScratchCardRenderType.MeshRenderer, meshRendererCard },
				{ ScratchCardRenderType.SpriteRenderer, spriteRendererCard },
				{ ScratchCardRenderType.CanvasRenderer, canvasRendererCard }
			};
			foreach (var card in cards)
			{
				var isActive = card.Key == RenderType;
				if (card.Value != null)
				{
					card.Value.gameObject.SetActive(isActive);
					if (isActive)
					{
						Card.SurfaceTransform = card.Value.transform;
					}
				}
				else if (isActive)
				{
					Debug.LogError($"{card.Key} Card is null! Assign reference to the component in Inspector");
					return false;
				}
			}
			return true;
		}

		public void SetNativeSize()
		{
			var cardRenderType = RenderType;
			if (cardRenderType == ScratchCardRenderType.MeshRenderer)
			{
				if (GetMeshRendererCard() != null && GetMeshRendererCard().sharedMaterial != null && GetMeshRendererCard().sharedMaterial.mainTexture != null)
				{
					float width, height;
					if (scratchSurfaceSprite != null)
					{
						width = scratchSurfaceSprite.rect.width;
						height = scratchSurfaceSprite.rect.height;
					}
					else
					{
						var texture = GetMeshRendererCard().sharedMaterial.mainTexture;
						width = texture.width;
						height = texture.height;
					}

					var meshCardTransform = GetMeshRendererCard().transform;
					meshCardTransform.localScale = new Vector3(width / Constants.General.PixelsPerUnit, 
						height / Constants.General.PixelsPerUnit, meshCardTransform.localScale.z);
				}
			}
			else if (cardRenderType == ScratchCardRenderType.SpriteRenderer)
			{
				if (SpriteRendererCard != null)
				{
					SpriteRendererCard.transform.localScale = Vector3.one;
				}
			}
			else if (cardRenderType == ScratchCardRenderType.CanvasRenderer)
			{
				if (CanvasRendererCard != null)
				{
					CanvasRendererCard.SetNativeSize();
				}
			}
		}

		#endregion
	}
}