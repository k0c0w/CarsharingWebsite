using Carsharing.ViewModels;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Carsharing.Controllers.Api;

[Route("api/chat")]
[ApiController]
public class ChatController
{
    private readonly CarsharingContext _chatContext;


    public ChatController(CarsharingContext chatContext)
    {
        _chatContext = chatContext;
    }

    //todo: validate wheter this is authorized user with sameId or manager

    [Route("{userId}/history")]
    [HttpGet]
    public async Task<IActionResult> GetChatHistoryWithUserAsync([FromRoute] string userId, [FromQuery] int limit = 100, [FromQuery] int offset = 0)
    {
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
                    u.FirstName,
                    UserId = u.Id,
                }
            )
            .Skip(offset)
            .Take(limit)
            .OrderByDescending(x => x.Time)
            .ToArrayAsync()
            .ConfigureAwait(false);

        if (!history.Any())
            return new JsonResult(Array.Empty<object>());

        var actingUsers = history.Select(x => x.UserId).Distinct().ToArray();

        var actingUsersHaveManagerRights = await _chatContext.Users
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Where(x => actingUsers.Contains(x.Id))
                    .ToDictionaryAsync(x => x.Id, x => x.UserRoles)
                    .ConfigureAwait(false);

        var upperRoleName = Role.Manager.ToString().ToUpper();
        return new JsonResult(history
            .Select(x => new ChatMessageVM()
            {
                AuthorName = x.FirstName!,
                IsFromManager = actingUsersHaveManagerRights[x.UserId].Select(x => x.Role.Name).Contains(upperRoleName),
                MessageId = x.UserId,
                Text = x.Text,
                Time = x.Time,
            })
          .ToArray());
    }
}
