using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class ServiceQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_services (ServiceTypeId, ServiceHeader, ServiceDescription, ServicePrice, ";
            query += "ServiceImage, IsActive)";
            query += " VALUES(@ServiceTypeId, @ServiceHeader, @ServiceDescription, @ServicePrice, @ServiceImage, 1)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_services SET ServiceTypeId = @ServiceTypeId, ServiceHeader = @ServiceHeader,";
            query += " ServiceDescription = @ServiceDescription, ServicePrice = @ServicePrice, ServiceImage = @ServiceImage";
            query += " WHERE ServiceId = @ServiceId";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_services SET IsActive = 0 WHERE ServiceId = @ServiceId";
            return query;
        }
        public string Select(int ServiceId)
        {
            if (ServiceId == 0)
            {
                query = "SELECT tbl_services.*, ServiceType FROM tbl_services ";
                query += "INNER JOIN tbl_servicetype ON tbl_servicetype.ServiceTypeId = tbl_services.ServiceTypeId ";
                query += " WHERE tbl_services.IsActive = 1";
            }
            else
            {
                query = "SELECT tbl_services.*, ServiceType FROM tbl_services ";
                query += "INNER JOIN tbl_servicetype ON tbl_servicetype.ServiceTypeId = tbl_services.ServiceTypeId ";
                query += " WHERE ServiceId = @ServiceId AND  tbl_services.IsActive = 1";
            }
            return query;
        }
    }
}
