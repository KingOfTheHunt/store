namespace Store.Domain.Services;

public interface IDeliveryFeeService
{
    decimal GetDeliveryFee(string zipCode);
}