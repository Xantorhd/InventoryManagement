using InventoryManagement.ConsoleMenu.Menus;

namespace InventoryManagement.ConsoleMenu;

public static class MenuManager
{
    private static Menu _menu;

    static MenuManager()
    {
        _menu = new StartMenu();
        
        PrintInfo();
    }

    public static Menu GetMenu()
    {
        return _menu;
    }
    
    private static void PrintInfo()
    {
        _menu.WriteLine("Use ←↑↓→ for navigation.");
        _menu.WriteLine("Press Esc for return to main menu.");
        _menu.WriteLine("Press Backspace for return to parent menu.");
        _menu.WriteLine("Press Del for clear log.");
    }
}
