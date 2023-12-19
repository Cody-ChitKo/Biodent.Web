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
    public class SubProthesisDAL:DataControllerBase
    {
        SubProthesisQuery query;
        public SubProthesisDAL()
        {
            query = new SubProthesisQuery();
        }
        public void Insert(SubProthesisModel subpro)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("SubProID", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("ProthesisID", subpro.ProthesisID);
            cmd.Parameters.AddWithValue("SubProthesisName", subpro.SubProthesisName);
            cmd.Parameters.AddWithValue("SalePrice", subpro.SalePrice);

            SaveChangeCommit();
        }
        public void Update(SubProthesisModel subpro)
        {
            cmd = new MySqlCommand("SubprothesisUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("SubProID", subpro.SubProID);
            cmd.Parameters.AddWithValue("ProthesisID", subpro.ProthesisID);
            cmd.Parameters.AddWithValue("SubProthesisName", subpro.SubProthesisName);
            cmd.Parameters.AddWithValue("SalePrice", subpro.SalePrice);

            SaveChangeCommit();
        }
        public void Delete(string ID)
        {
            cmd = new MySqlCommand("SubprothesisDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("SubProID", ID);

            SaveChangeCommit();
        }
        public List<SubProthesisModel> Select()
        {
            List<SubProthesisModel> SubProList = new List<SubProthesisModel>();

            cmd = new MySqlCommand(query.Select(""), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                SubProthesisModel subprothesis;
                while (rdr.Read())
                {
                    subprothesis = new SubProthesisModel();
                    subprothesis.ProthesisID = rdr["ProthesisID"].ToString();
                    subprothesis.ProthesisName = rdr["ProthesisName"].ToString();
                    subprothesis.SubProID = rdr["SubProID"].ToString();
                    subprothesis.SubProthesisName = rdr["SubProthesisName"].ToString();
                    subprothesis.SalePrice = Convert.ToDecimal(rdr["SalePrice"]);
                    subprothesis.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    SubProList.Add(subprothesis);
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
            return SubProList;
        }
        public SubProthesisModel SelectByID(string SubProID)
        {
            SubProthesisModel subprothesis = new SubProthesisModel();
            cmd = new MySqlCommand(query.Select(SubProID), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("SubProID", SubProID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    subprothesis.ProthesisID = rdr["ProthesisID"].ToString();
                    subprothesis.ProthesisName = rdr["ProthesisName"].ToString();
                    subprothesis.SubProID = rdr["SubProID"].ToString();
                    subprothesis.SubProthesisName = rdr["SubProthesisName"].ToString();
                    subprothesis.SalePrice = Convert.ToDecimal(rdr["SalePrice"]);
                    subprothesis.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return subprothesis;
        }

        public List<SubProthesisModel> SelectByProthesisID(string ProthesisID)
        {
            List<SubProthesisModel> SubProList = new List<SubProthesisModel>();

            cmd = new MySqlCommand(query.SelectByProthesisID(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ProthesisID", ProthesisID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                SubProthesisModel subprothesis;
                while (rdr.Read())
                {
                    subprothesis = new SubProthesisModel();
                    subprothesis.ProthesisID = rdr["ProthesisID"].ToString();
                    subprothesis.ProthesisName = rdr["ProthesisName"].ToString();
                    subprothesis.SubProID = rdr["SubProID"].ToString();
                    subprothesis.SubProthesisName = rdr["SubProthesisName"].ToString();
                    subprothesis.SalePrice = Convert.ToDecimal(rdr["SalePrice"]);
                    subprothesis.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    SubProList.Add(subprothesis);
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
            return SubProList;
        }

        public List<SubProthesisModel> SelectToothPrice()
        {
            List<SubProthesisModel> SubProList = new List<SubProthesisModel>();

            cmd = new MySqlCommand(query.SelectToothPrice(), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                SubProthesisModel subprothesis;
                while (rdr.Read())
                {
                    subprothesis = new SubProthesisModel();
                    subprothesis.ProthesisName = rdr["ProthesisName"].ToString();
                    subprothesis.SubProthesisName = rdr["SubProthesisName"].ToString();
                    subprothesis.SalePrice = Convert.ToDecimal(rdr["SalePrice"]);
                    SubProList.Add(subprothesis);
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
            return SubProList;
        }
    }
}
