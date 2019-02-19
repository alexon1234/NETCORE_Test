using System;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Payment.Domain.Event
{
    public class PaymentFailed: IEvent
    {
        public Guid PaymentId { get; protected set; }

        public PaymentFailed(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}