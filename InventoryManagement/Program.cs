using System.Text;
using InventoryManagement.ConsoleMenu;
using InventoryManagement.Inventory;
using InventoryManagement.Users;

namespace InventoryManagement
{
    public static class Program
    {
        public static UserManager UserManager = new(new UserRepository(""));

        public static InventoryManager InventoryManager = new("");

        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                args = new[] { "users.txt", "inventory.txt" };
            }

            UserManager = new UserManager(new UserRepository(args[0]));

            InventoryManager = new InventoryManager(args[1]);
            
            InitializeConsole();
        }

        private static void InitializeConsole()
        {
            Console.OutputEncoding = Encoding.Default;

            MenuManager.GetMenu().Begin();
        }
    }
}
