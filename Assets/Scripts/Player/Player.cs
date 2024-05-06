using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IShopCustomer
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] GameObject handslot;
    [SerializeField] RangedWeapon defaultWeapon;
    [SerializeField] MeleeWeapon meleeWeapon;
    [SerializeField] RangedWeapon thirdWeapon;
    
    [SerializeField] GameObject UIGameOver;

    float _attackSpeed = 1;
    float AttackSpeed
    {
        get { return _attackSpeed; }
        set
        {
            _attackSpeed = value;
        }
    }

    bool _isAttacking = false;
    override protected bool IsAttacking
    {
        get { return _isAttacking; }
        set
        {
            if (!value)
            {
                Inventory.CurrentWeapon.IsActive = false;
            }
            _isAttacking = value; movement.disableMove = value;
            Inventory.IsChangeEnabled = !value;
        }
    }

    public PlayerInventory Inventory;

    protected override int MaxHealth => 100;
    protected override int InitialHealth => 100;

    new void Start()
    {
        base.Start();
        Inventory = new PlayerInventory(handslot, defaultWeapon, meleeWeapon, thirdWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        Inventory?.Update();

        // get mouse is down not currently clicked
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (IsAttacking || movement.PlayerMovementState == PlayerMovementState.Jumping)
        {
            return;
        }
        movement.TurnToCamera();
        BaseWeapon weapon = Inventory.CurrentWeapon;
        movement.Anim.SetFloat("AttackSpeed", AttackSpeed * weapon.AttackSpeedMultiplier);
        weapon.AnimateAttack(movement.Anim);
    }

    protected override void OnDeath()
    {
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
        UIGameOver.SetActive(true);


    }

    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
        PlayerStatsEvents.PlayerStatsChanged(this);
        // insert code for play sound effect, update ui here
        // print($"Damaged, current health {currentHealth}");
    }

    public void BuyItem(ShopItem.ShopItemType item)
    {
        ShopItem.BuyItem(item);
    }
}
