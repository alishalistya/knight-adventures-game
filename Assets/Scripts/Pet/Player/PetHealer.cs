
using System;
using System.Threading.Tasks;
using UnityEngine;

public class PetHealer: BasePetPlayer
{
    protected int baseHealth = 150;
    
    protected int initialHealth;
    
    protected override int MaxHealth => initialHealth;
    protected override int InitialHealth => initialHealth;

    protected override float TimeBetweenAbility => 2f;

    protected int baseHeal = 10;
    protected int heal;
    
    protected override int AbilityEffect => heal;
    
    bool isReadyToHeal = true;
    
    protected string HealAnimationMovement => "Heal";

    protected new void Awake()
    {
        base.Awake();
        initialHealth = (int)(_playerDifficultyMultiplier * baseHealth);
        heal = (int)(_playerDifficultyMultiplier * baseHeal);
    }

    private void FixedUpdate()
    {
        if (!movement.ownerEntity.IsDead 
            && !IsDead 
            && isReadyToHeal
            && Vector3.Distance(movement.owner.transform.position, transform.position) < movement._distanceToOwner
            )
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