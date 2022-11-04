namespace TodoAPIClass.ViewModel
{
    public class PaymentRequestModel
    {
        public string tx_ref { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; } = "NGN";
        public string redirect_url { get; set; }
        public Customer customer { get; set; }
    }

    public class Customer
    {
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string name { get; set; }
    }
}
