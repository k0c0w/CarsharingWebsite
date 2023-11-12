using Shared.CQRS;

namespace Features.CarManagement;

public record CloseCarCommand(string LicensePlate) : ICommand;