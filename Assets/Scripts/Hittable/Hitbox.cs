using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] public Entity entity;

    [SerializeField] public Damageable damageable;
    public delegate void OnHit(Hurtbox hurtbox);
    public event OnHit OnHitEvent;

    private readonly List<Hurtbox> _triggered = new List<Hurtbox>();

    private int defaultLayer;

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

        if (hurtbox == null)
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

        if (hurtbox != null)
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
