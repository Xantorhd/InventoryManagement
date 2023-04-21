using InventoryManagement.Inventory;

namespace InventoryManagement.Mocks;

public class FakeItem : Item
{
    public FakeItem(string name, int quantity, int price)
    {
        Name = name;
        
        Quantity = quantity;
        
        Price = price;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void SetPrice(int price)
    {
        Price = price;
    }
}