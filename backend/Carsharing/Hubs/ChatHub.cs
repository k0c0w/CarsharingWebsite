using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.Chat.ChatEntites.SignalRModels;
using Domain.Common;
using Persistence;

namespace Carsharing.ChatHub;

public class ChatHub : Hub<IChatClient>
{
    private const string ADMIN_GROUP = "managers";

    private readonly IMessageProducer _publisher;
    private readonly UserManager<User> _userManager;
    private readonly IChatUserRepository<ChatUser> _chatUserRepository;
    private readonly IChatRoomRepository<TechSupportChatRoom> _chatRoomRepository;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ILogger<ChatHub> logger, IMessageProducer publisher, UserManager<User> userManager, IChatUserRepository<ChatUser> userRepository, IChatRoomRepository<TechSupportChatRoom> roomRepository)
    {
        _logger = logger;
        _publisher = publisher;
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
            await _publisher.SendMessageAsync(new ChatMessageDto()
            {
                AuthorId = chatUser!.UserId,
                RoomInitializerId = roomId,
                Text = message.Text,
                Time = message.Time,
                IsAuthorManager = isCurrentUserManager,
            });

        await Clients.Group(roomId).RecieveMessage(message);
    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HubMethodName("JoinRoom")]
    public async Task JoinRoomAsync(string roomId)
    {
        var managerId = Context.UserIdentifier!;
        var connectionId = Context.ConnectionId;

        if (!(_chatUserRepository.ContainsUserById(managerId) && _chatRoomRepository.TryGetRoom(roomId, out var room)))
        {
            await Clients.Client(connectionId).JoinRoomResult(new JoinRoomResult() { RoomId = roomId });
            return;
        }

        bool roomWasEmpty = room!.ProcessingManagersCount == 0;
        if (roomWasEmpty || !room!.ProcessingManagersIds.Contains(managerId))
        {
            room.AssignManager(managerId);
        }

        await AddConnectionToGroupAsync(connectionId, roomId);
        await Clients.Client(connectionId).JoinRoomResult(new JoinRoomResult() { RoomId = roomId, Success = true });

        if (roomWasEmpty)
        {
            await Clients.Group(ADMIN_GROUP).ChatRoomUpdate(new ChatRoomUpdate()
            {
                RoomId = roomId,
                Event = RoomUpdateEvent.ManagerJoined,

            });
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
            await Clients.Client(connectionId).LeaveRoomResult(new LeaveRoomResult () { RoomId = roomId });
            return;
        }

        if (!room!.ProcessingManagersIds.Contains(managerId))
        {
            await Clients.Client(connectionId).LeaveRoomResult (new LeaveRoomResult() { RoomId = roomId });
            return;
        }

        room.RevokeManager(managerId);

        if (room.ProcessingManagersCount == 0)
        {
            await Clients.Group(roomId).ChatRoomUpdate (new ChatRoomUpdate() { RoomId = roomId, Event = RoomUpdateEvent.ManagerLeft });
        }

        await Groups.RemoveFromGroupAsync(connectionId, roomId)
            .ContinueWith((previousTask) => Clients.Client(connectionId).LeaveRoomResult(new LeaveRoomResult() { RoomId = roomId, Success = true }));
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("New Connection");

        var connectedUserId = Context.UserIdentifier;

        if (connectedUserId == null)
            await CreateNewUserWithRoomAndNotifyAsync(false);
        else
            await ExecuteAuthorizedPipelineAsync();

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var disconnectedUserId = GetUserId();
        var actualUserId = Context.UserIdentifier;
        if (actualUserId == null)
            await ExecuteUnauthorizeDisconnectAsync(disconnectedUserId);
        else
            await ExecuteAuthorizedDisconnectAsync(disconnectedUserId, actualUserId);

        await base.OnDisconnectedAsync(exception);
    }

    private async Task ExecuteUnauthorizeDisconnectAsync(string disconnectedUserId)
    {
        _chatRoomRepository.TryGetRoom(disconnectedUserId, out var room);
        await Clients.Group(ADMIN_GROUP).ChatRoomUpdate(new ChatRoomUpdate() { RoomId = room!.RoomId, Event = RoomUpdateEvent.Deleted });
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
                            await Groups.RemoveFromGroupAsync(managerConnectionId, room.RoomId);
                }

                _chatUserRepository.TryRemoveUser(actualUserId, out _);
                _chatRoomRepository.TryRemoveRoom(disconnectedUserId, out _);
                await Clients.Group(ADMIN_GROUP).ChatRoomUpdate(new ChatRoomUpdate() { RoomId = room.RoomId, Event = RoomUpdateEvent.Deleted });
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
                await SendRoomIdToConnectionAsync(connectionId, GetUserId());
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
        var newChatUser = await CreateNewChatUserAsync(isUserAuthenticated);

        // if user is manger, we dont need to create room for him
        if (isUserAuthenticated && IsCurrentUserManager())
            return;

        //Create room and assign it to user (anonymous or authorized customers)
        var room = await CreateNewRoomForUserAsync(newChatUser);

        if (room == null)
            return;

        //Notify managers about room creation
        await NotifyAboutRoomCreationAsync(room.RoomId, room.Client.Name);
    }

    /// <summary>
    /// Creates new room and assignes it to user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Created room id</returns>
    private async Task<TechSupportChatRoom?> CreateNewRoomForUserAsync(ChatUser user)
    {
        var room = new TechSupportChatRoom(user);
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
            name = (await _userManager.FindByIdAsync(GetUserId()))!.FirstName!;

        ChatUser newUser = new(GetUserId(), name);

        newUser.AddConnection(connectionId);

        if (isUserAuthenticated && IsCurrentUserManager())
        {
            await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
        }

        _chatUserRepository.TryAddUser(newUser.UserId, newUser);

        return newUser;
    }

    private Task AddConnectionToGroupAsync(string connectionId, string groupName)
        => Groups.AddToGroupAsync(connectionId, groupName);

    private string GetUserId() => Context.UserIdentifier ?? $"anonymous_{Context.ConnectionId}";

    /// <summary>
    /// Notify admins to maintain roomlist
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="userFirstName"></param>
    /// <returns></returns>
    private Task NotifyAboutRoomCreationAsync(string roomId, string userFirstName)
    {
        return Clients.Group(ADMIN_GROUP).ChatRoomUpdate(new ChatRoomUpdate() { RoomId = roomId, RoomName = userFirstName, Event = RoomUpdateEvent.Created})
        .ContinueWith((task) => SendRoomIdToConnectionAsync(Context.ConnectionId, roomId));
    }

    /// <summary>
    /// Used to send room id to client, admins might know roomid
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="roomId"></param>
    /// <returns></returns>
    private Task SendRoomIdToConnectionAsync(string connectionId, string roomId)
        => Clients.Clients(connectionId).RecieveRoomId(roomId);

    private bool IsCurrentUserManager()
    {
        return Context.User != null && Context.User.IsInRole(Role.Manager.ToString());
    }
}
