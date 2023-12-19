using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class ToothSimpleModel
    {
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public DateTime SaveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
