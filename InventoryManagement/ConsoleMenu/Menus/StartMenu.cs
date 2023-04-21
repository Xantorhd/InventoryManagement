using InventoryManagement.Localization;

namespace InventoryManagement.ConsoleMenu.Menus;

public class StartMenu : Menu
{
    public StartMenu() 
        : base(new MenuItem(GetItems())
    {
        NameEnum = TextEnum.MenuTitleMain,
        MaxColumns = 1
    })
    { }

    private static MenuItem[] GetItems()
    {
        return new[]
        {
            new MenuItem(GetUserInputItems())
            {
                NameEnum = TextEnum.MenuTitleLogin
            },
            new MenuItem(GetLanguageItems())
            {
                NameEnum = TextEnum.MenuTitleLanguages
            }
        };
    }

    private static MenuInputItem[] GetUserInputItems()
    {
        return Program.UserManager.GetUsernames().Select(username =>
            new MenuInputItem(username, username, CheckLogin)).ToArray();
    }

    private static void CheckLogin(string password)
    {
        if (Program.UserManager.CheckPassword(MenuManager.GetMenu().Selected.GetName(), password))
        {
            MenuManager.LoginSuccess();
        }
        else
        {
            MenuManager.GetMenu().WriteLine(LocalizationManager.GetText(TextEnum.ErrorInvalidPassword));
        }
    }
}