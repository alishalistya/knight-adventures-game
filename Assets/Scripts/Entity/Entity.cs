using System;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    protected abstract int MaxHealth
    {
        get;
    }
    
    protected abstract int InitialHealth
    {
        get;
    }

    protected EntityHealth Health;

    public bool IsDead => Health.IsDead; 

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

    public void TakeDamage(int amount)
    {
        Health.CurrentHealth.value -= amount;
    }

    public void Heal(int amount)
    {
        Health.CurrentHealth.value = Math.Min(Health.CurrentHealth.value + amount, Health.MaxHealth.value);
    }

    protected abstract void OnDeath();

    protected abstract void OnDamaged(int prevHealth, int currentHealth);
}
