using Domain.Entities;
using Shared.CQRS;

namespace Features.Users.Queries.GetUserInfoById;

public record GetUserInfoByIdQuery(string Id) : IQuery<UserInfo>;