using Domain.Entities;
using Shared.CQRS;

namespace Features.Users.Queries.GetAllInfo;

public record GetAllInfoQuery : IQuery<List<UserInfo>>;