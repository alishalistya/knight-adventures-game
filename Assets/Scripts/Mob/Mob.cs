using UnityEngine;
using UnityEngine.AI;

public abstract class Mob : MonoBehaviour
{
    [SerializeField] protected MobMovement movement;
    
    protected bool playerInRange;
    protected float timeBetweenAttacks = 0.5f;
    protected int attackDamage = 10;
    protected float attackTimer = 0f;

    protected abstract string GetAttackAnimationMovement();
    
    protected void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == movement.player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == movement.player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }
    
    protected void Update ()
    {
        // Add the time since Update was last called to the timer.
        attackTimer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(attackTimer >= timeBetweenAttacks && playerInRange)
        {
            // ... attack.
            Attack ();
        }
    }

    void Attack ()
    {
        // Reset the timer.
        attackTimer = 0f;
        
        movement.Anim.Play(GetAttackAnimationMovement());

        // // If the player has health to lose...
        // if(playerHealth.currentHealth > 0)
        // {
        //     // ... damage the player.
        //     playerHealth.TakeDamage (attackDamage);
        // }
    }
}