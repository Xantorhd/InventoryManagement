using InventoryManagement.Inventory;
using InventoryManagement.Mocks;

namespace InventoryManagementTests;

[TestClass]
public class InventoryManagerTests
{
    [TestMethod]
    public void TestCreateInventoryItemWithFake()
    {
        // Arrange
        var fakeItem = new FakeItem("Test Item", 5, 10);
        var inventory = new Inventory();
        
        // Act
        inventory.AddItem(fakeItem);
    
        // Assert
        Assert.IsTrue(inventory.GetItems().Any(item => item is { Name: "Test Item", Quantity: 5, Price: 10 }));
    }
}