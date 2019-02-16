using System;
using System.ComponentModel.DataAnnotations;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain.ValueObject
{
    public class Ammount: BaseValueObject
    {
        
        [Required(ErrorMessage = "Ammount is required")]
        [Range(0, Double.MaxValue)]
        public decimal Value { get; private set; }

        protected Ammount() {}

        public Ammount(decimal value)
        {
            Value = value;
        }
    }
}