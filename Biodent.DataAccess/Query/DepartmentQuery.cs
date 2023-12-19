using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class DepartmentQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_department(DepartmentID, DepartmentName, CreatedDate, IsActive)";
            query += " VALUES(@DepartmentID, @DepartmentName, @CreatedDate, 1)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_department SET DepartmentName = @DepartmentName WHERE DepartmentID = @DepartmentID";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_department SET IsActive = 0 WHERE DepartmentID = @DepartmentID";
            return query;
        }
        public string Select(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                query = "SELECT * FROM tbl_department WHERE IsActive = 1";
            }
            else
            {
                query = "SELECT * FROM tbl_department WHERE IsActive = 1 AND DepartmentID = @DepartmentID";
            }
            return query;
        }
    }
}
