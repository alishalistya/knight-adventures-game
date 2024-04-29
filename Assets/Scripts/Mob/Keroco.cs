using UnityEngine;

public class Keroco : Mob
{
    protected override string GetAttackAnimationMovement()
    {
        return "AttackMovement";
    }
    
    void Update()
    {
        if (playerInRange)
        {
            movement.Anim.Play("AttackMovement");
        }
    }
}