using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEnabler : MonoBehaviour
{
    bool isShopOpen;

    [SerializeField] private UI_Shop uiShop;

    void Start()
    {
        isShopOpen = false;

    }

    private void OnTriggerStay(Collider other)
    {
        IShopCustomer customer = other.gameObject.GetComponent<IShopCustomer>();

        var shopBtnObj = GameObject.FindGameObjectWithTag("ShopButton");
        print(shopBtnObj);
        if (shopBtnObj != null)
        {
            var shopButton = shopBtnObj.GetComponent<Button>();
            shopButton.onClick.RemoveAllListeners();
            shopButton.onClick.AddListener(() =>
            {

                if (!isShopOpen && customer != null)
                {
                    uiShop.Show(customer);
                    isShopOpen = true;
                }
                else if (isShopOpen && customer != null)
                {
                    uiShop.Hide();
                    isShopOpen = false;
                }
            });
        }

        if (!isShopOpen && Input.GetKeyDown(KeyCode.B) && customer != null)
        {
            uiShop.Show(customer);
            isShopOpen = true;
            return;
        }
        else if (isShopOpen && Input.GetKeyDown(KeyCode.B) && customer != null)
        {
            uiShop.Hide();
            isShopOpen = false;
            return;
        }

        else if (isShopOpen && Input.GetKeyDown(KeyCode.Alpha1) && customer != null)
        {
            uiShop.TryBuyItem(ShopItem.ShopItemType.Pet_1);
            return;
        }
        else if (isShopOpen && Input.GetKeyDown(KeyCode.Alpha2) && customer != null)
        {
            uiShop.TryBuyItem(ShopItem.ShopItemType.Pet_2);
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IShopCustomer customer = other.gameObject.GetComponent<IShopCustomer>();
        if (customer != null)
        {
            uiShop.Hide();
            isShopOpen = false;
            return;
        }
    }
}
