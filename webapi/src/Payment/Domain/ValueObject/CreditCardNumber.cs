using System.ComponentModel.DataAnnotations;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain.ValueObject
{
    public class CreditCardNumber: BaseValueObject
    {
        [Required(ErrorMessage = "Credit Card is required")]
        [CreditCardAttribute(ErrorMessage = "Credit Card not have the format needed")]
        public string Value { get; private set; }

        protected CreditCardNumber() {}        

        public CreditCardNumber(string value)
        {
            Value = value;
            ValidateModel();
        }

    }
}