namespace Carsharing.Hubs.ChatEntities
{
    public class Room
    {
        public string RoomName { get; set; } = String.Empty;
        public List<Message> Messages { get; set; } = new List<Message>();
        public string UserId { get; set; } = String.Empty;
        public string TechSupportId { get; set; } = String.Empty;
    }
}
