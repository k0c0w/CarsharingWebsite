using Contracts.User;
using Shared.CQRS;

namespace Features.Users.Queries.GetProfileInfo;

public record GetProfileInfoQuery(string UserId) : IQuery<ProfileInfoDto>;