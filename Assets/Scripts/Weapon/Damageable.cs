using System;
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
    
    protected float _enemyDifficultyMultiplier;
    
    protected float _playerDifficultyMultiplier;

    protected void Awake()
    {
        _enemyDifficultyMultiplier = GameManager.Instance.Difficulty switch
        {
            Difficulty.Easy => 1f,
            Difficulty.Medium => 1.5f,
            Difficulty.Hard => 2f,
            _ => 1f
        };
        _playerDifficultyMultiplier = GameManager.Instance.Difficulty switch
        {
            Difficulty.Easy => 1f,
            Difficulty.Medium => 0.8f,
            Difficulty.Hard => 0.6f,
            _ => 1f
        };
    }
}