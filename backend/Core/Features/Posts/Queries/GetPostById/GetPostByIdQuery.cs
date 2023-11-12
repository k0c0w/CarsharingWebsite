using Contracts.NewsService;
using Shared.CQRS;

namespace Features.Posts;

public class GetPostByIdQuery : IQuery<PostDto>
{
    public GetPostByIdQuery(int postId)
    {
        PostId = postId;
    }

    public int PostId { get; }
    
}