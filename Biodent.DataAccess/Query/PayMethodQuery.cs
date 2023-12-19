using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class PayMethodQuery
    {
        private string query;
        public string Insert()
        {
            query = "INSERT tbl_paymethod(PayMethodName, IsActive) VALUES(@PayMethodName,1)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_paymethod SET PayMethodName = @PayMethodName WHERE PayMethodId = @PayMethodId";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_paymethod SET IsActive = 0 WHERE PayMethodId = @PayMethodId";
            return query;
        }
        public string Select(int PaymethodId)
        {
            if (PaymethodId == 0)
            {
                query = "SELECT * FROM tbl_paymethod WHERE IsActive = 1";
            }
            else
            {
                query = "SELECT * FROM tbl_paymethod WHERE IsActive = 1 AND PaymethodId = @PaymethodId";
            }
            
            return query;
        }
    }
}
