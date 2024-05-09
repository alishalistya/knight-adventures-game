using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damageable
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float maxDistance = 200f;
    public Entity Entity
    {
        set =>
        // get hitbox child
        gameObject.GetComponentInChildren<Hitbox>().entity = value;
    }
    public Vector3 direction;
    public Vector3 startPosition;

    public override int Damage => damage;

    void Start()
    {
        startPosition = transform.position;
        Hitbox hitbox = gameObject.GetComponentInChildren<Hitbox>();
        hitbox.damageable = this;
        hitbox.OnHitEvent += OnHit;
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;

        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnHit(Hurtbox hurtbox)
    {
        Destroy(gameObject);
    }
}
