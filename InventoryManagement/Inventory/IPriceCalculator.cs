namespace InventoryManagement.Inventory;

public interface IPriceCalculator
{
    decimal CalculatePrice(Item item);
}