using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeCustomerRepository : ICustomerRepository
{
    public Customer? GetByEmail(string email)
    {
        if (email == "teste@email.com")
            return new Customer("Davi Francisco", "teste@email.com");

        return null;
    }
}