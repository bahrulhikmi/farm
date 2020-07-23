using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    private static GridManager mInstance;
    public int Width;
    public int Height;
    public Vector3 gridPivot = Vector3.zero;
    public int CellSize = 10;

    public Cell[,] cells;

    public static GridManager Instance
    {
        get
        {
            if (!mInstance)
            {
                mInstance = FindObjectOfType(typeof(GridManager)) as GridManager;

                if (!mInstance)
                {
                    Debug.LogError("There needs to be one active GridManager script on a GameObject in your scene.");
                }
                else
                {
                    mInstance.Init();
                }
            }

            return mInstance;
        }
        
    }

    private void Init()
    {

    }
    
    public Vector3 GetClosestCell(Vector3 position)
    {
        float minDistance = float.MaxValue;
        Cell closestCell = null;
        foreach(Cell cell in cells)
        {
            
            float distance = Vector3.Distance(position, getCellPosition(cell.data.X, cell.data.Y));
            if (distance< minDistance)
            {
                minDistance = distance;
                closestCell = cell;
            }
        }

        if (closestCell == null)
        {
            return Vector3.zero;
        }

        return getCellPosition(closestCell.data.X, closestCell.data.Y);
    }

    public Vector3 FindAvailableCell(int width, int height)
    {       
        return Vector3.zero;
    }

    /// <summary>
    /// Check whether the cell is overlapped with another cell
    /// </summary>        
    public bool IsCellOverlapped(CellData cell)
    {
        return false;
    }

    /// <summary>
    /// Get position of the specified index of the grid
    /// </summary>    
    public Vector3 getCellPosition(int x, int y)
    {
        float posX, posY, posZ;

        posX = gridPivot.x + x * CellSize;
        posY = gridPivot.y;
        posZ = gridPivot.z + x * CellSize;

        return new Vector3(posX, posY, posZ);
    }

  


}
