using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class WalletModel
    {
        public int WalletId { get; set; }
        public int UsersId { get; set; }
        public string UsersName { get; set; }
        public decimal WalletAmount { get; set; }
        public DateTime AddDate { get; set; }
        public int ApproveUserId { get; set; }
        public string ApproveUserName { get; set; }
        public DateTime ApproveDate { get; set; }
        public int PayMethodId { get; set; }
        public string PayMethodName { get;set; }
        public IFormFile ss_Image { get; set; }
        public string ss_ImageUrl { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }

        public List<PayMethodModel> PayMethods { get; set; }
    }
}
