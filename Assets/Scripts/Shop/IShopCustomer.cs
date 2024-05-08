public interface IShopCustomer
{
    void BuyItem(ShopItem.ShopItemType item);
    void AddGold(int amount);
    void RemoveGold(int amount);
    bool CheckGold(int cost);
}