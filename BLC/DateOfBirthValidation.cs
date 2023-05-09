using System.ComponentModel.DataAnnotations;

namespace BLC
{
    public class ValidateDateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateOfBirth = (DateTime)value;
            DateTime currentDate = DateTime.Today;

            if (dateOfBirth >= currentDate)
            {
                return new ValidationResult("Date of Birth must be in the past");
            }

            return ValidationResult.Success;
        }
    }
}
