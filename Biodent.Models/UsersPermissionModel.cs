using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class UsersPermissionModel
    {
        public int PermissionId { get; set; }
        public int UsersID { get; set; }
        public string UsersName { get; set; }
        public string Control_Id { get; set; }
        public string Control_URL { get; set; }
        public bool Full_Access { get; set; }
        public bool List_Access { get; set; }
        public bool Create_Access { get; set; }
        public bool Edit_Access { get; set; }
        public bool Delete_Access { get; set; }
        public bool Approve_Access { get; set; }
        public List<ControlModel> Controls { get; set; }
        public virtual UsersModel Users { get; set; }

    }
}
