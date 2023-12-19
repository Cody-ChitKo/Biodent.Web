using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class HistoryModel
    {
        public int TransactionId { get; set; }

        public string TranDesc { get; set; }
        public DateTime TranDate { get; set; }
        public int UsersId { get; set; }
    }
}
