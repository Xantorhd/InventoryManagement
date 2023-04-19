namespace InventoryManagement.Users;

public interface IUserRepository
{
    void AddUser(User user);
    
    List<User> GetUsers();
}