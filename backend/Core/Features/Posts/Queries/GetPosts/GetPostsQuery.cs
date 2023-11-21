using Contracts.NewsService;
using Shared.CQRS;

namespace Features.Posts;

public class GetPostsQuery : IQuery<IEnumerable<PostDto>>
{
    
}