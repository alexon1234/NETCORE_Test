using System;
using webapi.src.Payment.Domain.ValueObject;

namespace webapi.test.Payment.Domain
{
    public class AmmountStub
    {
        public static Ammount Random()
        {
            var positiveAmmount = new Random().Next(1, 10000);
            return new Ammount((decimal) positiveAmmount);
        }

        public static Ammount LessThan20()
        {
            var lessThan20 = new Random().Next(1, 20);
            return new Ammount((decimal) lessThan20);
        }

        public static Ammount BiggerThan20()
        {
            var biggerThan20 = new Random().Next(20, 500);
            return new Ammount((decimal) biggerThan20);
        }
    }
}