using Contracts.User;

namespace Features.Users.Queries.GetProfileInfo;

public record GetProfileInfoQuery(string UserId) : IQuery<ProfileInfoDto>;