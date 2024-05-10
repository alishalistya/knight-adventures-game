using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PetAttacker : /* BasePetPlayer */ Entity, IWeaponAnimationHandler
{
    public PetPlayerAttackMovement movement;
    protected override bool IsAttacking { get; set; }

    protected int baseHealth = 150;

    protected int initialHealth;

    protected override int MaxHealth => initialHealth;
    protected override int InitialHealth => initialHealth;

    protected int AbilityEffect => 100;
    protected float TimeBetweenAbility => 2f;

    protected bool _targetInRange;

    private bool TargetInRange
    {
        get => _targetInRange;
        set
        {
            _targetInRange = value;
        }
    }

    protected float _playerDifficultyMultiplier;

    protected Damageable weapon;
    bool isReadyToAttack = true;

    protected string AttackAnimationMovement => "Attack";

    protected void Awake()
    {
        weapon = GetComponentInChildren<Damageable>();
        _playerDifficultyMultiplier = GameManager.Instance.Difficulty switch
        {
            Difficulty.Easy => 1f,
            Difficulty.Medium => 0.8f,
            Difficulty.Hard => 0.6f,
            _ => 1f
        };
        initialHealth = (int)(_playerDifficultyMultiplier * baseHealth);
    }

    private void FixedUpdate()
    {
        if (TargetInRange && movement.targetEntity != null && !movement.targetEntity.IsDead && !IsDead && isReadyToAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        movement.Anim.Play(AttackAnimationMovement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (movement.target == null) return;

        if (other.gameObject == movement.target)
        {
            TargetInRange = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject == movement.target)
        {
            TargetInRange = false;
        }
    }


    public override void OnStartAttackAnim()
    {
        base.OnStartAttackAnim();
        isReadyToAttack = false;
    }

    public override void OnEndAttackAnim()
    {
        base.OnEndAttackAnim();
        Task.Delay((int)(TimeBetweenAbility * 1000)).ContinueWith(t =>
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

    protected override void OnDeath()
    {
        movement.nav.enabled = false;
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
        Destroy(gameObject, 2f);
    }


    protected override void OnDamaged(int prevHealth, int currentHealth)
    {

    }
}