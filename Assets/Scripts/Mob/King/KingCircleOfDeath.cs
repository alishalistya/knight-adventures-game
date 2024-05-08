﻿using UnityEngine;

public class KingCircleOfDeath: Damageable
{
    public override int Damage => 5;

    private float activeTime = 0f;
    private float hitInterval = 1f; // every 1 seconds

    private void Awake()
    {
        IsActive = true;
    }

    private void FixedUpdate()
    {
        activeTime += Time.deltaTime;

        if (activeTime >= hitInterval)
        {
            activeTime = 0;
            IsActive = false;
            IsActive = true;
        }
    }
}