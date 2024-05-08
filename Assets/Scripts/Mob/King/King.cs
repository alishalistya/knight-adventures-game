using System;
using System.Threading.Tasks;

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
    
    protected override float TimeBetweenAttack => 2f;
    protected float TimeBetweenRangedAttack => 5f;
    
    protected override int MaxHealth => 300;
    protected override int InitialHealth => 300;
    
    public override int ID => 3;

    protected new void Awake()
    {
        base.Awake();
        maceWeapon = GetComponentInChildren<KingMaceWeapon>();
        rangedWeapon = GetComponent<KingThrowableWeapon>();
    }

    private void FixedUpdate()
    {
        if (!movement.playerEntity.IsDead && !IsDead && isReadyToAttack)
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