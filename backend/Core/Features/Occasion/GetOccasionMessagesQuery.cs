
using Shared.CQRS;

namespace Features.Occasion;

public class GetOccasionMessagesQuery : IQuery<IEnumerable<OccasionMessageDto>>
{
    public Guid OccasionId { get; }

    public string ApplicantId { get; }

    public GetOccasionMessagesQuery(Guid occasionId, string applicantId)
    {
        OccasionId = occasionId;
        ApplicantId = applicantId;
    }
}
