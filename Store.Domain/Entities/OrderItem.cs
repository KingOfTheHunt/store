using Flunt.Validations;

namespace Store.Domain.Entities;

public class OrderItem : Entity
{
    public Product Product { get; private set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public OrderItem(Product product, int quantity)
    {
        AddNotifications(new Contract<OrderItem>()
            .Requires()
            .IsNotNull(product, "Product", "Produto inválido.")
            .IsGreaterThan(quantity, 0, "Quantity", "A quantidade deve ser maior que zero."));
        
        Product = product;
        Price = product != null ? product.Price : 0;
        Quantity = quantity;
    }
    
    public decimal Total() => 
        Price * Quantity;
}