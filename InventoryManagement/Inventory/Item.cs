namespace InventoryManagement.Inventory;

public class Item
{
    public string Name
    {
        get; 
        set;
    }

    public int Quantity
    {
        get; 
        set;
    }

    public int Price
    {
        get; 
        set;
    }

    public IPriceCalculator PriceCalculator
    {
        get; 
        set;
    }
}