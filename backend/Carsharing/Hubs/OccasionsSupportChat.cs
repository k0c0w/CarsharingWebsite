using System.Runtime.CompilerServices;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Persistence;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace Carsharing.ChatHub;

[Authorize]
public class OccasionsSupportChat : Hub<IChatClient>
{
    private const string ADMIN_GROUP = "managers";

    private readonly IMessageProducer _publisher;
    private readonly UserManager<User> _userManager;
    private readonly IChatUserRepository<OccasionChatUser> _chatUserRepository;
    private readonly IChatRoomRepository<OccasionsSupportChatRoom> _chatRoomRepository;

    public OccasionsSupportChat(IMessageProducer publisher, UserManager<User> userManager, IChatUserRepository<OccasionChatUser> userRepository, IChatRoomRepository<OccasionsSupportChatRoom> roomRepository)
    {
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
            })
            .ConfigureAwait(false);

        await Clients.Group(roomId).RecieveMessage(message).ConfigureAwait(false);
    }

    [Authorize(Roles = nameof(Role.Manager))]
    [HubMethodName("JoinRoom")]
    public async Task JoinRoomAsync(string roomId)
    {
        var managerId = Context.UserIdentifier!;
        var connectionId = Context.ConnectionId;

        if (!(_chatUserRepository.ContainsUserById(managerId) && _chatRoomRepository.TryGetRoom(roomId, out var room)))
        {
            await Clients.Client(connectionId).JoinRoomResult(new JoinRoomResult() { RoomId = roomId }).ConfigureAwait(false);
            return;
        }

        bool roomWasEmpty = room!.ProcessingManagersCount == 0;
        if (roomWasEmpty || !room!.ProcessingManagersIds.Contains(managerId))
        {
            room.AssignManager(managerId);
        }

        await AddConnectionToGroupAsync(connectionId, roomId);
        await Clients.Client(connectionId).JoinRoomResult(new JoinRoomResult() { RoomId = roomId, Success = true }).ConfigureAwait(false);

        if (roomWasEmpty)
        {
            await Clients.Group(ADMIN_GROUP).ChatRoomUpdate(new ChatRoomUpdate()
            {
                RoomId = roomId,
                Event = RoomUpdateEvent.ManagerJoined,

            }).ConfigureAwait(false);
        }
    }

    [HubMethodName("CloseOccasionChat")]
    public async Task CloseOccasionChat()
    {
        if (IsCurrentUserManager())
            return;
        
        var disconnectedUserId = GetUserId();
        var actualUserId = Context.UserIdentifier;
        
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
                await Clients.Group(ADMIN_GROUP).ChatRoomUpdate(new ChatRoomUpdate() { RoomId = room.RoomId, Event = RoomUpdateEvent.Deleted }).ConfigureAwait(false);
            }
        }

        await OnDisconnectedAsync(null);
    }


    [Authorize(Roles = nameof(Role.Manager))]
    [HubMethodName("LeaveRoom")]
    public async Task LeaveRoomAsync(string roomId)
    {
        var managerId = Context.UserIdentifier!;
        var connectionId = Context.ConnectionId;
        
        if (!(_chatUserRepository.ContainsUserById(managerId) && _chatRoomRepository.TryGetRoom(roomId, out var room)))
        {
            await Clients.Client(connectionId).LeaveRoomResult(new LeaveRoomResult () { RoomId = roomId }).ConfigureAwait(false);
            return;
        }

        if (!room!.ProcessingManagersIds.Contains(managerId))
        {
            await Clients.Client(connectionId).LeaveRoomResult (new LeaveRoomResult() { RoomId = roomId }).ConfigureAwait(false);
            return;
        }

        room.RevokeManager(managerId);

        if (room.ProcessingManagersCount == 0)
        {
            await Clients.Group(roomId).ChatRoomUpdate (new ChatRoomUpdate() { RoomId = roomId, Event = RoomUpdateEvent.ManagerLeft }).ConfigureAwait(false);
        }

        await Groups.RemoveFromGroupAsync(connectionId, roomId)
            .ContinueWith((previousTask) => Clients.Client(connectionId).LeaveRoomResult(new LeaveRoomResult() { RoomId = roomId, Success = true }))
            .ConfigureAwait(false);
    }

    /// Если входит юзер, то мы проверяем, есть ли комната по его occasion.
    /// Если да, то присоединяем его туда
    public override async Task OnConnectedAsync()
    {
        var connectedUserId = Context.UserIdentifier;

        if (connectedUserId == null)
            return;

        if (!(_chatUserRepository.TryGetUser(connectedUserId, out var user) 
              && 
              _chatRoomRepository.TryGetRoom(user!.UserId, out var room)))
            return;

        await AddConnectionToGroupAsync(connectedUserId, room!.RoomId);
        
        await base.OnConnectedAsync().ConfigureAwait(false);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var disconnectedUserId = GetUserId();
        var actualUserId = Context.UserIdentifier;
        if (actualUserId == null)
            return;
        
        await ExecuteAuthorizedDisconnectAsync(disconnectedUserId).ConfigureAwait(false);

        await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
    }

    private async Task ExecuteAuthorizedDisconnectAsync(string disconnectedUserId)
    {
        if (_chatUserRepository.TryGetUser(disconnectedUserId, out var user) && _chatRoomRepository.TryGetRoom(disconnectedUserId, out var room))
        {
            var connectionId = Context.ConnectionId;
            user!.RemoveConnection(connectionId);
            
            // Оставляем нашего юзера и комнату. Убираем лишь соединение
            // if (user.ConnectionsCount == 0)
            // {
            //     _chatUserRepository.TryRemoveUser(disconnectedUserId, out _);
            //     _chatRoomRepository.TryRemoveRoom(disconnectedUserId, out _);
            //     await Clients.Group(ADMIN_GROUP).ChatRoomUpdate(new ChatRoomUpdate() { RoomId = room.RoomId, Event = RoomUpdateEvent.Deleted }).ConfigureAwait(false);
            // }
        }
    }

    /// После подключения юзер отправляет гуид с нужным occasion
    /// Создается комната с его userId
    private async Task SendOccasionRequest(Guid occasionId)
    {
        var newChatUser = await CreateNewChatUserAsync(IsAuthenticatedUser()).ConfigureAwait(false);

        await CreateRoomAsync(IsAuthenticatedUser(), occasionId);

        await NotifyAboutRoomCreationAsync(newChatUser.UserId, newChatUser.Name, occasionId);
    }

    private async Task CreateRoomAsync(bool isUserAuthenticated, Guid occasionId)
    {
        // Create new chat user
        var newChatUser = await CreateNewChatUserAsync(isUserAuthenticated).ConfigureAwait(false);

        // if user is manger, we dont need to create room for him
        if (isUserAuthenticated && IsCurrentUserManager())
            return;

        //Create room and assign it to user (authorized customers)
        var room = new OccasionsSupportChatRoom(newChatUser, newChatUser.UserId, occasionId);
        var roomId = room.RoomId;
        if (!_chatRoomRepository.TryAddRoom(roomId, room))
            return;

        if (room == null)
            return;
        
        // assign room to chatUser
        await AddConnectionToGroupAsync(Context.ConnectionId, roomId);
    }

    private async Task<OccasionChatUser> CreateNewChatUserAsync(bool isUserAuthenticated)
    {
        var connectionId = Context.ConnectionId;

        string? name = default;
        if (isUserAuthenticated)
            name = (await _userManager.FindByIdAsync(GetUserId()).ConfigureAwait(false))!.FirstName!;

        OccasionChatUser newUser = new(GetUserId(), name);

        newUser.AddConnection(connectionId);

        if (isUserAuthenticated && IsCurrentUserManager())
        {
            await AddConnectionToGroupAsync(connectionId, ADMIN_GROUP);
        }

        _chatUserRepository.TryAddUser(newUser.UserId, newUser);

        return newUser;
    }

    /// <summary>
    /// Notify admins to maintain roomlist
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="userFirstName"></param>
    /// <returns></returns>
    private Task NotifyAboutRoomCreationAsync(string roomId, string userFirstName, Guid occasionId)
    {
        return Clients.Group(ADMIN_GROUP)
            .ChatRoomUpdate(new OccasionChatRoomUpdate(occasionId) { RoomId = roomId, RoomName = userFirstName, Event = RoomUpdateEvent.Created})
            .ContinueWith((task) => SendRoomIdToConnectionAsync(Context.ConnectionId, roomId));
    }

    private ConfiguredTaskAwaitable AddConnectionToGroupAsync(string connectionId, string groupName)
        => Groups.AddToGroupAsync(connectionId, groupName).ConfigureAwait(false);

    private bool IsAuthenticatedUser() => Context.User != null && Context.User.Identity != null && Context.User.Identity.IsAuthenticated;

    private string GetUserId() => Context.UserIdentifier ?? throw new Exception();
    
    private Task SendRoomIdToConnectionAsync(string connectionId, string roomId)
        => Clients.Clients(connectionId).RecieveRoomId(roomId);

    private bool IsCurrentUserManager()
    {
        return Context.User != null && Context.User.IsInRole(Role.Manager.ToString());
    }
}