using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class TransactionQuery
    {
        private string query;
        public string Insert()
        {
            query = "INSERT tbl_transaction(TranDesc, TranDate, UsersId) VALUES(@TranDesc, @TranDate, @UsersId)";
            return query;
        }
        public string GetTransaction()
        {
            query = "SELECT * FROM tbl_transaction WHERE UsersId = @UsersId";
            return query;
        }
        public string GetAllTransaction()
        {

            return query;
        }
    }
}
