using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RangedWeapon
{
  public override int Damage => 5;

  public override void StartProjectile(Entity entity)
  {
  }
}
