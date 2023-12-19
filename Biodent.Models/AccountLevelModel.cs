using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class AccountLevelModel
    {
        public int AccountLevel_Id { get; set; }

        public string AccountLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
