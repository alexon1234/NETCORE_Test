using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace webapi.src.Shared.Domain.ValueObject
{
    public class BaseValueObject
    {
        protected void ValidateModel()
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, ctx, validationResults, true);
            if(validationResults.Count > 0) 
            {
                throw new ValidationException(
                    $"Error validation failed: {string.Join(Environment.NewLine, validationResults.Select(v => v.ErrorMessage))}"
                );
            }
        }
    }
}