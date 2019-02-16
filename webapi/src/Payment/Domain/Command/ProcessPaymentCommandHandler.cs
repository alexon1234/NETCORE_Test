using System.Threading;
using System.Threading.Tasks;
using MediatR;
using webapi.src.Payment.Application;
using webapi.src.Payment.Domain.ValueObject;
using webapi.src.Shared.Domain.Command;
using webapi.src.Shared.Domain.Event;

namespace webapi.src.Payment.Domain.Command
{
    public class ProcessPaymentCommandHandler : ICommandHandler<ProcessPaymentCommand>
    {
        private readonly PaymentCreator _paymentcreator;

        public ProcessPaymentCommandHandler(IPaymentRepository paymentRepository, IEventBus eventBus)
        {
            _paymentcreator = new PaymentCreator(paymentRepository, eventBus);
        }

        protected override async Task Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            
            await _paymentcreator.Create(
                new PaymentId(request.PaymentId),
                new CreditCardNumber(request.CreditCardNumber),
                new CardHolder(request.CardHolder),
                new ExpirationDate(request.ExpirationDate),
                new Ammount(request.Ammount),
                new SecurityCode(request.SecurityCode)
            );
        }
    }
}