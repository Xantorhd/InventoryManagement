namespace InventoryManagement.Inventory;

public class Inventory
{
    private List<Item> _items;

    public Inventory()
    {
        _items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
    }

    public void UpdateItem(Item item)
    {
        var index = _items.FindIndex(i => i.Name == item.Name);
        
        if (index != -1)
        {
            _items[index] = item;
        }
    }

    public List<Item> GetItems()
    {
        return _items;
    }
}