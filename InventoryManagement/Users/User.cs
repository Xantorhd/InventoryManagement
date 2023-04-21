namespace InventoryManagement.Users;

public class User
{
    public string Username
    {
        get; 
        set;
    }

    public string Password
    {
        get; 
        set;
    }

    public User(string username) : this(username, "placeholder")
    {
    }
    
    public User(string username, string password)
    {
        Username = username;
        
        Password = password;
    }
}