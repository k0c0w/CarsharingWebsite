using Contracts.User;
using Shared.CQRS;

namespace Features.Users.Queries.GetPersonalInfo;

public sealed record GetPersonalInfoQuery(string UserId) : IQuery<UserInfoDto>;