using Contracts;

namespace Services.Abstractions;

public interface IBookingService
{
    Task BookCarAsync(RentCarDto rentCarInfo);
}