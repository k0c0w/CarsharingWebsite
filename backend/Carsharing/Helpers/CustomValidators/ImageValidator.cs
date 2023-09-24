using System.ComponentModel.DataAnnotations;

namespace Carsharing.Helpers.CustomValidators;

[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
public class ImageValidatorAttribute : ValidationAttribute
{
    private readonly long _maxLength;

    public ImageValidatorAttribute(long maxFileLength) => _maxLength = maxFileLength;
    
    
    public override bool IsValid(object? value)
    {
        var file = value as IFormFile;
        if (file == null)
            throw new ArgumentException("Property or field must be IFormFile", paramName: nameof(value));

        if (!file.ContentType.StartsWith("image/"))
        {
            ErrorMessage = "Ожидался 'Content-type: image/x-png'";
            return false;
        }

        if (file.Length > _maxLength)
        {
            ErrorMessage = "Файл слишком большой";
            return false;
        }
        
        var lastDot = file.FileName.LastIndexOf('.');
        if (lastDot == -1) return false;
        var extension = file.FileName.Substring(lastDot);

        if (extension != ".png")
        {
            ErrorMessage = "Поддерживаются только png";
            return false;
        }
        
        return true;
    }
}