using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class PaymentModel
    {
        public int PaymentID { get; set; }
        public string? InvoiceID { get; set; }
        public string? InvNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal Discount { get; set; }
        public string PayNo { get; set; }
        public DateTime PayDate { get; set; }
        public decimal PayAmount { get; set; }
        public string? Remark { get; set; }
        public int UsersID { get; set; }
        public string? UserName { get; set; }
        public bool IsActive { get; set; }
    }
}
