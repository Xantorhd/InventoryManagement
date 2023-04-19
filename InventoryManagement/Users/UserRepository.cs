namespace InventoryManagement.Users;

public class UserRepository : IUserRepository
{
    private readonly string _filePath;

    public UserRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void AddUser(User user)
    {
        var userString = $"{user.Username},{user.Password}";
        
        File.AppendAllLines(_filePath, new[] { userString });
    }

    public List<User> GetUsers()
    {
        var users = new List<User>();
        var lines = File.ReadAllLines(_filePath);
        
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var user = new User(parts[0], parts[1]);
            
            users.Add(user);
        }
        
        return users;
    }
}