using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provide the functionality to Manage resources
/// </summary>
public static class ResourceManager {

    public static Dictionary<string, string> resource = new Dictionary<string, string>()
    {
		//TODO: put into config file.
        { "Tomato" ,"Crops/Tomato"},
        { "Turnip" ,"Crops/Turnip"},
    };
    public static Object getResource(string resourceId)
    {        
        if (resource.ContainsKey(resourceId)) { 

            return Resources.Load(resource[resourceId]);
        }
        return null;
    }

    public static Object getEntity(string resourceId)
    {
        Object resource = Resources.Load(string.Format("Entity/{0}", resourceId));
        if (resource == null)
        {
            Debug.LogError(string.Format("Entity/{0}", resourceId));
        }
            return resource;
        
    }

}
