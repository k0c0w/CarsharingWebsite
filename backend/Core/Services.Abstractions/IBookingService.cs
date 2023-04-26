using Contracts;

namespace Services.Abstractions;

public interface IBookingService
{
    Task BookCarAsync(RentCarDto rentCarInfo);

    Task<IEnumerable<FreeCarDto>> GetFreeCars(int tariffId, GeoPoint coordinates,
        double locationRadiusInMeters = 100, int limit = 500);
}