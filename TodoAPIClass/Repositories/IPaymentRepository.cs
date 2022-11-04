using System.Threading.Tasks;
using TodoAPIClass.ViewModel;

namespace TodoAPIClass.Repositories
{
    public interface IPaymentRepository
    {
        //initiate payment
        Task<PaymentResponseModel> InitiatePayment(PaymentRequestModel model);
        //validate payment
        Task<PaymentVerificationResponseMOdel> VerifyPayment(string transactionId);

    }


}
