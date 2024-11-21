using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Domain.Services;
using Store.Tests.Repositories;
using Store.Tests.Services;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly OrderHandler _handler;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDeliveryFeeService _deliveryFeeService;

    public OrderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _discountRepository = new FakeDiscountRepository();
        _orderRepository = new FakeOrderRepository();
        _productRepository = new FakeProductRepository();
        _deliveryFeeService = new FakeDeliveryFeeService();
        
        _handler = new OrderHandler(_customerRepository, _discountRepository, _orderRepository, 
            _productRepository, _deliveryFeeService);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldCreateOrderWhenCommandIsValid()
    {
        var command = new CreateOrderCommand();
        command.Customer = "teste@email.com";
        command.ZipCode = "12345678";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        _handler.Handle(command);
        
        Assert.IsTrue(_handler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldNotCreateOrderWhenCustomerDoesNotExist()
    {
        var command = new CreateOrderCommand();
        command.Customer = "email@email.com";
        command.ZipCode = "12345678";
        command.PromoCode = "111111111";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        _handler.Handle(command);

        Assert.IsFalse(_handler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldCreateOrderWhenZipIsInvalid()
    {
        var command = new CreateOrderCommand();
        command.Customer = "teste@email.com";
        command.ZipCode = "12345679";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        _handler.Handle(command);
        
        Assert.IsTrue(_handler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldCreateOrderWhenPromoCodeIsInvalid()
    {
        var command = new CreateOrderCommand();
        command.Customer = "teste@email.com";
        command.ZipCode = "12345678";
        command.PromoCode = "12345679";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        _handler.Handle(command);
        
        Assert.IsTrue(command.IsValid);
    }
}