using System;
using UnityEngine;
using UnityEditor;

public class CreateQuadMesh : Editor
{

    [MenuItem("Assets/Create/Quad Mesh", false, 10000)]
    public static void Create()
    {
        Mesh mesh = BuildQuad(1, 1);
        string name = "Quad Mesh";
        mesh.name = name;
        AssetDatabase.CreateAsset(mesh, String.Format("Assets/{0}.asset", name));
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = mesh;
    }

    private static Mesh BuildQuad(float width, float height)
    {
        Mesh mesh = new Mesh();

        // Setup vertices
        Vector3[] newVertices = new Vector3[4];
        float size = 1;
        newVertices[0] = new Vector3(0, 0, 0);
        newVertices[1] = new Vector3(-size, 0, 0);
        newVertices[2] = new Vector3(-size, 0 , -size);
        newVertices[3] = new Vector3(0, 0, -size);

        // Setup UVs
        Vector2[] newUVs = new Vector2[newVertices.Length];
        newUVs[0] = new Vector2(0, 0);
        newUVs[1] = new Vector2(0, 1);
        newUVs[2] = new Vector2(1, 1);
        newUVs[3] = new Vector2(1, 0);

        // Setup triangles
        int[] newTriangles = new int[] { 2, 1, 0, 3, 2, 0 };



        // Create quad
        mesh.vertices = newVertices;
        mesh.uv = newUVs;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}