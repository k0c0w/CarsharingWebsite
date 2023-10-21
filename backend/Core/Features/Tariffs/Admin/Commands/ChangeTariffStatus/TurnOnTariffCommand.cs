namespace Features.Tariffs.Admin;

public class TurnOnTariffCommand : ChangeTariffStatusCommand
{
    public TurnOnTariffCommand(int tariffId) : base(tariffId, true)
    {
    }
}
