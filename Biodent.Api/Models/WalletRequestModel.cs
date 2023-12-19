namespace Biodent.Api.Models
{
    public class WalletRequestModel
    {
        public int UsersID { get;set; }
        public int PayMethodId { get;set; }
        public decimal WalletAmount { get; set; }
        public string Remark { get;set; }
        public string ss_Image { get; set; }

    }
}
