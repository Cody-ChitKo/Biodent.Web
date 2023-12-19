using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class UsersModel
    {
        public int UsersID { get; set; }
        public string? UsersName { get; set; }
        public string? RegionName { get;set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? UserLevel { get; set; }
        public int? AccountLevel_Id { get; set; }
        public string? UserSalt { get; set; }
        public string? VerificationCode { get; set; }
        public decimal? WalletAmount { get; set; }
        public decimal? PackageAmount { get; set; }
        public DateTime? PackageExpire { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? RegionID { get; set; }
        public string? Address { get; set; }
        public string Status { get; set; }
        public List<RegionModel> RegionList { get; set; }

    }
}
