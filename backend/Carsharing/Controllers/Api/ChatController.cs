using Carsharing.ViewModels;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Carsharing.Controllers.Api;

[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly CarsharingContext _chatContext;
    private readonly IChatRoomRepository _chatRoomRepository;

    public ChatController(CarsharingContext chatContext, IChatRoomRepository chatRoomRepository)
    {
        _chatContext = chatContext;
        _chatRoomRepository = chatRoomRepository;
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

        var history = await _chatContext.Messages
        .AsNoTracking()
        .Where(m => m.TopicAuthorId == userId)
        .Join(
            _chatContext.Users,
            m => m.AuthorId,
            u => u.Id,
            (m, u) =>
            new
            {
                m.Id,
                m.Text,
                m.Time,
                m.IsFromManager,
                u.FirstName,
                UserId = u.Id,
            }
        )
        .Skip(offset)
        .Take(limit)
        .OrderByDescending(x => x.Time)
        .ToArrayAsync()
        .ConfigureAwait(false);

        return new JsonResult(history
            .Select(x => new ChatMessageVM()
            {
                AuthorName = x.FirstName!,
                IsFromManager = x.IsFromManager,
                MessageId = x.UserId,
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
        return new JsonResult(_chatRoomRepository.GetAll().Select(x => new
        {
            RoomName = x.Client.Name,
            x.RoomId,
            x.ProcessingManagersCount
        }));
    }
}
