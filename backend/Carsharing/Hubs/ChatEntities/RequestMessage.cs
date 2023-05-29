using System.Diagnostics.CodeAnalysis;

namespace Carsharing.Hubs.ChatEntities
{
    public class RequestMessage
    {
        public string Text { get; set; } = string.Empty;
        public int MemberTypeInt { get; set; }
        //[AllowNull]
        //public string? SessionId { get; set; }
    }
}
