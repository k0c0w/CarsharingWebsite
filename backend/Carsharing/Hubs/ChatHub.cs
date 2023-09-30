using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Carsharing.ChatHub
{
    //toread Сопопставление пользователей SignalR с подключениями

    public class ChatMessage
    {
        public string Text { get; set; }
        
        public ChatUser Author { get; set; }

        public DateTime Time { get; set; }
    }

    public class Room
    {
        public List<ChatUser> Users { get; set; }

        public List<ChatMessage> Messages { get; set; }

        public string InitializerUserId { get; set; }
    }

    public class ChatUser
    {

        public bool IsAnonymous { get; }

        public string UserId { get; }
        public ChatUser(string userId, bool isAnonymous)
        {
            UserId = userId;
            IsAnonymous = isAnonymous;
        }

        public List<string> UserConnections { get;} = new List<string>();

        public void AddConnection(string connection)
        {
            UserConnections.Add(connection);
        }

        public void RemoveConnection(string connection)
        {
            UserConnections.Remove(connection);
        }
    }

    // ДЛЯ АДМИНОВ
    /*
     *  1. Отправить инфу о существующих комнатах
     *  2. Подписать конекшн на обновления комнат 
     *      (если админ какимто конекшном оказался в комнате - отправлять ему уведомления о сообщениях юзера в чатах, 
     *      если другой админ зашел в комнату то менять статус комнаты на в работе если это был сам админ то должно прийти уведомление в моей работе,
     *      если комнату покинули все админы прислать уведомление свободен)
     *      
     *      
     *  
     */

    // ДЛЯ ЮЗЕРОВ
    /*
     *  1. Если еще нет комнаты - создать комнату с юзером, иницировать уведомление о созданной комнате для апдейтов админа
     *  2. Если юзер авторизован и имеет конекшн - назначить ему существущую комнату (анонимные сессии всегда уникальные комнаты)
     *  
     */
    public class ChatHub : Hub
    {
        // to map user and their connections
        // for anonymous users we will use their session token (notice that it can be hacked)
        public Dictionary<string, Room> Rooms = new Dictionary<string, Room>();

        public Dictionary<string, ChatUser> ConnectedUsers = new Dictionary<string, ChatUser>();
        public HashSet<ChatUser> admins = new HashSet<ChatUser>();



        public async Task SendMessageAsync(ChatMessage message)
        {
            var userId = Context.UserIdentifier ?? $"a_{Context.UserIdentifier}";

            var room = Rooms[userId];

            //todo: saveMessage in db
            // map message from dto to model, validate model
            room.Messages.Add(message);

            await Clients.Clients(room.Users.SelectMany(x => x.UserConnections).ToList()).SendAsync("RecieveMessage", message);
        }



        public override Task OnConnectedAsync()
        {
            var connectedUserId = Context.UserIdentifier;
            if (connectedUserId == null)
            {
                AnonymousPipeline();
            }

            AuthorizedPipeline();

            return base.OnConnectedAsync();
        }


        private async Task AnonymousPipeline()
        {
            var userId = $"a_{GetConnectionId()}";

            var user = new ChatUser(userId, true);

            ConnectedUsers.Add(userId, user);
            var room = new Room()
            {
                Messages = new List<ChatMessage>(),
                Users = new List<ChatUser>() { user }
            };


            Rooms.Add(userId, room);

            //Notify about room
            await Clients.Clients(admins.SelectMany(x => x.UserConnections).ToList()).SendAsync("NewRoomCreated");
        }

        private async void AuthorizedPipeline(UserConnection connection)
        {
            // if user is admin
            if(false)
            {
                if (ConnectedUsers.ContainsKey(userId))
                {
                    var existingAdmin = ConnectedUsers[userId];
                    existingAdmin.AddConnection(connection);
                }
                else
                {
                    var newAdmin = new ChatUser(Context.UserIdentifier, true);
                    admins.Add(Context.UserIdentifier, newAdmin);
                    ConnectedUsers.Add(Context.UserIdentifier, newAdmin);
                }
                // no message about room
                return;
            }

            var userId = Context.UserIdentifier;


            if (ConnectedUsers.ContainsKey(userId))
            {
                var existingUser = ConnectedUsers[userId];
                existingUser.AddConnection(connection);
            }
            else
            {
                var newUser = new ChatUser(Context.UserIdentifier, false);
                ConnectedUsers.Add(Context.UserIdentifier, newUser);
                Rooms.Add(Context.UserIdentifier, new Room() { InitializerUserId = Context.UserIdentifier, Users = new List<ChatUser> { newUser }, Messages = new List<ChatMessage>() });
                await Clients.Clients(admins.SelectMany(x => x.UserConnections).ToList()).SendAsync("NewRoomCreated");
            }
        }

        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("recieveMesaage", message);
        }

        private string GetConnectionId() => Context.ConnectionId;

    }

    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime SentTime { get; set; }

        public virtual User Author { get; set; }
    }
}
