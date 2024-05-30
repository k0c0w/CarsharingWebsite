using Entities.Entities;
using Entities.Repository;
using Microsoft.Extensions.Logging;

namespace Features.Occasion.Admin;

public class GetOccasionQueryHandler : IQueryHandler<GetOccasionQuery, Occassion?>
{
    private readonly IOccasionRepository _occasionRepository;
    private readonly ILogger<Exception> _logger;

    public GetOccasionQueryHandler(IOccasionRepository occasionRepository, ILogger<Exception> logger)
    {
        _occasionRepository = occasionRepository;
        _logger = logger;
    }

    public async Task<Result<Occassion?>> Handle(GetOccasionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var occasion = await _occasionRepository.GetByIdAsync(request.OccasionId);

            return new Ok<Occassion?>(occasion);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Error<Occassion?>();
        }
    }
}
