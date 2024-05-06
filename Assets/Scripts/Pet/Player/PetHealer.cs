
using System;
using System.Threading.Tasks;

public class PetHealer: BasePetPlayer
{
    protected override int MaxHealth => 30;
    protected override int InitialHealth => 30;

    protected override float TimeBetweenAbility => 5f;
    protected override int AbilityEffect => 10;
    
    bool isReadyToHeal = true;
    protected string HealAnimationMovement => "Heal";

    private void FixedUpdate()
    {
        if (!movement.ownerEntity.IsDead && !IsDead && isReadyToHeal)
        {
            Heal();
        }
    }
    
    void Heal()
    {
        movement.Anim.Play(HealAnimationMovement);
        movement.ownerEntity.Heal(AbilityEffect);
        isReadyToHeal = false;
        
        Task.Delay((int)(TimeBetweenAbility * 1000)).ContinueWith(t =>
        {
            isReadyToHeal = true;
        });
    }

    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
    }
}