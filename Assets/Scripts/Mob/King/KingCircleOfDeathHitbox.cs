using System;
using System.Collections.Generic;
using UnityEngine;


public class KingCircleOfDeathHitbox : MonoBehaviour
{
    [SerializeField] public Entity entity;
    [SerializeField] public KingCircleOfDeath damageable;
    
    private AttackMultiplierBuff attackDebuff;
    private MovementSpeedMultiplierBuff movementDebuff;
    
    public delegate void OnHit(Hurtbox hurtbox);
    
    public event OnHit OnHitEvent;

    private readonly List<Hurtbox> _triggered = new List<Hurtbox>();

    private int defaultLayer;

    private float AttackDebuffValue => -0.1f;
    private float MovementSpeedDebuffValue => -0.2f;

    protected void Awake()
    {
        defaultLayer = LayerMask.NameToLayer("Default");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != entity.gameObject && other.gameObject.layer == defaultLayer)
        {
            OnHitEvent?.Invoke(null);
            return;
        }
        
        var hurtbox = other.gameObject.GetComponent<Hurtbox>();

        if (hurtbox is null)
        {
            return;
        }

        var player = (Player)hurtbox.entity;

        if (player is not null)
        {
            attackDebuff = new AttackMultiplierBuff("kingatkdebuff", AttackDebuffValue);
            player.AttackMultiplierBuffs.Add(attackDebuff);
            movementDebuff = new MovementSpeedMultiplierBuff("kingmovdebuff", MovementSpeedDebuffValue);
            player.Movement.MovementSpeedMultiplierBuffs.Add(movementDebuff);
        }
        
        _triggered.Add(hurtbox);

        PerformDamage();
    }

    private void Update()
    {
        PerformDamage();
    }

    private void OnTriggerExit(Collider other)
    {
        var hurtbox = other.gameObject.GetComponent<Hurtbox>();

        if (hurtbox is not null)
        {
            _triggered.Remove(hurtbox);
            
            var player = (Player)hurtbox.entity;

            if (player is not null)
            {
                attackDebuff.IsActive = false;
                player.AttackMultiplierBuffs.Remove(attackDebuff);
                attackDebuff = null;
                movementDebuff.IsActive = false;
                player.Movement.MovementSpeedMultiplierBuffs.Remove(movementDebuff);
                movementDebuff = null;
            }
        }
    }

    private void PerformDamage()
    {
        if (!damageable.IsActive)
        {
            return;
        }

        _triggered.ForEach(trigger =>
        {
            var instanceId = trigger.GetInstanceID();
            if (!damageable.IsHitRegistered(instanceId) && !trigger.entity.IsDead)
            {
                damageable.RegisterHit(instanceId);
                var damage = (int)(entity.DamageMultiplier * damageable.Damage);
                trigger.entity.TakeDamage(damage);
                OnHitEvent?.Invoke(trigger);
                print($"Giving damage {damage}");
            }
        });
    }
}