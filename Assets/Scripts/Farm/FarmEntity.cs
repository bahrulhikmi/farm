using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represent all items that is element of Farm
/// </summary>
public class FarmEntity : Entity, IUserInteraction
{
    public float MaximumAge = 10;
    public float HungerDecreaseEachSec = 0.1f;
    public float GrowthRates = 1;
    public float StartHungryAt = 50;

    #region Properties
    [SerializeField]
    private FarmEntityData data;

    public List<FarmEntityTransformation> ageTransformation;

    public FarmEntityData Data
    {
        get { return data; }
        set { data = value; }
    }

    public bool MaximumAgeReached
    {
        get { return data.Age >= MaximumAge; }
    }

    public bool IsHungry
    {
        get { return data.HungerLevel >= StartHungryAt; }
    }

    public bool IsDead
    {
        get { return data.Age == 999; }
        set { if (value) data.Age = 999; }
    }

    #endregion

    #region "Methods"
    /// <summary>
    /// Grow the entity using the default Growth rates
    /// </summary>
    /// <summary>
    /// Grow the entity using a specified Growth rates
    /// </summary>
    /// <param name="growthRates"></param>
    public void Grow()
    {
        lastUpdate++;
        if (lastUpdate >= GrowthRates)
        {
            lastUpdate = 0;
            data.Age++;

            UpdateVisual(false);

        }
    }
    private float lastUpdate;

    /// <summary>
    /// Update the visual of the Farm Entity
    /// </summary>
    public void UpdateVisual(bool forceUpdate)
    {
        if (!forceUpdate && (!ageTransformation.Exists(x => x.Age == data.Age))) return;
  

        FarmEntityTransformation candidateTransformation = null;
        FarmEntityTransformation matchingTrans = null;
        foreach (FarmEntityTransformation trans in ageTransformation)
        {     
            if (matchingTrans==null&&((data.Age == trans.Age) && data.HealthyLevel >= trans.MinimumHealthyLevel))
            {
                matchingTrans = trans;
            }
            else if ((data.Age > trans.Age) && data.HealthyLevel >= trans.MinimumHealthyLevel)
            {
                candidateTransformation = trans;
                trans.Transformation.SetActive(false);               
            }
            else
            {
                trans.Transformation.SetActive(false);
            }
        }
 
        //if no matching transformation found, use the candidate instead
        if (matchingTrans!=null)
        {
            matchingTrans.Transformation.SetActive(true);
        }
        else if (candidateTransformation!=null)
        {
            candidateTransformation.Transformation.SetActive(true);
        }

    }


    /// <summary>
    /// Create a new Farm Entity
    /// </summary>
    /// <param name="location"></param>
    public void Create(int locationIndex, string id, string entityName, string description, string category)
    {
        var data = new FarmEntityData
        {
            LocationIndex = locationIndex,
            ID = id,
            EntityName = entityName,
            Description = description,
            Category = category
        };
    }

    public bool Remove()
    {
        Destroy(this.gameObject);
        return true;
    }

    /// <summary>
    /// Harvest the Farm Entity
    /// </summary>
    /// <returns>Return <i>true</i> if the Harvest is causing the Entity to be destroyed, otherwiset <i>false</i></returns>
    public virtual bool Harvest()
    {
        Destroy(this.gameObject);
        return true;
    }

    public virtual bool IsHarvestable()
    {
        return data.Age == MaximumAge;
    }

    /// <summary>
    /// Fee the Farm Entity
    /// </summary>
    public virtual bool Feed()
    {
        data.HungerLevel -= 2;
        Debug.Log(data.HungerLevel);
        return !IsHungry;
    }

    #endregion 

    protected virtual void UpdateCondition()
    {
        data.HungerLevel += HungerDecreaseEachSec;
        if (!MaximumAgeReached && !IsHungry)
        {
            Grow();
        }

        if (IsHungry)
        {
            data.HealthyLevel--;
        }

        if (data.HealthyLevel <= 0)
        {
            IsDead = true;
            Grow();
        }
    }

    private float accumDeltaTIme;
    void Update()
    {
        if (IsDead) return;

        accumDeltaTIme += Time.deltaTime;
        //Update only each second
        if (accumDeltaTIme < 1) return;
        accumDeltaTIme = 0;

        UpdateCondition();      

    }

    protected static string ACTION_REMOVEPLANT = "Remove";
    protected string ACTION_HARVEST = "Harvest";
    protected string ACTION_FEED = "Feed";
    public virtual List<string> getAvailableActions()
    {
        List<string> actions = new List<string>() {ACTION_REMOVEPLANT };

        if (IsHungry)
        {
            actions.Add(ACTION_FEED);
        }

        if(IsHarvestable())
        {
            actions.Add(ACTION_HARVEST);
        }

        return actions;
    }

    public virtual bool RunAction(string action)
    {
        if (action == ACTION_REMOVEPLANT) return Remove();
        if (action == ACTION_HARVEST) return Harvest();
        if (action == ACTION_FEED) return Feed();
        return false;
    }
}
