using System.Collections.Generic;
/// <summary>
/// Container Class for FarmArea Data
/// </summary>
[System.Serializable]
public class FarmAreaData
{
    [UnityEngine.SerializeField]
    private int id;
    [UnityEngine.SerializeField]
    private string description;
    [UnityEngine.SerializeField]
    private string name;
    [UnityEngine.SerializeField]
    private int maximumFarmEntity;
    [UnityEngine.SerializeField]
    private List<FarmEntityData> farmEntities;
    [UnityEngine.SerializeField]
    private bool available;

    /// <summary>
    /// Unique Identifier of the Farm Area
    /// </summary>
    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    /// <summary>
    /// Maximum Number of Farm Entity for this plantation
    /// </summary>
    public int MaximumFarmEntity
    {
        get { return maximumFarmEntity; }
        set { maximumFarmEntity = value; }   
    }

    /// <summary>
    /// List of Farm Entity ID that is belong to this plantation
    /// </summary>
    public List<FarmEntityData> FarmEntities
    {
        get { return farmEntities; }
        set { farmEntities = value; }
    }

    /// <summary>
    /// Description of the FarmArea
    /// </summary>
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    /// <summary>
    /// Name of the FarmArea;
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// Value to check whether the Farm Area is Available or not
    /// </summary>
    public bool Available
    {
        get { return available; }
        set { available = value; }
    }


}