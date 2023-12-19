using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class PayMethodModel
    {
        public int PayMethodId { get; set; }
        public string PayMethodName { get; set; }
        public bool IsActive { get; set; }
    }
}
