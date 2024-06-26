﻿using Entities.Entities;

namespace Features.Occasion;

public class GetOpenedUserOccasionQuery : IQuery<Guid?>
{
    public string UserId { get; set; }

    public GetOpenedUserOccasionQuery(string id)
    {
        UserId = id;
    }
}
