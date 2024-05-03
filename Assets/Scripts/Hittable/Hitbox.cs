using System;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox: MonoBehaviour
{
    [SerializeField] public Entity entity;

    [SerializeField] public Damageable damageable;

    private readonly List<Hurtbox> _triggered = new List<Hurtbox>();

    private void OnTriggerEnter(Collider other)
    {
        var hurtbox = other.gameObject.GetComponent<Hurtbox>();
        
        if (hurtbox == null || !damageable.IsActive)
        {
            return;
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
            if (!damageable.IsHitRegistered(instanceId) && !trigger.entiy.IsDead)
            {
                damageable.RegisterHit(instanceId);
                var damage = (int)(entity.DamageMultiplier * damageable.Damage);
                trigger.entiy.TakeDamage(damage);
                print($"Giving damage {damage}");
            }
        });
    }
}
