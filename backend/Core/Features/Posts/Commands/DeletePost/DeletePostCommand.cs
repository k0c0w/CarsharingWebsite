using Shared.CQRS;

namespace Features.Posts.Commands.DeletePost;

public class DeletePostCommand : ICommand
{
    public DeletePostCommand(int postId)
    {
        PostId = postId;
    }

    public int PostId { get; }
}