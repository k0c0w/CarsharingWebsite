namespace Carsharing.Contracts.UserEvents
{
    public class UserUpdatedEvent : UserBasedEvent
    {
        public string Name { get; set; }

        public string[] Roles { get; set; }
    }
}
