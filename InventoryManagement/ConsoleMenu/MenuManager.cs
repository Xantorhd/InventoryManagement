using InventoryManagement.ConsoleMenu.Menus;

namespace InventoryManagement.ConsoleMenu;

public static class MenuManager
{
    private static Menu _menu;

    static MenuManager()
    {
        _menu = new StartMenu();
        
        PrintHelp();
    }

    public static void LoginSuccess()
    {
        _menu.Close();
        
        _menu = new MainMenu();
        
        _menu.Begin();
    }
    
    public static Menu GetMenu()
    {
        return _menu;
    }
    
    public static void PrintHelp()
    {
        _menu.PrintHelp();
    }
}
