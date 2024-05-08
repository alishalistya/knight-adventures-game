using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceManager
{
    const string STATISTICS_FILE_NAME = "statistics.json";
    const string GAME_SAVES_DESCRIPTION_FILE_NAME = "saves.json";
    const string GAME_SAVES_FOLDER = "saves/";
    private static PersistanceManager _instance;
    public SaveDescriptions SaveDescriptions;
    public GameStatistics GlobalStat = new();
    public static PersistanceManager Instance
    {
        get
        {
            _instance ??= new PersistanceManager();
            _instance.LoadStatistics();
            return _instance;
        }
    }

    public void AssertInit()
    {
    }

    private PersistanceManager()
    {
        Debug.Log("PersistanceManager created");
    }

    public void LoadStatistics()
    {
        if (FileManager.LoadFromFile(STATISTICS_FILE_NAME, out string jsonString))
        {
            GlobalStat = JsonUtility.FromJson<GameStatistics>(jsonString);
        }
        else
        {
            GlobalStat = new GameStatistics();
            SaveStatistics();
        };
    }

    public void SaveStatistics()
    {
        var jsonString = JsonUtility.ToJson(GlobalStat);
        FileManager.WriteToFile(STATISTICS_FILE_NAME, jsonString);

    }

    public void LoadSaveDescriptions()
    {
        if (FileManager.LoadFromFile(GAME_SAVES_DESCRIPTION_FILE_NAME, out string jsonString))
        {
            SaveDescriptions = JsonUtility.FromJson<SaveDescriptions>(jsonString);
        }
        else
        {
            SaveDescriptions = new SaveDescriptions();
        }
    }

    public void SaveSaveDescriptions()
    {
        var jsonString = JsonUtility.ToJson(SaveDescriptions);
        FileManager.WriteToFile(STATISTICS_FILE_NAME, jsonString);
    }

    public void SaveGame(SaveData saveData, SaveDescriptions.Description description, int saveIndex)
    {
        SaveDescriptions.Descriptions[saveIndex] = description;
        SaveSaveDescriptions();
        var jsonString = JsonUtility.ToJson(saveData);
        FileManager.WriteToFile(GAME_SAVES_FOLDER + saveIndex.ToString() + ".json", jsonString);
    }

    public SaveData LoadGame(int saveIndex)
    {
        FileManager.LoadFromFile(GAME_SAVES_FOLDER + saveIndex.ToString() + ".json", out string jsonString);
        return JsonUtility.FromJson<SaveData>(jsonString);
    }
}
