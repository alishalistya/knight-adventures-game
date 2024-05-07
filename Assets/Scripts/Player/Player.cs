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
    PlayerCheats cheats;

    [SerializeField] private GameObject[] PetPrefabs;

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

    private void Awake()
    {
        PersistanceManager.Instance.AssertInit();
        cheats = GetComponent<PlayerCheats>();
    }

    new void Start()
    {
        base.Start();
        Inventory = new PlayerInventory(handslot, defaultWeapon, meleeWeapon, thirdWeapon);
    }

    void Update()
    {
        if (IsDead)
        {
            return;
        }

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
        if (weapon is RangedWeapon)
        {
            PersistanceManager.Instance.Statistics.AddTotalShot();
        }
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
        Instantiate(PetPrefabs[(int)item], transform.position, Quaternion.identity);
    }

    public override void TakeDamage(int amount)
    {
        if (cheats.IsCheat(StatusCheats.NO_DAMAGE))
        {
            return;
        }
        base.TakeDamage(amount);
    }
}
