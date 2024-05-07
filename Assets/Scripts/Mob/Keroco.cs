using UnityEngine;

public class Keroco : MobMeele
{
    public override int ID => 0;
    protected override string AttackAnimationMovement => "AttackMovement";

    protected override int MaxHealth => 50;
    protected override int InitialHealth => 50;

    protected override float TimeBetweenAttack => 1.5f;

    protected override void OnDamaged(int prevHealth, int currentHealth)
    {

    }
}