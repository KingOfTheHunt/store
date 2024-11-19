using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class CreateOrderItemCommand(Guid productId, int quantity) : Notifiable<Notification>, ICommand
{
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .AreEquals(ProductId.ToString().Length, 32, "ProductId", "Produto inválido.")
            .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida."));
    }
}