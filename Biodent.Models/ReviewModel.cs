using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class ReviewModel
    {
        public int ReviewID { get; set; }
        public string? InvoiceID { get; set; }
        public string? InvNo { get; set; }
        public string? ReviewDesp { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public int UsersID { get; set; }
        public string? UsersName { get; set; }
        public bool IsActive { get; set; }
    }
}
