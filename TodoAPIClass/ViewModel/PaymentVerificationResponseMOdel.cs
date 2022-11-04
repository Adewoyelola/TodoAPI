namespace TodoAPIClass.ViewModel
{
    public class PaymentVerificationResponseMOdel
    {
        public string status { get; set; }
        public string message { get; set; }

        public VerifyData data { get; set; }

    }
    public class VerifyData
    {
        public long id { get; set; }
        public long amount { get; set; }
        // public string status { get; set; }
    }
}
