using AutoMapper;
using Contracts.Tariff;
using Domain.Entities;
using Entities.Repository;

namespace Features.Tariffs;

public class GetActiveTariffsQueryHandler : IQueryHandler<GetActiveTariffsQuery, IEnumerable<TariffDto>>
{
    private readonly static Ok<IEnumerable<TariffDto>> _emptyResponse = new (Array.Empty<TariffDto>());

    private readonly ITariffRepository _tariffRepository;
    private readonly IMapper _mapper;

    public GetActiveTariffsQueryHandler(ITariffRepository tariffRepository, IMapper mapper)
    {
        _mapper = mapper;
        _tariffRepository = tariffRepository;
    }

    public async Task<Result<IEnumerable<TariffDto>>> Handle(GetActiveTariffsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Tariff> tariffs;

            if (request.IsTariffByIdRequest)
            {
                if (request.TariffId < 1)
                    return _emptyResponse;

                var tariff = await _tariffRepository.GetByIdAsync(request.TariffId!.Value);
                if (tariff == null || !tariff.IsActive)
                    return _emptyResponse;

                tariffs = new[] { tariff };
            }
            else
                tariffs = await _tariffRepository.GetAllActiveAsync();

            return new Ok<IEnumerable<TariffDto>>(MapTariffs(tariffs));
        }
        catch (Exception ex)
        {
            return new Error<IEnumerable<TariffDto>>(ex.Message);
        }
    }

    private TariffDto[] MapTariffs(IEnumerable<Tariff> tariffs)
        => tariffs
            .Select(x => _mapper.Map<Tariff, TariffDto>(x))
            .ToArray();
}
