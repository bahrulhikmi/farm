using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    /// <summary>
    /// Save the specified gameData.
    /// </summary>
    /// <param name="gameData">Game data.</param>
    public static void Save(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Constants.SavePath+ gameData.name + ".sav");
        bf.Serialize(file, gameData);
        file.Close();
        Debug.Log("Saved Game: " + gameData.name);
    }

	/// <summary>
	/// Load the specified Data based on dataName.
	/// </summary>
	/// <param name="dataName">Data name.</param>
    public static GameData Load(string dataName)
    {
        if (File.Exists(Constants.SavePath+ dataName + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Constants.SavePath + dataName + ".sav", FileMode.Open);
            GameData loadedGame = (GameData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded Game: " + Constants.SavePath + loadedGame.name);
            return loadedGame;
        }
        else
        {           
            Debug.Log("File doesn't exist!");
            return null;
        }

    }


}