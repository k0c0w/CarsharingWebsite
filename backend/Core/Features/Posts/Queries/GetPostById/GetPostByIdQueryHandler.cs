using Contracts.NewsService;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Posts.Queries.GetPostById;

public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery,PostDto>
{
    private readonly IPostRepository _postRepository;

    public GetPostByIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result<PostDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postRepository.GetByIdAsync(request.PostId).ConfigureAwait(false);
            if (post is null)
                    return new Error<PostDto>("Post was not found");
            return new Ok<PostDto>(new PostDto()
            {
                Body = post.Body,
                CreatedAt = post.CreatedAt,
                Id = post.Id,
                Title = post.Title
            });
        }
        catch (Exception e)
        {
            return new Error<PostDto>(e.Message);
        }
    }
}