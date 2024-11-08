using Store.Domain.Services;

namespace Store.Tests.Services;

public class FakeDeliveryFeeService : IDeliveryFeeService
{
    public decimal GetDeliveryFee(string zipCode)
    {
        return 10;
    }
}