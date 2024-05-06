using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public abstract class BasePet<TEntity> : Entity where TEntity : Entity
{
    // public PetMovement movement;
    //
    // protected bool _enemyInRange;
    // override protected bool IsAttacking { get; set; }
    //
    // protected bool EnemyInRange
    // {
    //     get => _enemyInRange;
    //     set
    //     {
    //         movement.UseDefaultRotation = !value;
    //         _enemyInRange = value;
    //     }
    // }
    //
    // protected abstract float TimeBetweenAbility { get; }
    // protected abstract int Ability { get; }

    // protected void OnTriggerEnter(Collider other)
    // {
    //     // If the entering collider is the player...
    //     if (other.gameObject == movement.target)
    //     {
    //         // ... the player is in range.
    //         EnemyInRange = true;
    //     }
    // }
    //
    // protected void OnTriggerExit(Collider other)
    // {
    //     // If the exiting collider is the player...
    //     if (other.gameObject == movement.target)
    //     {
    //         // ... the player is no longer in range.
    //         EnemyInRange = false;
    //     }
    // }
    //
    // protected override void OnDeath()
    // {
    //     movement.nav.enabled = false;
    //     movement.enabled = false;
    //     movement.Anim.SetTrigger("Death");
    //     Destroy(gameObject, 2f);
    // }
    
    public PetMovement<TEntity> movement;
    
    protected override bool IsAttacking { get; set; }
    protected abstract float TimeBetweenAbility { get; }
    protected abstract int AbilityEffect { get; }
    
    protected override void OnDeath()
    {
        movement.nav.enabled = false;
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
        Destroy(gameObject, 2f);
    }
}