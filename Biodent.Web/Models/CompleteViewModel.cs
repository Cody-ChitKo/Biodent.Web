namespace Biodent.Web.Models
{
    public class CompleteViewModel
    {
        public string? InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DeliverdDate { get; set; }
        public string? Usersname { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public string? ToothNo { get; set; }
        public string? ProthesisName { get; set; }
        public string? SubProthesisName { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public string? ReviewDesp { get; set; }
        public int Rating { get; set; }
    }
}
