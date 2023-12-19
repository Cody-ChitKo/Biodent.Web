using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class DashBoardModel
    {
        public int NewCustomer { get; set; }
        public int TotalCustomer { get; set; }
        public int NewOrder { get; set; }
        public int ProcessOrder { get; set; }
        public int PendingOrder { get; set; }
        public int DeliveringOrder { get; set; }
        public int ToDeliveryOrder { get; set; }
        public int CloseOrder { get; set; }
        public int BalanceOrder { get; set; }
        public int TotalProcessingOrderItem { get; set; }
    }
}
