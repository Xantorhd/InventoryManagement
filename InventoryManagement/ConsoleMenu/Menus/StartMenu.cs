using InventoryManagement.Localization;
using InventoryManagement.Users;

namespace InventoryManagement.ConsoleMenu.Menus;

public class StartMenu : Menu
{
    public StartMenu() : base(new MenuItem(LocalizationManager.GetText(TextEnum.MenuTitleMain), GetItems()){NameEnum = TextEnum.MenuTitleMain})
    { }

    private static MenuItem[] GetItems()
    {
        return new[]
        {
            new MenuItem(LocalizationManager.GetText(TextEnum.MenuTitleLogin), GetUserInputItems()){NameEnum = TextEnum.MenuTitleLogin},
            new MenuItem(LocalizationManager.GetText(TextEnum.MenuTitleLanguages), GetLanguageItems()){NameEnum = TextEnum.MenuTitleLanguages}
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
            //MenuManager.GetMenu().WriteLine("Login success");
        }
        else
        {
            //MenuManager.GetMenu().WriteLine("No Login success");
        }
    }

    private static MenuItem[] GetLanguageItems()
    {
        return LocalizationManager.GetAvailableLanguages().Select(language =>
            new MenuItem(language.Name, SwitchLanguage)).ToArray();
    }

    private static void SwitchLanguage()
    {
        LocalizationManager.SetCurrentLanguage(LocalizationManager.GetLanguageByName(MenuManager.GetMenu().Selected.GetName()));
    }
}