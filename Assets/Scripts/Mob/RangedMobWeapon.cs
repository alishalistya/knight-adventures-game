using UnityEngine;

public abstract class RangedMobWeapon: Damageable
{
    [SerializeField] protected GameObject projectilePrefab;
    
    // this is damage for the crossbow, not the projectile
    public override int Damage => 0;

    public abstract void StartProjectile(Entity entity);
}