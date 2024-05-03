using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable: MonoBehaviour
{
    public abstract int Damage { get; }

    private bool _isActive = false;

    private HashSet<int> registeredHits = new();

    public void RegisterHit(int gameID)
    {
        registeredHits.Add(gameID);
    }

    public bool IsHitRegistered(int gameID)
    {
        return registeredHits.Contains(gameID);
    }

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (value)
            {
                registeredHits.Clear();
            }

            _isActive = value;
        }
    }
}