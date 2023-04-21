namespace InventoryManagement.ConsoleMenu;

public class MenuInputItem: MenuItem
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

    public string Title
    {
        get;
    }

    public MenuInputItem(string name, string title, Action<string> action) : base(name, null as Action, 0)
    {
        Title = title;
        Action = action;
    }
}