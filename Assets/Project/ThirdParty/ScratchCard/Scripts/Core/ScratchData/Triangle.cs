using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.ScratchData
{
    public class Triangle
    {
        private readonly Vector3 v0;
        private readonly Vector3 v1;
        private readonly Vector3 v2;
        private readonly Vector2 uv0;
        private readonly Vector2 uv1;
        private readonly Vector2 uv2;

        public Triangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Vector2 uv0, Vector2 uv1, Vector2 uv2)
        {
            v0 = vertex0;
            v1 = vertex1;
            v2 = vertex2;
            this.uv0 = uv0;
            this.uv1 = uv1;
            this.uv2 = uv2;
        }

        public Vector2 GetUV(Vector3 point)
        {
            Vector3 distance0 = v0 - point;
            Vector3 distance1 = v1 - point;
            Vector3 distance2 = v2 - point;
            //calculate the areas
            Vector3 va = Vector3.Cross(v0 - v1, v0 - v2);
            Vector3 va1 = Vector3.Cross(distance1, distance2);
            Vector3 va2 = Vector3.Cross(distance2, distance0);
            Vector3 va3 = Vector3.Cross(distance0, distance1);
            float area = va.magnitude;
            //calculate barycentric with sign
            float a1 = va1.magnitude / area * Mathf.Sign(Vector3.Dot(va, va1));
            float a2 = va2.magnitude / area * Mathf.Sign(Vector3.Dot(va, va2));
            float a3 = va3.magnitude / area * Mathf.Sign(Vector3.Dot(va, va3));
            Vector2 uv = uv0 * a1 + uv1 * a2 + uv2 * a3;
            return uv;
        }
    }
}