using System.Text;
using InventoryManagement.Localization;

namespace InventoryManagement.ConsoleMenu;

public class Menu
{
    private int _lasLen = 0;
    
    private bool _canWork = true;
    
    private bool _confirmMode = false;
    
    private bool _inputMode = false;
    
    private MenuItem _selected;

    public MenuItem Main
    {
        get;
    }

    private string ConfirmText
    {
        get; 
        set;
    } = "Are you sure? [Y/N]";

    public MenuItem Selected
    {
        get => _selected;
        
        set
        {
            _selected = value;

            if (_selected.ActionOnSelected)
            {
                _selected.Action?.Invoke();
            }
        }
    }

    private MenuItem Current
    {
        get; 
        set;
    }

    public List<MenuItem> Items;

    private List<string> Log
    {
        get;
    } = new ();

    public Menu(): this("", Array.Empty<MenuItem>())
    {
    }

    public Menu(MenuItem[] items)
        : this("", items)
    {
    }

    public Menu(string title, MenuItem[] items)
    {
        Main = new MenuItem(title, items);
        
        Items = new List<MenuItem>(items);

        Current = Main;

        if (items.Length > 0)
        {
            Selected = items[0];
        }
        else
        {
            Selected = Main;
        }
    }

    public Menu(MenuItem main)
    {
        Main = main;

        Current = Main;
        
        if (Main.Items.Count > 0)
        {
            Selected = Main.Items[0];
        }
        else
        {
            Selected = Main;
        }
    }

    public void GoUp()
    {
            if (Current.Parent != null)
            {
                foreach (var itm in Current.Parent.Items)
                {
                    if (itm != Current)
                    {
                        continue;
                    }

                    Selected = itm;

                    break;
                }

                Current = Current.Parent;
            }
    }

    // Worker cycle
    public void Begin()
    {
        Console.CursorVisible = false;

        while (_canWork)
        {
            Refresh();

            escape: var info = Console.ReadKey(true);

            switch (info.Key)
            {
                case System.ConsoleKey.Backspace:
                {
                    GoUp();

                    break;
                }
                case ConsoleKey.Escape:
                {
                    Current = Main;

                    if (Main.Items.Count > 0)
                    {
                        Selected = Main.Items[0];
                    }

                    break;
                }
                case ConsoleKey.Enter:
                {
                    if (Selected is MenuInputItem)
                    {
                        _inputMode = true;

                        break;
                    }


                    if (Selected.ActionIfConfirmed)
                    {
                        _confirmMode = true;

                        break;
                    }

                    Selected.Action?.Invoke();

                    if (Selected.Items.Count > 0)
                    {
                        Current = Selected;
                        
                        Selected = Current.Items[0];
                    }

                    break;
                }
                case ConsoleKey.UpArrow:
                {
                    var sel = GetIndex();

                    if (sel > -1)
                    {
                        sel -= _lasLen;

                        if (sel < 0)
                        {
                            sel += Current.Items.Count;
                        }
                    }

                    Selected = Current.Items[sel];

                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    var sel = GetIndex();

                    if (sel > -1)
                    {
                        sel += _lasLen;

                        if (sel >= Current.Items.Count)
                        {
                            sel -= Current.Items.Count;
                        }
                    }

                    Selected = Current.Items[sel];

                    break;
                }
                case ConsoleKey.LeftArrow:
                {
                    var sel = GetIndex();

                    if (sel > -1)
                    {
                        sel--;

                        if (sel < 0)
                        {
                            sel = Current.Items.Count - 1;
                        }
                    }

                    Selected = Current.Items[sel];

                    break;
                }
                case ConsoleKey.RightArrow:
                {
                    var sel = GetIndex();

                    if (sel > -1)
                    {
                        sel++;

                        if (sel == Current.Items.Count)
                        {
                            sel = 0;
                        }
                    }

                    Selected = Current.Items[sel];

                    break;
                }
                case ConsoleKey.Delete:
                {
                    Log.Clear();
                    
                    break;
                }
                default:
                {
                    if (_confirmMode)
                    {
                        if (info.Key == ConsoleKey.Y)
                        {
                            Selected.Action?.Invoke();
                        }

                        _confirmMode = false;

                        break;
                    }

                    goto escape;
                }
            }
        }

        int GetIndex()
        {
            var sel = -1;

            for (var i = 0; i < Current.Items.Count; ++i)
            {
                if (Selected != Current.Items[i])
                {
                    continue;
                }
                
                sel = i;

                break;
            }

            if (sel != -1)
            {
                return sel;
            }
            
            if (Current.Items.Count > 0)
            {
                sel = 0;
            }

            return sel;
        }
    }

