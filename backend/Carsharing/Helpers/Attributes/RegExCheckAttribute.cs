using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Carsharing.Helpers.Attributes
{

    public class RegExCheckAttribute : ValidationAttribute
    {
        private readonly Regex _regex;
        public RegExCheckAttribute(string epression)
        {
            _regex = new Regex(epression);
        }
        public RegExCheckAttribute(Regex regex)
        {
            _regex = regex;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (string)value;

            if (_regex.IsMatch(currentValue))
                return ValidationResult.Success;


            return new ValidationResult(ErrorMessage ?? "This field is not correct!");
        }
    }
}
