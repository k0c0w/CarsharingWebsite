using Contracts;

namespace Features.CarManagement;

public record GetAvailableCarsByLocationQuery : IQuery<IEnumerable<FreeCarDto>>
{
    public int CarModelId { get; init; }

    public decimal Longitude { get; init; }

    public decimal Latitude { get; init; }

    public int Radius { get; init; }

    public int Limit { get; init; } = 256;
}