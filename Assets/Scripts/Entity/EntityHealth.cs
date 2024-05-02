using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class ObservableHealth
{
    private int _value;
    private List<Action<int, int>> _subscriber;

    public int value
    {
        get => _value;
        set
        {
            int prevValue = _value;
            _value = value;
            _subscriber.ForEach(func =>
            {
                func(prevValue, value);
            });
        }
    }

    public ObservableHealth(int initialValue)
    {
        this._value = initialValue;
        this._subscriber = new List<Action<int, int>>();
    }

    public void Observe(Action<int, int> action)
    {
        _subscriber.Add(action);
    }
}

public class EntityHealth
{
    public ObservableHealth MaxHealth { get; }
    public ObservableHealth InitialHealth { get; }

    public ObservableHealth CurrentHealth { get; }

    public bool IsDead => CurrentHealth.value <= 0;

    public EntityHealth(
        int maxHealth,
        int initialHealth
    )
    {
        MaxHealth = new ObservableHealth(maxHealth);
        InitialHealth = new ObservableHealth(initialHealth);
        CurrentHealth = new ObservableHealth(initialHealth);
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth.value -= amount;
    }

    public void Heal(int amount)
    {
        CurrentHealth.value = Math.Min(CurrentHealth.value + amount, MaxHealth.value);
    }
}
