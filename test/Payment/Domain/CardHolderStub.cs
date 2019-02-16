using webapi.src.Payment.Domain.ValueObject;
using AutoFixture;
using System.Net.Mail;

namespace webapi.test.Payment.Domain
{
    public class CardHolderStub
    {
        public static CardHolder Random()
        {
            var emailAddress = new Fixture().Create<MailAddress>().Address;
            return new CardHolder(emailAddress);
        }
    }
}