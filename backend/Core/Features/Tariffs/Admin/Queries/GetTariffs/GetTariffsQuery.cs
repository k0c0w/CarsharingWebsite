﻿using Contracts.Tariff;
using Shared.CQRS;

namespace Features.Tariffs.Admin;

public class GetTariffsQuery : IQuery<IEnumerable<AdminTariffDto>>    
{
    public bool IsTariffByIdRequest => TariffId.HasValue;

    public int? TariffId { get; }

    public GetTariffsQuery(int? tariffId = default)
    {
        TariffId = tariffId;
    }
}
