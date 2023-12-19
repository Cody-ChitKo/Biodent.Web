using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class ServiceTypeQuery
    {
        private string query = "";
        public string Insert()
        {
            return query = "INSERT tbl_servicetype(ServiceType, IsActive) VALUES(@ServiceType,1)";
        }
        public string Update()
        {
            return query = "UPDATE tbl_servicetype SET ServiceType = @ServiceType WHERE ServiceTypeId= @ServiceTypeId";
        }
        public string Delete()
        {
            return query = "UPDATE tbl_servicetype SET IsActive = 0 WHERE ServiceTypeId= @ServiceTypeId";
        }
        public string Select(int id)
        {
            if (id == 0)
            {
                query = "SELECT * FROM tbl_servicetype WHERE IsActive = 1";
            }
            else
            {
                query = "SELECT * FROM tbl_servicetype WHERE IsActive = 1 AND ServiceTypeId = @ServiceTypeId";
            }
            return query;
        }
    }
}
