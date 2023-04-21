using InventoryManagement.Localization;

namespace InventoryManagement.ConsoleMenu;

public class MenuItem
{
    public MenuItem Parent
    {
        get; 
        private set;
    }

    private string Name
    {
        get;
    }

    public TextEnum? NameEnum
    {
        get; 
        set;
    }

    public Action? Action
    {
        get;
    }

    public IReadOnlyList<MenuItem> Items
    {
        get;
    }

    public object Tag
    {
        get; 
        set;
    }

    public bool ActionOnSelected
    {
        get; 
        set;
    } = false;

    public bool ActionIfConfirmed
    {
        get; 
        set;
    } = false;

    public int MaxColumns
    {
        get; 
        set;
    }

    public MenuItem(MenuItem[] items, int maxColumns = 0)
        : this("", items, maxColumns)
    {
    }
    
    public MenuItem(Action? action, int maxColumns = 0) 
        : this("", action, maxColumns)
    {
    }
    
    public MenuItem(string name, Action? action, int maxColumns = 0) 
        : this(name, action, Array.Empty<MenuItem>(), maxColumns)
    {
    }

    public MenuItem(string name, MenuItem[] items, int maxColumns = 0) 
        : this(name, null, items, maxColumns)
    {
    }

    public MenuItem(string name, Action? action, MenuItem[] items, int maxColumns = 0)
    {
        Name = name;
        Action = action;
        MaxColumns = maxColumns;
        Items = new List<MenuItem>(items);

        foreach (var itm in items)
        {
            itm.Parent = this;
        }
    }

    public string GetName()
    {
        if (NameEnum != null)
        {
            return LocalizationManager.GetText(NameEnum.Value);
        }

        return Name;
    }
    
    public void Add(MenuItem[] menuItems)
    {
        foreach (var menuItem in menuItems)
        {
            Add(menuItem);
        }
    }
    
    public void Add(MenuItem menuItem)
    {
        menuItem.Parent = this;

        ((List<MenuItem>)Items).Add(menuItem);
    }

    public MenuItem Add(string name, Action a, int maxColumns = 0)
    {
        var itm = new MenuItem(name, a, maxColumns) { Parent = this };

        ((List<MenuItem>)Items).Add(itm);

        return itm;
    }

    public MenuItem Add(string name, Action a, object tag, int maxColumns = 0)
    {
        var itm = new MenuItem(name, a, maxColumns) { Parent = this, Tag = tag };

        ((List<MenuItem>)Items).Add(itm);

        return itm;
    }

    public void Clear()
    {
        ((List<MenuItem>)Items).Clear();
    }
}