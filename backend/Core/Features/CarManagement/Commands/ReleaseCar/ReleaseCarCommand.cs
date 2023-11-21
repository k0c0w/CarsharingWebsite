using Shared.CQRS;

namespace Features.CarManagement;

public record ReleaseCarCommand(int CarId) : ICommand;