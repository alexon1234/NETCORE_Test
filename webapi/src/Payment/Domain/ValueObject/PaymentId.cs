using System;
using System.ComponentModel.DataAnnotations;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain.ValueObject
{
    public class PaymentId: BaseValueObject
    {
        [Required(ErrorMessage = "Payment Id is required")]
        public Guid Value { get; private set; }

        protected PaymentId() {}

        public PaymentId(Guid value)
        {
            Value = value;
            ValidateModel();
        }
    }
}