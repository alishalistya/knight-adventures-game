public class KerocoWeapon: Damageable
{
    protected int baseDamage = 10;
    protected int _damage;
    
    public override int Damage => _damage;

    protected new void Awake()
    {
        base.Awake();
        _damage = (int)(_enemyDifficultyMultiplier * baseDamage);
    }
}