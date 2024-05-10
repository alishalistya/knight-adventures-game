using System.Numerics;

public class ShotgunProjectile : Projectile
{
    protected int baseDamage;

    public override int Damage
    {
        get
        {
            var dist = (startPosition - transform.position).magnitude;

            if (dist < 5f)
            {
                return baseDamage;
            }

            if (dist < 10f)
            {
                return (int)(0.5 * baseDamage);
            }

            return (int)(0.25 * baseDamage);
        }
    }

    protected new void Awake()
    {
        base.Awake();
        baseDamage = (int)(_playerDifficultyMultiplier * damage);
    }
}