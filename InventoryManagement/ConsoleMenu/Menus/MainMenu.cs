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
                    new MenuItem(ShowCreateItem)
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
            return;
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
        /*if (MenuManager.GetMenu().Selected.BackgroundData != null)
        {
            var user = (User)MenuManager.GetMenu().Selected.BackgroundData;

            user.Password = value;

            Program.UserManager.AddUser(user);
            
            MenuManager.GetMenu().GoUp();
        }
        else
        {
            MenuManager.GetMenu().WriteLine(value);
            
            MenuManager.GetMenu().Selected.Name = LocalizationManager.GetText(TextEnum.PromptNewUserPassword);
            MenuManager.GetMenu().Selected.BackgroundData = new User(value);
        }*/
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