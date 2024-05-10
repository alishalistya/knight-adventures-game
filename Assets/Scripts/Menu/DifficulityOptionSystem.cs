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
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        difficulty = GetGameDifficulty(toggle.name);
        PlayerPrefs.SetInt(DIFFICULTY_PREFS_KEY, (int)difficulty);
        // PlayerPrefs.Save();
    }
    
    public Difficulty GetGameDifficulty(string _difficulty)
    {
        _difficulty = _difficulty.Substring(0, _difficulty.Length - 6);
        return (Difficulty)Enum.Parse(typeof(Difficulty), _difficulty);
    }
    
    public string GetGameDifficulty(Difficulty _difficulty)
    {
        return _difficulty.ToString() + "Option";
    }
    
    public void SetCurrentDifficulty()
    {
        int currentDifficulty = PlayerPrefs.GetInt(DIFFICULTY_PREFS_KEY);
        Difficulty difficulty = (Difficulty)currentDifficulty;
        Toggle toggle = toggleGroup.transform.Find(GetGameDifficulty(difficulty)).GetComponent<Toggle>();
        toggle.isOn = true;
        PlayerPrefs.SetInt(DIFFICULTY_PREFS_KEY, (int)difficulty);
        // PlayerPrefs.Save();
    }
}