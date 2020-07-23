using UnityEngine;
public static class Constants {

    public static string SavePath
    {
        get
        {
            return Application.persistentDataPath + "/";
        }
    }
}
