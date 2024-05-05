using System.ComponentModel.DataAnnotations;

namespace GraphQL.API.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValidateAgeAttribute : ValidationAttribute
{
    public int AgeThreshold { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var birthDate = (DateTime?)value;
        return birthDate?.AddYears(AgeThreshold) > DateTime.Today
            ? new ValidationResult(ErrorMessage)
            : ValidationResult.Success;
    }
}