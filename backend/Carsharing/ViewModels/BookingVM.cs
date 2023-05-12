using System.Text.Json.Serialization;

namespace Carsharing.ViewModels;

public class BookingVM
{
    public int CarId { get; set; }
    
    public int TariffId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}