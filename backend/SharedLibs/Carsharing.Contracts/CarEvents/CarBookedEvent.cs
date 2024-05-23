using System;

namespace Carsharing.Contracts.CarEvents
{
    public class CarBookedEvent
    {
        public string TariffName { get; set; }

        public string CarModelName { get; set; }

        public string CarLicensePlate { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public DateTime SubscriptionStartTimeUtc { get; set; }
    }
}
