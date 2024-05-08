using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    private void CreateItemButton(ShopItem.ShopItemType shopItemType, string itemName, int itemCost, int positionIndex, string clonedObjectName)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.name = clonedObjectName;
        shopItemTransform.Find("nameText").GetComponent<TMPro.TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("priceText").GetComponent<TMPro.TextMeshProUGUI>().SetText(itemCost.ToString());

        float shopItemHeight = 50f;
        shopItemTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.gameObject.SetActive(true);

        var button = shopItemTransform.GetComponent<Button>();

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
        CreateItemButton(ShopItem.ShopItemType.Pet_1, "Pet 1", ShopItem.GetCost(ShopItem.ShopItemType.Pet_1), 0, "item1");
        CreateItemButton(ShopItem.ShopItemType.Pet_2, "Pet 2", ShopItem.GetCost(ShopItem.ShopItemType.Pet_2), 1, "item2");

        Hide();
    }

    public void Show(IShopCustomer shopCustomer)
    {
        if (!isShopOpen) return;
        
        var item1 = container.Find("item1");
        var item2 = container.Find("item2");

        if (shopCustomer.CheckGold(ShopItem.GetCost(ShopItem.ShopItemType.Pet_1)))
        {
            setHintForButton(item1, true);
        }
        else
        {
            setHintForButton(item1, false);
        }

        if (shopCustomer.CheckGold(ShopItem.GetCost(ShopItem.ShopItemType.Pet_2)))
        {
            setHintForButton(item2, true);
        }
        else
        {
            setHintForButton(item2, false);
        }
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
    private void setHintForButton(Transform item, bool isActive)
    {
        item.GetComponent<Button>().interactable = isActive;
        item.Find("disableOverlay").gameObject.SetActive(!isActive);
        item.Find("warningText").gameObject.SetActive(!isActive);
    }
}
