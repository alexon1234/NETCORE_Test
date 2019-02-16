using System;
using webapi.src.Payment.Domain.ValueObject;

namespace webapi.test.Payment.Domain
{
    public class CreditCardNumberStub
    {
        public static CreditCardNumber Random()
        {
            Random rand = new Random(100);
            int ccc=  rand.Next(000000000, 999999999);
            return new CreditCardNumber(ccc.ToString());            
        }
    }
}