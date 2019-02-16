using System;
using webapi.src.Payment.Domain.ValueObject;

namespace webapi.test.Payment.Domain
{
    public class PaymentIdStub
    {
        public static PaymentId Random()
        {
            return new PaymentId(Guid.NewGuid());
        }
    }
}