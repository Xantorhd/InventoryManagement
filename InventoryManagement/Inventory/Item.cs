namespace InventoryManagement.Inventory;

public class Item
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public IPriceCalculator PriceCalculator { get; set; }
    
    public void ApplyDiscount()
    {
        Price *= 0.9m;
    }
}