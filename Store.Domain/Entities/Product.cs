using Flunt.Validations;

namespace Store.Domain.Entities;

public class Product : Entity
{
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    public Product(string title, decimal price, bool active)
    {
        AddNotifications(new Contract<Product>()
            .Requires()
            .IsNotNullOrEmpty(title, "Title", "O nome do produto deve ser informado.")
            .IsGreaterThan(price, 0m, "Price", "O preço deve ser maior do que zero."));
        
        Title = title;
        Price = price;
        Active = active;
    }
}