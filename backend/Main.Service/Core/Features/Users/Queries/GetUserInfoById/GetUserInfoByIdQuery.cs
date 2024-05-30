using Domain.Entities;

namespace Features.Users.Queries.GetUserInfoById;

public record GetUserInfoByIdQuery(string Id) : IQuery<UserInfo>;