    // Drawing
    public void Refresh()
    {
        if (_inputMode)
        {
            var inp = Selected as MenuInputItem;

            Console.BackgroundColor = ConsoleColor.Black;
            
            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.Clear();
            
            Console.Write(inp.Title + ": ");
            
            Console.ResetColor();
            
            inp.Value = Console.ReadLine();

            _inputMode = false;

            inp.Action?.Invoke(inp.Value);

            return;
        }


        if (_confirmMode)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            
            Console.Clear();
            
            Console.WriteLine();
            
            Console.WriteLine();
            
            Console.WriteLine(Selected.GetName().PadLeft(40));
            
            Console.WriteLine();
            
            Console.WriteLine(ConfirmText.PadLeft(40));

            Console.ResetColor();

            return;
        }

        Console.Clear();

        // Drawing nav
        var nav = Current.GetName();
        
        var cur = Current.Parent;
        
        var cursor = 0;

        while (cur != null)
        {
            nav = cur.GetName() + " => " + nav;

            cur = cur.Parent;
        }

        Console.WriteLine(nav);
        
        Console.WriteLine();
        
        var maxWidth = -1; 

        for (var i = 0; i < Current.Items.Count; i++)
        {
            var itm = Current.Items[i];

            if (itm.GetName().Length > maxWidth)
            {
                maxWidth = itm.GetName().Length;
            }
        }

        const int colSpace = 5;
        
        var len = System.Console.WindowWidth / (maxWidth + colSpace) - 1;

        if (Current.MaxColumns > 0 && Current.MaxColumns < len)
        {
            len = Current.MaxColumns;
        }

        _lasLen = len;

        for (var i = 0; i < Current.Items.Count; i += len)
        {
            for (var j = 0; j < len && i + j < Current.Items.Count; ++j)
            {
                var itm = Current.Items[i + j];

                if (itm == Selected)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                var name = itm.GetName().PadRight(maxWidth + 2);

                if (name.Length >= System.Console.LargestWindowWidth)
                {
                    name = name.Substring(0, System.Console.LargestWindowWidth - 5) + "...";
                }

                Console.Write(name);

                Console.ResetColor();

                Console.Write("".PadRight(colSpace));
            }

            Console.WriteLine();

            var tmp = Console.CursorTop;

            if (tmp > cursor)
            {
                cursor = tmp;
            }
        }

        Console.CursorTop = cursor + 1;
        
        Console.CursorLeft = 0;

        if (Log.Count <= 0)
        {
            return;
        }
        
        var sb = new StringBuilder();
        
        sb.AppendLine("______________________________");
        
        sb.AppendLine("");
        
        foreach (var itm in Log)
        {
            sb.AppendLine(itm);
        }

        Console.Write(sb);
    }
    
    public void Close()
    {
        _canWork = false;
    }

    public void WriteLine(string str)
    {
        Log.Add(str);
        
        Refresh();
    }
    
    protected static MenuItem[] GetLanguageItems()
    {
        return LocalizationManager.GetAvailableLanguages().Select(language =>
            new MenuItem(language.Name, SwitchLanguage)).ToArray();
    }

    private static void SwitchLanguage()
    {
        LocalizationManager.SetCurrentLanguage(LocalizationManager.GetLanguageByName(MenuManager.GetMenu().Selected.GetName()));

        MenuManager.PrintHelp();
    }

    public void PrintHelp()
    {
        Log.Clear();
        
        WriteLine(LocalizationManager.GetText(TextEnum.PrintHelpRow1));
        
        WriteLine(LocalizationManager.GetText(TextEnum.PrintHelpRow2));
        
        WriteLine(LocalizationManager.GetText(TextEnum.PrintHelpRow3));
        
        WriteLine(LocalizationManager.GetText(TextEnum.PrintHelpRow4));
    }
}