using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels;

public class BookingVM
{
    public int CarId { get; set;  }
    
    public int TariffId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int UserInfoId { get; set; }
}