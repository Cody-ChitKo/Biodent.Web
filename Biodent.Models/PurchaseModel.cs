using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class PurchaseModel
    {
        public int PurchaseId { get; set; }
        public string PurchaseType { get; set; }
        public string? InvNo { get; set; }
        public string Pur_Desc { get; set; }
        public DateTime Pur_Date { get; set; }
        public int? UsersId { get; set; }
        public decimal Pur_Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
