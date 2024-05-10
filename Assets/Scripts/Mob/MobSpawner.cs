using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;

    void Awake()
    {
        CombatEvents.OnMobKilled += SpawnMob;
    }
    
    void SpawnMob(Vector3 position, Mob mob)
    {
        Instantiate(mobPrefab, transform.position, Quaternion.identity);
    }

    void OnDestroy()
    {
        CombatEvents.OnMobKilled -= SpawnMob;
    }
}
