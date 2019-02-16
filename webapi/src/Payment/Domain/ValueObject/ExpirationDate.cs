using System;
using System.ComponentModel.DataAnnotations;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain.ValueObject
{
    public class ExpirationDate: BaseValueObject
    {
        [Required(ErrorMessage = "Expiration Date is required")]
        [DateTimeBiggerThanNow(ErrorMessage = "Expiration Date needs to be bigger than now")]
        public DateTime Value { get; private set; }

        protected ExpirationDate() {}
        
        public ExpirationDate(DateTime value)
        {
            Value = value;
            ValidateModel();
        }
    }
}