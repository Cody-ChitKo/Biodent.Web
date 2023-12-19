using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class InvoiceDetailModel
    {
        public int InvDetailID { get; set; }
        public string InvoiceID { get; set; }
        public string ToothNo { get; set; }
        public int Qty { get; set; }
        public string ProthesisID { get; set; }
        public string ProthesisName { get; set; }
        public string SubProID { get; set; }
        public string SubProthesisName { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string CaseType { get; set; }
        public bool IsActive { get; set; }
    }
}
