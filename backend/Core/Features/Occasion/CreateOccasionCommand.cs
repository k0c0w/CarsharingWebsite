using Entities.Entities;
using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion;

public class CreateOccasionCommand : ICommand<Result<Guid>>
{
    public Guid IssuerId { get; }

    public string Topic { get; }

    public OccasionTypeDefinition OccasionType { get; }

    public CreateOccasionCommand(Guid issuerId, string topic, OccasionTypeDefinition occasionType)
    {
        IssuerId = issuerId;
        Topic = topic;
        OccasionType = occasionType;
    }
}
