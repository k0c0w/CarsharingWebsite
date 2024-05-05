using System.ComponentModel.DataAnnotations;

namespace GraphQL.API.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class DateEndAttribute : ValidationAttribute
{
    public string DateStartProperty { get; set; } = string.Empty;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return ValidationResult.Success;
    }
}