using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private IEnumerable<Product> _products;

    public ProductQueriesTests()
    {
        _products = new[]
        {
            new Product("Produto 1", 10, true),
            new Product("Produto 2", 10, true),
            new Product("Produto 3", 10, true),
            new Product("Produto 4", 10, false),
            new Product("Produto 5", 10, false),
        };
    }
    
    [TestMethod]
    [TestCategory("Queries")]
    public void ShouldReturn3ActiveProducts()
    {
        var activeProducts = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
        
        Assert.AreEqual(3, activeProducts.Count());
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void ShouldReturn2InactiveProducts()
    {
        var inactiveProducts = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
        
        Assert.AreEqual(2, inactiveProducts.Count());
    }
}