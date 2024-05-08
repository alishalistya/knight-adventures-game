using UnityEngine;

public class KingThrowableWeapon: RangedMobWeapon
{
   
    [SerializeField] Vector3 startRelativePosition = new(0.5f, 0.5f, 0.5f);

    public override void StartProjectile(Entity entity)
    {
        Quaternion rotation = entity.transform.rotation;
        GameObject projectile = Instantiate(projectilePrefab, entity.transform.position + (entity.transform.rotation * startRelativePosition), rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.direction = entity.transform.forward;
        projectileScript.Entity = entity;
        projectileScript.IsActive = true;
    }
}