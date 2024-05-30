
namespace Carsharing.Contracts.UserEvents
{
    public abstract class UserBasedEvent
    {
        public string UserId { get; set; } = string.Empty;

    }
}
