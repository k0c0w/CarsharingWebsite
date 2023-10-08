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

        if (!(Rooms.TryGetValue(roomId, out var room) && ConnectedUsers.TryGetValue(GetUserId(), out var chatUser)))
            return;

        var connectionId = Context.ConnectionId;
        // it is better to check if admin has connected
        if (!(room.Client.UserConnections.Contains(connectionId) || IsCurrentUserManager()))
            return;

        message.IsFromManager = IsCurrentUserManager();
        message.Time = DateTime.UtcNow;
        message.AuthorName = chatUser!.Name;
        //todo: saveMessage in db (should call backgroundservice here)
        if (IsAuthenticatedUser())
            await _publisher.Publish(new ChatMessageDto()
            {
                AuthorId = chatUser!.UserId,
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
        // todo: semaphore slim
        var managerId = Context.UserIdentifier!;
        var connectionId = Context.ConnectionId;

        if (!(ConnectedUsers.ContainsKey(managerId) && Rooms.TryGetValue(roomId, out var room)))
        {
            await Clients.Client(connectionId).SendAsync(nameof(JoinRoomResult), new JoinRoomResult() { RoomId = roomId }).ConfigureAwait(false);
            return;
        }

        bool roomWasEmpty = room!.ProcessingManagersCount == 0;
        if (roomWasEmpty || !room!.ProcessingManagersIds.Contains(managerId))
        {
            room.AssignManager(managerId);
        }

        await AddConnectionToGroupAsync(connectionId, roomId);
        await Clients.Client(connectionId).SendAsync(nameof(JoinRoomResult), new JoinRoomResult() { RoomId = roomId, Success = true }).ConfigureAwait(false);

        if (roomWasEmpty)
        {
            await Clients.Group(ADMIN_GROUP).SendAsync(nameof(ChatRoomUpdate), new ChatRoomUpdate()
            {
                RoomId = roomId,
                Event = RoomUpdateEvent.ManagerJoined,

            }).ConfigureAwait(false);
        }
    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HubMethodName("LeaveRoom")]
    public async Task LeaveRoomAsync(string roomId)
    {
        var managerId = Context.UserIdentifier!;
        var connectionId = Context.ConnectionId;

        if (!(ConnectedUsers.ContainsKey(managerId) && Rooms.TryGetValue(roomId, out var room)))
        {
            await Clients.Client(connectionId).SendAsync(nameof(LeaveRoomResult), new LeaveRoomResult () { RoomId = roomId }).ConfigureAwait(false);
            return;
        }

        if (!room!.ProcessingManagersIds.Contains(managerId))
        {
            await Clients.Client(connectionId).SendAsync(nameof(LeaveRoomResult), new LeaveRoomResult() { RoomId = roomId }).ConfigureAwait(false);
            return;
        }

        room.RevokeManager(managerId);

        if (room.ProcessingManagersCount == 0)
        {
            await Clients.Group(roomId).SendAsync(nameof(ChatRoomUpdate), new ChatRoomUpdate() { RoomId = roomId, Event = RoomUpdateEvent.ManagerLeft }).ConfigureAwait(false);
        }

        await Groups.RemoveFromGroupAsync(connectionId, roomId)
            .ContinueWith((previousTask) => Clients.Client(connectionId).SendAsync(nameof(LeaveRoomResult), new LeaveRoomResult() { RoomId = roomId, Success = true }))
            .ConfigureAwait(false);
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

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var disconnectedUserId = GetUserId();
        var actualUserId = Context.UserIdentifier;
        if (actualUserId == null)
        {
            var room = Rooms[disconnectedUserId]!;
            await Clients.Group(ADMIN_GROUP).SendAsync(nameof(ChatRoomUpdate), new ChatRoomUpdate() { RoomId = room.RoomId, Event = RoomUpdateEvent.Deleted }).ConfigureAwait(false);
            ConnectedUsers.TryRemove(disconnectedUserId, out _);
        }
        else
        {
            if (ConnectedUsers.TryGetValue(disconnectedUserId, out var user) && Rooms.TryGetValue(actualUserId, out var room))
            {
                var connectionId = Context.ConnectionId;
                user.RemoveConnection(connectionId);

                if (user.ConnectionsCount == 0)
                {
                    foreach(var managerId in room.ProcessingManagersIds)
                    {
                        if (ConnectedUsers.TryGetValue(managerId, out var chatManager))
                        {
                            foreach (var managerConnectionId in chatManager.UserConnections)
                            {
                                await Groups.RemoveFromGroupAsync(managerConnectionId, room.RoomId).ConfigureAwait(false);
                            }
                            // todo: create list<Task> and wait
                        }
                    }

                    ConnectedUsers.TryRemove(actualUserId, out _);
                    await Clients.Group(ADMIN_GROUP).SendAsync(nameof(ChatRoomUpdate), new ChatRoomUpdate() { RoomId = room.RoomId, Event = RoomUpdateEvent.Deleted }).ConfigureAwait(false);
                }
            }
        }

        await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
    }

    private async Task ExecuteAuthorizedPipelineAsync()
    {
        var connectionId = Context.ConnectionId;
        var userId = Context.UserIdentifier;
        bool isAuthenticated = IsAuthenticatedUser();

        // if user has already connected with other session, he must be saved in connections and connection must be assigned to user groups
        if (isAuthenticated && ConnectedUsers.TryGetValue(userId!, out var chatUser))
        {
            chatUser.AddConnection(connectionId);

            if (IsCurrentUserManager())
            {
                await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
            }
            else
            {
                await AddConnectionToGroupAsync(connectionId, GetUserId());
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
        var newChatUser = await CreateNewChatUserAsync(isUserAuthenticated).ConfigureAwait(false);

        // if user is manger, we dont need to create room for him
        if (isUserAuthenticated && IsCurrentUserManager())
            return;

        //Create room and assign it to user (anonymous or authorized customers)
        var room = await CreateNewRoomForUserAsync(newChatUser).ConfigureAwait(false);

        if (room == null)
            return;

        //Notify managers about room creation
        await NotifyAboutRoomCreationAsync(room.RoomId, room.Client.Name).ConfigureAwait(false);
    }

    /// <summary>
    /// Assignes group to all chat user connections and saves new group in chat user context
    /// </summary>
    /// <param name="chatUser">Existing chat user</param>
    /// <param name="group">Group to add</param>
    private async Task AddChatUserToGroupAsync(ChatUser chatUser, string group)
    {
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
    private async Task<ChatRoom?> CreateNewRoomForUserAsync(ChatUser user)
    {
        var room = new ChatRoom(user);
        var roomId = room.RoomId;
        if (!Rooms.TryAdd(roomId, room))
            return null!;

        // assign room to chatUser
        await AddConnectionToGroupAsync(Context.ConnectionId, roomId);

        return room;
    }

    private async Task<ChatUser> CreateNewChatUserAsync(bool isUserAuthenticated)
    {
        var connectionId = Context.ConnectionId;

        string? name = default;
        if (isUserAuthenticated)
            name = (await _userManager.FindByIdAsync(GetUserId()).ConfigureAwait(false))!.FirstName!;

        ChatUser newUser = new ChatUser(GetUserId(), name);

        newUser.AddConnection(connectionId);

        if (isUserAuthenticated && IsCurrentUserManager())
        {
            await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
        }

        ConnectedUsers.TryAdd(newUser.UserId, newUser);

        return newUser;
    }

    private ConfiguredTaskAwaitable AddConnectionToGroupAsync(string connectionId, string groupName)
        => Groups.AddToGroupAsync(connectionId, groupName).ConfigureAwait(false);

    private string GetUserId() => Context.UserIdentifier ?? $"anonymous_{Context.ConnectionId}";

    /// <summary>
    /// Notify admins to maintain roomlist
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="userFirstName"></param>
    /// <returns></returns>
    private Task NotifyAboutRoomCreationAsync(string roomId, string userFirstName)
    {
        return SendToAdminsAsync(nameof(ChatRoomUpdate), new ChatRoomUpdate() { RoomId = roomId, RoomName = userFirstName, Event = RoomUpdateEvent.Created})
        .ContinueWith((task) => SendRoomIdToConectionAsync(Context.ConnectionId, roomId));
    }

    /// <summary>
    /// Used to send room id to client, admins might know roomid
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="roomId"></param>
    /// <returns></returns>
    private Task SendRoomIdToConectionAsync(string connectionId, string roomId)
        => Clients.Clients(connectionId).SendAsync("RecieveRoomId", roomId);

    private Task SendToAdminsAsync<TMessage>(string hubMethod, TMessage message) 
        => Clients.Group(ADMIN_GROUP).SendAsync(hubMethod, message); 

    private bool IsCurrentUserManager()
    {
        return Context.User != null && Context.User.IsInRole(Role.Manager.ToString());
    }
}
