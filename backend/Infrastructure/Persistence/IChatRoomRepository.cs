using Persistence.Chat.ChatEntites.SignalRModels;
using Services.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IChatRoomRepository : IRepository<string, ChatRoom>
    {
    }
}
