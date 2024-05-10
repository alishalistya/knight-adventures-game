using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RangedWeapon
{
  public override int Damage => 0;
  public override float AttackSpeedMultiplier => 2f;
  Vector3 startRelativePosition = new(0.1f, 1.25f, 1.75f);

  public override void StartProjectile(Entity entity)
  {
    bool hasHit = false;
    for (int i = 0; i < 5; i++)
    {
      AddProjectile(entity, Quaternion.Euler(0, (i - 2) * 5, 0), () =>
      {
        if (!hasHit)
        {
          hasHit = true;
          PersistanceManager.Instance.GlobalStat.AddTotalHit();
          GameManager.Instance.Statistics.AddTotalHit();
        }
      });
    }
  }

  public void AddProjectile(Entity entity, Quaternion rotationOffset, Action onHit)
  {
    Quaternion rotation = entity.transform.rotation * rotationOffset * Quaternion.Euler(0, 270, 90);
    GameObject projectile = Instantiate(projectilePrefab, entity.transform.position + (entity.transform.rotation * startRelativePosition), rotation);
    Projectile projectileScript = projectile.GetComponent<Projectile>();
    projectileScript.OnHitEnemyEvent += () =>
    {
      onHit();
    };
    projectileScript.direction = rotationOffset * entity.transform.forward;
    projectileScript.Entity = entity;
    projectileScript.IsActive = true;
  }
}
