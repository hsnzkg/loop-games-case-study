using UnityEngine;

namespace Project.Thirdparty.ScratchCard.Scripts.Core.ScratchData
{
    public class SpriteRendererData : BaseData
    {
        private readonly SpriteRenderer renderer;

        public override Vector2 TextureSize => renderer.sprite.rect.size;
        protected override Vector2 Bounds => renderer.bounds.size;
        
        public SpriteRendererData(Transform surface, Camera camera) : base(surface, camera)
        {
            if (surface.TryGetComponent(out renderer))
            {
                InitTriangle();
            }
        }
    }
}