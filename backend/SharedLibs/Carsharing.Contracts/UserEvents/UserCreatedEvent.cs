using Carsharing.Contracts.UserEvents;
using System;

namespace Carsharing.Contracts.UserEvents
{
    public class UserCreatedEvent : UserBasedEvent
    {
        public string Name { get; set; } = string.Empty;

        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
