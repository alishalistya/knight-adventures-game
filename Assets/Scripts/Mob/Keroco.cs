using UnityEngine;

public class Keroco : MobMeele
{
    protected override string AttackAnimationMovement => "AttackMovement";

    protected override int MaxHealth => 50;
    protected override int InitialHealth => 50;
    
    protected override int AttackDamage => 10;

    protected override float TimeBetweenAttack => 2f;

    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
        // todo
    }

    protected override void OnDeath()
    {
        //
    }
}