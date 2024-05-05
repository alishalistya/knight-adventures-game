using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseWeapon : Damageable
{
  public virtual float AttackSpeedMultiplier => 1f;
  public abstract void AnimateAttack(Animator playerAnim);
}
