using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class SubProthesisModel
    {
        public string SubProID { get; set; }
        public string ProthesisID { get; set; }
        public string ProthesisName { get; set; }
        public string SubProthesisName { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }

        public List<ProthesisModel> Protheses { get; set; }
    }
}
