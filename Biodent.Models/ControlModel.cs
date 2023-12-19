using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class ControlModel
    {
        public string Control_Id { get; set; }
        public string Control_Name { get; set; }
        public string Control_URL { get; set; }
        public bool IsActive { get; set; }
    }
}
