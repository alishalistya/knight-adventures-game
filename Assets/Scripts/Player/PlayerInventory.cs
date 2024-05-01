using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    GameObject handslot;
    BaseWeapon[] weapons;
    BaseWeapon _currentWeapon;

    float lastWeaponChangeTime = 0;
    public BaseWeapon CurrentWeapon
    {
        get { return _currentWeapon; }
        set
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.gameObject.SetActive(false);
            }
            _currentWeapon = value;
            _currentWeapon.gameObject.SetActive(true);
        }
    }

    int _currentWeaponIndex;

    int CurrentWeaponIndex
    {
        get { return _currentWeaponIndex; }
        set
        {
            while (value < 0)
            {
                value += weapons.Length;
            }
            int resolvedIndex = value % weapons.Length;
            CurrentWeapon = weapons[resolvedIndex];
            _currentWeaponIndex = resolvedIndex;
        }
    }

    public PlayerInventory(GameObject handslot, RangedWeapon defaultWeapon, MeleeWeapon meleeWeapon, RangedWeapon thirdWeapon)
    {
        weapons = new BaseWeapon[] { defaultWeapon, meleeWeapon, thirdWeapon };
        CurrentWeapon = defaultWeapon;
        this.handslot = handslot;
    }

    void CheckWeaponChange()
    {
        if (Time.time - lastWeaponChangeTime < 0.1f)
        {
            return;
        }
        bool weaponChanged = true;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            CurrentWeaponIndex++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            CurrentWeaponIndex--;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentWeaponIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentWeaponIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentWeaponIndex = 2;
        }
        else
        {
            weaponChanged = false;
        }
        if (weaponChanged)
        {
            lastWeaponChangeTime = Time.time;
        }
    }

    public void Update()
    {
        CheckWeaponChange();
    }
}
