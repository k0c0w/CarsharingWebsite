using Shared.CQRS;

namespace Features.Posts;

public class CreatePostCommand : ICommand<int>
{
    public CreatePostCommand( string title, string body)
    {
        Title = title;
        Body = body;
    }

    public string Title { get; }

    public string Body { get;  }
}