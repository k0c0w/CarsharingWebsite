namespace Domain.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsersByIdsAsync(params string[] ids);
    public Task<User?> GetUserByIdAsync(string id);
}
