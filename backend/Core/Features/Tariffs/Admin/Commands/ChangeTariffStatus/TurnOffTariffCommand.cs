namespace Features.Tariffs.Admin;

public class TurnOffTariffCommand : ChangeTariffStatusCommand
{
    public TurnOffTariffCommand(int tariffId) : base(tariffId, false)
    {
    }
}
