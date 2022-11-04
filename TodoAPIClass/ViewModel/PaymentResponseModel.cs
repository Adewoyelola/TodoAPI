namespace TodoAPIClass.ViewModel
{
    public class PaymentResponseModel
    {
        public string message { get; set; }
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string link { get; set; }
    }
}
