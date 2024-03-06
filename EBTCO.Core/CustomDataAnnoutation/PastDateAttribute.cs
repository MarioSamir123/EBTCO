using System.ComponentModel.DataAnnotations;

namespace ToursYard.Core.CustomDataAnnoutation
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ErrorMessage);
            DateTime dateTime;
            if (value is DateOnly)
            {
                var dateOnly = (DateOnly)value;
                dateTime = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
            }
            else
            {
                dateTime = (DateTime)value;
            }

            if (dateTime < DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
