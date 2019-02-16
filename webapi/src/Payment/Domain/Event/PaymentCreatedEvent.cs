using System;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Payment.Domain.Event
{
    public class PaymentCreatedEvent: IEvent
    {
        public Guid PaymentId { get; }
        public string CreditCardNumber { get; }
        public string CardHolder { get; }
        public DateTime ExpirationDate { get; }
        public decimal Ammount { get; }
        public string SecurityCode { get; }
        public PaymentStatus PaymentStatus { get; }

        public PaymentCreatedEvent(
            Guid id,
            string creditCardNumber, 
            string cardHolder, 
            DateTime expirationDate, 
            decimal ammount, 
            string securityCode, 
            PaymentStatus paymentStatus
        )
        {
            PaymentId = id;
            CreditCardNumber = creditCardNumber;
            CardHolder = cardHolder;
            ExpirationDate = expirationDate;
            Ammount = ammount;
            SecurityCode = securityCode;
            PaymentStatus = paymentStatus;
        }
    }
}