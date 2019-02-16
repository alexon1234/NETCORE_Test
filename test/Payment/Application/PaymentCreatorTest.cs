using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using webapi.src.Payment.Application;
using webapi.src.Payment.Domain;
using webapi.src.Payment.Domain.ValueObject;
using webapi.src.Shared.Infrastructure;
using webapi.test.Payment.Domain;
using Xunit;

namespace test.Payment.Application
{
    public class PaymentCreatorTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IPaymentRepository> _paymentRepository;
        private readonly PaymentCreator _paymentCreator;

        public PaymentCreatorTest()
        {
            _paymentRepository = new Mock<IPaymentRepository>();
            _mediator = new Mock<IMediator>();
            _paymentRepository
                            .Setup(u => u.CreateOrUpdateAsync(It.IsAny<webapi.src.Payment.Domain.Payment>()))
                            .Returns(Task.CompletedTask);

            var eventBus = new EventBus(_mediator.Object);
            _paymentCreator = new PaymentCreator(_paymentRepository.Object, eventBus);
        }

        [Fact]
        public async void Event_have_been_publish_after_payment_created()
        {
            var paymentId = PaymentIdStub.Random();
            var creditCardNumber = CreditCardNumberStub.Random();
            var cardHolder = CardHolderStub.Random();
            var expirationDate = ExpirationDateStub.Random();
            var ammount = AmmountStub.Random();
            var securityCode = SecurityCodeStub.Random();

            await _paymentCreator
                        .Create(
                            paymentId, 
                            creditCardNumber,
                            cardHolder,
                            expirationDate,
                            ammount,
                            securityCode
                        );

            _mediator.Verify(m =>
                        m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>())
                        , Times.Once);
        }
    }
}
