using Shared.CQRS;

namespace Features.Balance.Commands.IncreaseBalance;

public record IncreaseBalanceCommand(string UserId, decimal Value) : ICommand;