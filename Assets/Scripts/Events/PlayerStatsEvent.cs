using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsEvents : MonoBehaviour
{
    public delegate void PlayerStatsEventHandler(Player player);

    public static event PlayerStatsEventHandler OnPlayerStatsChanged;

    public static void PlayerStatsChanged(Player player)
    {
        OnPlayerStatsChanged?.Invoke(player);

    }
}