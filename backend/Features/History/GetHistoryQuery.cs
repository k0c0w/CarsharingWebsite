using Shared.CQRS;

namespace Features.History;

public record GetHistoryQuery(string RequesterUserId, string Topic, int Limit = 100, int Offset = 0) : IQuery<IEnumerable<MessageDto>>;
