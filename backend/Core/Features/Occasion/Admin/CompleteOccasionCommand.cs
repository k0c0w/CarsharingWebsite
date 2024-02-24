using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion.Admin;

public class CompleteOccasionCommand : ICommand
{
    public Guid OccasionId { get; }

    public CompleteOccasionCommand(Guid occasionId)
    {
        OccasionId = occasionId;
    }
}
