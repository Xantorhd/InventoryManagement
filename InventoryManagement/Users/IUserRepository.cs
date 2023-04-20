namespace InventoryManagement.Users;

public interface IUserRepository
{
    void AddUser(User user);

    bool RemoveUser(User user);
    
    List<User> GetUsers();
}