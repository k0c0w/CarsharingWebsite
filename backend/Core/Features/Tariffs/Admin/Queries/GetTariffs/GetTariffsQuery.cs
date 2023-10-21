using Contracts.Tariff;
using Shared.CQRS;

namespace Features.Tariffs.Admin;

public class GetTariffsQuery : IQuery<IEnumerable<TariffDto>>    
{
    public bool IsTariffByIdRequest => TariffId.HasValue;

    public int? TariffId { get; }

    public GetTariffsQuery(int? tariffId)
    {
        TariffId = tariffId;
    }
}
