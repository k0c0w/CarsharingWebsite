using Carsharing.Hubs.ChatEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Carsharing.ChatHub
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly string _informator;
        private IDictionary<string, UserConnection> _connections;
        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            _informator = "information";
            _connections = connections;
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                userConnection.IsOpen = true;
                Clients.Group(userConnection.Room.RoomName).SendAsync("ReceiveMessage", _informator, $"{Context?.User?.Identity?.Name} has left");
                //SendUsersConnected(userConnection.Room);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task CreateChatRoom(UserConnection userConnection)
        {
            var id = Context.UserIdentifier ?? "undefined";
            var roomName = $"{id}_{DateTime.Now.ToString()}";

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



        public async Task ConnectTechSupporToClient(ConnectTechSupporDTO dto)
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
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                var id = Context.UserIdentifier ?? "undefined";
                Message mess = new Message(text: message.Text, fromSpeakerId: id, DateTime.Now, (ChatMembers)message.MemberTypeInt);
                var chat = _connections[Context.ConnectionId];
                chat.Room.Messages.Add(mess);


                await Clients.Group(userConnection.Room.RoomName).SendAsync("ReceiveMessage", message.MemberTypeInt, message.Text);
            }
        }

        //public Task SendUsersConnected(string room)
        //{
        //    var users = _chats.Values
        //        .Where(c => c.Room == room)
        //        .Select(c => c.User);

        //    return Clients.Group(room).SendAsync("ReceiveRoomName", users);
        //}

    }
}
