﻿using Entities.Entities;
using Features.Occasion.Inputs;
using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion;

public class CreateOccasionCommand : ICommand<Guid>
{
    public Guid IssuerId { get; }

    public CreateOccasionDto OccasionInfo { get; }

    public CreateOccasionCommand(Guid issuerId, CreateOccasionDto occasionInfo)
    {
        IssuerId = issuerId;
        OccasionInfo = occasionInfo;
    }
}
