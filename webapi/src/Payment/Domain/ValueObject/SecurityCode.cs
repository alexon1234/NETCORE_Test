using System.ComponentModel.DataAnnotations;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain.ValueObject
{
    public class SecurityCode: BaseValueObject
    {
        [StringLength(3, MinimumLength = 3)]
        public string Value { get; protected set; }

        protected SecurityCode() {}

        public SecurityCode(string value)
        {
            Value = value;
            ValidateModel();
        }
    }
}