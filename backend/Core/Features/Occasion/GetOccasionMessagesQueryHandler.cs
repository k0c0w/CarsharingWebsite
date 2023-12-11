using Clients.S3ServiceClient;
using Domain.Entities;
using Entities.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Persistence.RepositoryImplementation;
using Shared.CQRS;
using Shared.Results;

namespace Features.Occasion;

public class GetOccasionMessagesQueryHandler : IQueryHandler<GetOccasionMessagesQuery, IEnumerable<OccasionMessageDto>>
{
    private readonly IOccasionRepository _occasionRepository;
    private readonly OccasionMessageRepository _occasionMessageRepository;
    private readonly S3ServiceClient _s3ServiceClient;
    
    private HttpContext RequestContext { get;  }

    public GetOccasionMessagesQueryHandler(IHttpContextAccessor httpContextAccessor, IOccasionRepository occasionRepository, IHttpClientFactory clientFactory, IConfiguration configuration, OccasionMessageRepository occasionMessageRepository)
    {
        _occasionRepository = occasionRepository;
        _occasionMessageRepository = occasionMessageRepository;
        RequestContext = httpContextAccessor.HttpContext!;
        _s3ServiceClient = new S3ServiceClient(configuration["KnownHosts:BackendHosts:FileService"], clientFactory.CreateClient("authorized"));
    }

    public async Task<Result<IEnumerable<OccasionMessageDto>>> Handle(GetOccasionMessagesQuery request, CancellationToken cancellationToken)
    {
        var occasion = await _occasionRepository.GetByIdAsync(request.OccasionId);
        if (occasion is null || !(occasion.IssuerId == request.ApplicantId && occasion.CloseDateUtc is null 
            || RequestContext.User.IsInRole(Role.Admin.ToString()) || RequestContext.User.IsInRole(Role.Manager.ToString())))
            return new Error<IEnumerable<OccasionMessageDto>>();

        var messages = await _occasionMessageRepository.GetMessagesAsync(request.OccasionId, 100, 0);

        var result = messages.Select(x => new OccasionMessageDto()
            {
                Id = x.MessageId,
                MessageText = x.Text,
                IsFromManager = x.IsFromManager,
                Attachments = default,
                AuthorName = x.AuthorName,
            }
        );
        result.ToList()
            .Add(
                new () { AuthorName = "Lol", IsFromManager = true, MessageText = "Hello!!!!", Id = Guid.NewGuid(), Attachments = default}
            );

        return new Ok<IEnumerable<OccasionMessageDto>>(result);
        
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
