using InventoryManagement.Inventory;
using InventoryManagement.Localization;

namespace InventoryManagement.ConsoleMenu.Menus;

public class MainMenu : Menu
{
    public MainMenu() : base(new MenuItem(GetItems())
    {
        NameEnum = TextEnum.MenuTitleMain,
        MaxColumns = 1
    })
    { }

    private static MenuItem[] GetItems()
    {
        return new[]
        {
            new MenuItem(new []
                {
                    new MenuItem(ShowInventoryItems)
                        { NameEnum = TextEnum.OptionShowInventoryItems },
                    new MenuItem(ShowCreateItem)
                        { NameEnum = TextEnum.OptionCreateInventoryItem }
                })
                { NameEnum = TextEnum.MenuTitleInventory },
            new MenuItem(GetLanguageItems())
                { NameEnum = TextEnum.MenuTitleLanguages }
        };
    }

    private static void ShowInventoryItems()
    {
        if (MenuManager.GetMenu().Selected.Items.Count > 0)
        {
            return;
        }

        var inventory = Program.InventoryManager.LoadInventory();

        foreach (var item in inventory.GetItems())
        {
            MenuManager.GetMenu().Selected.Add(CreateMenuItemForItem(item));
        }

        static MenuItem CreateMenuItemForItem(Item item)
        {
            return new MenuItem(item.Name, new[]
            {
                new MenuItem(LocalizationManager.GetText(TextEnum.ItemName) + ": " + item.Name, EditItemName),
                new MenuItem(LocalizationManager.GetText(TextEnum.ItemQuantity) + ": " + item.Quantity, EditItemQuantity),
                new MenuItem(LocalizationManager.GetText(TextEnum.ItemPrice) + ": " + item.Price, EditItemPrice)
            });
        }
    }

    private static void ShowCreateItem()
    {
        
    }

    private static void EditItemName()
    {
        MenuManager.GetMenu().WriteLine("Edit Item");
    }

    private static void EditItemQuantity()
    {
        
    }

    private static void EditItemPrice()
    {
        
    }
}