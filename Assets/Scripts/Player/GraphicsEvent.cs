using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsEvent : MonoBehaviour, IWeaponAnimationHandler
{
    [SerializeField] Player player;

    public void OnStartAttackAnim()
    {
        player.OnStartAttackAnim();
    }

    public void OnEndAttackAnim()
    {
        player.OnEndAttackAnim();
    }

    public void OnStartAttackTrigger()
    {
        Debug.Log("OnStartAttackTrigger");
        if (player.Inventory.CurrentWeapon is RangedWeapon weapon)
        {
            weapon.StartProjectile(player);
        }
        else if (player.Inventory.CurrentWeapon is MeleeWeapon meleeWeapon)
        {
            meleeWeapon.IsActive = true;
        }
    }

    public void OnEndAttackTrigger()
    {
        Debug.Log("OnEndAttackTrigger");
        if (player.Inventory.CurrentWeapon is MeleeWeapon meleeWeapon)
        {
            meleeWeapon.IsActive = false;
        }
    }
}
