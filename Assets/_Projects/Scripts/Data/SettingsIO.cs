using System.IO;
using UnityEngine;

public static class SettingsIO 
{
    private static readonly string SavePath = Application.persistentDataPath + "/options.json";

    public static void Save(GameSettingsSO data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static void Load(GameSettingsSO data)
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            JsonUtility.FromJsonOverwrite(json, data);
        }
    }
}
