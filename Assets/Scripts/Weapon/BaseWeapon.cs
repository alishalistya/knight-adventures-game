using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseWeapon : Damageable
{
  public virtual float AttackSpeedMultiplier => 1f;
  public virtual void AnimateAttack(Animator playerAnim)
  {
    if (AttackSound != null)
    {
      AttackSound.Play();
    }
  }

  [SerializeField] public AudioSource AttackSound;
}
