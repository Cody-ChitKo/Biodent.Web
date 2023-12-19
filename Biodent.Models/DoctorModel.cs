using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class DoctorModel
    {
        public int DoctorID { get; set; }
        public int UsersID { get; set; }
        public string UsersName { get; set; }
        public string DoctorName { get; set; }
        public bool IsActive { get; set; }
    }
}
