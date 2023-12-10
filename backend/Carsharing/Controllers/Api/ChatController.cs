using Carsharing.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Chat;
using Persistence.Chat.ChatEntites.SignalRModels;
using Persistence.RepositoryImplementation;
using Persistence.Chat.ChatEntites;
using OccasionChatRepository = Persistence.Chat.ChatEntites.OccasionChatRepository;

namespace Carsharing.Controllers.Api;

[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMessageRepository _messageUoW;
    private readonly IChatRoomRepository<TechSupportChatRoom> _chatRoomRepository;
    private readonly OccasionMessageRepository _occasionMessageRepository;
    private readonly OccasionChatRepository _occasionChatRepository;
        

    public ChatController(IMessageRepository messageUnitOfWork, IChatRoomRepository<TechSupportChatRoom> chatRoomRepository, OccasionMessageRepository occasionMessageRepository, OccasionChatRepository occasionChatRepository)
    {
        _messageUoW = messageUnitOfWork;
        _chatRoomRepository = chatRoomRepository;
        _occasionMessageRepository = occasionMessageRepository;
        _occasionChatRepository = occasionChatRepository;
    }

    [Route("{userId}/history")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetChatHistoryWithUserAsync([FromRoute] string userId, [FromQuery] int limit = 100, [FromQuery] int offset = 0)
    {
        var currentUserId = User.GetId();
        if (!(currentUserId == userId || User.IsInRole(Role.Manager.ToString())))
            return Forbid();

        if (string.IsNullOrEmpty(userId))
            return new NotFoundResult();

        var history = await _messageUoW.GetMessagesAssosiatedWithUserAsync(userId, offset, limit).ConfigureAwait(false);

        return new JsonResult(history
            .Select(x => new ChatMessageVM()
            {
                AuthorName = x.AuthorName!,
                IsFromManager = x.IsFromManager,
                MessageId = x.MessageId!,
                Text = x.Text,
                Time = x.Time,
            })
          .ToArray());
    }

    [Route("{userId}/occasion_history")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOccasionChatHistoryAsync([FromRoute] Guid occasionId,
        [FromQuery] int limit = 100, [FromQuery] int offset = 0)
    {
        var history = await _occasionMessageRepository.GetMessagesAssosiatedWithUserAsync(occasionId, offset, limit).ConfigureAwait(false);

        return new JsonResult(history
            .Select(x => new ChatMessageVM()
            {
                AuthorName = x.AuthorName!,
                IsFromManager = x.IsFromManager,
                MessageId = x.MessageId!,
                Text = x.Text,
                Time = x.Time,
            })
            .ToArray());
    }

    [Route("rooms")]
    [Authorize(Roles = nameof(Role.Manager))]
    [HttpGet]
    public IActionResult GetAllRooms()
    {
        var regularChats = _chatRoomRepository.GetAll().Select(x => new
        {
            RoomName = x.Client.Name,
            x.RoomId,
            x.ProcessingManagersCount,
            IsOccasion = false
        });
        return new JsonResult(regularChats);
    }
    
    [Route("occasions_rooms")]
    [Authorize(Roles = nameof(Role.Manager))]
    [HttpGet]
    public IActionResult GetAllOccasionsRooms()
    {
        var occasionChats = _occasionChatRepository.GetAll().Select(x => new
        {
            RoomName = x.Client.Name,
            RoomId = x.RoomId.ToString(),
            x.ProcessingManagersCount,
            IsOccasion = true
        });
        return new JsonResult(occasionChats);
    }
}
