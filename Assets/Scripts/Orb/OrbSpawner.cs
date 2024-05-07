using UnityEngine;

public class OrbSpawner : MonoBehaviour
 
{
    [SerializeField] private GameObject orbDamageBuff;
    [SerializeField] private GameObject orbHeal;
    [SerializeField] private GameObject orbSpeedBuff;
    public void Awake()
    {
        CombatEvents.OnMobKilled += SpawnOrb;
    }
    public void SpawnOrb(Vector3 position, Mob mob)
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                Instantiate(orbDamageBuff, position, Quaternion.identity);
                break;
            case 1:
                Instantiate(orbHeal, position, Quaternion.identity);
                break;
            case 2:
                Instantiate(orbSpeedBuff, position, Quaternion.identity);
                break;
        }
    }
}