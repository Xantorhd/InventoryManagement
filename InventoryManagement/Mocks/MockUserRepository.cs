using InventoryManagement.Users;

namespace InventoryManagement.Mocks;

public class MockUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public MockUserRepository()
    {
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public bool RemoveUser(User user)
    {
        return _users.Remove(user);
    }

    public List<User> GetUsers()
    {
        return _users;
    }
}