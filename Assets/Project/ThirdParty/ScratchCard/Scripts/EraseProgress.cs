using System;
using System.Collections;
using Project.ThirdParty.ScratchCard.Scripts.Core;
using Project.ThirdParty.ScratchCard.Scripts.Tools;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Project.ThirdParty.ScratchCard.Scripts
{
	/// <summary>
	/// Calculates scratching progress in range from 0 to 1, where 0 - card scratched completely, 1 - scratch surface is whole
	/// </summary>
	public class EraseProgress : MonoBehaviour
	{
		#region Events

		public event Action<float> OnProgress;
		public event Action<float> OnCompleted;

		#endregion

		#region Variables

		[SerializeField] private ScratchCard card;

        public ScratchCard GetCard() => card;

        public void SetCard(ScratchCard value) => card = value;

        [SerializeField] private Material progressMaterial;

        public Material GetProgressMaterial() => progressMaterial;

        public void SetProgressMaterial(Material value) => progressMaterial = value;

        [SerializeField] private bool sampleSourceTexture;

        public bool GetSampleSourceTexture() => sampleSourceTexture;

        public void SetSampleSourceTexture(bool value) => sampleSourceTexture = value;

        [SerializeField] private ProgressAccuracy progressAccuracy;

        public ProgressAccuracy GetProgressAccuracy()
        {
            return progressAccuracy;
        }
        public void SetProgressAccuracy(ProgressAccuracy  accuracy)
        {
            progressAccuracy = accuracy;
            UpdateAccuracy();
            if (progressAccuracy == ProgressAccuracy.Default)
            {
                updateProgress = false;
                if (pixelsBuffer.IsCreated)
                {
                    pixelsBuffer.Dispose(default);
                }
            }
        }

		private ScratchMode scratchMode;
		private NativeArray<byte> pixelsBuffer;
		private int asyncGPUReadbackFrame;
		private int updateProgressFrame;
		private Color[] sourceSpritePixels;
		private CommandBuffer commandBuffer;
		private Mesh mesh;
		private RenderTexture percentRenderTexture;
		private RenderTargetIdentifier percentTargetIdentifier;
		private Rect percentTextureRect;
		private Texture2D progressTexture;
		private float progress;
		private int bitsPerPixel = 1;
		private bool updateProgress;
		private bool isCalculating;
		private bool isCompleted;

		#endregion

		#region MonoBehaviour Methods

		private void Start()
		{
			Init();
		}

		private void OnDestroy()
		{
			if (pixelsBuffer.IsCreated)
			{
				pixelsBuffer.Dispose(default);
			}

			if (percentRenderTexture != null && percentRenderTexture.IsCreated())
			{
				percentRenderTexture.Release();
				Destroy(percentRenderTexture);
				percentRenderTexture = null;
			}
			
			if (progressTexture != null)
			{
				Destroy(progressTexture);
				progressTexture = null;
			}

			if (mesh != null)
			{
				Destroy(mesh);
				mesh = null;
			}

			if (commandBuffer != null)
			{
				commandBuffer.Release();
				commandBuffer = null;
			}

			if (card != null)
			{
				card.OnRenderTextureInitialized -= OnCardRenderTextureInitialized;
			}
		}

		private void LateUpdate()
		{
			if (card.GetMode() != scratchMode)
			{
				scratchMode = card.GetMode();
				ResetProgress();
			}
			
			if ((card.GetIsScratched() || updateProgress) && !isCompleted)
			{
				UpdateProgress();
			}
		}

		#endregion

		#region Private Methods

		private void Init()
		{
			if (card == null)
			{
				Debug.LogError("Card field is not assigned!");
				enabled = false;
				return;
			}
			
			if (card.Initialized)
			{
				OnCardRenderTextureInitialized(card.GetRenderTexture());
			}
			
			card.OnRenderTextureInitialized += OnCardRenderTextureInitialized;
			UpdateAccuracy();
			scratchMode = card.GetMode();
			commandBuffer = new CommandBuffer {name = "EraseProgress"};
			mesh = MeshGenerator.GenerateQuad(Vector3.one, Vector3.zero);
			percentRenderTexture = new RenderTexture(1, 1, 0, RenderTextureFormat.ARGB32);
			percentTargetIdentifier = new RenderTargetIdentifier(percentRenderTexture);
			percentTextureRect = new Rect(0, 0, percentRenderTexture.width, percentRenderTexture.height);
			progressTexture = new Texture2D(percentRenderTexture.width, percentRenderTexture.height, TextureFormat.ARGB32, false, true);
		}
		
		private void OnCardRenderTextureInitialized(RenderTexture renderTexture)
		{
			bitsPerPixel = renderTexture.format == RenderTextureFormat.R8 ? 1 : 4;
		}

		private void UpdateAccuracy()
		{
		}

		/// <summary>
		/// Calculates scratch progress
		/// </summary>
		private IEnumerator CalcProgress()
		{
			if (!isCompleted && !isCalculating)
			{
				isCalculating = true;
				if (progressAccuracy == ProgressAccuracy.Default)
				{
					var prevRenderTexture = RenderTexture.active;
					RenderTexture.active = percentRenderTexture;
					progressTexture.ReadPixels(percentTextureRect, 0, 0);
					progressTexture.Apply();
					RenderTexture.active = prevRenderTexture;
					var pixel = progressTexture.GetPixel(0, 0);
					progress = pixel.r;
				}
				
				OnProgress?.Invoke(progress);
				if (OnCompleted != null)
				{
					var completeValue = card.GetMode() == ScratchMode.Erase ? 1f : 0f;
					if (Mathf.Abs(progress - completeValue) < float.Epsilon)
					{
						OnCompleted?.Invoke(progress);
						isCompleted = true;
					}
				}
				isCalculating = false;
			}
            yield return null;
        }
		
		#endregion
		
		#region Public Methods

		public float GetProgress()
		{
			return progress;
		}

		public void UpdateProgress()
		{
			if (commandBuffer == null)
			{
				Debug.LogError("Can't update progress cause commandBuffer is null!");
				return;
			}
			GL.LoadOrtho();
			commandBuffer.Clear();
			commandBuffer.SetRenderTarget(percentTargetIdentifier);
			commandBuffer.ClearRenderTarget(false, true, Color.clear);
			var pass = sampleSourceTexture ? 1 : 0;
			commandBuffer.DrawMesh(mesh, Matrix4x4.identity, progressMaterial, 0, pass);
			Graphics.ExecuteCommandBuffer(commandBuffer);
			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(CalcProgress());
			}
		}

		public void ResetProgress()
		{
			isCompleted = false;
		}

		public void SetSpritePixels(Color[] pixels)
		{
			sourceSpritePixels = pixels;
		}

		#endregion
	}
}