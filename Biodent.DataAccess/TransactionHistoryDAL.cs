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
    public class TransactionHistoryDAL:DataControllerBase
    {
        TransactionQuery query;
        public TransactionHistoryDAL()
        {
            query = new TransactionQuery();
        }
        public void Insert(HistoryModel history)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("UsersId", history.UsersId);
            cmd.Parameters.AddWithValue("TranDesc", history.TranDesc);
            cmd.Parameters.AddWithValue("TranDate", history.TranDate);
            SaveChangeCommit();
        }
        public List<HistoryModel> Select()
        {
            List<HistoryModel> histories = new List<HistoryModel>();
            cmd = new MySqlCommand(query.GetAllTransaction(), con);
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                HistoryModel history;
                while (rdr.Read())
                {
                    history = new HistoryModel();
                    history.TransactionId = Convert.ToInt32(rdr["TransactionId"]);
                    history.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    history.TranDesc = rdr["TranDesc"].ToString();
                    history.TranDate = Convert.ToDateTime(rdr["TranDate"]);
                    histories.Add(history);
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
            return histories;
        }
        
        public List<HistoryModel> SelectByUsersID(int UsersID)
        {
            List<HistoryModel> histories = new List<HistoryModel>();
            cmd = new MySqlCommand(query.GetTransaction(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersId", UsersID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                HistoryModel history;
                while (rdr.Read())
                {
                    history = new HistoryModel();
                    history.TransactionId = Convert.ToInt32(rdr["TransactionId"]);
                    history.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    history.TranDesc = rdr["TranDesc"].ToString();
                    history.TranDate = Convert.ToDateTime(rdr["TranDate"]);
                    histories.Add(history);
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
            return histories;
        }
    }
}
