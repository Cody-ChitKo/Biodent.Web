using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class NotiModel
    {
        public int NotiID { get; set; }
        public int UsersID { get; set; }
        public DateTime NotiDate { get; set; }
        public string NotiTitleEng { get; set; }
        public string NotiTitleMyan { get; set; }
        public int NotiType { get; set; }
        public string NotiMsgEng { get; set; }
        public string NotiMsgMyan { get; set; }
        public bool IsSeen { get; set; }
        public bool IsActive { get; set; }
    }
}
