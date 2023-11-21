using Shared.CQRS;

namespace Features.Tariffs.Admin;

public abstract class ChangeTariffStatusCommand : ICommand
{
    public int Tariffd { get; }

    public bool TurnOn { get; }

    protected ChangeTariffStatusCommand(int tariffId, bool turnOn) 
    {
        Tariffd = tariffId;
        TurnOn = turnOn;
    }
}
