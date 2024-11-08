using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeProductRepository : IProductRepository
{
    public IEnumerable<Product> GetProducts(IEnumerable<Guid> ids)
    {
        var products = new List<Product>
        {
            new("Produto 1", 10, true),
            new("Produto 2", 10, true),
            new("Produto 3", 10, true),
            new("Produto 4", 10, false),
            new("Produto 5", 10, false),
        };
        
        return products;
    }
}