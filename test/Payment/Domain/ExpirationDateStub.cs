using System;
using AutoFixture;
using webapi.src.Payment.Domain.ValueObject;

namespace webapi.test.Payment.Domain
{
    public static class ExpirationDateStub
    {
        public static ExpirationDate Random()
        {
            var futureDateTime = DateTime.Now.AddMonths(new Random().Next(1, 100));
            return new ExpirationDate(futureDateTime);
        }
    }
}