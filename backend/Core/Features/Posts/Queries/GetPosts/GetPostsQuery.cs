using Contracts.NewsService;
using Shared.CQRS;

namespace Features.Posts.Queries.GetPosts;

public class GetPostsQuery : IQuery<IEnumerable<PostDto>>
{
    
}