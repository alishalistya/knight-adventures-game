public class MobProjectile: Projectile
{
    public override int Damage => finalDamage;

    protected int finalDamage;

    protected new void Awake()
    {
        base.Awake();
        finalDamage = (int)(_enemyDifficultyMultiplier * damage);
    }
}