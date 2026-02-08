using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Tools
{
	public static class MeshGenerator
	{
		public static Mesh GenerateQuad(Vector3 size, Vector3 offset)
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(0f, 1f * size.y, 0f) - offset;
            vertices[1] = new Vector3(1f * size.x, 1f * size.y, 0) - offset;
            vertices[2] = new Vector3(1f * size.x, 0f, 0f) - offset;
            vertices[3] = new Vector3(0f, 0f, 0f) - offset;
            
            Vector2[] uv = new Vector2[4];
            uv[0] = new Vector2(0f, 1f);
            uv[1] = new Vector2(1f, 1f);
            uv[2] = new Vector2(1f, 0f);
            uv[3] = new Vector2(0f, 0f);
            
            int[] triangles = new  int[6];
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 2;
            triangles[4] = 3;
            triangles[5] = 0;

            Color[] colors = new Color[4];
            colors[0] = Color.white;
            colors[1] = Color.white;
            colors[2] = Color.white;
            colors[3] = Color.white;
            
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mesh.colors = colors;
            
            return mesh;
        }
	}
}