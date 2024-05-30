namespace Features.Posts;

public class DeletePostCommand : ICommand
{
    public DeletePostCommand(int postId)
    {
        PostId = postId;
    }

    public int PostId { get; }
}