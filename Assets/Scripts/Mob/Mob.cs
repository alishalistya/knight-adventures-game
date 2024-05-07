using UnityEngine;

public abstract class Mob : Entity
{
    public abstract int ID {get;}
    [SerializeField] protected MobMovement movement;
    [SerializeField] protected AudioClip audioAwake;
    [SerializeField] protected AudioClip audioDeath;
    [SerializeField] protected AudioSource audioSource;

    protected bool _playerInRange;
    protected override bool IsAttacking { get; set; }

    protected void Awake()
    {
        audioSource.PlayOneShot(audioAwake);
    }

    protected bool PlayerInRange
    {
        get => _playerInRange;
        set
        {
            _playerInRange = value;
        }
    }

    protected abstract float TimeBetweenAttack { get; }

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
        if (other is not null && other.gameObject == movement.player)
        {
            // ... the player is no longer in range.
            PlayerInRange = false;
        }
    }

    protected override void OnDeath()
    {
        CombatEvents.MobKilled(transform.position, this);
        movement.nav.enabled = false;
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
        audioSource.PlayOneShot(audioDeath);
        Destroy(gameObject, 2f);
    }
}