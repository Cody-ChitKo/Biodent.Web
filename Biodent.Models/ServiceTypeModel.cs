using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class ServiceTypeModel
    {
        public int ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        public bool IsActive { get; set; }
    }
}
