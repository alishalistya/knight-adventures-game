using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public delegate void MobEventHandler(Mob mob);
    public static event MobEventHandler OnMobKilled;

    public static void MobKilled(Mob mob)
    {
        OnMobKilled?.Invoke(mob);
    }
}