using System;
using System.Threading.Tasks;
using webapi.src.Payment.Domain;

namespace webapi.src.Payment.Infrastructure
{
    public class PaymentRepository : IPaymentRepository
    {   
        private readonly PaymentDbContext _context;
        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrUpdateAsync(Domain.Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Payment> FindAsync(Guid paymentId)
        {
            return await _context.FindAsync<Domain.Payment>(paymentId);
        }
    }
}