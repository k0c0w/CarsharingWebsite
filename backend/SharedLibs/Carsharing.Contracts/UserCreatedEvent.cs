using System;

namespace Carsharing.Contracts
{
    public class UserCreatedEvent
    {
        public string UserId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string[] Roles { get; set; } = Array.Empty<string>();
    }

}
