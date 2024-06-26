﻿using Entities.Entities;

namespace Features.Occasion.Admin;

public class GetOccasionQuery : IQuery<Occassion?>
{
    public Guid OccasionId { get; }

    public GetOccasionQuery(Guid occasionId)
    {
        OccasionId = occasionId;
    }
}
