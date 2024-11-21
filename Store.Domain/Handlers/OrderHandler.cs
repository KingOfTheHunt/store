using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Services;
using Store.Domain.Utils;

namespace Store.Domain.Handlers;

// Um handler pode manipular um único command ou vários.
public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDeliveryFeeService _deliveryFeeService;

    public OrderHandler(ICustomerRepository customerRepository, IDiscountRepository discountRepository, 
        IOrderRepository orderRepository, IProductRepository productRepository, IDeliveryFeeService deliveryFeeService)
    {
        _customerRepository = customerRepository;
        _discountRepository = discountRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _deliveryFeeService = deliveryFeeService;
    }

    public ICommandResult Handle(CreateOrderCommand command)
    {
        command.Validate();

        if (command.IsValid == false)
            return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

        var customer = _customerRepository.GetByEmail(command.Customer);
        var deliveryFee = _deliveryFeeService.GetDeliveryFee(command.ZipCode);
        var discount = _discountRepository.Get(command.PromoCode);
        var order = new Order(customer, deliveryFee, discount);
        var products = _productRepository.GetProducts(ExtractGuids.Extract(command.Items));

        foreach (var item in command.Items)
        {
            var product = products.Where(x => x.Id == item.ProductId).FirstOrDefault();
            order.AddItem(product, item.Quantity);
        }
        
        AddNotifications(order.Notifications);
        
        if (IsValid == false)
            return new GenericCommandResult(false, "Erro ao gerar o pedido", Notifications);
        
        _orderRepository.Save(order);
        
        return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
    }
}