using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public delegate void MobEventHandler(Vector3 position, Mob mob);
    public static event MobEventHandler OnMobKilled;

    public static void MobKilled(Vector3 position, Mob mob)
    {
        OnMobKilled?.Invoke(position, mob);
    }
}