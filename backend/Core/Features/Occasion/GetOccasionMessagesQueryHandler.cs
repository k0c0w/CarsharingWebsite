using Clients.S3ServiceClient;
using Domain.Entities;
using Entities.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion;

public class GetOccasionMessagesQueryHandler : IQueryHandler<GetOccasionMessagesQuery, IEnumerable<OccasionMessageDto>>
{
    private readonly IOccasionRepository _occasionRepository;

    private readonly S3ServiceClient _s3ServiceClient;
    private HttpContext RequestContext { get;  }

    public GetOccasionMessagesQueryHandler(IHttpContextAccessor httpContextAccessor, IOccasionRepository occasionRepository, IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _occasionRepository = occasionRepository;
        RequestContext = httpContextAccessor.HttpContext!;
        _s3ServiceClient = new S3ServiceClient(configuration["KnownHosts:BackendHosts:FileService"], clientFactory.CreateClient("authorized"));
    }

    public async Task<Result<IEnumerable<OccasionMessageDto>>> Handle(GetOccasionMessagesQuery request, CancellationToken cancellationToken)
    {
        var occasion = await _occasionRepository.GetByIdAsync(request.OccasionId);
        if (!(occasion.IssuerId == request.ApplicantId && occasion.CloseDateUtc is null 
            || RequestContext.User.IsInRole(Role.Admin.ToString()) || RequestContext.User.IsInRole(Role.Manager.ToString())))
            return new Error<IEnumerable<OccasionMessageDto>>();


        // todo: get history

        //if attachments
        /*
        foreach(var attachment in attachments)
        {
            var links = await s3ServiceClient.GetAttachments(occasion.Id);
        }
        */

        //todo: return OccasionMessageDto
        throw new NotImplementedException();
    }
}
