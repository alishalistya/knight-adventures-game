using System;
using UnityEngine;

public class SuperKeroco : MobRanged
{
    public override int ID => 1;
    protected override string AttackAnimationMovement => "AttackMovement";

    protected int baseHealth = 80;
    protected int initialHealth;

    protected override int MaxHealth => initialHealth;
    protected override int InitialHealth => initialHealth;

    protected override float TimeBetweenAttack => 3f;

    protected float lastSpawnKeroco = 0f;
    protected float spawnKerocoInterval = 25f;

    protected new void Awake()
    {
        base.Awake();
        initialHealth = (int)(_difficultyMultiplier * baseHealth);
    }

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