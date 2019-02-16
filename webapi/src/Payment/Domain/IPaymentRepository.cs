using System;
using System.Threading.Tasks;

namespace webapi.src.Payment.Domain
{
    public interface IPaymentRepository
    {
        Task CreateOrUpdateAsync(Payment payment);
        Task<Payment> FindAsync(Guid paymentId);
    }
}