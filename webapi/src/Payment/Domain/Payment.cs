using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.src.Payment.Domain.Event;
using webapi.src.Payment.Domain.ValueObject;
using webapi.src.Shared.Domain.AggregateRoot;
using webapi.src.Shared.Domain.ValueObject;

namespace webapi.src.Payment.Domain
{
    public class Payment: AggregateRoot
    {
        public PaymentId Id { get; protected set; }
        public CreditCardNumber CreditCardNumber { get; protected set; }
        public CardHolder CardHolder { get; protected set; }
        public ExpirationDate ExpirationDate { get; protected set;}
        public Ammount Ammount { get; protected set;}
        public SecurityCode SecurityCode { get; protected set; }
        public PaymentStatus PaymentStatus { get; protected set; }

        [NotMapped]
        public bool IsPaymentSuccessful { get => PaymentStatus == PaymentStatus.Processed; }

        protected Payment() {}

        protected Payment(
            PaymentId id,
            CreditCardNumber creditCardNumber, 
            CardHolder cardHolder, 
            ExpirationDate expirationDate, 
            Ammount ammount, 
            SecurityCode securityCode, 
            PaymentStatus paymentStatus)
        {
            Id = id;
            CreditCardNumber = creditCardNumber;
            CardHolder = cardHolder;
            ExpirationDate = expirationDate;
            Ammount = ammount;
            SecurityCode = securityCode;
            PaymentStatus = paymentStatus;
        }

        public static Payment CreatePayment( 
            PaymentId id,            
            CreditCardNumber creditCardNumber, 
            CardHolder cardHolder, 
            ExpirationDate expirationDate, 
            Ammount ammount, 
            SecurityCode securityCode)
        {
            var payment = new Payment(
                id, 
                creditCardNumber, 
                cardHolder, 
                expirationDate, 
                ammount, 
                securityCode, 
                PaymentStatus.Pending);
                
            payment.Record(
                new PaymentCreatedEvent(
                    id.Value, 
                    creditCardNumber.Value,
                    cardHolder.Value, 
                    expirationDate.Value, 
                    ammount.Value, 
                    securityCode.Value, 
                    PaymentStatus.Pending));

            return payment;
        }

        public void PaymentProcessed()
        {
            PaymentStatus = PaymentStatus.Processed;
            Record(new PaymentProcessed(Id.Value));
            
        }

        public void PaymentFailed()
        {
            PaymentStatus = PaymentStatus.Failed;
            Record(new PaymentFailed(Id.Value));
        }

    }



}