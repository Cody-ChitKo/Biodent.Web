using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class DepartmentDAL:DataControllerBase
    {
        DepartmentQuery query;
        public DepartmentDAL() 
        {
            query = new DepartmentQuery();
        }
        public void Insert(DepartmentModel dept)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("DepartmentID", Guid.NewGuid());
            cmd.Parameters.AddWithValue("DepartmentName", dept.DepartmentName);
            cmd.Parameters.AddWithValue("CreatedDate", dept.CreatedDate);
            SaveChangeCommit();

        }
        public void Update(DepartmentModel dept)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("DepartmentID", dept.DepartmentID);
            cmd.Parameters.AddWithValue("DepartmentName", dept.DepartmentName);
            SaveChangeCommit();
        }
        public void Delete(string ID)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("DepartmentID", ID);
            SaveChangeCommit();
        }
        public List<DepartmentModel> Select()
        {
            List<DepartmentModel> deptList = new List<DepartmentModel>();

            cmd = new MySqlCommand(query.Select(""), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DepartmentModel dept = new DepartmentModel();

                    dept.DepartmentID = rdr["DepartmentID"].ToString();
                    dept.DepartmentName = rdr["DepartmentName"].ToString();
                    dept.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    dept.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    deptList.Add(dept);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return deptList;
        }
        public DepartmentModel SelectByID(string ID)
        {
            DepartmentModel dept = new DepartmentModel();
            cmd = new MySqlCommand(query.Select(ID), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("DepartmentID", ID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    dept.DepartmentID = rdr["DepartmentID"].ToString();
                    dept.DepartmentName = rdr["DepartmentName"].ToString();
                    dept.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    dept.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dept;
        }
    }
}
