using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class PurchaseQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_purchase(PurchaseType, InvNo, Pur_Desc, Pur_Date, UsersId, Pur_Amount, IsActive) ";
            query += " VALUES(@PurchaseType, @InvNo, @Pur_Desc, @Pur_Date, @UsersId, @Pur_Amount, 1)";
            return query;
        }
        public string Update()
        {
            return query;
        }
        public string Select(string PurchaseType)
        {
            if (string.IsNullOrEmpty(PurchaseType))
            {
                query = "SELECT * FROM tbl_purchase WHERE IsActive =1";
            }
            else
            {
                query = "SELECT * FROM tbl_purchase WHERE IsActive =1";
                query += " AND PurchaseType = @PurchaseType";
            }
            return query;
        }
        public string SelectByUserID()
        {
            query = "SELECT * FROM tbl_purchase WHERE IsActive =1";
            query += " AND UsersId = @UsersId";
            return query;
        }
    }
}
