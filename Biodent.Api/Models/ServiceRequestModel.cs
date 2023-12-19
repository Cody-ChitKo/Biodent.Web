namespace Biodent.Api.Models
{
    public class ServiceRequestModel
    {
        public string ServiceHeader { get; set; }
        public decimal ServicePrice { get; set; }
        public int? UsersId { get; set; }
        public string InvNo { get; set; }
    }
}
