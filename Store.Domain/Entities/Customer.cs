using Flunt.Validations;

namespace Store.Domain.Entities;

public class Customer : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Customer(string name, string email)
    {
        AddNotifications(new Contract<Customer>()
            .Requires()
            .IsNotNullOrEmpty(name, "Name", "O nome do precisa ser informado.")
            .IsEmail(email, "Email", "E-mail informado inválido."));
        
        Name = name;
        Email = email;
    }
}