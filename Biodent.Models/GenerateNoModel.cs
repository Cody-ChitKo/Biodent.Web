using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class GenerateNoModel
    {
        public int GenerateID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime GenerateDate { get; set; }
        public string FirstSymbol { get; set; }
        public int LastValue { get; set; }
        public string GenerateType { get; set; }
    }
}
