using Contracts.Occasion;
using Domain.Common;
using Entities.Repository;
using Microsoft.Extensions.Logging;
using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion.Admin;

public class CompleteOccasionCommandHandler : ICommandHandler<CompleteOccasionCommand>
{
    private readonly IOccasionRepository _occasionRepository;
    private readonly ILogger<Exception> _logger;
    private readonly IMessageProducer _messageProducer;


    public CompleteOccasionCommandHandler(IOccasionRepository occasionRepository, ILogger<Exception> logger, IMessageProducer messageProducer)
    {
        _occasionRepository = occasionRepository;
        _logger = logger;
        _messageProducer = messageProducer;
    }

    public async Task<Result> Handle(CompleteOccasionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var occasion = await _occasionRepository.GetByIdAsync(request.OccasionId);

            if (occasion is null || occasion.CloseDateUtc is not null)
                return Result.ErrorResult;

            occasion.CloseDateUtc = DateTime.UtcNow;

            await _occasionRepository.UpdateAsync(occasion);


            await _messageProducer.SendMessageAsync(new OccasionStatusChangeDto
            {
                OccasionId = request.OccasionId,
                IssuerId = Guid.Parse(occasion.IssuerId),
                ChangeType = OccasionStatusChange.Completed
            });

            return Result.SuccessResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.ErrorResult;
        }
    }
}
