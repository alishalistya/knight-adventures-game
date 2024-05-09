using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public abstract class BasePet<TEntity> : Entity where TEntity : Entity
{
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
    
    public PetMovement<TEntity> movement;
    
    protected override bool IsAttacking { get; set; }
    protected abstract float TimeBetweenAbility { get; }
    protected abstract int AbilityEffect { get; }
    
    protected override void OnDeath()
    {
        movement.nav.enabled = false;
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
        Destroy(gameObject, 2f);
    }
}