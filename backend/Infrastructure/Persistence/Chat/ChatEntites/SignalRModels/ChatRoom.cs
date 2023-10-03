using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Chat.ChatEntites.SignalRModels
{
    public class ChatRoom
    {
        public List<ChatUser> Users { get; set; }

        public string InitializerUserId { get; set; }
    }
}
