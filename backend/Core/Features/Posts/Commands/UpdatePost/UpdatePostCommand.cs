using Shared.CQRS;

namespace Features.Posts.Commands.UpdatePost;

public class UpdatePostCommand : ICommand
{
    public UpdatePostCommand(string body, string title, int postId)
    {
        Body = body;
        Title = title;
        PostId = postId;
    }

    public string Body { get; }
    public int PostId { get; }
    public string Title { get;}
}