using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class ControlQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_control (Control_Id, Control_Name, Control_URL, IsActive) ";
            query += "VALUES(@Control_Id, @Control_Name, @Control_URL, 1)";
            return this.query;
        }
        public string Update()
        {
            query = "UPDATE tbl_control SET Control_Name = @Control_Name, ";
            query += "Control_URL = @Control_URL WHERE Control_Id = @Control_Id";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_control SET IsActive = 0 WHERE Control_Id = @Control_Id";
            return this.query;
        }
        public string Select()
        {
            query = "SELECT * FROM tbl_control WHERE IsActive = 1";
            return this.query;
        }
        public string SelectByName()
        {
            query = "SELECT * FROM tbl_control WHERE IsActive = 1 AND Control_Name = @Control_Name";
            return query;
        }
    }
}
