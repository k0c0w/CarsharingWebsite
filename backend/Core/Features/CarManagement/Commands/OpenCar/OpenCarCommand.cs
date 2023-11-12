using Shared.CQRS;

namespace Features.CarManagement;

public record OpenCarCommand(int CarId) : ICommand;