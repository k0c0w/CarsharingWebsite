using Domain.Entities;

namespace Features.Users.Queries.GetAllInfo;

public record GetAllInfoQuery : IQuery<List<UserInfo>>;