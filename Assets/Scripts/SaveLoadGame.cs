using UnityEngine;
using System.IO;

public class SaveLoadGame
{

    public static string filePath;

    public static SaveData savedGame = new SaveData();

    public static void Save()
    {
        filePath = Path.Combine(Application.persistentDataPath, "Save.json");

        savedGame = SaveData.currentSave;

        string jsonString = JsonUtility.ToJson(savedGame);

        Debug.Log("Saving " + jsonString);
        Debug.Log(filePath);

        File.WriteAllText(filePath, jsonString);
    }

    public static SaveData Load()
    {

        filePath = Path.Combine(Application.persistentDataPath, "Save.json");
        string jsonString = File.ReadAllText(filePath);

        savedGame = JsonUtility.FromJson<SaveData>(jsonString);

        Debug.Log("Loading " + jsonString);

        return savedGame;

    }
}
