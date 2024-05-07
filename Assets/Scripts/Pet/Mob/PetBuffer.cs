using System;
using System.Threading.Tasks;

public class PetBuffer: BasePetMob
{
    protected override int MaxHealth => 50;
    protected override int InitialHealth => 50;
    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
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
        // TODO: Implement the buff effect
        // movement.ownerEntity.DamageMultiplier += AbilityEffect * 0.01f;
        isReadyToBuff = false;
        
        Task.Delay((int)(TimeBetweenAbility * 1000)).ContinueWith(t =>
        {
            isReadyToBuff = true;
        });
    }
}