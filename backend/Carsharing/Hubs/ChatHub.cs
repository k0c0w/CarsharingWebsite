using Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.Chat.ChatEntites.SignalRModels;
using System.Runtime.CompilerServices;
using Persistence;

namespace Carsharing.ChatHub;

public class ChatHub : Hub
{
    private const string ADMIN_GROUP = "managers";

    private readonly IBus _publisher;
    private readonly UserManager<User> _userManager;
    private readonly IChatUserRepository _chatUserRepository;
    private readonly IChatRoomRepository _chatRoomRepository;

    public ChatHub(IBus bus, UserManager<User> userManager, IChatUserRepository userRepository, IChatRoomRepository roomRepository)
    {
        _publisher = bus;
        _userManager = userManager;
        _chatUserRepository = userRepository;
        _chatRoomRepository = roomRepository;
    }

    [HubMethodName("SendMessage")]
    public async Task SendMessageAsync(ChatMessage message)
    {
        var roomId = message.RoomId;

        if (!(_chatRoomRepository.TryGetRoom(roomId, out var room) && _chatUserRepository.TryGetUser(GetUserId(), out var chatUser)))
            return;

        var connectionId = Context.ConnectionId;
        var isCurrentUserManager = IsCurrentUserManager();
        // it is better to check if admin has connected
        if (!(room!.Client.UserConnections.Contains(connectionId) || isCurrentUserManager))
            return;

        message.IsFromManager = isCurrentUserManager;
        message.Time = DateTime.UtcNow;
        message.AuthorName = chatUser!.Name;
        if (IsAuthenticatedUser())
            await _publisher.Publish(new ChatMessageDto()
            {
                AuthorId = chatUser!.UserId,
                RoomInitializerId = roomId,
                Text = message.Text,
                Time = message.Time,
                IsAuthorManager = isCurrentUserManager,
            })
            .ConfigureAwait(false);

        await Clients.Group(roomId).SendAsync("RecieveMessage", message).ConfigureAwait(false);
    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HubMethodName("JoinRoom")]
    public async Task JoinRoomAsync(string roomId)
    {
        var managerId = Context.UserIdentifier!;
        var connectionId = Context.ConnectionId;

        if (!(_chatUserRepository.ContainsUserById(managerId) && _chatRoomRepository.TryGetRoom(roomId, out var room)))
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

        if (!(_chatUserRepository.ContainsUserById(managerId) && _chatRoomRepository.TryGetRoom(roomId, out var room)))
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
            await ExecuteUnauthorizeDisconnectAsync(disconnectedUserId).ConfigureAwait(false);
        else
            await ExecuteAuthorizedDisconnectAsync(disconnectedUserId, actualUserId).ConfigureAwait(false);

        await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
    }

    private async Task ExecuteUnauthorizeDisconnectAsync(string disconnectedUserId)
    {
        _chatRoomRepository.TryGetRoom(disconnectedUserId, out var room);
        await Clients.Group(ADMIN_GROUP).SendAsync(nameof(ChatRoomUpdate), new ChatRoomUpdate() { RoomId = room!.RoomId, Event = RoomUpdateEvent.Deleted }).ConfigureAwait(false);
        _chatUserRepository.TryRemoveUser(disconnectedUserId, out _);
        _chatRoomRepository.TryRemoveRoom(disconnectedUserId, out _);
    }

    private async Task ExecuteAuthorizedDisconnectAsync(string disconnectedUserId, string actualUserId)
    {
        if (_chatUserRepository.TryGetUser(disconnectedUserId, out var user) && _chatRoomRepository.TryGetRoom(actualUserId, out var room))
        {
            var connectionId = Context.ConnectionId;
            user!.RemoveConnection(connectionId);

            if (user.ConnectionsCount == 0)
            {
                foreach (var managerId in room!.ProcessingManagersIds)
                {
                    if (_chatUserRepository.TryGetUser(managerId, out var chatManager))
                        foreach (var managerConnectionId in chatManager!.UserConnections)
                            await Groups.RemoveFromGroupAsync(managerConnectionId, room.RoomId).ConfigureAwait(false);
                }

                _chatUserRepository.TryRemoveUser(actualUserId, out _);
                _chatRoomRepository.TryRemoveRoom(disconnectedUserId, out _);
                await Clients.Group(ADMIN_GROUP).SendAsync(nameof(ChatRoomUpdate), new ChatRoomUpdate() { RoomId = room.RoomId, Event = RoomUpdateEvent.Deleted }).ConfigureAwait(false);
            }
        }
    }


    private async Task ExecuteAuthorizedPipelineAsync()
    {
        var connectionId = Context.ConnectionId;
        var userId = Context.UserIdentifier;
        bool isAuthenticated = IsAuthenticatedUser();

        // if user has already connected with other session, he must be saved in connections and connection must be assigned to user groups
        if (isAuthenticated && _chatUserRepository.TryGetUser(userId!, out var chatUser))
        {
            chatUser!.AddConnection(connectionId);

            if (IsCurrentUserManager())
            {
                await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
            }
            else
            {
                await AddConnectionToGroupAsync(connectionId, GetUserId());
                await SendRoomIdToConectionAsync(connectionId, GetUserId());
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
    /// Creates new room and assignes it to user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Created room id</returns>
    private async Task<ChatRoom?> CreateNewRoomForUserAsync(ChatUser user)
    {
        var room = new ChatRoom(user);
        var roomId = room.RoomId;
        if (!_chatRoomRepository.TryAddRoom(roomId, room))
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

        ChatUser newUser = new(GetUserId(), name);

        newUser.AddConnection(connectionId);

        if (isUserAuthenticated && IsCurrentUserManager())
        {
            await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
        }

        _chatUserRepository.TryAddUser(newUser.UserId, newUser);

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
