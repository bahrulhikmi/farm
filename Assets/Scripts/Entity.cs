using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public List<CellData> occupiedCells;
    public int width, height;
    public string entityType;
}
