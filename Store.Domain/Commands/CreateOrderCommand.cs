using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class CreateOrderCommand : Notifiable<Notification>, ICommand
{
    public string Customer { get; set; }
    public string ZipCode { get; set; } = string.Empty;
    public string PromoCode { get; set; } = string.Empty;
    public IList<CreateOrderItemCommand> Items { get; set; }

    public CreateOrderCommand()
    {
        Items = new List<CreateOrderItemCommand>();
    }

    public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
    {
        Customer = customer;
        ZipCode = zipCode;
        PromoCode = promoCode;
        Items = items;
    }
    
    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsGreaterThan(Customer.Length, 8, "Customer", "Cliente inválido.")
            .AreEquals(ZipCode.Length, 8, "ZipCode", "CEP Inválido.")
            .IsGreaterThan(Items.Count, 0, "Items", "O pedido precisa ter ao menos um produto."));
    }
}