using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class SubProthesisQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_subprothesis (SubProID, ProthesisID, SubProthesisName, SalePrice, IsActive)";
            query += " VALUES(@SubProID, @ProthesisID, @SubProthesisName, @SalePrice, 1)";
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
        public string Select(string SubProID)
        {
            if (string.IsNullOrEmpty(SubProID))
            {
                query = "SELECT sub.*, ProthesisName FROM tbl_subprothesis sub";
                query += " INNER JOIN tbl_prothesis pro ON pro.ProthesisID = sub.ProthesisID";
                query += " WHERE sub.IsActive = 1";
            }
            else
            {
                query = "SELECT sub.*, ProthesisName FROM tbl_subprothesis sub";
                query += " INNER JOIN tbl_prothesis pro ON pro.ProthesisID = sub.ProthesisID";
                query += " WHERE sub.IsActive = 1 AND SubProID = @SubProID";
            }
            return query;
        }
        public string SelectToothPrice()
        {
            query = "SELECT ProthesisName, SubProthesisName, SalePrice FROM tbl_subprothesis S";
            query += " INNER JOIN tbl_Prothesis P on P.ProthesisID = S.ProthesisID";
            query += " WHERE S.IsActive = 1 AND P.IsActive = 1";
            return query;
        }
        public string SelectByProthesisID()
        {
            query = "SELECT s.*, p.ProthesisName FROM tbl_subprothesis s ";
            query += " INNER JOIN tbl_Prothesis p ON p.ProthesisID = s.ProthesisID";
            query += " WHERE s.IsActive = 1 AND s.ProthesisID = @ProthesisID";
            return query;
        }
    }
}
