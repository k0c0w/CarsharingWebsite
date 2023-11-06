using Shared.CQRS;

namespace Features.Posts.Commands.CreatePost;

public class CreatePostCommand : ICommand
{
    public CreatePostCommand( string title, string body)
    {
        Title = title;
        Body = body;
    }

    public string Title { get; }

    public string Body { get;  }
    
}