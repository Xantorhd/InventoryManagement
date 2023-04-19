using InventoryManagement.ConsoleMenu.Menus;

namespace InventoryManagement.ConsoleMenu;

public static class MenuManager
{
    public static readonly Menu Menu;

    static MenuManager()
    {
        Menu = new Menu
        (
            "Main",
            StartMenu.Get()
        )
        {
            Main = { MaxColumns = 1 }
        };
        
        PrintInfo();
    }

    static void PrintInfo()
    {
        Menu.WriteLine("Use ←↑↓→ for navigation.");
        Menu.WriteLine("Press Esc for return to main menu.");
        Menu.WriteLine("Press Backspace for return to parent menu.");
        Menu.WriteLine("Press Del for clear log.");
    }
}
