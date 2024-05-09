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

    public void TryBuyItem(ShopItem.ShopItemType itemName)
    {
        if (shopCustomer.CheckGold(ShopItem.GetCost(itemName)))
        {
            shopCustomer.BuyItem(itemName);
            shopCustomer.RemoveGold(ShopItem.GetCost(itemName));
        }
        else
        {
            Debug.Log("Not enough gold");
        }
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
            SetHintForButton(item1, true, 1);
        }
        else
        {
            SetHintForButton(item1, false, 1);
        }

        if (shopCustomer.CheckGold(ShopItem.GetCost(ShopItem.ShopItemType.Pet_2)))
        {
            SetHintForButton(item2, true, 2);
        }
        else
        {
            SetHintForButton(item2, false, 2);
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
    private void SetHintForButton(Transform item, bool isActive, int itemNumber)
    {
        item.GetComponent<Button>().interactable = isActive;
        item.Find("disableOverlay").gameObject.SetActive(!isActive);
        if (isActive)
        {
            item.Find("warningText").GetComponent<TMPro.TextMeshProUGUI>().SetText("Click or Press " + itemNumber + " to buy");
            item.Find("warningText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
            return;
        } else
        {
            item.Find("warningText").GetComponent<TMPro.TextMeshProUGUI>().SetText("Not enough gold");
            item.Find("warningText").GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
        }
    }
}
