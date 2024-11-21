using Store.Domain.Commands;

namespace Store.Domain.Utils;

public static class ExtractGuids
{
    public static IEnumerable<Guid> Extract(IList<CreateOrderItemCommand> itemsCommand)
    {
        return itemsCommand.Select(command => command.ProductId);
    }
}