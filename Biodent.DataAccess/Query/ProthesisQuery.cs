using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class ProthesisQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_prothesis (ProthesisID, DepartmentID, ProthesisName, IsActive)";
            query += " VALUES(@ProthesisID, @DepartmentID, @ProthesisName, 1)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_prothesis SET DepartmentID = @DepartmentID, ProthesisName = @ProthesisName ";
            query += "WHERE ProthesisID = @ProthesisID";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_Prothesis SET IsActive = 0 WHERE ProthesisID = @ProthesisID";
            return query;
        }
        public string Select(string ProthesisID)
        {
            if(string.IsNullOrEmpty(ProthesisID))
            {
                query = "SELECT ps.*, DepartmentName FROM tbl_prothesis ps";
                query += " INNER JOIN tbl_department dept ON dept.DepartmentID = ps.DepartmentID";
                query += " WHERE ps.IsActive = 1";
            }
            else
            {
                query = "SELECT ps.*, DepartmentName FROM tbl_prothesis ps ";
                query += "INNER JOIN tbl_department dept ON dept.DepartmentID = ps.DepartmentID ";
                query += "WHERE ps.IsActive = 1 AND ProthesisID = @ProthesisID";
            }
            return query;
        }
    }
}
