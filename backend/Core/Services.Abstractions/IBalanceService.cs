namespace Services.Abstractions;

public interface IBalanceService
{
    public Task<string> IncreaseBalance(string userId, decimal val);
    public Task<string> DecreaseBalance(string userId, decimal val);
}