using Entities.Repository;
namespace Features.Posts.Commands.DeletePost;

public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        if (request.PostId < 1)
            return new Error("Wrong post id.");
        
        try
        {
            await _postRepository.RemoveByIdAsync(request.PostId).ConfigureAwait(false);
            return Result.SuccessResult;
        }
        catch (Exception e)
        {
            return new Error(e.Message);
        }
    }
}