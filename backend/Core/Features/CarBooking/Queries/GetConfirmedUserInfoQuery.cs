using Domain.Entities;
using Shared.CQRS;

namespace Features.CarBooking.Queries;

public record GetConfirmedUserInfoQuery(string UserId) : IQuery<UserInfo>;