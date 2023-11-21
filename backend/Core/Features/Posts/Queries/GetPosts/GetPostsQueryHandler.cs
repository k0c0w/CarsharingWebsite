using Contracts.NewsService;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Posts.Queries.GetPosts;

public class GetPostsQueryHandler: IQueryHandler<GetPostsQuery,IEnumerable<PostDto>>
{
    private readonly IPostRepository _postRepository;

    public GetPostsQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }


    public async Task<Result<IEnumerable<PostDto>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var posts = await _postRepository.GetBatchAsync().ConfigureAwait(false);
            return new Ok<IEnumerable<PostDto>>(posts.Select(post => new PostDto()
            {
                Body = post.Body,
                CreatedAt = post.CreatedAt,
                Id = post.Id,
                Title = post.Title
            }));
        }
        catch (Exception e)
        {
            return new Error<IEnumerable<PostDto>>(e.Message);
        }
    }
}