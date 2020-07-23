using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

    private static GameDataManager gameDataManager;
    private bool isLoaded;

    public GameData gameData;

    public bool IsLoaded
    {
        get { return isLoaded; }
    }

    /// <summary>
    /// Instannce of the Event Manager
    /// </summary>
    public static GameDataManager instance
    {
        get
        {
            if (!gameDataManager)
            {
                gameDataManager = FindObjectOfType(typeof(GameDataManager)) as GameDataManager;

                if (!gameDataManager)
                {
                    Debug.LogError("There needs to be one active GameDataManager script on a GameObject in your scene.");
                }
                else
                {
                    gameDataManager.Init();
                }
            }

            return gameDataManager;
        }
    }

    void Init()
    {
        isLoaded = false;
    }

    public void Save()
    {
        SaveLoad.Save(gameData);
    }

    public void Load()
    {
        GameData loadedGameData = SaveLoad.Load(gameData.name);
        if (loadedGameData!=null)
        {
            gameData = loadedGameData;
        }

        isLoaded = true;
    }

}
