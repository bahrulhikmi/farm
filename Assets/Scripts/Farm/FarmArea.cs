using UnityEngine;

/// <summary>
/// Represent the FarmArea
/// </summary>
public class FarmArea : MonoBehaviour
{
    [SerializeField]
    private FarmAreaData data;

    public System.Collections.Generic.List<FarmAreaItem> farmAreaItems;

    public FarmAreaData Data
    {
        get { return data; }
        set { data = value; }
    }

    /// <summary>
    /// Add a Farm Entity to the Area
    /// </summary>
    /// <param name="farmEntity"></param>
    public void Add(FarmEntityData farmEntity)
    {
       data.FarmEntities[farmEntity.LocationIndex] = farmEntity;
    }

    /// <summary>
    /// Remove Farm Entity from the Area
    /// </summary>
    /// <param name="farmEntity"></param>
    public void Remove(FarmEntityData farmEntity)
    {
        data.FarmEntities[farmEntity.LocationIndex] = null;

    }

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        if (!GameDataManager.instance.IsLoaded)
        {
            GameDataManager.instance.Load();
        }

        data = GameDataManager.instance.gameData.farmAreas.Find(x => x.ID == data.ID);
        data.MaximumFarmEntity = farmAreaItems.Count;
        int index = -1;
        foreach (FarmAreaItem faItem in farmAreaItems)
        {
            index++;
            faItem.LocationIndex = index;
            if (index >= data.FarmEntities.Count)
            {
                data.FarmEntities.Add(new FarmEntityData());
                continue;
            }

            faItem.Plant(data.FarmEntities[index]);
        }
    }
}