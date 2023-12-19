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
    public class ServiceTypeDAL:DataControllerBase
    {
        ServiceTypeQuery query;
        public ServiceTypeDAL()
        {
            query = new ServiceTypeQuery();
        }

        public int Delete(int id)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ServiceTypeId", id);


            return SaveChangeCommit(0);
        }

        public int Insert(ServiceTypeModel serviceType)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ServiceType", serviceType.ServiceType);


            return SaveChangeCommit(0);
        }

        public int Update(ServiceTypeModel serviceType)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ServiceTypeId", serviceType.ServiceTypeId);
            cmd.Parameters.AddWithValue("ServiceType", serviceType.ServiceType);

            return SaveChangeCommit(0);
        }
        public List<ServiceTypeModel> Select(int id)
        {
            List<ServiceTypeModel> serviceTypes = new List<ServiceTypeModel>();

            cmd = new MySqlCommand(query.Select(id), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ServiceTypeId", id);

            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ServiceTypeModel servicetype = new ServiceTypeModel();
                    servicetype.ServiceTypeId = Convert.ToInt32(rdr["ServiceTypeId"]);
                    servicetype.ServiceType = rdr["ServiceType"].ToString();
                    servicetype.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    serviceTypes.Add(servicetype);
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
            return serviceTypes;
        }

        public ServiceTypeModel SelectByID(int id)
        {
            ServiceTypeModel servicetype = new ServiceTypeModel();

            cmd = new MySqlCommand(query.Select(id), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ServiceTypeId", id);

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    servicetype.ServiceTypeId = Convert.ToInt32(rdr["ServiceTypeId"]);
                    servicetype.ServiceType = rdr["ServiceType"].ToString();
                    servicetype.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return servicetype;
        }
    }
}
