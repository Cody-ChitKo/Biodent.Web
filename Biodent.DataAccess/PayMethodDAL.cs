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
    public class PayMethodDAL:DataControllerBase
    {
        PayMethodQuery query;
        public PayMethodDAL()
        {
            query = new PayMethodQuery();
        }
        public void Insert(PayMethodModel payMethod)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("PayMethodName", payMethod.PayMethodName);
            SaveChangeCommit();
        }
        public void Update(PayMethodModel payMethod)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PayMethodId", payMethod.PayMethodId);
            cmd.Parameters.AddWithValue("PayMethodName", payMethod.PayMethodName);

            SaveChangeCommit();
        }
        public void Delete(int id)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PayMethodId", id);

            SaveChangeCommit();
        }
        public List<PayMethodModel> Select()
        {
            List<PayMethodModel> payMethods = new List<PayMethodModel>();

            cmd = new MySqlCommand(query.Select(0), con);
            cmd.CommandType = CommandType.Text;
            PayMethodModel paymethod;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    paymethod = new PayMethodModel();

                    paymethod.PayMethodId = (int)Convert.ToInt64(rdr["PayMethodId"]);
                    paymethod.PayMethodName = rdr["PayMethodName"].ToString();
                    paymethod.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    payMethods.Add(paymethod);
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
            return payMethods;
        }
        public PayMethodModel SelectById(int id)
        {
            PayMethodModel paymethod = new PayMethodModel();
            cmd = new MySqlCommand(query.Select(id), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("PayMethodId", id);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    paymethod.PayMethodId = (int)Convert.ToInt64(rdr["PayMethodId"]);
                    paymethod.PayMethodName = rdr["PayMethodName"].ToString();
                    paymethod.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return paymethod;
        }
    }
}
