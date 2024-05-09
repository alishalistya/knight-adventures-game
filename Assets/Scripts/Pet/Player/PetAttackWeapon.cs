public class PetAttackWeapon : Damageable
{
    protected int baseDamage = 20;
    protected int damage;
    
    public override int Damage  => damage;

    protected new void Awake()
    {
        base.Awake();
        damage = (int)(_playerDifficultyMultiplier * baseDamage);
    }
}