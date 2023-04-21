using System.Net.Mime;
using InventoryManagement.Inventory;
using InventoryManagement.Localization;
using InventoryManagement.Users;

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
                    {
                        NameEnum = TextEnum.OptionShowInventoryItems
                    },
                    new MenuInputItem(LocalizationManager.GetText(TextEnum.OptionCreateInventoryItem),
                        LocalizationManager.GetText(TextEnum.ItemName), CreateItem)
                    {
                        NameEnum = TextEnum.OptionCreateInventoryItem
                    }
                })
            {
                NameEnum = TextEnum.MenuTitleInventory
            },
            new MenuItem(AddUserItems)
            {
                NameEnum = TextEnum.MenuTitleUsers
            },
            new MenuItem(GetLanguageItems())
            {
                NameEnum = TextEnum.MenuTitleLanguages
            },
            new MenuItem(MenuManager.PrintHelp)
            {
                NameEnum = TextEnum.PrintHelp
            }
        };
    }

    private static void AddUserItems()
    {
        if (MenuManager.GetMenu().Selected.Items.Count > 0)
        {
            MenuManager.GetMenu().Selected.Items.Clear();
        }

        MenuManager.GetMenu().Selected.Add(
            new MenuInputItem(LocalizationManager.GetText(TextEnum.PromptNewUser),
                LocalizationManager.GetText(TextEnum.PromptNewUserName), CreateNewUser));

        var users = Program.UserManager.GetUsers();

        foreach (var user in users)
        {
            MenuManager.GetMenu().Selected.Add(new MenuItem(user.Username, new[]
            {
                new MenuItem(DeleteUser)
                {
                    NameEnum = TextEnum.DeleteUser,
                    BackgroundData = user
                }
            }));
        }
    }

    private static void CreateNewUser(string value)
    {
        if (MenuManager.GetMenu().Selected.BackgroundData != null)
        {
            var user = (User)MenuManager.GetMenu().Selected.BackgroundData;

            user.Password = value;

            Program.UserManager.AddUser(user);

            MenuManager.GetMenu().Selected.BackgroundData = null;
            
            MenuManager.GetMenu().GoUp();
            
            AddUserItems();
            
            MenuManager.GetMenu().Refresh();
        }
        else
        {
            MenuManager.GetMenu().Selected.Name = LocalizationManager.GetText(TextEnum.PromptPassword);
            ((MenuInputItem)MenuManager.GetMenu().Selected).Title =
                LocalizationManager.GetText(TextEnum.PromptPassword);
            MenuManager.GetMenu().Selected.BackgroundData = new User(value);

            MenuManager.GetMenu().Selected.Parent.Items
                .RemoveAll(itm => itm.Name != LocalizationManager.GetText(TextEnum.PromptPassword));
            
            MenuManager.GetMenu().Refresh();
        }
    }
    
    private static void DeleteUser()
    {
        var user = (User)MenuManager.GetMenu().Selected.BackgroundData;

        Program.UserManager.RemoveUser(user);

        MenuManager.GetMenu().Selected.Parent.Parent.Items.RemoveAll(itm => itm.GetName().Equals(user.Username));
        
        MenuManager.GetMenu().GoUp();
    }
    
    private static void ShowInventoryItems()
    {
        if (MenuManager.GetMenu().Selected.Items.Count > 0)
        {
            MenuManager.GetMenu().Selected.Items.Clear();
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
                new MenuItem(DeleteItem)
                {
                    NameEnum = TextEnum.DeleteItem,
                    BackgroundData = item
                },
                
                new MenuInputItem(LocalizationManager.GetText(TextEnum.ItemName)
                                    + ": "
                                    + item.Name,
                                    LocalizationManager.GetText(TextEnum.ItemName),
                                EditItemName)
                {
                    BackgroundData = item
                },
                new MenuInputItem(LocalizationManager.GetText(TextEnum.ItemQuantity)
                                    + ": "
                                    + item.Quantity,
                                        LocalizationManager.GetText(TextEnum.ItemQuantity),
                                EditItemQuantity)
                {
                    BackgroundData = item
                },
                new MenuInputItem(LocalizationManager.GetText(TextEnum.ItemPrice)
                                    + ": "
                                    + item.Price,
                                        LocalizationManager.GetText(TextEnum.ItemPrice),
                                EditItemPrice)
                {
                    BackgroundData = item
                }
            });
        }
    }

    private static void DeleteItem()
    {
        var item = (Item)MenuManager.GetMenu().Selected.BackgroundData;

        var inventory = Program.InventoryManager.LoadInventory();
        
        inventory.RemoveItem(item);
        
        Program.InventoryManager.SaveInventory(inventory);

        MenuManager.GetMenu().Selected.Parent.Parent.Items.RemoveAll(itm => itm.GetName().Equals(item.Name));
        
        MenuManager.GetMenu().GoUp();
    }
    
    private static void CreateItem(string value)
    {
        if (MenuManager.GetMenu().Selected.Name.Equals(LocalizationManager.GetText(TextEnum.OptionCreateInventoryItem)))
        {
            // Name

            var itemBuilder = new ItemBuilder();

            itemBuilder = itemBuilder.WithName(value);

            MenuManager.GetMenu().Selected.BackgroundData = itemBuilder;

            MenuManager.GetMenu().Selected.Name = LocalizationManager.GetText(TextEnum.ItemQuantity);
            ((MenuInputItem)MenuManager.GetMenu().Selected).Title =
                LocalizationManager.GetText(TextEnum.ItemQuantity);
            MenuManager.GetMenu().Selected.NameEnum = null;

            MenuManager.GetMenu().Selected.Parent.Items
                .RemoveAll(itm => itm.Name != LocalizationManager.GetText(TextEnum.ItemQuantity));
            
            MenuManager.GetMenu().Refresh();
        }
        else if (MenuManager.GetMenu().Selected.Name.Equals(LocalizationManager.GetText(TextEnum.ItemQuantity)))
        {
            // Quantity

            var itemBuilder = (ItemBuilder)MenuManager.GetMenu().Selected.BackgroundData;

            var quantity = int.Parse(value);

            itemBuilder = itemBuilder.WithQuantity(quantity);

            MenuManager.GetMenu().Selected.BackgroundData = itemBuilder;
            
            MenuManager.GetMenu().Selected.Name = LocalizationManager.GetText(TextEnum.ItemPrice);
            ((MenuInputItem)MenuManager.GetMenu().Selected).Title =
                LocalizationManager.GetText(TextEnum.ItemPrice);
            
            MenuManager.GetMenu().Selected.Parent.Items
                .RemoveAll(itm => itm.Name != LocalizationManager.GetText(TextEnum.ItemPrice));
            
            MenuManager.GetMenu().Refresh();
        }
        else if (MenuManager.GetMenu().Selected.Name.Equals(LocalizationManager.GetText(TextEnum.ItemPrice)))
        {
            // Price

            var itemBuilder = (ItemBuilder)MenuManager.GetMenu().Selected.BackgroundData;

            var price = int.Parse(value);

            itemBuilder = itemBuilder.WithPrice(price);

            var inventory = Program.InventoryManager.LoadInventory();
            
            inventory.AddItem(itemBuilder.Build());
            
            Program.InventoryManager.SaveInventory(inventory);

            MenuManager.GetMenu().Selected.BackgroundData = null;

            MenuManager.GetMenu().Selected.Parent.Items.Clear();

            MenuManager.GetMenu().GoUp();

            MenuManager.GetMenu().Selected.Add(new[]
            {
                new MenuItem(ShowInventoryItems)
                {
                    NameEnum = TextEnum.OptionShowInventoryItems
                },
                new MenuInputItem(LocalizationManager.GetText(TextEnum.OptionCreateInventoryItem),
                    LocalizationManager.GetText(TextEnum.ItemName), CreateItem)
                {
                    NameEnum = TextEnum.OptionCreateInventoryItem
                }
            });
            
            MenuManager.GetMenu().Refresh();
        }
    }

    private static void EditItemName(string name)
    {
        var item = (Item)MenuManager.GetMenu().Selected.BackgroundData;

        var oldName = item.Name;
        
        item.Name = name;

        var inventory = Program.InventoryManager.LoadInventory();
        
        inventory.UpdateItem(item, oldName);
        
        Program.InventoryManager.SaveInventory(inventory);

        MenuManager.GetMenu().GoUp();
        
        MenuManager.GetMenu().Selected.Name = name;

        MenuManager.GetMenu().Selected.Items.First(itm => itm.GetName().EndsWith(oldName)).Name =
            LocalizationManager.GetText(TextEnum.ItemName) + ": " + name;
        
        MenuManager.GetMenu().Refresh();
    }

    private static void EditItemQuantity(string quantity)
    {
        int quant;

        if (!int.TryParse(quantity, out quant))
        {
            return;
        }
        
        var item = (Item)MenuManager.GetMenu().Selected.BackgroundData;

        item.Quantity = quant;

        var inventory = Program.InventoryManager.LoadInventory();
        
        inventory.UpdateItem(item, item.Name);
        
        Program.InventoryManager.SaveInventory(inventory);

        MenuManager.GetMenu().GoUp();
        
        MenuManager.GetMenu().Selected.Name = item.Name;

        var nameIndex = MenuManager.GetMenu().Selected.Items.FindIndex(itm => itm.GetName().EndsWith(item.Name));

        MenuManager.GetMenu().Selected.Items[nameIndex + 1].Name =
            LocalizationManager.GetText(TextEnum.ItemQuantity) + ": " + quantity;

        MenuManager.GetMenu().Refresh();
    }

    private static void EditItemPrice(string price)
    {
        int pric;

        if (!int.TryParse(price, out pric))
        {
            return;
        }
        
        var item = (Item)MenuManager.GetMenu().Selected.BackgroundData;

        item.Price = pric;

        var inventory = Program.InventoryManager.LoadInventory();
        
        inventory.UpdateItem(item, item.Name);
        
        Program.InventoryManager.SaveInventory(inventory);

        MenuManager.GetMenu().GoUp();
        
        MenuManager.GetMenu().Selected.Name = item.Name;

        var nameIndex = MenuManager.GetMenu().Selected.Items.FindIndex(itm => itm.GetName().EndsWith(item.Name));

        MenuManager.GetMenu().Selected.Items[nameIndex + 2].Name =
            LocalizationManager.GetText(TextEnum.ItemPrice) + ": " + pric;

        MenuManager.GetMenu().Refresh();
    }
}