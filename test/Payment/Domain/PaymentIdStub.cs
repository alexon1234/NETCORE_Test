using System;
using webapi.src.Payment.Domain.ValueObject;

namespace webapi.test.Payment.Domain
{
    public static class PaymentIdStub
    {
        public static PaymentId Random()
        {
            return new PaymentId(Guid.NewGuid());
        }
    }
}