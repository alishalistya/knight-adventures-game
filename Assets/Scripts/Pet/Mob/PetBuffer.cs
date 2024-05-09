using System;
using System.Threading.Tasks;

public class PetBuffer: BasePetMob
{
    protected int baseHealth = 40;
    protected int initialHealth;
    
    protected override int MaxHealth => initialHealth;
    protected override int InitialHealth => initialHealth;
    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
    }

    protected AttackMultiplierBuff buff;

    protected new void Awake()
    {
        base.Awake();
        // convert to percent
        buff = new AttackMultiplierBuff("pet", AbilityEffect / 100f);
        movement.ownerEntity.AttackMultiplierBuffs.Add(buff);
        initialHealth = (int)(_enemyDifficultyMultiplier * baseHealth);
    }

    protected override void OnDeath()
    {
        movement.ownerEntity.AttackMultiplierBuffs.Remove(buff);   
        base.OnDeath();
    }

    protected override float TimeBetweenAbility => 10f;
    protected override int AbilityEffect => 20;
    
    bool isReadyToBuff = true;
    
    protected string BuffAnimationMovement => "Buff";

    private void FixedUpdate()
    {
        if (!movement.ownerEntity.IsDead && !IsDead && isReadyToBuff)
        {
            Buff();
        }
    }

    void Buff()
    {
        movement.Anim.Play(BuffAnimationMovement);
        isReadyToBuff = false;
        
        Task.Delay((int)(TimeBetweenAbility * 1000)).ContinueWith(t =>
        {
            isReadyToBuff = true;
        });
    }
}