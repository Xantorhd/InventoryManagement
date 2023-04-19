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

    public List<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }
}