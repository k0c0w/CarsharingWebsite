using Shared.CQRS;

namespace Features.Tariffs.Admin;

public  class ChangeTariffStatusCommand : ICommand
{
    public int Tariffd { get; }

    public bool TurnOn { get; }

    public ChangeTariffStatusCommand(int tariffId, bool turnOn) 
    {
        Tariffd = tariffId;
        TurnOn = turnOn;
    }
}
