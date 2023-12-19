using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class RegionDAL:DataControllerBase
    {
        RegionQuery query;
        public RegionDAL() 
        {
            query = new RegionQuery();
        }
        public void Add(RegionModel region)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("RegionName", region.RegionName);
            SaveChangeCommit();

        }
        public void Edit(RegionModel region)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("RegionId", region.RegionId);
            cmd.Parameters.AddWithValue("RegionName", region.RegionName);
            SaveChangeCommit();
        }
        public void Delete(int ID)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("RegionId", ID);
            SaveChangeCommit();
        }
        public List<RegionModel> Select()
        {
            List<RegionModel> regionList = new List<RegionModel>();

            cmd = new MySqlCommand(query.Select(0), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    RegionModel region = new RegionModel();
                    region.RegionId = Convert.ToInt32(rdr["RegionId"]);
                    region.RegionName = rdr["RegionName"].ToString();
                    region.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    regionList.Add(region);
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
            return regionList;
        }

        public RegionModel SelectById(int id)
        {
            cmd = new MySqlCommand(query.Select(id), con);
            cmd.CommandType = CommandType.Text;
            RegionModel region = new RegionModel();
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("RegionId", id);
                while (rdr.Read())
                {
                    region.RegionId = Convert.ToInt32(rdr["RegionId"]);
                    region.RegionName = rdr["RegionName"].ToString();
                    region.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return region;
        }
    }
}
