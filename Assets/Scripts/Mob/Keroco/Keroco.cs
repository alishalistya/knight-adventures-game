using UnityEngine;

public class Keroco : MobMeele
{
    public override int ID => 0;
    protected override string AttackAnimationMovement => "AttackMovement";

    private int baseHealth = 50;
    private int initialHealth;

    protected new void Awake()
    {
        base.Awake();
        initialHealth = (int)(baseHealth * _difficultyMultiplier);
    }

    protected override int MaxHealth => initialHealth;
    protected override int InitialHealth => initialHealth;

    protected override float TimeBetweenAttack => 2f;
}