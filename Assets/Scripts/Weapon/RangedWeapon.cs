using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class RangedWeapon : BaseWeapon
{
    [SerializeField] protected GameObject projectilePrefab;
    public override void AnimateAttack(Animator playerAnim)
    {
        playerAnim.Play("AttackRanged");
    }


    public abstract void StartProjectile(Entity entity);
}
