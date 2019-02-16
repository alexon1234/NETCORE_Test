using System;
using System.Threading.Tasks;
using webapi.src.Payment.Domain;
using webapi.src.Payment.Domain.ValueObject;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Payment.Application
{
    public class PaymentCreator
    {
        private readonly IPaymentRepository _repository;
        private readonly IEventBus _eventBus;

        public PaymentCreator(IPaymentRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task Create(           
            PaymentId id, 
            CreditCardNumber creditCardNumber, 
            CardHolder cardHolder, 
            ExpirationDate expirationDate, 
            Ammount ammount, 
            SecurityCode securityCode)
        {
            var payment = Domain.Payment.CreatePayment(
                    id, 
                    creditCardNumber, 
                    cardHolder, 
                    expirationDate,
                    ammount,
                    securityCode);

            await _repository.CreateOrUpdateAsync(payment);
            await _eventBus.Publish(payment.PullDomainEvents()); 
        }
            
    }
}