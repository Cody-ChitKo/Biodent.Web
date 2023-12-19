using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class UsersPermissionQuery
    {
        private string query = "";

        public string Select(int UsersId)
        {
            if (UsersId == 0)
            {
                query = "SELECT up.*, UsersName, Control_URL FROM tbl_userpermission up ";
                query += " INNER JOIN tbl_users ON tbl_users.UsersID = up.UsersID";
                query += " INNER JOIN tbl_control ctr ON ctr.Control_Id = up.Control_Id";
            }
            else
            {
                query = "SELECT up.*, UsersName, Control_URL FROM tbl_userpermission up ";
                query += " INNER JOIN tbl_users ON tbl_users.UsersID = up.UsersID";
                query += " INNER JOIN tbl_control ctr ON ctr.Control_Id = up.Control_Id";
                query += " WHERE up.UsersID = @UsersID";
            }
           
            return query;
        }
        public string SelectPermission()
        {
            query = "SELECT col.Control_Id, col.Control_Name, col.Control_URL, up.PermissionId, UsersID, up.Control_Id, Full_Access, ";
            query += " List_Access, Create_Access, Edit_Access, Delete_Access, Approve_Access FROM tbl_control col ";
            query += "LEFT OUTER JOIN tbl_userpermission up ON col.Control_Id = up.Control_Id;";
            return query;
        }
        public string SelectPermissionByControlId()
        {
            query = "SELECT up.*, Control_Name FROM tbl_userpermission up ";
            query += " INNER JOIN tbl_control ON tbl_control.Control_Id = up.Control_Id ";
            query += "WHERE tbl_control.IsActive = 1 AND up.Control_Id = @Control_Id AND up.UsersID = @UsersID;";
            return query;
        }
    }
}
