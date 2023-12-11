using Clients.Objects;
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
            return new Shared.Results.Error<IEnumerable<OccasionMessageDto>>();

        var messages = await _occasionMessageRepository.GetMessagesAsync(request.OccasionId, 100, 0);

        var result = new List<OccasionMessageDto>();

        foreach (var message in messages)
        {
            IEnumerable<AttachmentInfo> links;
            if (message.AttachmentId is not null)
            {
                var webCallResult = await _s3ServiceClient.GetAttachmentInfosByIdsAsync(message.AttachmentId.Value);
                if (webCallResult.Success)
                {
                    OccasionMessageDto dto = new OccasionMessageDto()
                    {
                        Id = message.MessageId,
                        MessageText = message.Text,
                        IsFromManager = message.IsFromManager,
                        AuthorName = message.AuthorName,
                        Attachments = webCallResult.Data.Select(x=>new OccasionMessageAttachmentDto(){ ContentType = x.ContentType, DownloadUrl = x.DownloadUrl})
                    };
                    result.Add(dto);
                }
            }
        }

        return new Ok<IEnumerable<OccasionMessageDto>>(result);
    }
}
