using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected abstract int MaxHealth
    {
        get;
    }

    protected abstract int InitialHealth
    {
        get;
    }

    public EntityHealth Health;

    protected HashSet<AttackMultiplierBuff> _attackMultiplierBuffs = new();

    public HashSet<AttackMultiplierBuff> AttackMultiplierBuffs => _attackMultiplierBuffs;

    public bool IsDead => Health.IsDead;

    protected virtual float _damageMultiplier => 1f;

    public float DamageMultiplier
    {
        get
        {
            return _damageMultiplier + _attackMultiplierBuffs.Sum(mul => mul.Value);
        }
    }

    protected abstract bool IsAttacking { get; set; }

    protected void Start()
    {
        Health = new EntityHealth(MaxHealth, InitialHealth);
        Health.CurrentHealth.Observe((prevHealth, currentHealth) =>
        {
            OnDamaged(prevHealth, currentHealth);

            if (Health.IsDead)
            {
                OnDeath();
            }
        });
    }

    public virtual void TakeDamage(int amount)
    {
        Health.CurrentHealth.value = Math.Max(Health.CurrentHealth.value - amount, 0);
    }

    public void Heal(int amount)
    {
        Health.CurrentHealth.value = Math.Min(Health.CurrentHealth.value + amount, Health.MaxHealth.value);
    }

    protected abstract void OnDeath();

    protected abstract void OnDamaged(int prevHealth, int currentHealth);

    public virtual void OnStartAttackAnim()
    {
        IsAttacking = true;
    }

    public virtual void OnEndAttackAnim()
    {
        IsAttacking = false;
    }
}
