using Domain.Entities;

namespace Carsharing.Hubs.ChatEntities
{
    public class UserConnection
    {
        public Room Room { get; set; } = null!;
        public bool IsOpen { get; set; } = true;
    }
}
