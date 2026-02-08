using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.ScratchData
{
    public class MeshRendererData : BaseData
    {
        private readonly MeshRenderer renderer;
        private readonly MeshFilter filter;
        private readonly Vector2 m_textureSize;

        public override Vector2 GetTextureSize()
        {
            return m_textureSize;
        }

        protected override Vector2 GetBounds()
        {
            return filter != null ? filter.sharedMesh.bounds.size : renderer.bounds.size;
        }

        public MeshRendererData(Transform surface, Camera camera) : base(surface, camera)
        {
            if (surface.TryGetComponent(out renderer) && surface.TryGetComponent(out filter))
            {
                InitTriangle();
                Material sharedMaterial = renderer.sharedMaterial;
                Vector4 offset = sharedMaterial.GetVector(Constants.MaskShader.Offset);
                m_textureSize = new Vector2(sharedMaterial.mainTexture.width * offset.z, sharedMaterial.mainTexture.height * offset.w);
            }
        }
    }
}