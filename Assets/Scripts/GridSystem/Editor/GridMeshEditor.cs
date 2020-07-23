using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridMesh))]
public class GridMeshEditor : Editor
{
    public int GridSize;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridMesh myScript = (GridMesh)target;
        if (GUILayout.Button("Generate Grid"))
        {
            myScript.CreateGridMesh();
        }
    }
}