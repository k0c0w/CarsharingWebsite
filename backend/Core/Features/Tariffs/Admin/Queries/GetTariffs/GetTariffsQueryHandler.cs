using Contracts.Tariff;
using Domain.Entities;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Tariffs.Admin;

public class GetTariffsQueryHandler : IQueryHandler<GetTariffsQuery, IEnumerable<AdminTariffDto>>
{
    private readonly static Ok<IEnumerable<AdminTariffDto>> _emptyResponse = new Ok<IEnumerable<AdminTariffDto>>(Array.Empty<AdminTariffDto>());

    private readonly ITariffRepository _tariffRepository;

    public GetTariffsQueryHandler(ITariffRepository tariffRepository)
    {
        _tariffRepository = tariffRepository;
    }

    public async Task<Result<IEnumerable<AdminTariffDto>>> Handle(GetTariffsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Tariff> tariffs;

            if(request.IsTariffByIdRequest)
            {
                if (request.TariffId < 1)
                    return _emptyResponse;

                var tariff = await _tariffRepository.GetByIdAsync(request.TariffId!.Value);
                if (tariff == null)
                    return _emptyResponse;

                tariffs = new[] { tariff }; 
            }
            else
                tariffs = await _tariffRepository.GetBatchAsync();

            return new Ok<IEnumerable<AdminTariffDto>>(MapTariffs(tariffs));
        }
        catch (Exception ex)
        {
            return new Error<IEnumerable<AdminTariffDto>>(ex.Message);
        }
    }

    private AdminTariffDto[] MapTariffs(IEnumerable<Tariff> tariffs)
        => tariffs
            .Select(tariff => new AdminTariffDto()
            {
                    Id = tariff.TariffId,
                    Description = tariff.Description,
                    Name = tariff.Name,
                    MaxMileage = tariff.MaxMileage,
                    PriceInRubles = tariff.PricePerMinute,
                    Image = tariff.ImageUrl,
                    IsActive = tariff.IsActive,
                    MaxBookMinutes = tariff.MinAllowedMinutes,
                    MinBookMinutes = tariff.MaxAllowedMinutes,
            })
            .ToArray();
}
