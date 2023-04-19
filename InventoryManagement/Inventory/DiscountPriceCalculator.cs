namespace InventoryManagement.Inventory;

public class DiscountPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(Item item)
    {
        return item.Price * 0.9m;
    }
}