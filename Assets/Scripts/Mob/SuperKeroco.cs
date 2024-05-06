using System;
using UnityEngine;

public class SuperKeroco: MobRanged
{
    public override int ID => 1;
    protected override string AttackAnimationMovement => "AttackMovement";
    
    protected override int MaxHealth => 75;
    protected override int InitialHealth => 75;
    
    protected override float TimeBetweenAttack => 2f;
    
    protected override void OnDamaged(int prevHealth, int currentHealth)
    {

    }

    protected float lastSpawnKeroco = 0f;
    protected float spawnKerocoInterval = 25f;
    
    [SerializeField] protected GameObject kerocoPrefab;

    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        lastSpawnKeroco += Time.deltaTime;

        if (lastSpawnKeroco >= spawnKerocoInterval)
        {
            lastSpawnKeroco = 0f;
            var pos = transform.position + new Vector3(-2, 0, -2);
            var spawned = Instantiate(kerocoPrefab, pos, Quaternion.identity);
            spawned.SetActive(true);
        }
    }
}