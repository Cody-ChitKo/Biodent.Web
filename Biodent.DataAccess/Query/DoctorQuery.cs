using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class DoctorQuery
    {
        private string query = string.Empty;
        public string Insert()
        {
            query = "INSERT tbl_doctor(UsersID, DoctorName, IsActive) ";
            query += " VALUES(@UsersID, @DoctorName, 1)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_doctor SET DoctorName = @DoctorName WHERE DoctorID = @DoctorID";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_doctor SET IsActive = 0 WHERE DoctorID = @DoctorID";
            return query;
        }
        public string Select(int userID)
        {
            if (userID == 0)
            {
                query = "SELECT tbl_doctor.*, UsersName FROM tbl_doctor";
                query += " INNER JOIN tbl_users us ON us.UsersID = tbl_doctor.UsersID";
                query += " WHERE tbl_doctor.IsActive=1";
            }
            else
            {
                query = "SELECT tbl_doctor.*, UsersName FROM tbl_doctor";
                query += " INNER JOIN tbl_users us ON us.UsersID = tbl_doctor.UsersID";
                query += " WHERE tbl_doctor.IsActive=1 AND us.UsersID = @UsersID";
            }
            
            return query;
        }
        public string SelectById()
        {
            query = "SELECT tbl_doctor.*, UsersName FROM tbl_doctor";
            query += " INNER JOIN tbl_users us ON us.UsersID = tbl_doctor.UsersID";
            query += " WHERE tbl_doctor.IsActive=1 AND tbl_doctor.DoctorID = @DoctorID";
            return query;
        }
    }
}
