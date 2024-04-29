using Carsharing.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Chat;
using Persistence.Chat.ChatEntites.SignalRModels;
using Shared;

namespace Carsharing.Controllers.Api;

[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMessageRepository _messageUoW;
    private readonly IChatRoomRepository<TechSupportChatRoom> _chatRoomRepository;
        

    public ChatController(IMessageRepository messageUnitOfWork, IChatRoomRepository<TechSupportChatRoom> chatRoomRepository)
    {
        _messageUoW = messageUnitOfWork;
        _chatRoomRepository = chatRoomRepository;
    }

    [Route("{userId}/history")]
    [HttpGet]
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

    [Route("rooms")]
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
}
