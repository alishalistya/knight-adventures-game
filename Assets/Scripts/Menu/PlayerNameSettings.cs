using TMPro;
using UnityEngine;

public class PlayerNameSettings : MonoBehaviour
{
    [SerializeField] private TMP_InputField playeTMPInputField;

    public const string PLAYER_NAME_PREFS_KEY = "playername";
    
    private void Start()
    {
        if (PlayerPrefs.HasKey(PLAYER_NAME_PREFS_KEY))
        {
            LoadPlayerName();
        }
        else
        {
            SetPlayerName();
        }
    }
    
    public void SetPlayerName()
    {
        string playerName = playeTMPInputField.text;
        PlayerPrefs.SetString(PLAYER_NAME_PREFS_KEY, playerName);
        Debug.Log("Masuk sih");
        // PlayerPrefs.Save();
    }
    
    private void LoadPlayerName()
    {
        playeTMPInputField.text = PlayerPrefs.GetString(PLAYER_NAME_PREFS_KEY);
        SetPlayerName();
    }
}