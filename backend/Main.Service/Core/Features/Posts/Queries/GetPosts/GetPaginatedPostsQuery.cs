using Contracts.NewsService;

namespace Features.Posts;

public record GetPaginatedPostsQuery : IQuery<IEnumerable<PostDto>>
{
    public int Page { get; init; }

    public int Limit { get; init; }

    public bool ByDescending { get; init; }
}
