using Contracts.User;

namespace Features.Users.Queries.GetPersonalInfo;

public sealed record GetPersonalInfoQuery(string UserId) : IQuery<UserInfoDto>;