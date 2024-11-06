namespace Store.Domain.Entities;

public class OrderItem
{
    public Product Product { get; private set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Price = product != null ? product.Price : 0;
        Quantity = quantity;
    }
    
    public decimal Total() => 
        Price * Quantity;
}