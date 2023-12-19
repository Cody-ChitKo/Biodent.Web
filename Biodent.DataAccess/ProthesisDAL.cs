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
    public class ProthesisDAL:DataControllerBase
    {
        ProthesisQuery query;
        public ProthesisDAL()
        {
            query = new ProthesisQuery();
        }
        public void Insert(ProthesisModel prothesis)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ProthesisID", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("DepartmentID", prothesis.DepartmentID);
            cmd.Parameters.AddWithValue("ProthesisName", prothesis.ProthesisName);

            SaveChangeCommit();
        }
        public void Edit(ProthesisModel prothesis)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ProthesisID", prothesis.ProthesisID);
            cmd.Parameters.AddWithValue("DepartmentID", prothesis.DepartmentID);
            cmd.Parameters.AddWithValue("ProthesisName", prothesis.ProthesisName);

            SaveChangeCommit();
        }
        public void Delete(string ID)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ProthesisID", ID);

            SaveChangeCommit();
        }
        public List<ProthesisModel> Select()
        {
            List<ProthesisModel> protheses = new List<ProthesisModel>();

            cmd = new MySqlCommand(query.Select(""), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                ProthesisModel prothesis;
                while (rdr.Read())
                {
                    prothesis = new ProthesisModel();
                    prothesis.ProthesisID = rdr["ProthesisID"].ToString();
                    prothesis.ProthesisName = rdr["ProthesisName"].ToString();
                    prothesis.DepartmentID = rdr["DepartmentID"].ToString();
                    prothesis.DepartmentName = rdr["DepartmentName"].ToString();
                    prothesis.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    protheses.Add(prothesis);
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
            return protheses;
        }
        public ProthesisModel SelectByID(string ProthesisID)
        {
            ProthesisModel prothesis = new ProthesisModel();
            cmd = new MySqlCommand(query.Select(ProthesisID), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ProthesisID", ProthesisID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prothesis.ProthesisID = rdr["ProthesisID"].ToString();
                    prothesis.ProthesisName = rdr["ProthesisName"].ToString();
                    prothesis.DepartmentID = rdr["DepartmentID"].ToString();
                    prothesis.DepartmentName = rdr["DepartmentName"].ToString();
                    prothesis.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return prothesis;
        }

    }
}
