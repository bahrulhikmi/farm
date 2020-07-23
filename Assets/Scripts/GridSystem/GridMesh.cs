using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GridManager))]
public class GridMesh : MonoBehaviour
{
    void Awake()
    {
        
    }

    public void CreateGridMesh()
    {
        int Width = GridManager.Instance.Width;
        int Height = GridManager.Instance.Height;
        int CellSize = GridManager.Instance.CellSize;

        if (CellSize == 0 || Width == 0 || Height == 0)
        {
            Debug.LogError("Cell/Width/Height size must be larger than 0");
            return;
        }

        for (int i = 0; i < Width; i++)
        {

            for (int j = 0; j < Height; j++)
            {
                string cellName = string.Format("Cell_{0}_{1}", i, j);
                GameObject go = Instantiate(Resources.Load("Cell")) as GameObject;
                Cell cell = go.GetComponent<Cell>();
                cell.data = new CellData();
                cell.data.X = i;
                cell.data.Y = j;
                cell.name = cellName;
                cell.transform.localScale = Vector3.one * CellSize;
                cell.transform.parent = this.transform;
                cell.transform.position = new Vector3(i * CellSize * -1, 0, j * CellSize * -1);
         

            }
        }


    }

    private void mCreateGridMesh()
    {
        int Width = GridManager.Instance.Width;
        int Height = GridManager.Instance.Height;
        int CellSize = GridManager.Instance.CellSize;
        if (CellSize == 0 || Width == 0 || Height == 0)
        {
            Debug.LogError("Cell/Width/Height size must be larger than 0");
            return;
        }

        Mesh mesh = CreateMesh(CellSize, "Grid_Cell_Mesh");

        for (int i = 0; i < Width; i++)
        {

            for (int j = 0; j < Height; j++)
            {
                string cellName = string.Format("Cell_{0}_{1}", i, j);
                GameObject cell = new GameObject(cellName, typeof(MeshRenderer), typeof(MeshFilter),
                                                   typeof(MeshCollider));
                cell.transform.parent = this.transform;
                cell.transform.position = new Vector3(i * CellSize * -1, 0, j * CellSize * -1);

                cell.GetComponent<MeshFilter>().mesh = mesh;
                var mr = cell.GetComponent<MeshRenderer>();
                mr.receiveShadows = false;
                mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                cell.GetComponent<MeshCollider>().sharedMesh = mesh;
            }
        }

    }

    Mesh CreateMesh(float size, string name)
    {
        Mesh m = new Mesh();
        m.name = name;
        m.vertices = new Vector3[] {
         new Vector3(0, -0.01f, 0),
         new Vector3(-size, -0.01f, 0),
         new Vector3(-size, -0.01f, -size),
         new Vector3(0, -0.01f, -size)
     };
        m.uv = new Vector2[] {
         new Vector2 (0, 0),
         new Vector2 (0, 1),
         new Vector2(1, 1),
         new Vector2 (1, 0)
     };
        m.triangles = new int[] { 2, 1, 0, 3, 2, 0 };
        m.RecalculateNormals();

        return m;
    }
}