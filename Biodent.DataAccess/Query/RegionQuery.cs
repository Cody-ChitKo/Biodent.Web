using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class RegionQuery
    {
        private string query = string.Empty;
        public string Insert()
        {
            query = "INSERT tbl_region (RegionName, IsActive) VALUES(@RegionName, 1)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_region SET RegionName = @RegionName WHERE RegionId = @RegionId";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_region SET IsActive = 0 WHERE RegionId = @RegionId";
            return query;
        }
        public string Select(int id)
        {
            if (id == 0)
            {
                query = "SELECT * FROM tbl_region WHERE IsActive = 1";
            }
            else
            {
                query = "SELECT * FROM tbl_region WHERE IsActive = 1 AND RegionId = @RegionId";
            }
            
            return query;
        }
    }
}
