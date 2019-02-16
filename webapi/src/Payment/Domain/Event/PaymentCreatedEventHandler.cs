using System.Threading;
using System.Threading.Tasks;
using webapi.src.Payment.Application;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Payment.Domain.Event
{
    public class PaymentCreatedEventHandler : IEventHandler<PaymentCreatedEvent>
    {
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly PaymentProcessor _paymentProcessor;

        public PaymentCreatedEventHandler(
            ICheapPaymentGateway cheapPaymentGateway, 
            IExpensivePaymentGateway expensivePaymentGateway,
            IPaymentRepository repository, 
            IEventBus eventBus)
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _paymentProcessor = new PaymentProcessor(repository, eventBus);
        }

        public async Task Handle(PaymentCreatedEvent notification, CancellationToken cancellationToken)
        {
            if(notification.Ammount < 20)
            {
                await _paymentProcessor.ProcessPayment(notification.PaymentId, _cheapPaymentGateway);
            }
            else 
            {
                var isProcessed = await _paymentProcessor.ProcessPayment(notification.PaymentId, _expensivePaymentGateway);
                if(!isProcessed) 
                {
                    await _paymentProcessor.ProcessPayment(notification.PaymentId, _cheapPaymentGateway);
                }
            }
        }
    }
}