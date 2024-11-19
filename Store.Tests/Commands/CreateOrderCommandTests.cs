using Store.Domain.Commands;

namespace Store.Tests.Commands;

[TestClass]
public class CreateOrderCommandTests
{
    [TestMethod]
    [TestCategory("Commands")]
    public void ShouldReturnInvalidCommandWhenCustomerIsInvalid()
    {
        var command = new CreateOrderCommand();
        command.Customer = "";
        command.ZipCode = "12345678";
        command.PromoCode = "123456789";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));
        command.Validate();
        
        Assert.IsFalse(command.IsValid);
    }

    [TestMethod]
    [TestCategory("Commands")]
    public void ShouldReturnInvalidCommandWhenZipCodeIsInvalid()
    {
        var command = new CreateOrderCommand();
        command.Customer = Guid.NewGuid().ToString("N");
        command.ZipCode = "";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();
        
        Assert.IsFalse(command.IsValid);
    }

    [TestMethod]
    [TestCategory("Commands")]
    public void ShouldReturnInvalidCommandWhenOrderHasNoItems()
    {
        var command = new CreateOrderCommand();
        command.Customer = Guid.NewGuid().ToString("N");
        command.ZipCode = "12345678";
        command.Validate();
        
        Assert.IsFalse(command.IsValid);
    }

    [TestMethod]
    [TestCategory("Commands")]
    public void ShouldReturnValidCommandWhenOrderIsValid()
    {
        var command = new CreateOrderCommand();
        command.Customer = Guid.NewGuid().ToString("N");
        command.ZipCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));
        command.Validate();
        
        Assert.IsTrue(command.IsValid);
    }
}