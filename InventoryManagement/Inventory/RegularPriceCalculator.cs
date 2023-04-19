namespace InventoryManagement.Inventory;

public class RegularPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(Item item)
    {
        return item.Price;
    }
}