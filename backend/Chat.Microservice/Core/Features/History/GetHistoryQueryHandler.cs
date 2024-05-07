using Domain;
using Domain.Repositories;
using MediatR;
using Shared.CQRS;
using Shared.Results;

namespace Features.History;

public class GetHistoryQueryHandler(
    IMessageRepository messageRepository, 
    IUserRepository userRepository)
    : IQueryHandler<GetHistoryQuery, IEnumerable<MessageDto>>
{
    private readonly IMessageRepository _messageRepository = messageRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<IEnumerable<MessageDto>>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var requester = await _userRepository.GetUserByIdAsync(request.RequesterUserId);

            if (!(requester.Id == request.Topic || requester.IsManager))
                return new Error<IEnumerable<MessageDto>>();

            var messages = await _messageRepository.GetMessagesByTopicAsync(request.Topic, limit: request.Limit, offset: request.Offset);
            var uniqueUserIds = messages
                                    .Select(x => x.AuthorId)
                                    .Distinct()
                                    .ToArray();
            var users = await _userRepository.GetUsersByIdsAsync(uniqueUserIds);

            var messageDtos = messages
                .Join(
                    users,
                    m => m.AuthorId,
                    u => u.Id,
                    (m, u) => new MessageDto
                    {
                        AuthorName = u.Name,
                        IsFromManager = u.IsManager,
                        MessageId = m.Id.ToString(),
                        Text = m.Text,
                        Time = m.Time,
                    }
                )
                .ToArray();

            return new Ok<IEnumerable<MessageDto>>(messageDtos);
        }
        catch
        {
            return new Error<IEnumerable<MessageDto>>();
        }
    }
}
