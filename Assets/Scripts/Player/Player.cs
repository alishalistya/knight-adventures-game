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

    float _attackSpeed = 1;
    float AttackSpeed
    {
        get { return _attackSpeed; }
        set
        {
            _attackSpeed = value; movement.Anim.SetFloat("AttackSpeed", value);
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
        if (IsAttacking || movement.MovementState == MovementState.Jumping)
        {
            return;
        }
        BaseWeapon weapon = Inventory.CurrentWeapon;
        weapon.AnimateAttack(movement.Anim);
    }

    protected override void OnDeath()
    {
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
    }

    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
        // insert code for play sound effect, update ui here
        // print($"Damaged, current health {currentHealth}");
    }

    public void BuyItem(ShopItem.ShopItemType item)
    {
        ShopItem.BuyItem(item);
    }
}
