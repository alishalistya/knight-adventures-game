using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RangedWeapon
{
  public override int Damage => 5;
  public override float AttackSpeedMultiplier => 2f;
  Vector3 startRelativePosition = new(0.1f, 1.25f, 1.75f);

  public override void StartProjectile(Entity entity)
  {
    for (int i = 0; i < 5; i++)
    {
      AddProjectile(entity, Quaternion.Euler(0, (i - 2) * 5, 0));
    }
  }

  public void AddProjectile(Entity entity, Quaternion rotationOffset)
  {
    Quaternion rotation = entity.transform.rotation * rotationOffset * Quaternion.Euler(0, 270, 90);
    GameObject projectile = Instantiate(projectilePrefab, entity.transform.position + (entity.transform.rotation * startRelativePosition), rotation);
    Projectile projectileScript = projectile.GetComponent<Projectile>();
    projectileScript.direction = rotationOffset * entity.transform.forward;
    projectileScript.Entity = entity;
    projectileScript.IsActive = true;
  }
}
