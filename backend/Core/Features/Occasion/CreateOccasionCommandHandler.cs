using Contracts.Occasion;
using Domain.Common;
using Entities.Entities;
using Entities.Repository;
using Microsoft.Extensions.Logging;
using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion;

public class CreateOccasionCommandHandler : ICommandHandler<CreateOccasionCommand, Guid>
{
    private readonly IOccasionRepository _occasionRepository;
    private readonly ILogger<Exception> _logger;
    private readonly IMessageProducer _messageProducer;

    public CreateOccasionCommandHandler(IOccasionRepository occasionRepository, ILogger<Exception> logger, IMessageProducer messageProducer)
    {
        _occasionRepository = occasionRepository;
        _logger = logger;
        _messageProducer = messageProducer;
    }

    public async Task<Result<Guid>> Handle(CreateOccasionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingOccasion = await _occasionRepository.GetOpenOccasionByIssuerIdAsync(request.IssuerId);
            if (existingOccasion is not null)
                return new Error<Guid>("You have alredy opened occasion!");

            if (!Enum.TryParse(request.OccasionInfo.OccasionType, out OccasionTypeDefinition occasionType))
                return new Error<Guid>($"Invalid value: {request.OccasionInfo.OccasionType}");

            var occasion = new Occassion()
            {
                CreationDateUtc = DateTime.UtcNow,
                IssuerId = request.IssuerId.ToString(),
                OccasionType = occasionType,
                Topic = request.OccasionInfo.Topic,
            };

            var createdId = await _occasionRepository.AddAsync(occasion);

            await _messageProducer.SendMessageAsync(new OccasionStatusChangeDto()
            {
                ChangeType = OccasionStatusChange.Created,
                OccasionId = createdId,
                IssuerId = request.IssuerId,
            });

            return new Ok<Guid>(createdId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Error<Guid>();
        }
    }
}
