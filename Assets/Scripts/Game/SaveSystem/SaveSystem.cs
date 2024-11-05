using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    //Find de Jason file
    private static string fileName = "save.json";


    public static void SerializeData(SaveData data)
    {
        //Vind de path naar de jason file
        string path = Path.Combine(Application.persistentDataPath, fileName);
        //Slaat de game data op
        using (StreamWriter writer = File.CreateText(path))
        {
            string json = JsonUtility.ToJson(data);
            writer.Write(json);
        }
        Debug.Log("saved game to " + path);
    }

    public static SaveData Deserialize()
    {
        //Vind de path naar de jason file
        string path = Path.Combine(Application.persistentDataPath, fileName);

        //Als er save data is dan laad de game deze in wanneer die opgestart word
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
        using (StreamReader reader = File.OpenText(path))
        {
            string json = reader.ReadToEnd();
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("loaded game from " + path);
            return data;
        }
    }
}