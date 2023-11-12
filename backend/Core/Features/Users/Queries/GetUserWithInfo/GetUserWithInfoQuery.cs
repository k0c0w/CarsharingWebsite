using Domain.Entities;
using Shared.CQRS;

namespace Features.Users.Queries.GetUserWithInfo;

public record GetUserWithInfoQuery (string UserId) : IQuery<User>;