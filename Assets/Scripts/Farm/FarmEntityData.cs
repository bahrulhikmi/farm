using UnityEngine;

/// <summary>
/// Data Container for Form Entity
/// </summary>
[System.Serializable]
public class FarmEntityData {

    [SerializeField]
    public string _id;
    [SerializeField]
    public string _entityName;
    [SerializeField]
    public string _description;
    [SerializeField]
    public int _locationIndex;
    [SerializeField]
    public string _category;
    [SerializeField]
    public int _age;
    [SerializeField]
    public int _healthyLevel;
    [SerializeField]
    public float _hungerLevel;
    [SerializeField]
    public string _type;

    /// <summary>
    /// The location of the Farm Entity relativest to the Farm Position
    /// </summary>
    public int LocationIndex
    {
        get { return _locationIndex; }
        set { _locationIndex = value; }
    }

    /// <summary>
    /// Unique Identifier of the Farm Entity
    /// </summary>
    public string ID
    {
        get { return _id; }
        set { _id = value; }
    }

    /// <summary>
    /// Readable name of the Farm Entity
    /// </summary>
    public string EntityName
    {
        get { return _entityName; }
        set { _entityName = value; }
    }

    /// <summary>
    /// The description of the Farm Entity
    /// </summary>
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    /// <summary>
    /// Category of the Farm Entity
    /// </summary>
    public string Category
    {
        get { return _category; }
        set { _category = value; }
    }

    /// <summary>
    /// The age of the Farm Entity
    /// </summary>
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    /// <summary>
    /// Healthy Level of the Farm Entity
    /// </summary>
    public int HealthyLevel
    {
        get { return _healthyLevel; }
        set { _healthyLevel = value; }
    }

    public float HungerLevel
    {
        get { return _hungerLevel; }
        set { _hungerLevel = value; }
    }

    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

}
