using Contracts.Tariff;
using Domain.Entities;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Tariffs.Admin;

public class GetTariffsQueryHandler : IQueryHandler<GetTariffsQuery, IEnumerable<TariffDto>>
{
    private readonly static Ok<IEnumerable<TariffDto>> _emptyResponse = new Ok<IEnumerable<TariffDto>>(Array.Empty<TariffDto>());

    private readonly ITariffRepository _tariffRepository;

    public GetTariffsQueryHandler(ITariffRepository tariffRepository)
    {
        _tariffRepository = tariffRepository;
    }

    public async Task<Result<IEnumerable<TariffDto>>> Handle(GetTariffsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Tariff> tariffs;

            if(request.IsTariffByIdRequest)
            {
                if (request.TariffId < 1)
                    return _emptyResponse;

                var tariff = await _tariffRepository.GetByIdAsync(request.TariffId!.Value).ConfigureAwait(false);
                if (tariff == null)
                    return _emptyResponse;

                tariffs = new[] { tariff }; 
            }
            else
                tariffs = await _tariffRepository.GetBatchAsync().ConfigureAwait(false);

            return new Ok<IEnumerable<TariffDto>>(MapTariffs(tariffs));
        }
        catch (Exception ex)
        {
            return new Error<IEnumerable<TariffDto>>(ex.Message);
        }
    }

    private TariffDto[] MapTariffs(IEnumerable<Tariff> tariffs)
        => tariffs
            .Select(tariff => new TariffDto()
            {
                    Id = tariff.TariffId,
                    Description = tariff.Description,
                    Name = tariff.Name,
                    MaxMileage = tariff.MaxMileage,
                    PriceInRubles = tariff.Price,
                    Image = $"/tariffs/{tariff.Name}.png"
            })
            .ToArray();
}
