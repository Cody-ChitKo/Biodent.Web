namespace Biodent.Api.Models
{
    public class PaymentRequestModel
    {
        public string PayNo { get; set; }
        public DateTime PayDate { get; set; }
        public decimal PayAmount { get; set; }
        public decimal PKAmount { get;set; }
        public string? Remark { get; set; }
        public int UsersID { get; set; }
    }
}
