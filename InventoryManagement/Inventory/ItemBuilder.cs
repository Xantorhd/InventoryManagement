namespace InventoryManagement.Inventory;

public class ItemBuilder
{
    private string _name;
    private int _quantity;
    private int _price;

    public ItemBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ItemBuilder WithQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    public ItemBuilder WithPrice(int price)
    {
        _price = price;
        return this;
    }

    public Item Build()
    {
        return new Item()
        {
            Name = _name,
            Quantity = _quantity,
            Price = _price
        };
    }
}