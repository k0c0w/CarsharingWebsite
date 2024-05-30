
namespace Features.CarManagement;

public record OpenCarCommand(string LicensePlate) : ICommand;