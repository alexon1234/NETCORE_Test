using System;
using System.Threading.Tasks;
using webapi.src.Payment.Domain;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Payment.Application
{
    public class PaymentProcessor
    {
        private readonly IPaymentRepository _repository;
        private readonly IEventBus _eventBus;

        public PaymentProcessor(IPaymentRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<bool> ProcessPayment(Guid paymentId, IPaymentGateway paymentGateway)
        {
            var payment = await _repository.FindAsync(paymentId);
            if(await paymentGateway.ProcessPayment(payment)) 
            {
                payment.PaymentProcessedSuccessful();
            }
            else 
            {
                payment.PaymentProcessedFail();
            }
            await _repository.CreateOrUpdateAsync(payment);
            await _eventBus.Publish(payment.PullDomainEvents());
            
            return payment.IsPaymentSuccessful;

        }
    }
}