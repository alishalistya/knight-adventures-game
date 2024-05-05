public class SuperKeroco: MobRanged
{
    public override int ID => 1;
    protected override string AttackAnimationMovement => "AttackMovement";
    
    protected override int MaxHealth => 75;
    protected override int InitialHealth => 75;
    
    protected override float TimeBetweenAttack => 2f;
    
    protected override void OnDamaged(int prevHealth, int currentHealth)
    {

    }
}