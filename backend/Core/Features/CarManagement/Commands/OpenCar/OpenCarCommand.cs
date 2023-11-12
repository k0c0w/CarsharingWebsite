using Shared.CQRS;

namespace Features.CarManagement;

public record OpenCarCommand(string LicensePlate) : ICommand;