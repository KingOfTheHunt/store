using Store.Domain.Entities;

namespace Store.Domain.Repositories;

public interface ICustomerRepository
{
    Customer? GetByEmail(string email);
}