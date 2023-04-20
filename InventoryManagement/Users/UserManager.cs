namespace InventoryManagement.Users;

public class UserManager
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(User user)
    {
        _userRepository.AddUser(user);
    }

    public bool RemoveUser(User user)
    {
        return _userRepository.RemoveUser(user);
    }
    
    public List<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }

    public List<string> GetUsernames()
    {
        return GetUsers().Select(user => user.Username).ToList();
    }

    public bool CheckPassword(string username, string password)
    {
        return _userRepository.GetUsers().Any(user => user.Username == username && user.Password == password);
    }
}