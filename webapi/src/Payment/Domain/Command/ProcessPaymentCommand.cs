using System;
using System.ComponentModel.DataAnnotations;
using webapi.src.Shared.Domain.Command;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain.Command
{
    public class ProcessPaymentCommand: ICommand
    {
        public Guid PaymentId { get; }
        public string CreditCardNumber { get; }
        public string CardHolder { get; }
        public DateTime ExpirationDate { get; }
        public decimal Ammount { get; }
        public string SecurityCode { get; }


        public ProcessPaymentCommand(
            Guid paymentId,
            string creditCardNumber,
            string cardHolder,
            DateTime expirationDate,
            decimal ammount,
            string securityCode
        )
        {
            PaymentId = paymentId;
            CreditCardNumber = creditCardNumber;
            CardHolder = cardHolder;
            ExpirationDate = expirationDate;
            Ammount = ammount;
            SecurityCode = securityCode;
        }
    }
}