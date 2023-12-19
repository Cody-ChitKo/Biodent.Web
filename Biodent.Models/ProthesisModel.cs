using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class ProthesisModel
    {
        public string ProthesisID { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
       
        public string ProthesisName { get; set; }
        public bool IsActive { get; set; }
        public List<DepartmentModel> Departments { get; set; }
    }
}
