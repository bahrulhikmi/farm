using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropAreaItem : FarmAreaItem {

    public const string STATE_ABANDONED = "abandoned";
    public const string STATE_DIRT = "dirt";
    public const string STATE_PLOUGH = "plough";
    public const string STATE_PLANTED = "planted";

public GameObject AbandonedModel, DirtModel, PlougModel, PlantedModel;


    public override void Initialize()
    {
        base.Initialize();
        
        if (string.IsNullOrEmpty(data.State))
        {
            State = STATE_DIRT;
        }
        UpdateVisual();
    }

    public override void StateChanged(string newState)
    {
        UpdateVisual();
    }

    /// <summary>
    /// Update visual of he CropAreaItem
    /// </summary>
    public void UpdateVisual()
    {
        if (AbandonedModel != null) AbandonedModel.SetActive(State == STATE_ABANDONED);
        if (DirtModel != null) DirtModel.SetActive(State == STATE_DIRT);
        if (PlougModel != null) PlougModel.SetActive(State == STATE_PLOUGH);        
        if(PlantedModel !=null) PlantedModel.SetActive(State == STATE_PLANTED);
    }

    #region "Available actions"
    public const string ACTION_PLOUGH = "Plough";
    public const string ACTION_CLEAN = "Clean";
    
    /// <summary>
    /// Get available actions for the Crop Area Item
    /// </summary>
    public override List<string> getAvailableActions()
    {
        List<string> actions = new List<string>();
        if (farmEntity != null)
        {
            actions.AddRange(((IUserInteraction)farmEntity).getAvailableActions());
        }

        if (data.State== STATE_ABANDONED)
        {
            actions.Add(ACTION_CLEAN);            
        }
        else if(data.State== STATE_DIRT)
        {
            actions.Add(ACTION_PLOUGH);
        }
        else if (data.State == STATE_PLOUGH)
        {
            actions.Add(ACTION_PLANT);
        }

        return actions;
    }

    public override bool RunAction(string action)
    {   
        switch (action)
        {
            case ACTION_PLANT: Plant(); State = STATE_PLANTED; return true;
            case ACTION_PLOUGH: State = STATE_PLOUGH; return true;
            case ACTION_CLEAN: State = STATE_DIRT; return true;            
            default:
                if (farmEntity != null)
                {
                    return ((IUserInteraction)farmEntity).RunAction(action);
                }
                break;

        }

        return false;
    }
    #endregion

}
