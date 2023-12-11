using Domain.Common;
using Domain.Entities;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Persistence.Chat.ChatEntites;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.Chat.ChatEntites.SignalRModels;
using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Carsharing.ChatHub;

[Authorize]
public class OccasionsSupportChatHub : Hub<IOccasionChatClient>
{
    private readonly IMessageProducer _publisher;
    private readonly UserManager<User> _userManager;
    private readonly OccasionChatRepository _occasionChatRepository;
    private readonly IOccasionRepository _occasionRepository;

    public OccasionsSupportChatHub(IMessageProducer publisher, UserManager<User> userManager, OccasionChatRepository occasionChatRepository, IOccasionRepository occasionRepository)
    {
        _publisher = publisher;
        _userManager = userManager;
        _occasionChatRepository = occasionChatRepository;
        _occasionRepository = occasionRepository;
    }

    [HubMethodName("SendMessage")]
    public async Task SendMessageAsync(OccasionChatMessage message)
    {
        var connectedUserId = GetUserId();

        if (await IsCurrentUserManagerOrAdmin())
        {
            if(_occasionChatRepository.TryAddAdminConnectionToGroup(Context.ConnectionId, message.OccasionId.ToString()))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, message.OccasionId.ToString());
            }
            if (!_occasionChatRepository.TryGetAdminConnectionGroup(Context.ConnectionId, out string groupName) || groupName != message.OccasionId.ToString())
                return;
        }
        else if (_occasionChatRepository.TryGetUser(connectedUserId, out OccasionChatUser? chatUser))
        {
            if (chatUser.JoinedRoomId != message.OccasionId)
                return;

            if (!chatUser!.UserConnections.Contains(Context.ConnectionId))
            {
                chatUser.AddConnection(Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, chatUser.JoinedRoomId.ToString());
            }
        }
        else
        {
            var occassion = await _occasionRepository.GetOpenOccasionByIssuerIdAsync(Guid.Parse(GetUserId()));
            if (occassion is not null && occassion.IssuerId == GetUserId())
            {
                chatUser = await CreateNewChatUserAsync();
                chatUser.JoinedRoomId = message.OccasionId;
                await Groups.AddToGroupAsync(Context.ConnectionId, chatUser.JoinedRoomId.ToString());
            }
        }

        message.AuthorName = (await _userManager.FindByIdAsync(GetUserId())).FirstName!;
        message.IsFromManager = await IsCurrentUserManagerOrAdmin();
        message.MessageId = Guid.NewGuid();

        /*
        await _publisher.SendMessageAsync(new OccasionChatMessageDto()
        {
            AuthorId = GetUserId(),
            Attachment = message.Attachment,
            OccasionId = message.OccasionId,
            Text = message.Text,
            Time = message.Time,
            IsAuthorManager = message.IsFromManager,
        });
        */

        await Clients.Group(message.OccasionId.ToString()).ReceiveMessage(message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var disconnectedUserId = GetUserId();
        if (await IsCurrentUserManagerOrAdmin())
        {
            _occasionChatRepository.TryRemoveAdminConnection(Context.ConnectionId, out _);
        }
        else
        {
            if (_occasionChatRepository.TryGetUser(disconnectedUserId, out var user))
            {
                var connectionId = Context.ConnectionId;
                user!.RemoveConnection(connectionId);
                if (user.UserConnections.Count == 0)
                    _occasionChatRepository.TryRemoveUser(disconnectedUserId, out _);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    private async Task<OccasionChatUser> CreateNewChatUserAsync()
    {
        var connectionId = Context.ConnectionId;

        var userName = (await _userManager.FindByIdAsync(GetUserId()).ConfigureAwait(false))!.FirstName!;

        OccasionChatUser newUser = new(GetUserId(), userName);

        newUser.AddConnection(connectionId);

        _occasionChatRepository.TryAddUser(newUser.UserId, newUser);

        return newUser;
    }

    private string GetUserId() => Context.UserIdentifier ?? throw new Exception();

    private async Task<bool> IsCurrentUserManagerOrAdmin()
    {
        var user = await _userManager.FindByIdAsync(GetUserId());
        if (user is null)
            return false;
        return (await _userManager.IsInRoleAsync(user, Role.Manager.ToString())) || (await _userManager.IsInRoleAsync(user, Role.Admin.ToString()));
    }
}