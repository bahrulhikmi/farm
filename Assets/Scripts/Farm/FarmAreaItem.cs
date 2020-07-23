using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmAreaItem : Entity, IUserInteraction {
    public FarmAreaItemData data;
    [HideInInspector]
    public FarmEntity farmEntity;
    [HideInInspector]
    public FarmArea farmArea;
    [HideInInspector]
    public int LocationIndex;

    public void Awake()
    {
        farmArea = transform.parent.GetComponent<FarmArea>();

        if(!farmArea)
        {
            Debug.LogError("A farm Area Item must be a child of Farm Area");
        }
    }    

    public string State
    {
        set
        {
            if (data.State != value)
            {
                data.State = value;
                StateChanged(value);
            }
        }
        get
        {
            return data.State;
        }
    }

    public virtual void StateChanged(string newState)
    {

    }

    public void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {        
        data.Type = this.GetType().Name;
    }

    public bool Plant()
    {
        Debug.Log("Planting");
        return Plant("Turnip");
    }

    public bool Plant(string type)
    {    
        GameObject newEntity = Instantiate(ResourceManager.getResource(type)) as GameObject;       
        newEntity.transform.position = transform.position;
        newEntity.transform.parent = transform;        
        farmEntity = newEntity.GetComponent<FarmEntity>();
        farmEntity.Data.LocationIndex = LocationIndex;
        farmEntity.Data.Type = type;
        newEntity.name = type + LocationIndex;
        farmArea.Add(farmEntity.Data);
        farmEntity.UpdateVisual(true);

        return true;
    }

    public bool Plant(FarmEntityData farmEntityData)
    {
        if (string.IsNullOrEmpty(farmEntityData.Type)) return true;
        Plant(farmEntityData.Type);
        farmEntity.Data = farmEntityData;
        farmEntity.UpdateVisual(true);
        farmArea.Add(farmEntity.Data);
        return true;
    }


    public const string ACTION_PLANT="Plant";
    public virtual List<string> getAvailableActions()
    {
        List<string> actions = new List<string>();        
        if(farmEntity!=null)
        {
            actions.AddRange(((IUserInteraction)farmEntity).getAvailableActions());
        }
        else
        {
            actions.Add(ACTION_PLANT);
        }     
        return actions;   
    }

    public virtual bool RunAction(string action) 
    {
        switch (action)
        {
            case ACTION_PLANT: Plant(); return true;
            default:
                if (farmEntity != null)
                {
                   return ((IUserInteraction)farmEntity).RunAction(action);
                }
            break;

        }

        return false;
    }
}
