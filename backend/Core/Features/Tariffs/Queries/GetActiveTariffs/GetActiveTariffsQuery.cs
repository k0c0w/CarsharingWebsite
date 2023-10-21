using Contracts.Tariff;
using Shared.CQRS;

namespace Features.Tariffs;

public class GetActiveTariffsQuery : IQuery<IEnumerable<TariffDto>>    
{
    public bool IsTariffByIdRequest => TariffId.HasValue;

    public int? TariffId { get; }

    public GetActiveTariffsQuery(int? tariffId)
    {
        TariffId = tariffId;
    }
}
