using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;

    public int mobId = 0;

    void Awake()
    {
        CombatEvents.OnMobKilled += SpawnMob;
    }
    
    void SpawnMob(Vector3 position, Mob mob)
    {
        if (mobId == mob.ID)
        {
            Instantiate(mobPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnDestroy()
    {
        CombatEvents.OnMobKilled -= SpawnMob;
    }
}
