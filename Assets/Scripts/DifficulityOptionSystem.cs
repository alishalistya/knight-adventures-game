using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class DifficultyOptionSystem : MonoBehaviour
{
    ToggleGroup toggleGroup;
    Difficulty difficulty;
    
    public DifficultyOptionSystem(Difficulty difficulty = Difficulty.Easy)
    {
        this.difficulty = difficulty;
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
        Toggle toggle = toggleGroup.transform.Find(GetGameDifficulty(difficulty)).GetComponent<Toggle>();
        toggle.isOn = true;
    }
}