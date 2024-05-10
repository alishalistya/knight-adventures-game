using UnityEngine;

public abstract class Mob : Entity
{
    public abstract int ID { get; }
    [SerializeField] protected MobMovement movement;
    [SerializeField] protected AudioClip audioAwake;
    [SerializeField] protected AudioClip audioDeath;
    [SerializeField] protected AudioClip audioHurt;
    [SerializeField] protected AudioClip audioAttack;
    [SerializeField] protected AudioSource audioSource;

    protected float _difficultyMultiplier;

    protected bool _playerInRange;
    protected override bool IsAttacking { get; set; }

    protected void Awake()
    {
        audioSource.PlayOneShot(audioAwake);

        _difficultyMultiplier = GameManager.Instance.Difficulty switch
        {
            Difficulty.Easy => 1f,
            Difficulty.Medium => 1.5f,
            Difficulty.Hard => 2f,
            _ => 1f
        };
    }

    protected bool PlayerInRange
    {
        get => _playerInRange;
        set { _playerInRange = value; }
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
        if (other != null && other.gameObject == movement.player)
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
        PersistanceManager.Instance.GlobalStat.AddKill();
        GameManager.Instance.Statistics.AddKill();
        Destroy(gameObject, 2f);
    }

    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
        if (currentHealth < prevHealth)
        {
            PersistanceManager.Instance.GlobalStat.AddDamageDealt(prevHealth - currentHealth);
            GameManager.Instance.Statistics.AddDamageDealt(prevHealth - currentHealth);
        }
        if (audioHurt != null)
        {
            audioSource.PlayOneShot(audioHurt);
        }
    }

    public override void OnStartAttackAnim()
    {
        base.OnStartAttackAnim();
        if (audioAttack != null)
        {
            audioSource.PlayOneShot(audioAttack);
        }

        if (movement.nav.isOnNavMesh)
        {
            movement.nav.isStopped = true;
        }
    }

    public override void OnEndAttackAnim()
    {
        if (movement.nav.isOnNavMesh)
        {
            movement.nav.isStopped = false;
        }
    }
}