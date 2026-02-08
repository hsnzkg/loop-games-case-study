using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.ScratchData
{
    public abstract class BaseData
    {
        private Camera m_camera;
        private Transform m_surface;
        private Triangle m_triangle;

        protected Camera GetCamera()
        {
            return m_camera;
        }

        public void SetCamera(Camera value)
        {
            m_camera = value;
        }

        public abstract Vector2 GetTextureSize();
        protected abstract Vector2 GetBounds();

        protected virtual bool GetIsOrthographic()
        {
            return GetCamera().orthographic;
        }

        protected Transform GetSurface()
        {
            return m_surface;
        }

        private void SetSurface(Transform value)
        {
            m_surface = value;
        }

        protected Triangle GetTriangle()
        {
            return m_triangle;
        }

        protected void SetTriangle(Triangle value)
        {
            m_triangle = value;
        }

        protected BaseData(Transform surface, Camera camera)
        {
            SetSurface(surface);
            SetCamera(camera);
        }
        
        protected void InitTriangle()
        {
	        var bounds = GetBounds();
	        //bottom left
	        var position0 = new Vector3(-bounds.x / 2f, -bounds.y / 2f, 0);
	        var uv0 = Vector2.zero;
	        //upper left
	        var position1 = new Vector3(-bounds.x / 2f, bounds.y / 2f, 0);
	        var uv1 = Vector2.up;
	        //upper right
	        var position2 = new Vector3(bounds.x / 2f, bounds.y / 2f, 0);
	        var uv2 = Vector2.one;
	        SetTriangle(new Triangle(position0, position1, position2, uv0, uv1, uv2));
        }

        public virtual Vector2 GetScratchPosition(Vector2 position)
        {
	        var scratchPosition = Vector2.zero;
	        var plane = new Plane(GetSurface().forward, GetSurface().position);
	        var ray = GetCamera().ScreenPointToRay(position);
	        if (plane.Raycast(ray, out var enter))
	        {
		        var point = ray.GetPoint(enter);
		        var pointLocal = GetSurface().InverseTransformPoint(point);
		        var uv = GetTriangle().GetUV(pointLocal);
		        scratchPosition = Vector2.Scale(GetTextureSize(), uv);
	        }
	        return scratchPosition;
        }

        public Vector2 GetLocalPosition(Vector2 texturePosition)
        {
	        var textureSize = GetTextureSize();
	        var bounds = GetBounds();
	        if (GetIsOrthographic())
	        {
		        return (texturePosition - textureSize / 2f) / textureSize * bounds / GetSurface().lossyScale;
	        }
	        return (texturePosition - textureSize / 2f) / textureSize * bounds;
        }
    }
}