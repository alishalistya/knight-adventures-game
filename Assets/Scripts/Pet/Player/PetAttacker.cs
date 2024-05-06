using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PetAttacker: /* BasePetPlayer */ Entity, IWeaponAnimationHandler
{
    // private PoolableObject Prefab;
    // [SerializeField]
    // private float AttackDelay = 0.33f;
    // [SerializeField]
    // private float AttackMoveSpeed = 1f;
    // [SerializeField] private Player Player;
    // private Coroutine AttackCoroutine;
    // private List<Attackable> AttackableObjects = new List<Attackable>();
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.TryGetComponent<Attackable>(out Attackable attackable))
    //     {
    //         AttackableObjects.Add(attackable);
    //
    //         if (AttackCoroutine != null)
    //         {
    //             StopCoroutine(AttackCoroutine);
    //         }
    //
    //         AttackCoroutine = StopCoroutine(Attack());
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other TryGetComponent<Attackable>(out Attackable attackable))
    //     {
    //         
    //     }
    // }

    public PetPlayerAttackMovement movement;
    protected override bool IsAttacking { get; set; }
    
    protected override int MaxHealth => 30;
    protected override int InitialHealth => 30;
    
    // protected override int AbilityEffect => 100;
    // protected override float TimeBetweenAbility => 1f;
    protected int AbilityEffect => 100;
    protected float TimeBetweenAbility => 1f;

    protected bool _targetInRange;
    
    private bool TargetInRange
    {
        get => _targetInRange;
        set
        {
            movement.UseDefaultRotation = !value;
            _targetInRange = value;
        }
    }
    
    protected Damageable weapon;
    bool isReadyToAttack = true;
    
    protected string AttackAnimationMovement => "Attack";

    protected void Awake()
    {
        weapon = GetComponentInChildren<Damageable>();
    }

    private void FixedUpdate()
    {
        if (TargetInRange && movement.targetEntity is not null && !movement.targetEntity.IsDead && !IsDead && isReadyToAttack)
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
        if (movement.target is null) return;
        
        if (other.gameObject == movement.target)
        {
            TargetInRange = true;
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