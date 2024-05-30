using Contracts.NewsService;
using Entities.Repository;
using Microsoft.Extensions.Logging;

namespace Features.Posts
{
    public class GetPaginatedPostsQueryHandler : IQueryHandler<GetPaginatedPostsQuery, IEnumerable<PostDto>>
    {
        private readonly ILogger _logger;
        private readonly IPostRepository _repository;

        public GetPaginatedPostsQueryHandler(ILogger<Exception> logger, IPostRepository postRepository)
        {
            _logger = logger;
            _repository = postRepository;
        }

        public async Task<Result<IEnumerable<PostDto>>> Handle(GetPaginatedPostsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var news = await _repository.GetPaginatedNoTrackingAsync(request.Page, request.Limit, request.ByDescending);

                return new Ok<IEnumerable<PostDto>>(news.Select(x => new PostDto
                {
                    Body = x.Body,
                    CreatedAt = x.CreatedAt,
                    Id = x.Id,
                    Title = x.Title,
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new Error<IEnumerable<PostDto>>("Internal error while retrieving posts!");
            }
        }
    }
}
