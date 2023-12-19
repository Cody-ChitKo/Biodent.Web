using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class BalanceOrderModel
    {
        public List<CompleteViewModel> completeViewModels { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}
