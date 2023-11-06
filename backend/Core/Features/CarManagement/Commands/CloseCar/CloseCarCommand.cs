using Shared.CQRS;

namespace Features.CarManagement;

public record CloseCarCommand(int CarId) : ICommand;