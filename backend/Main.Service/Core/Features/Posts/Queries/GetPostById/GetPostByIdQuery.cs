using Contracts.NewsService;

namespace Features.Posts;

public class GetPostByIdQuery : IQuery<PostDto>
{
    public GetPostByIdQuery(int postId)
    {
        PostId = postId;
    }

    public int PostId { get; }
    
}