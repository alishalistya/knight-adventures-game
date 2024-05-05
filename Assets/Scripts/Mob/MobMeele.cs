using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

public abstract class MobMeele : Mob, IWeaponAnimationHandler
{
    protected Damageable weapon;
    bool isReadyToAttack = true;

    protected abstract string AttackAnimationMovement { get; }

    protected void Awake()
    {
        base.Awake();
        // we just assume that mob only have one weapon
        weapon = GetComponentInChildren<Damageable>();
    }


    private void FixedUpdate()
    {
        if (PlayerInRange && !movement.playerEntity.IsDead && !IsDead && isReadyToAttack)
        {
            Attack();
        }
    }

    void Attack()
    {

        movement.Anim.Play(AttackAnimationMovement);
    }

    public override void OnStartAttackAnim()
    {
        base.OnStartAttackAnim();
        isReadyToAttack = false;
    }

    public override void OnEndAttackAnim()
    {
        base.OnEndAttackAnim();
        Task.Delay((int)(TimeBetweenAttack * 1000)).ContinueWith(t =>
        {
            isReadyToAttack = true;
        });
    }

    public void OnStartAttackTrigger()
    {
        weapon.IsActive = true;
    }

    public void OnEndAttackTrigger()
    {
        weapon.IsActive = false;
    }
}