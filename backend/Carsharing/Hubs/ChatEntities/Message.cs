namespace Carsharing.Hubs.ChatEntities
{
    public class Message
    {
        public string Text { get; set; }
        public string FromSpeakerId { get; set; }
        public DateTime Date { get; set; }
        public ChatMembers Member { get; set; }

        public Message (string text, string fromSpeakerId, DateTime date, ChatMembers member)
        {
            Text = text;
            FromSpeakerId = fromSpeakerId;
            Date = date;
            Member = member;
        }
    }
}
