using Flunt.Notifications;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class GenericCommandResult : ICommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }
    public IEnumerable<Notification>? Notifications { get; set; }

    public GenericCommandResult(bool success, string message, object data, 
        IEnumerable<Notification>? notifications = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Notifications = notifications;
    }
}