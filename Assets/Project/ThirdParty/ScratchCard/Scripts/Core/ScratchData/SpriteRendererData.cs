using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.ScratchData
{
    public class SpriteRendererData : BaseData
    {
        private readonly SpriteRenderer renderer;

        public override Vector2 GetTextureSize()
        {
            return renderer.sprite.rect.size;
        }

        protected override Vector2 GetBounds()
        {
            return renderer.bounds.size;
        }

        public SpriteRendererData(Transform surface, Camera camera) : base(surface, camera)
        {
            if (surface.TryGetComponent(out renderer))
            {
                InitTriangle();
            }
        }
    }
}