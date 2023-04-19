using InventoryManagement.Users;

namespace InventoryManagement.ConsoleMenu.Interactions;

public class Login
{
    public static void AskLogin()
    {
        if (MenuManager.Menu.Selected.Items.Count > 0)
        {
            return;
        }

        foreach (var user in new UserManager(new UserRepository("users.txt")).GetUsers())
        {
            MenuManager.Menu.Selected.Add(new InputMenuItem(user.Username, $"Password for {MenuManager.Menu.Selected.Name}",
                DoLogin));
        }
    }

    private static void DoLogin(string password)
    {
        MenuManager.Menu.WriteLine(MenuManager.Menu.Selected.Name+": "+(MenuManager.Menu.Selected as InputMenuItem).Value);
    }
}