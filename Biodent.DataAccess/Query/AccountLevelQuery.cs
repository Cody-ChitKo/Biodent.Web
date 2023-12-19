using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class AccountLevelQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_accountlevel(AccountLevel, IsActive) VALUES(@AccountLevel, 1)";
            return query;
        }
        public string Select()
        {
            query = "SELECT * FROM tbl_accountlevel WHERE IsActive = 1 ";
            return query;
        }
    }
}
