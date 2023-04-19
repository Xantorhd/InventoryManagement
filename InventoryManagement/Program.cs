using System.Text;
using InventoryManagement.ConsoleMenu;

namespace InventoryManagement
{
    class Program
    {
        private static Menu _menu = new ();

        private static void Print()
        {
            _menu.WriteLine("Selected: " + _menu.Selected.Name);
        }

        private static void Generate()
        {
            if (_menu.Selected.Items.Count > 0)
            {
                return;
            }


            for (var i = 0; i < 50; ++i)
            {
                var sub = new MenuItem("Dynamic - " + i, Print);
                

                _menu.Selected.Add(sub);
            }
        }

        private static void InputTest(string str)
        {
            var inp = _menu.Selected as InputMenuItem;

            _menu.WriteLine("You wrote: " + inp?.Value);
        }

        private static void Exit()
        {
            _menu.Close();
        }

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;

            _menu = new Menu(
                "Main",
                new[]
                {
                    new MenuItem("Static", new[]
                    {
                        new MenuItem("Sub-0", Print),
                        new MenuItem("Sub-1", Print)
                    }),
                    new MenuItem("Dynamic", Generate),
                    new InputMenuItem("Input Test", "Please input you name", InputTest),
                    new MenuItem("Confirm", Print) { ActionIfConfirmed = true },
                    new MenuItem("Exit", Exit),
                }
            );
            
            _menu.Main.MaxColumns = 1;
            
            _menu.WriteLine("Use ←↑↓→ for navigation.");
            _menu.WriteLine("Press Esc for return to main menu.");
            _menu.WriteLine("Press Backspace for return to parent menu.");
            _menu.WriteLine("Press Del for clear log.");

            _menu.Begin();
        }
    }
}
