﻿using System;
using System.Threading.Tasks;
using UnityEngine;

public enum KingAttackState
{
    Meele = 0,
    Ranged = 1
}

public class King: Mob, IWeaponAnimationHandler
{
    protected KingMaceWeapon maceWeapon;
    protected KingThrowableWeapon rangedWeapon;

    private bool isReadyToAttack = true;

    private KingAttackState attackState;
    
    protected string MeeleAttackAnimationMovement => "AttackMovement";
    protected string RangedAttackAnimationMovement => "ThrowMovement";
    
    protected override float TimeBetweenAttack => 4f;
    protected float TimeBetweenRangedAttack => 6f;

    protected int baseHealth = 400;
    protected int initialHealth;
    
    protected override int MaxHealth => initialHealth;
    protected override int InitialHealth => initialHealth;
    
    public override int ID => 3;
    
    [SerializeField] protected GameObject kerocoPrefab;
    protected float lastSpawnKeroco = 0f;
    protected float spawnKerocoInterval = 15f;

    protected new void Awake()
    {
        base.Awake();
        maceWeapon = GetComponentInChildren<KingMaceWeapon>();
        rangedWeapon = GetComponent<KingThrowableWeapon>();
        initialHealth = (int)(_difficultyMultiplier * baseHealth);
    }

    private void FixedUpdate()
    {
        lastSpawnKeroco += Time.deltaTime;

        if (lastSpawnKeroco >= spawnKerocoInterval)
        {
            lastSpawnKeroco = 0f;
            var pos = transform.position + new Vector3(-2, 0, -2);
            var spawned = Instantiate(kerocoPrefab, pos, Quaternion.identity);
            spawned.SetActive(true);
        }
        
        if (!movement.playerEntity.IsDead && !IsDead && isReadyToAttack && movement.isTriggered)
        {
            if (PlayerInRange)
            {
                AttackMeele();
            }
            else
            {
                AttackRanged();
            }
        }
    }

    void AttackMeele()
    {
        attackState = KingAttackState.Meele;
        movement.Anim.Play(MeeleAttackAnimationMovement);
    }

    void AttackRanged()
    {
        attackState = KingAttackState.Ranged;
        movement.Anim.Play(RangedAttackAnimationMovement);
    }
    
    public override void OnStartAttackAnim()
    {
        base.OnStartAttackAnim();
        isReadyToAttack = false;
    }

    public override void OnEndAttackAnim()
    {
        base.OnEndAttackAnim();

        if (attackState is KingAttackState.Meele)
        {
            Task.Delay((int)(TimeBetweenAttack * 1000)).ContinueWith(t =>
            {
                isReadyToAttack = true;
            });
        }
        else
        {
            Task.Delay((int)(TimeBetweenRangedAttack * 1000)).ContinueWith(t =>
            {
                isReadyToAttack = true;
            });
        }
    }

    public void OnStartAttackTrigger()
    {
        if (attackState is KingAttackState.Meele)
        {
            maceWeapon.IsActive = true;
        }
        else
        {
            rangedWeapon.StartProjectile(this);
        }
    }

    public void OnEndAttackTrigger()
    {
        if (attackState is KingAttackState.Meele)
        {
            maceWeapon.IsActive = false;
        }
    }
}