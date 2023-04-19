namespace InventoryManagement.ConsoleMenu;

public class InputMenuItem: MenuItem
{
    public new Action<string> Action
    {
        get;
    }

    public string Value
    {
        get;
        
        set;
    } = "";
    
    public string Title {
        get;
    }

    public InputMenuItem(string name, string title, Action<string> action) : base(name, null as Action, 0)
    {
        Title = title;
        Action = action;
    }
}