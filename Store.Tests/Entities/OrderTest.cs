using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTest
{
    private readonly Customer _customer;
    private readonly Discount _discount;
    private readonly Order _validOrder;
    private readonly Product _product;

    public OrderTest()
    {
        _customer = new Customer("Davi Francisco", "davi@email.com");
        _discount = new Discount(15, DateTime.Now.AddDays(3));
        _validOrder = new Order(_customer, 15, _discount);
        _product = new Product("Produto 1", 10, true);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnAnNumberWith8CharsWhenOrderIsValid()
    {
        Assert.AreEqual(8, _validOrder.Number.Length);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnWaitingForPaymentStatusWhenOrderIsValid()
    { 
        Assert.AreEqual(EOrderStatus.WaitingForPayment, _validOrder.Status);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnWaitingForDeliveryStatusWhenThePaymentIsValid()
    {
        _validOrder.AddItem(_product, 1);
        _validOrder.Pay(10);
        
        Assert.AreEqual(EOrderStatus.WaitingForDelivery, _validOrder.Status);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnCanceledStatusWhenOrderIsCanceled()
    {
        var order = new Order(_customer, 15, _discount);
        order.Cancel();
        
        Assert.AreEqual(EOrderStatus.Canceled, order.Status);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldNotAddAnItemWithoutAProduct()
    {
        var order = new Order(_customer, 15, _discount);
        order.AddItem(null, 1);
        
        Assert.AreEqual(0, order.Items.Count);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldNotAddAnItemWithQuantityLowerOrEqualsThanZero()
    {
        var order = new Order(_customer, 15, _discount);
        order.AddItem(_product, 0);
        
        Assert.AreEqual(0, order.Items.Count);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnTotalPrice50WhenTheOrderIsValid()
    {
        _validOrder.AddItem(_product, 5);
        var total = _validOrder.Total();
        
        Assert.AreEqual(50, total);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnTotalPrice60WhenTheDiscountIsExpired()
    {
        var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-3));
        var order = new Order(_customer, 10, expiredDiscount);
        order.AddItem(_product, 5);
        var total = order.Total();
        
        Assert.AreEqual(60, total);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnTotalPrice60WhenTheDiscountIsInvalid()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        var total = order.Total();
        
        Assert.AreEqual(60, total);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnTotalPrice50WhenTheDiscountIsValid()
    {
        var discount  = new Discount(10, DateTime.Now.AddDays(3));
        var order = new Order(_customer, 10, discount);
        order.AddItem(_product, 5);
        var total = order.Total();
        
        Assert.AreEqual(50, total);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnTotalPrice60WhenDeliveryFeePriceIs10()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        var total = order.Total();
        
        Assert.AreEqual(60, total);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnInvalidWhenCustomerIsNull()
    {
        var order = new Order(null, 10, _discount);
        Assert.IsFalse(order.IsValid);
    }
}