using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : BaseWeapon
{
    public override void AnimateAttack(Animator playerAnim)
    {
        playerAnim.Play("AttackRanged");
    }
    
    public override int Damage => 0;
}
