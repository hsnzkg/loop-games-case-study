using UnityEngine;
using UnityEngine.UI;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.ScratchData
{
    public class ImageData : BaseData
    {
        private readonly Image image;
        private readonly bool isCanvasOverlay;

        public override Vector2 GetTextureSize()
        {
            return image.sprite.rect.size;
        }

        protected override Vector2 GetBounds()
        {
            return image.rectTransform.rect.size;
        }

        public ImageData(Transform surface, Camera camera) : base(surface, camera)
        {
            if (surface.TryGetComponent(out image))
            {
                isCanvasOverlay = image.canvas.renderMode == RenderMode.ScreenSpaceOverlay;
                InitTriangle();
            }
        }

        public override Vector2 GetScratchPosition(Vector2 position)
        {
            if (isCanvasOverlay)
            {
                Vector2 scratchPosition = Vector2.zero;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle((RectTransform)GetSurface(), position, null, out Vector3 worldPosition))
                {
                    Vector3 pointLocal = GetSurface().InverseTransformPoint(worldPosition);
                    Vector2 uv = GetTriangle().GetUV(pointLocal);
                    scratchPosition = Vector2.Scale(GetTextureSize(), uv);
                }
                return scratchPosition;
            }
            return base.GetScratchPosition(position);
        }
    }
}