using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGold : MonoBehaviour
{

    [SerializeField] private TMP_Text goldValue;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UIGold Start");
        PlayerStatsEvents.OnPlayerStatsChanged += PlayerStatsChanged;
        
    }

    void PlayerStatsChanged(Player player)
    {
        int value = player.Gold;
        goldValue.text = value.ToString();
    }
}
