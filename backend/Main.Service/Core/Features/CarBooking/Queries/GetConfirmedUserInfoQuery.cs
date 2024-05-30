using Domain.Entities;

namespace Features.CarBooking.Queries;

public record GetConfirmedUserInfoQuery(string UserId) : IQuery<UserInfo>;