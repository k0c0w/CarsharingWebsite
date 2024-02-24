using Entities.Entities;
using Shared.CQRS;


namespace Features.Occasion.Admin;

public class GetOpenedOccasionsQuery : IQuery<IEnumerable<Occassion>>
{
}
