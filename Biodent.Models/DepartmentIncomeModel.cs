using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class DepartmentIncomeModel
    {
        public string DepartmentName { get; set; }
        public string SubProthesisName { get; set; }
        public int Qty { get; set; }
        public string CaseType { get; set; }
        public decimal NetAmount { get; set; }
    }
}
