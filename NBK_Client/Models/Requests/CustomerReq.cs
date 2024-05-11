namespace NBK_Client.Models.Requests
{
    public class CustomerReq
    {
        public string CustomerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

    }
    public class CustomerRes
    {
        public int CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

    }
}
