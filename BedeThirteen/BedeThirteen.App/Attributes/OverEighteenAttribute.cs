using System;
using System.ComponentModel.DataAnnotations;

namespace BedeThirteen.App.Attributes
{
    public class OverEighteenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            // This assumes inclusivity, i.e. exactly six years ago is okay
            if (DateTime.Now.AddYears(-6).CompareTo(value) <= 0 && DateTime.Now.CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be within the last six years!");
            }
        }
    }
}