using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class MessageQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_message (MessageText, TextDate, ToUsersId, FromUsersId)";
            query += " VALUES(@MessageText, @TextDate, @ToUsersId, @FromUsersId)";
            return query;
        }
        public string Update()
        {
            return query;
        }
        public string Delete()
        {
            return query;
        }
        public string Select(int usersId)
        {
            if(usersId == 0)
            {
                query = "SELECT tbl_message.*, UsersName FROM tbl_message ";
                query += "INNER JOIN tbl_Users ON tbl_Users.UsersID = tbl_message.ToUsersId";
            }
            else
            {
                query = "SELECT tbl_message.*, UsersName FROM tbl_message";
                query += " INNER JOIN tbl_users ON tbl_Users.UsersID = tbl_message.ToUsersId";
                query += " WHERE tbl_message.ToUsersId = @ToUsersId";
            }
            return query;
        }
    }
}
