using Domain.Entities;

namespace Features.Users.Queries.GetUserWithInfo;

public record GetUserWithInfoQuery (string UserId) : IQuery<User>;