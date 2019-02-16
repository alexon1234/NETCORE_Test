using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.src.Shared.Domain.ValueObject
{
    public class DateTimeBiggerThanNowAttribute : ValidationAttribute
    {
        public DateTimeBiggerThanNowAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime objectDateTime = (DateTime)value;

            if (objectDateTime < DateTime.Now)
            {
                return new ValidationResult("The DateTime needs to be bigger than the current DateTime");
            }

            return ValidationResult.Success;
        }
    }
}