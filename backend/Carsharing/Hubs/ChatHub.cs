using Carsharing.Hubs.ChatEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Carsharing.ChatHub
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly string _informator;
        private readonly IDictionary<string, UserConnection> _connections;
        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            _informator = "information";
            _connections = connections;
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                userConnection.IsOpen = true;
                if (userConnection.Room is not null)
                {
                    var mess = "Техподдержка вышла.";
                    var roomName = userConnection.Room.RoomName;
                    if (Context.UserIdentifier == userConnection.Room.UserId)
                    {
                        userConnection.Room = null!;
                        mess = "Пользователь вышел. Чат закрыт.";
                    }
                    Clients.Group(roomName).SendAsync("ReceiveMessage", _informator, mess);
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task CreateChatRoom(UserConnection userConnection)
        {
            var id = Context.UserIdentifier ?? "undefined";
            var roomName = $"{id}_{DateTime.Now}";

            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            Room room = new Room()
            {
                RoomName = roomName,
                UserId = id,
            };
            UserConnection chatConnections = new UserConnection()
            {
                Room = room
            };

            _connections[Context.ConnectionId] = chatConnections;

            await Clients.Group(roomName).SendAsync("ReceiveMessage", _informator,
                $"Диалог открыт. Техподдержка с Вами скоро свяжется.");
        }

        public async Task ConnectTechSupporToClient(ConnectTechSupporDto dto)
        {
            var userConnection = _connections[dto.ConnectionId];
            if (userConnection == null)
                return;

            _connections[Context.ConnectionId] = userConnection;

            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room.RoomName);
            userConnection.IsOpen = false;
            userConnection.Room.TechSupportId = Context.UserIdentifier ?? "undefined";

            await Clients.Group(userConnection.Room.RoomName).SendAsync("ReceiveMessage", _informator,
                $"Техподдержа подключилась к чату.");
        }

        public async Task SendMessage(RequestMessage message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                if (userConnection.Room is null)
                {
                    _connections.Remove(Context.ConnectionId);
                    return;
                }
                var id = Context.UserIdentifier ?? "undefined";
                Message mess = new Message(text: message.Text, fromSpeakerId: id, DateTime.Now, (ChatMembers)message.MemberTypeInt);
                var chat = _connections[Context.ConnectionId];
                chat.Room.Messages.Add(mess);


                await Clients.Group(userConnection.Room.RoomName).SendAsync("ReceiveMessage", message.MemberTypeInt, message.Text);
            }
        }

        public async Task GetLatestMessages(RequestMessage message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out var userConnection))
            {
                var roles = userConnection.Room.Messages.Select(x => (int)x.Member).ToList();
                var text = userConnection.Room.Messages.Select(x => x.Text).ToList();

                await Clients.Caller.SendAsync("ReceiveLatestMessages", roles, text);
            }
        }
    }
}
