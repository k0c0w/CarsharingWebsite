using Newtonsoft.Json;
using Persistence.Chat.ChatEntites.SignalRModels;
using Services.Abstractions.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Chat
{
    internal class ChatRoomRepository : IRepository<string, ChatRoom>
    {
        private readonly IConnectionMultiplexer _redis;

        private IDatabase Database => _redis.GetDatabase();

        public ChatRoomRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task AddAsync(ChatRoom model)
        {
            var serilized = await JsonConvert.SerializeObjectAsync(model).ConfigureAwait(false);

            var succseed = await Database.StringSetAsync(model.InitializerUserId, serilized).ConfigureAwait(false);

            if (!succseed)
                throw new InvalidOperationException("Cannot create room");
        }

        public async Task<ChatRoom> GetByIdAsync(string id)
        {
            var serilized = await Database.StringGetAsync(id).ConfigureAwait(false);

            return await JsonConvert.DeserializeObjectAsync<ChatRoom>(serilized).ConfigureAwait(false);
        }

        public async Task RemoveByIdAsync(string id)
        {
            await Database.StringGetDeleteAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ChatRoom model)
        {
            var stored = await GetByIdAsync(model.InitializerUserId).ConfigureAwait(false);
            stored.Users = model.Users;

            var serilized = await JsonConvert.SerializeObjectAsync(stored).ConfigureAwait(false);
            await Database.StringSetAsync(stored.InitializerUserId, serilized).ConfigureAwait(false);
        }
    }
}
