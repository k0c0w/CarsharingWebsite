using Entities.Entities;
using Entities.Repository;
using Microsoft.Extensions.Logging;

namespace Features.Occasion.Admin;

public class GetOpenedOccasionsQueryHandler : IQueryHandler<GetOpenedOccasionsQuery, IEnumerable<Occassion>>
{
    private readonly IOccasionRepository _occasionRepository;
    private readonly ILogger<Exception> _logger;

    public GetOpenedOccasionsQueryHandler(IOccasionRepository occasionRepository, ILogger<Exception> logger)
    {
        _occasionRepository = occasionRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Occassion>>> Handle(GetOpenedOccasionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var occasions = await _occasionRepository.GetOpenedOccasionsAsync();
            return new Ok<IEnumerable<Occassion>>(occasions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Error<IEnumerable<Occassion>>();
        }
    }
}
