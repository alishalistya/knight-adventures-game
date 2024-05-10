using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager
{
    public static GameManager Instance { get; private set; } = new GameManager();
    public GameStatistics Statistics = new();
    public int QuestNumber = 1;
    public bool FromLoad = false;
    public int PlayerGold = 0;
    public int PlayerHealth = 200;
    public bool[] IsAyamAlive = new bool[] { false, false, false, false, false };
    public Difficulty Difficulty = Difficulty.Easy;
    public int buffDamageTaken = 0;
    public Quest CurrentQuest = null;
    private GameManager()
    {
        PersistanceManager.Instance.LoadStatistics();
    }

    public static void NewGame()
    {
        Instance = new GameManager
        {
            Statistics = new GameStatistics(),
            Difficulty = (Difficulty)PlayerPrefs.GetInt(DifficultyOptionSystem.DIFFICULTY_PREFS_KEY, (int)Difficulty.Easy)
        };
        PersistanceManager.Instance.LoadSaveDescriptions();
        SceneManager.LoadScene("Quest-1");
    }


    public static void LoadGame(SaveData saveData)
    {
        Instance = new GameManager
        {
            Statistics = saveData.Statistics,
            QuestNumber = saveData.QuestNumber,
            PlayerHealth = saveData.PlayerHealth,
            PlayerGold = saveData.PlayerGold,
            FromLoad = true,
            Difficulty = saveData.Difficulty,
            buffDamageTaken = saveData.BuffDamageTaken,
            IsAyamAlive = saveData.IsAyamAlive,
            CurrentQuest = saveData.CurrentQuest
        };
        SceneManager.LoadScene("Quest-" + Instance.QuestNumber);
    }

    public static void SaveGame(SaveData saveData, SaveDescriptions.Description description, int saveIndex)
    {
        PersistanceManager.Instance.SaveGame(saveData, description, saveIndex);
    }


    public static void NextQuest()
    {
        Instance.QuestNumber++;
        Instance.FromLoad = false;
        SceneManager.LoadScene("Quest-" + Instance.QuestNumber);
    }
}