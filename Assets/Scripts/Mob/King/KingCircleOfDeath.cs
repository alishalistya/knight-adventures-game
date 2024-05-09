using UnityEngine;

public class KingCircleOfDeath: Damageable
{
    protected int baseDamage = 2;
    protected int damage;
    
    public override int Damage => damage;

    private float activeTime = 0f;
    private float hitInterval = 1f; // every 1 seconds

    private new void Awake()
    {
        base.Awake();
        damage = (int)(_enemyDifficultyMultiplier * baseDamage);
        IsActive = true;
    }

    private void FixedUpdate()
    {
        activeTime += Time.deltaTime;

        if (activeTime >= hitInterval)
        {
            activeTime = 0;
            IsActive = false;
            IsActive = true;
        }
    }
}