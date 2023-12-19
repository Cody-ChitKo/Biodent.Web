using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    internal class TransactionModel
    {
        public int TransactionId { get; set; }

        public string TranDesc { get; set; }
        public DateTime TranDate { get; set; }
        public int UsersId { get; set; }
    }
}
