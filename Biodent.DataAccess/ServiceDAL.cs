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
    public class ServiceDAL:DataControllerBase
    {
        ServiceQuery query;
        public ServiceDAL()
        {
            query = new ServiceQuery();
        }

        public int Delete(int id)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ServiceId", id);

            return SaveChangeCommit(0);
        }

        public int Insert(ServiceModel service)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ServiceHeader", service.ServiceHeader);
            cmd.Parameters.AddWithValue("ServiceTypeId", service.ServiceTypeId);
            cmd.Parameters.AddWithValue("ServiceDescription", service.ServiceDescription);
            cmd.Parameters.AddWithValue("ServicePrice", service.ServicePrice);
            cmd.Parameters.AddWithValue("ServiceImage", service.ImagePathUrl);

            return SaveChangeCommit(0);
        }

        public int Update(ServiceModel service)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("ServiceId", service.ServiceId);
            cmd.Parameters.AddWithValue("ServiceHeader", service.ServiceHeader);
            cmd.Parameters.AddWithValue("ServiceTypeId", service.ServiceTypeId);
            cmd.Parameters.AddWithValue("ServiceDescription", service.ServiceDescription);
            cmd.Parameters.AddWithValue("ServicePrice", service.ServicePrice);
            cmd.Parameters.AddWithValue("ServiceImage", service.ServiceImage);

            return SaveChangeCommit(0);
        }
        public List<ServiceModel> Select(int id)
        {
            List<ServiceModel> services = new List<ServiceModel>();

            cmd = new MySqlCommand(query.Select(id), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ServiceId", id);
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ServiceModel service = new ServiceModel();

                    service.ServiceId = Convert.ToInt32(rdr["ServiceId"]);
                    service.ServiceTypeId = Convert.ToInt32(rdr["ServiceTypeId"]);
                    service.ServiceType = rdr["ServiceType"].ToString();
                    service.ServiceHeader = rdr["ServiceHeader"].ToString();
                    //service.ServiceImage = rdr["ServiceImage"].ToString();
                    //ImagePath
                    service.ImagePathUrl = rdr["ServiceImage"].ToString();
                    service.ServiceDescription = rdr["ServiceDescription"].ToString();
                    service.ServicePrice = Convert.ToDecimal(rdr["ServicePrice"]);
                    service.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    services.Add(service);
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
            return services;
        }

        public ServiceModel SelectByID(int id)
        {
            ServiceModel service = new ServiceModel();
            cmd = new MySqlCommand(query.Select(id), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("ServiceId", id);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    service.ServiceId = Convert.ToInt32(rdr["ServiceId"]);
                    service.ServiceTypeId = Convert.ToInt32(rdr["ServiceTypeId"]);
                    service.ServiceType = rdr["ServiceType"].ToString();
                    service.ServiceHeader = rdr["ServiceHeader"].ToString();
                    //service.ServiceImage = rdr["ServiceImage"].ToString();
                    //ImagePath
                    service.ImagePathUrl = rdr["ServiceImage"].ToString();
                    service.ServiceDescription = rdr["ServiceDescription"].ToString();
                    service.ServicePrice = Convert.ToDecimal(rdr["ServicePrice"]);
                    service.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return service;
        }
    }
}
