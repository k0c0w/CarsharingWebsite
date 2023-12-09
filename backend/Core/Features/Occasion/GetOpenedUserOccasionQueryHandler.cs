using Entities.Repository;
using Microsoft.Extensions.Logging;
using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion;

public class GetOpenedUserOccasionQueryHandler : IQueryHandler<GetOpenedUserOccasionQuery, Guid?>
{
    private readonly IOccasionRepository _occasionRepository;
    private readonly ILogger<Exception> _logger;

    public GetOpenedUserOccasionQueryHandler(IOccasionRepository occasionRepository, ILogger<Exception> logger)
    {
        _occasionRepository = occasionRepository;
        _logger = logger;
    }

    public async Task<Result<Guid?>> Handle(GetOpenedUserOccasionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (Guid.TryParse(request.UserId, out Guid id))
                return new Error<Guid?>();

            var occasion = await _occasionRepository.GetOpenOccasionByIssuerIdAsync(Guid.Parse(request.UserId));

            if (occasion == null)
            {
                return new Ok<Guid?>(default(Guid?));
            }

            return new Ok<Guid?>(occasion.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Error<Guid?>();
        }
    }
}
