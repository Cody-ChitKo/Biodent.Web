using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class ReviewQuery
    {
        private string query;
        public string Insert()
        {
            query = "INSERT tbl_review (InvoiceID, InvNo, ReviewDesp, Rating, ReviewDate, UsersID, IsActive) ";
            query += "VALUES(@InvoiceID, @InvNo, @ReviewDesp, @Rating, @ReviewDate, @UsersID , 1)";
            return query;
        }
        public string Select(int UsersID)
        {
            if(UsersID == 0)
            {
                query = "SELECT rv.*, UsersName FROM tbl_review rv";
                query += " INNER JOIN tbl_users ON tbl_users.UsersID = rv.UsersID";
                query += " WHERE rv.IsActive = 1";
            }
            else
            {
                query = "SELECT rv.*, UsersName FROM tbl_review rv";
                query += " INNER JOIN tbl_users ON tbl_users.UsersID = rv.UsersID";
                query += " WHERE rv.IsActive = 1 AND rv.UsersID = @UsersID";
            }
            
            return query;
        }
    }
}
