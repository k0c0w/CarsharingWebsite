using Shared.CQRS;

namespace Features.Tariffs.Admin;

public class DeleteTariffCommand : ICommand
{
    public int TariffId { get; }

    public DeleteTariffCommand(int tariffId)
    {
        TariffId = tariffId;
    }
}
