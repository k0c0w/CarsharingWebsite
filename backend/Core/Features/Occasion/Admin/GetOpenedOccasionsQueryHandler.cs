using Entities.Entities;
using Entities.Repository;
using Microsoft.Extensions.Logging;
using Shared.CQRS;
using Shared.Results;

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
            occasions = new Occassion[] { 
                new (){ 
                    CreationDateUtc = DateTime.Now, 
                    CloseDateUtc = null, 
                    OccasionType = OccasionTypeDefinition.RoadAccident, 
                    Id = Guid.NewGuid(), 
                    Topic = "", 
                    IssuerId = Guid.NewGuid().ToString() 
                },
                new (){ 
                    CreationDateUtc = DateTime.Now, 
                    CloseDateUtc = null, 
                    OccasionType = OccasionTypeDefinition.Other, 
                    Id = Guid.NewGuid(), 
                    Topic = "", 
                    IssuerId = Guid.NewGuid().ToString() 
                } 
            };
            return new Ok<IEnumerable<Occassion>>(occasions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Error<IEnumerable<Occassion>>();
        }
    }
}
