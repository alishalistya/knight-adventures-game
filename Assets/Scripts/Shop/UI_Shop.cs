using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Transform shopItemTemplate;
    private Transform container;
    private IShopCustomer shopCustomer;
    private bool isShopOpen = false;
    private bool isShopHadBeenOpened = false;
    

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void CreateItemButton(ShopItem.ShopItemType shopItemType, string itemName, int itemCost, int positionIndex)
    {
        
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.Find("nameText").GetComponent<TMPro.TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("priceText").GetComponent<TMPro.TextMeshProUGUI>().SetText(itemCost.ToString());

        float shopItemHeight = 50f;
        shopItemTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.gameObject.SetActive(true);

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyItem(shopItemType);
        });

    }

    private void TryBuyItem(ShopItem.ShopItemType itemName)
    {
        shopCustomer.BuyItem(itemName);
    }
    void Start()
    {
        CreateItemButton(ShopItem.ShopItemType.Pet_1, "Pet 1", ShopItem.GetCost(ShopItem.ShopItemType.Pet_1), 0);
        CreateItemButton(ShopItem.ShopItemType.Pet_2, "Pet 2", ShopItem.GetCost(ShopItem.ShopItemType.Pet_2), 1);
        CreateItemButton(ShopItem.ShopItemType.Pet_3, "Pet 3", ShopItem.GetCost(ShopItem.ShopItemType.Pet_3), 2);

        Hide();
    }

    public void Show(IShopCustomer shopCustomer)
    {
        if (!isShopOpen) return;
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetShopOpen(bool isShopOpen)
    {
        this.isShopOpen = isShopOpen;
    }

    public void SetShopHadBeenOpened(bool isShopHadBeenOpened)
    {
        this.isShopHadBeenOpened = isShopHadBeenOpened;
    }
}
