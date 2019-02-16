using System.ComponentModel.DataAnnotations;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain.ValueObject
{
    public class CardHolder: BaseValueObject
    {
        [EmailAddress(ErrorMessage = "Card Holder needs to be an email")]
        [Required(ErrorMessage = "Card Holder is needed")]
        public string Value { get; private set; }

        protected CardHolder() {}

        public CardHolder(string value)
        {
            Value = value;   
            ValidateModel(); 
        }
    }
}