using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] GameObject handslot;
    [SerializeField] RangedWeapon defaultWeapon;
    [SerializeField] MeleeWeapon meleeWeapon;
    [SerializeField] RangedWeapon thirdWeapon;

    readonly float baseAttackSecond = 1.1f / 1.5f;

    float _attackSpeed = 1;
    float AttackSpeed
    {
        get { return _attackSpeed; }
        set
        {

            _attackSpeed = value; movement.Anim.SetFloat("AttackSpeed", value);
        }
    }

    float AttackTime
    {
        get { return baseAttackSecond / AttackSpeed; }
    }

    bool _isAttacking = false;
    bool IsAttacking
    {
        get { return _isAttacking; }
        set
        {
            if (value)
            {
                lastAttackTime = Time.time;
            }
            _isAttacking = value; movement.disableMove = value;
        }
    }

    float lastAttackTime;

    PlayerInventory inventory;

    void Start()
    {
        inventory = new PlayerInventory(handslot, defaultWeapon, meleeWeapon, thirdWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsAttacking();
        inventory?.Update();

        // get mouse is down not currently clicked
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    void UpdateIsAttacking()
    {
        if (IsAttacking && Time.time - lastAttackTime > AttackTime)
        {
            IsAttacking = false;
        }
    }

    void Attack()
    {
        if (IsAttacking || movement.MovementState == MovementState.Jumping)
        {
            return;
        }
        IsAttacking = true;
        BaseWeapon weapon = inventory.CurrentWeapon;
        weapon.AnimateAttack(movement.Anim);
    }
}
