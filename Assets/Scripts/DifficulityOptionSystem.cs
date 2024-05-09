using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

public class DifficultyOptionSystem : MonoBehaviour
{
    ToggleGroup toggleGroup;
    Difficulty difficulty;

    public const string DIFFICULTY_PREFS_KEY = "difficulty";

    private void Awake()
    {
        difficulty = (Difficulty)PlayerPrefs.GetInt(DIFFICULTY_PREFS_KEY, (int)Difficulty.Easy);
    }

    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        SetCurrentDifficulty();
    }
    
    public void SetGameDifficulty()
    {
        // todo yoni set game difficulty to prefs?
        // PlayerPrefs.SetInt(DIFFICULTY_PREFS_KEY, difficulty);
        // PlayerPrefs.Save();
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        difficulty = GetGameDifficulty(toggle.name);
    }
    
    public Difficulty GetGameDifficulty(string _difficulty)
    {
        return (Difficulty)Enum.Parse(typeof(Difficulty), _difficulty);
    }
    
    public string GetGameDifficulty(Difficulty _difficulty)
    {
        return _difficulty.ToString();
    }
    
    public void SetCurrentDifficulty()
    {
        // todo yoni set game difficulty to prefs?
        // PlayerPrefs.SetInt(DIFFICULTY_PREFS_KEY, difficulty);
        // PlayerPrefs.Save();
        Toggle toggle = toggleGroup.transform.Find(GetGameDifficulty(difficulty)).GetComponent<Toggle>();
        toggle.isOn = true;
    }
}