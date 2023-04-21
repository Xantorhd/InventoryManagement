namespace InventoryManagement.Inventory;

public class InventoryManager
{
    private readonly string _filePath;

    public InventoryManager(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveInventory(Inventory inventory)
    {
        using (var writer = new StreamWriter(_filePath))
        {
            foreach (var item in inventory.GetItems())
            {
                writer.WriteLine($"{item.Name},{item.Quantity},{item.Price}");
            }
        }
    }

    public Inventory LoadInventory()
    {
        var inventory = new Inventory();
        
        if (!File.Exists(_filePath))
        {
            return inventory;
        }

        var lines = File.ReadAllLines(_filePath);
        
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var item = new Item
            {
                Name = parts[0],
                Quantity = int.Parse(parts[1]),
                Price = int.Parse(parts[2])
            };
            
            inventory.AddItem(item);
        }

        return inventory;
    }
}