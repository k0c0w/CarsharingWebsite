﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Carsharing.Helpers.Attributes;

namespace Carsharing.ViewModels.Admin.UserInfo;

public record EditUserVM
{

    [JsonPropertyName("surname")]
    [Required(ErrorMessage = "Поле обязательно")]
    public string? Surname { get; set; } 

    
    [JsonPropertyName("name")]
    [Required(ErrorMessage = "Поле обязательно")]
    public string? Name { get; set; }

    
    [EmailAddress(ErrorMessage ="В почте должен содержаться спецсимвол @")]
    [JsonPropertyName("email")]
    [Required(ErrorMessage = "Поле обязательно")]
    public string? Email { get; set; }
    

    [ValidateAge(AgeThreshold = 18, ErrorMessage = "Вам должно быть не менее 18 лет.")]
    [JsonPropertyName("birthdate")]
    [Required(ErrorMessage = "Поле обязательно")]
    public DateTime Birthdate { get; set; }
}