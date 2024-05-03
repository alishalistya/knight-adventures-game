using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

public abstract class MobMeele : Mob
{
    protected float attackTimer = 0f;
    protected Damageable weapon;

    protected float hitDelay = 0.3f;
    protected bool hitActivated = false;

    protected abstract string AttackAnimationMovement { get; }

    protected void Awake()
    {
        // we just assume that mob only have one weapon
        weapon = GetComponentInChildren<Damageable>();
    }

    protected void Update ()
    {
        // Add the time since Update was last called to the timer.
        attackTimer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(attackTimer >= TimeBetweenAttack && PlayerInRange && !movement.playerEntity.IsDead)
        {
            // ... attack.
            Attack ();
        }
    }

    void Attack ()
    {
        // Reset the timer.
        attackTimer = 0f;
        hitDelay = 0f;
        hitActivated = false;
        
        movement.Anim.Play(AttackAnimationMovement);
        
        // we delay the hit after some time for sword because we don't want to register hit immediately 
        // at the beginning of the animation
        // instead, we want the sword to hit in the middle of animation
        // this could be improved with better way, but let's just settle with this for now
        // ttd.
        // Akbar
        
        // in the middle of a hit
        Task.Delay(100).ContinueWith(t =>
        {
            weapon.IsActive = true;
        });
        
        // after hit
        Task.Delay(500).ContinueWith(t =>
        {
            weapon.IsActive = false;
        });
    }
}