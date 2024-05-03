using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class MeleeWeapon : BaseWeapon
{
  public override void AnimateAttack(Animator playerAnim)
  {
    playerAnim.Play("AttackMelee", 0);
    
    // we delay the hit after some time for sword because we don't want to register hit immediately 
    // at the beginning of the animation
    // instead, we want the sword to hit in the middle of animation
    // this could be improved with better way, but let's just settle with this for now
    // ttd.
    // Akbar
        
    // in the middle of a hit
    Task.Delay(200).ContinueWith((t) =>
    {
      IsActive = true;
    });
  }
}
