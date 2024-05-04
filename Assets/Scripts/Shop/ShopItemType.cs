using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public enum ShopItemType
    {
        Pet_1,
        Pet_2,
        Pet_3,
    }

    public static int GetCost(ShopItemType shopItemType)
    {
        switch (shopItemType)
        {
            default:
            case ShopItemType.Pet_1: return 10;
            case ShopItemType.Pet_2: return 20;
            case ShopItemType.Pet_3: return 30;
        }
    }

    public static void BuyItem(ShopItemType shopItemType)
    {
        switch (shopItemType)
        {
            default:
            case ShopItemType.Pet_1: Debug.Log("Pet 1 bought"); break;
            case ShopItemType.Pet_2: Debug.Log("Pet 2 bought"); break;
            case ShopItemType.Pet_3: Debug.Log("Pet 3 bought"); break;
        }
    }
}
