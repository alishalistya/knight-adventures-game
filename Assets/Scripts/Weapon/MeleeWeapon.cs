using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
  public override void AnimateAttack(Animator playerAnim)
  {
    playerAnim.Play("AttackMelee", 0);
  }
}
