namespace InventoryManagement.ConsoleMenu.Menus;

public abstract class StartMenu : IMenu
{
    public static MenuItem[] Get()
    {
        return new []
        {
            new MenuItem("Login", Interactions.Login.AskLogin)
        };
    }
}