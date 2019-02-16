using System.Threading.Tasks;

namespace webapi.src.Payment.Domain
{
    public interface IPaymentGateway
    {
        Task<bool> ProcessPayment(Payment payment);
    }
}