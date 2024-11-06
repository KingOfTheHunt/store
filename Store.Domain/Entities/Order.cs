using Store.Domain.Enums;

namespace Store.Domain.Entities;

public class Order
{
    private List<OrderItem> _orderItems = [];
    
    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public string Number { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _orderItems;
    public decimal DeliveryFee { get; private set; }
    public Discount Discount { get; private set; }
    public EOrderStatus Status { get; private set; }

    public Order(Customer customer, decimal deliveryFee, Discount discount)
    {
        Customer = customer;
        Date = DateTime.Now;
        Number = Guid.NewGuid().ToString()[..8];
        DeliveryFee = deliveryFee;
        Discount = discount;
        Status = EOrderStatus.WaitingForPayment;
    }

    public void AddItem(Product product, int quantity)
    {
        var item = new OrderItem(product, quantity);
        _orderItems.Add(item);
    }

    public decimal Total()
    {
        var total = _orderItems.Sum(x => x.Total());
        total += DeliveryFee;

        if (Discount is not null)
            total -= Discount.Value();
        
        return total;
    }

    public void Pay(decimal amount)
    {
        if (amount == Total())
        {
            Status = EOrderStatus.WaitingForDelivery;
        }
    }

    public void Cancel()
    {
        Status = EOrderStatus.Canceled;
    }
}