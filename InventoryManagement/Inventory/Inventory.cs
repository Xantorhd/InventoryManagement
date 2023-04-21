namespace InventoryManagement.Inventory;

public class Inventory
{
    private readonly List<Item> _items;

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
        _items.RemoveAll(itm => itm.Name == item.Name);
    }

    public void UpdateItem(Item item, string oldName)
    {
        var index = _items.FindIndex(i => i.Name == oldName);
        
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