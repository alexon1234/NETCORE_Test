using System;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Payment.Domain.Event
{
    public class PaymentProcessed: IEvent
    {
        public Guid PaymentId { get; protected set; }

        public PaymentProcessed(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}