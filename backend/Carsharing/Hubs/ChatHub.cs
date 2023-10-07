using Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.Chat.ChatEntites.SignalRModels;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Carsharing.ChatHub;

//toread Сопопставление пользователей SignalR с подключениями


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

    private readonly IBus _publisher;
    private readonly UserManager<User> _userManager;

    // to map user and their connections
    // for anonymous users we will use their session token (notice that it can be hacked)
    private static readonly ConcurrentDictionary<string, ChatRoom> Rooms = new ConcurrentDictionary<string, ChatRoom>();

    private static readonly ConcurrentDictionary<string, ChatUser> ConnectedUsers = new ConcurrentDictionary<string, ChatUser>();



    public ChatHub(IBus bus, UserManager<User> userManager)
    {
        _publisher = bus;
        _userManager = userManager;
    }

    [HubMethodName("SendMessage")]
    public async Task SendMessageAsync(ChatMessage message)
    {
        var roomId = message.RoomId;

        if (!Rooms.TryGetValue(roomId, out var room))
            return;

        if (!room.Users.SelectMany(x => x.UserConnections).Contains(Context.ConnectionId))
            return;

        message.Time = DateTime.UtcNow;
        //todo: saveMessage in db (should call backgroundservice here)
        if (IsAuthenticatedUser())
            await _publisher.Publish(new ChatMessageDto()
            {
                AuthorId = Context.UserIdentifier!,
                RoomInitializerId = roomId,
                Text = message.Text,
                Time = message.Time,
            })
            .ConfigureAwait(false);

        await Clients.Group(roomId).SendAsync("RecieveMessage", message).ConfigureAwait(false);
    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HubMethodName("JoinRoom")]
    public async Task JoinRoomAsync(string roomId)
    {
        var managerId = Context.UserIdentifier!;

        if (!Rooms.ContainsKey(roomId) || !ConnectedUsers.ContainsKey(managerId))
        {
            return;
        }

        if (!(Rooms.TryGetValue(roomId, out var room) && ConnectedUsers.TryGetValue(managerId, out var managerUser)))
            return;

        // cannot enter room twice
        if (room.Users.Contains(managerUser))
            return;

        await AddChatUserToGroupAsync(managerUser, roomId).ConfigureAwait(false);
        room.Users.Add(managerUser);

        // todo: event admin entered the room
    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HubMethodName("LeaveRoom")]
    public async Task LeaveRoomAsync(string roomId)
    {
        var managerId = Context.UserIdentifier!;

        if (!Rooms.ContainsKey(roomId) || !ConnectedUsers.ContainsKey(managerId))
        {
            return;
        }

        var room = Rooms[managerId];
        var managerUser = ConnectedUsers[managerId];

        // cannot enter room twice
        if (!room.Users.Contains(managerUser))
            return;

        await AddChatUserToGroupAsync(managerUser, roomId).ConfigureAwait(false);
        room.Users.Add(managerUser);


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

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        //todo: удаление комнатыесли юзер отключился
        var disconnectedUserId = Context.UserIdentifier;
        if (disconnectedUserId != null)
        {
            ConnectedUsers.TryGetValue(disconnectedUserId, out var user);
            if (user != null)
            {
                if (user.UserConnections.Count > 1)
                {
                    user.RemoveConnection(disconnectedUserId);
                    return Task.CompletedTask;
                }
                ConnectedUsers.TryRemove(disconnectedUserId, out user);
                if (!IsCurrentUserManager())
                    Rooms.TryRemove("", out var room);
            }
            if (user != null && user.UserConnections.Count != 0)
            {
                user.RemoveConnection(Context.ConnectionId);
            }

             

        }
        // не понятно как удалить админа из комнаты
        //

        return base.OnDisconnectedAsync(exception);
    }

    private async Task ExecuteAuthorizedPipelineAsync()
    {
        var connectionId = Context.ConnectionId;
        var userId = Context.UserIdentifier!;
        bool isAuthenticated = IsAuthenticatedUser();

        // if user has already connected with other session, he must be saved in connections and connection must be assigned to user groups
        if (isAuthenticated && ConnectedUsers.TryGetValue(userId, out var chatUser))
        {
            chatUser.AddConnection(connectionId);

            // assign all user`s groups to new connection
            foreach(var group in chatUser.UserGroups)
                await Groups.AddToGroupAsync(connectionId, group).ConfigureAwait(false);

            if (IsCurrentUserManager())
            {
                var connections = ConnectedUsers[userId].UserConnections;
                await Clients.Clients(Context.ConnectionId).SendAsync("OnlineRooms", Rooms
                .Select(x => new {RoomId=x.Key, Name=x.Value.Users.First(y => y.UserId == x.Key).Name}));
            }

            return;
        }

        // Create user, create room, notify admins about new room
        await CreateNewUserWithRoomAndNotifyAsync(isAuthenticated);
    }


    private bool IsAuthenticatedUser() => Context.User != null && Context.User.Identity != null && Context.User.Identity.IsAuthenticated;

    private async Task CreateNewUserWithRoomAndNotifyAsync(bool isUserAuthenticated)
    {
        // Create new chat user
        var newChatUser = await CreateNewChatUserAsync().ConfigureAwait(false);

        // if user is manger, we dont need to create room for him
        if (isUserAuthenticated && IsCurrentUserManager())
            return;

        //Create room and assign it to user (anonymous or authorized customers)
        var roomId = await CreateNewRoomForUserAsync(newChatUser).ConfigureAwait(false);

        if (roomId == null)
            return;

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
    private async Task<string?> CreateNewRoomForUserAsync(ChatUser user)
    {
        var roomId = user.UserId;

        var room = new ChatRoom()
        {
            InitializerUserId = roomId,
            Users = new List<ChatUser>() { user }
        };

        if (!Rooms.TryAdd(roomId, room))
            return null!;

        // assign room to chatUser
        await AddChatUserToGroupAsync(user, roomId).ConfigureAwait(false);

        return roomId;
    }

    private async Task<ChatUser> CreateNewChatUserAsync()
    {
        var isAuthenticated = Context.User?.Identity?.IsAuthenticated;
        var connectionId = Context.ConnectionId;

        string name = string.Empty;
        if (isAuthenticated.HasValue && isAuthenticated.Value)
            name = (await _userManager.FindByIdAsync(GetUserId()).ConfigureAwait(false))!.FirstName!;

        ChatUser newUser = new ChatUser(GetUserId(), isAuthenticated ?? false, name);

        newUser.AddConnection(connectionId);

        if (isAuthenticated.HasValue && isAuthenticated.Value && IsCurrentUserManager())
        {
            await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
            newUser.AddGroup(ADMIN_GROUP);
        }

        ConnectedUsers.TryAdd(newUser.UserId, newUser);

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

    private bool IsCurrentUserManager()
    {
        return Context.User != null && Context.User.IsInRole(Role.Manager.ToString());
    }
}
