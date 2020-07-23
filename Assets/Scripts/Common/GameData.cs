using System.Collections.Generic;

/// <summary>
/// Game Data definition
/// </summary>
[System.Serializable]
public class GameData {
	/// <summary>
	/// Allow to have multiple Game Data by specifying the name of the data
	/// </summary>
    public string name = "Default";
    public List<FarmAreaData> farmAreas;
    
}
