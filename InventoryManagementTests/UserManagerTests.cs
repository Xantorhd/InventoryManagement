using InventoryManagement.Mocks;
using InventoryManagement.Users;

namespace InventoryManagementTests;

[TestClass]
public class UserManagerTests
{
    private IUserRepository _userRepository;
    private UserManager _userManager;

    [TestInitialize]
    public void Setup()
    {
        _userRepository = new MockUserRepository();
        _userManager = new UserManager(_userRepository);
    }

    [TestMethod]
    public void GetUsernames_ReturnsCorrectUsernames()
    {
        // Arrange
        var expectedUsernames = new List<string> { "Alice", "Bob", "Charlie" };
        foreach (var username in expectedUsernames)
        {
            _userManager.AddUser(new User(username, "password"));
        }

        // Act
        var actualUsernames = _userManager.GetUsernames();

        // Assert
        CollectionAssert.AreEqual(expectedUsernames, actualUsernames);
    }
}