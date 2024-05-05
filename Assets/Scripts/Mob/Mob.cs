using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Mob : Entity
{
    [SerializeField] protected MobMovement movement;
    [SerializeField] protected AudioClip audioAwake;
    [SerializeField] protected AudioClip audioDeath;
    [SerializeField] protected AudioSource audioSource;

    protected bool _playerInRange;
    override protected bool IsAttacking { get; set; }

    protected void Awake()
    {
        audioSource.PlayOneShot(audioAwake);
    }

    protected bool PlayerInRange
    {
        get => _playerInRange;
        set
        {
            movement.UseDefaultRotation = !value;
            _playerInRange = value;
        }
    }

    protected abstract float TimeBetweenAttack { get; }
    protected abstract int AttackDamage { get; }

    protected void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == movement.player)
        {
            // ... the player is in range.
            PlayerInRange = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == movement.player)
        {
            // ... the player is no longer in range.
            PlayerInRange = false;
        }
    }

    protected override void OnDeath()
    {
        movement.nav.enabled = false;
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
        audioSource.PlayOneShot(audioDeath);
        Destroy(gameObject, 2f);
    }
}