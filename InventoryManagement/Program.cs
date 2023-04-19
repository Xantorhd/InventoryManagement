using System.Text;
using InventoryManagement.ConsoleMenu;

namespace InventoryManagement
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            
            MenuManager.Menu.Begin();
        }
    }
}
