using System.ComponentModel.DataAnnotations;

namespace Carsharing.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class DateEndAttribute : ValidationAttribute
{
    public string DateStartProperty { get; set; }
    
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return ValidationResult.Success;
        //todo:
        /*var r = validationContext.Items[DateStartProperty];
        var dateEnd = (DateTime)value;
        var dateStart = (DateTime)r;

        return dateStart < dateEnd ? ValidationResult.Success : new ValidationResult(ErrorMessage);*/
    }
}