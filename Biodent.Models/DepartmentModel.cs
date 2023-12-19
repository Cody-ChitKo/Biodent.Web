using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class DepartmentModel
    {
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
