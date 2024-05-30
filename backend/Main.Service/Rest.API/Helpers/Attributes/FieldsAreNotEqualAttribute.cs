using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Carsharing.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FieldsAreNotEqualAttribute : ValidationAttribute
{
    public string OtherProperty { get; set; } = string.Empty;
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);
        if (otherPropertyInfo == null) throw new ArgumentException($"{OtherProperty} was not found");
        var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

        if (value == null || otherValue == null) return new ValidationResult("Не все поля представленны.");
        
        return value.Equals(otherValue) ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
    }
}