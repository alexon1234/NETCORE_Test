using System;
using webapi.src.Payment.Domain.ValueObject;

namespace webapi.test.Payment.Domain
{
    public static class SecurityCodeStub
    {
        public static SecurityCode Random() 
        {
            int value = new Random().Next(1000);
            string digit = value.ToString("000");
            return new SecurityCode(digit);

        }       
    }
}