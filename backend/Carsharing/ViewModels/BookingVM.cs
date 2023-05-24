using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Carsharing.Helpers.Attributes;

namespace Carsharing.ViewModels;

public class BookingVM
{
    [Required(ErrorMessage = "car_id обязательно.")]
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }
    
    public int TariffId { get; set; }
    
    [Required(ErrorMessage = "start_date обязательно.")]
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "end_date обязательно.")]
    [DateEnd(DateStartProperty="StartDate", ErrorMessage = "Не верные даты.")]
    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }
    
    public int UserInfoId { get; set; }
}