using Shared.CQRS;

namespace Features.Balance.Commands.DecreaseBalance;

public record DecreaseBalanceCommand(string UserId, decimal Value) : ICommand;