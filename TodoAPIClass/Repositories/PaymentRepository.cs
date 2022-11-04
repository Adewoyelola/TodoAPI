using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoAPIClass.ViewModel;

namespace TodoAPIClass.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConfiguration _config;
        public PaymentRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<PaymentResponseModel> InitiatePayment(PaymentRequestModel model)
        {
            PaymentResponseModel deserialize;
            var key = _config.GetValue<string>("Flutterwave:SecretKey");
            var url = $"{_config.GetValue<string>("Flutterwave:url")}/payments";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");

            var data = JsonConvert.SerializeObject(model);
            var sendRequest = await client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json"));
            var response = await sendRequest.Content.ReadAsStringAsync();

            deserialize= JsonConvert.DeserializeObject<PaymentResponseModel>(response);

            return deserialize;

        }

        public async Task<PaymentVerificationResponseMOdel> VerifyPayment(string transactionId)
        {
            var key = _config.GetValue<string>("Flutterwave:SecretKey");
            var url = $"{_config.GetValue<string>("Flutterwave:url")}/transactions/{transactionId}/verify";

            PaymentVerificationResponseMOdel deserialize;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
            var sendRequest = await client.GetAsync(url);
            var response = await sendRequest.Content.ReadAsStringAsync();
            deserialize = JsonConvert.DeserializeObject<PaymentVerificationResponseMOdel>(response);

            return deserialize;


        }
    }
}
