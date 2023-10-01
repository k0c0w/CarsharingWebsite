using Carsharing.Hubs.ChatEntities;
using Carsharing.ViewModels.Admin.UserInfo;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Carsharing.ChatHub
{
    //toread Сопопставление пользователей SignalR с подключениями

    public class ChatMessage
    {
        public string Text { get; set; } = string.Empty;

        public string AuthorName { get; set; } = string.Empty;

        public bool IsFromManager { get; set; }

        public DateTime Time { get; set; }

        public string RoomId { get; set; }
    }

    public class Room
    {
        public List<ChatUser> Users { get; set; }

        public List<ChatMessage> Messages { get; set; }


        public string InitializerUserId { get; set; }
    }

    public class ChatUser
    {
        public bool IsManager { get; set; }

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

        public List<string> UserGroups { get; } = new List<string>();

        public void AddGroup(string group)
        {
            UserGroups.Add(group);
        }

        public void RemoveGroup(string group)
        {
            UserGroups.Remove(group);
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
    public class ChatHub : Hub
    {
        private const string ADMIN_GROUP = "managers";

        private readonly UserManager<User> _userManager;


        // to map user and their connections
        // for anonymous users we will use their session token (notice that it can be hacked)
        public static Dictionary<string, Room> Rooms = new Dictionary<string, Room>();

        public static Dictionary<string, ChatUser> ConnectedUsers = new Dictionary<string, ChatUser>();
        public static HashSet<ChatUser> admins = new HashSet<ChatUser>();


        public ChatHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HubMethodName("SendMessage")]
        public async Task SendMessageAsync(ChatMessage message)
        {
            var roomId = message.RoomId;

            if (!Rooms.ContainsKey(roomId))
                return;

            var room = Rooms[roomId];

            if (!room.Users.SelectMany(x => x.UserConnections).Contains(Context.ConnectionId))
                return;

            //todo: saveMessage in db
            // map message from dto to model, validate model
            room.Messages.Add(message);

            await Clients.Group(roomId).SendAsync("RecieveMessage", message).ConfigureAwait(false);
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HubMethodName("JoinRoom")]
        public async Task JoinRoomAsync(string roomId)
        {
            //

            var managerId = Context.UserIdentifier!;


            if (!Rooms.ContainsKey(roomId) || !ConnectedUsers.ContainsKey(managerId))
            {
                return;
            }


            var room = Rooms[managerId];
            var managareUser = ConnectedUsers[managerId];

            // cannot enter room twice
            if (room.Users.Contains(managareUser))
                return;

            await AddChatUserToGroupAsync(managareUser, roomId).ConfigureAwait(false);
            room.Users.Add(managareUser);


            // todo: event admin entered the room
        }

        [Authorize(Roles = nameof(Role.Admin))]
        public async Task LeaveRoomAsync(string roomId)
        {
            var managerId = Context.UserIdentifier!;

            if (!Rooms.ContainsKey(roomId) || !ConnectedUsers.ContainsKey(managerId))
            {
                return;
            }

            var room = Rooms[managerId];
            var managareUser = ConnectedUsers[managerId];

            // cannot enter room twice
            if (room.Users.Contains(managareUser))
                return;

            await AddChatUserToGroupAsync(managareUser, roomId).ConfigureAwait(false);
            room.Users.Add(managareUser);


            // todo: event admin left the room
        }

        public override async Task OnConnectedAsync()
        {
            var connectedUserId = Context.UserIdentifier;

            if (connectedUserId == null)
                await CreateNewUserWithRoomAndNotifyAsync(false).ConfigureAwait(false);
            else
                await ExecuteAuthorizedPipelineAsync().ConfigureAwait(false);

            await base.OnConnectedAsync().ConfigureAwait(false);
        }

        private async Task ExecuteAuthorizedPipelineAsync()
        {
            var connectionId = Context.ConnectionId;
            var userId = Context.UserIdentifier!;
            bool isAuthenticated = Context.User != null && Context.User.Identity != null && Context.User.Identity.IsAuthenticated;

            // if user has already connected with other session, he must be saved in connections and connection must be assigned to user groups
            if (isAuthenticated && ConnectedUsers.ContainsKey(userId))
            {
                var chatUser = ConnectedUsers[userId];
                chatUser.AddConnection(connectionId);

                // assign all user`s groups to new connection
                foreach(var group in chatUser.UserGroups)
                    await Groups.AddToGroupAsync(connectionId, group).ConfigureAwait(false);

                return;
            }

            // Create user, create room, notify admins about new room
            await CreateNewUserWithRoomAndNotifyAsync(isAuthenticated);
        }


        private async Task CreateNewUserWithRoomAndNotifyAsync(bool isUserAuthenticated)
        {
            // Create new chat user
            var newChatUser = await CreateNewChatUserAsync().ConfigureAwait(false);

            // if user is manger, we dont need to create room for him
            if (isUserAuthenticated && await HasManagerRoleAsync(Context.UserIdentifier!).ConfigureAwait(false))
                return;

            //Create room and assign it to user (anonymous or authorized customers)
            var roomId = await CreateNewRoomForUserAsync(newChatUser).ConfigureAwait(false);

            //Notify managers about room creation
            await NotifyAboutRoomCreationAsync(roomId).ConfigureAwait(false);
        }

        /// <summary>
        /// Assignes group to all chat user connections and saves new group in chat user context
        /// </summary>
        /// <param name="chatUser">Existing chat user</param>
        /// <param name="group">Group to add</param>
        private async Task AddChatUserToGroupAsync(ChatUser chatUser, string group)
        {
            chatUser.UserGroups.Add(group);
            
            foreach (var connection in chatUser.UserConnections)
            {
                await AddConnectionToGroupAsync(connection, group);
            }
        }

        /// <summary>
        /// Creates new room and assignes it to user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Created room id</returns>
        private async Task<string> CreateNewRoomForUserAsync(ChatUser user)
        {
            var roomId = user.UserId;

            var room = new Room()
            {
                InitializerUserId = roomId,
                Messages = new List<ChatMessage>(),
                Users = new List<ChatUser>() { user }
            };

            Rooms.Add(roomId, room);

            // assign room to chatUser
            await AddChatUserToGroupAsync(user, roomId).ConfigureAwait(false);

            return roomId;
        }

        private async Task<ChatUser> CreateNewChatUserAsync()
        {
            var isAuthenticated = Context.User?.Identity?.IsAuthenticated;
            var connectionId = Context.ConnectionId;

            ChatUser newUser = new ChatUser(GetUserId(), isAuthenticated ?? false);
            newUser.AddConnection(connectionId);

            if (isAuthenticated.HasValue && isAuthenticated.Value && await HasManagerRoleAsync(GetUserId()))
            {
                newUser.IsManager = true;
                await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
                newUser.AddGroup(ADMIN_GROUP);
            }

            ConnectedUsers.Add(newUser.UserId, newUser);

            return newUser;
        }

        private ConfiguredTaskAwaitable AddConnectionToGroupAsync(string connectionId, string groupName)
            => Groups.AddToGroupAsync(connectionId, groupName).ConfigureAwait(false);

        private string GetUserId() => Context.UserIdentifier ?? $"anonymous_{Guid.NewGuid()}";

        private Task NotifyAboutRoomCreationAsync(string roomId)
        {
            return SendToAdminsAsync("NewRoomCreated", roomId)
            .ContinueWith((task) => SendRoomIdToConectionAsync(Context.ConnectionId, roomId));
        }

        private Task SendRoomIdToConectionAsync(string connectionId, string roomId)
            => Clients.Clients(connectionId).SendAsync("RecieveRoomId", roomId);

        private Task SendToAdminsAsync<TMessage>(string hubMethod, TMessage message) 
            => Clients.Group(ADMIN_GROUP).SendAsync(hubMethod, message); 

        private async Task<bool> HasManagerRoleAsync(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (currentUser == null)
                return false;

            return await _userManager.IsInRoleAsync(currentUser, Role.Manager.ToString()).ConfigureAwait(false);
        }
    }
}
