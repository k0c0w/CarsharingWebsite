
namespace Features.CarManagement;

public record ReleaseCarCommand(int CarId) : ICommand;