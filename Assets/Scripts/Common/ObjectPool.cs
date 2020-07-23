using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manage all object pools
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public List<ObjectPoolResource> resources;
    private Dictionary<string, Stack<GameObject>> inactiveInstances = new Dictionary<string, Stack<GameObject>>();
    private static ObjectPool mObjectPool;

    public static ObjectPool Instance
    {
        get
        {
            if(mObjectPool==null)
            {
                mObjectPool = FindObjectOfType(typeof(ObjectPool)) as ObjectPool;
            }

            if (!mObjectPool)
            {
                Debug.LogError("No Object Pool found in the scene.");
            }
            else
            {
                //mObjectPool.Init()
            }

            return mObjectPool;
        }
    }

    /// <summary>
    /// Get object from pool by specifying the name
    /// </summary>
    public GameObject GetObject(string name)
    {

        GameObject spawnedGameObject;

        // if there is an inactive instance of the prefab ready to return, return that
        if (inactiveInstances.ContainsKey(name) && inactiveInstances[name].Count > 0)
        {
            // remove the instance from teh collection of inactive instances
            spawnedGameObject = inactiveInstances[name].Pop();
        }
        // otherwise, create a new instance
        else
        {
            GameObject prefab = resources.Find(x => x.name == name).prefab;

            if (prefab == null)
            {
                Debug.LogError("Can't find "+name+" in the resource pool");
                return null;
            }

            spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);

            // add the PooledObject component to the prefab so we know it came from this pool
            PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
            pooledObject.resourceName = name;
        }

        // put the instance in the root of the scene and enable it
        spawnedGameObject.transform.SetParent(null);
        spawnedGameObject.SetActive(true);

        // return a reference to the instance
        return spawnedGameObject;
    }

    /// <summary>
    /// Return an object to the pool
    /// </summary>
    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        // if the instance came from this pool, return it to the pool
        if (pooledObject != null && pooledObject.pool == this)
        {
            toReturn.transform.SetParent(transform);
            toReturn.SetActive(false);

            if (!inactiveInstances.ContainsKey(pooledObject.resourceName))
            {
                inactiveInstances.Add(pooledObject.resourceName, new Stack<GameObject>());
            }
            inactiveInstances[pooledObject.resourceName].Push(toReturn);
        }
        else
        {
            Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
            Destroy(toReturn);
        }
    }
}

/// <summary>
/// A component that simply identifies the actual GameObject of the ObjectPool
/// </summary>
public class PooledObject : MonoBehaviour
{
    public ObjectPool pool;
    public string resourceName;
}

/// <summary>
/// Resource file of the object pool
/// </summary>
[System.Serializable]
public class ObjectPoolResource
{
    public string name;
    public GameObject prefab;
}