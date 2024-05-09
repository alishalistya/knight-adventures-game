public class KingMaceWeapon: Damageable
{
    protected int baseDamage = 30;
    protected int damage;

    protected new void Awake()
    {
        base.Awake();
        damage = (int)(_enemyDifficultyMultiplier * baseDamage);
    }
    
    public override int Damage => damage;
}