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
    public class WalletDAL:DataControllerBase
    {
        WalletQuery query;
        public WalletDAL()
        {
            query = new WalletQuery();
        }

        public void Save(WalletModel wallte)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("UsersId", wallte.UsersId);
            cmd.Parameters.AddWithValue("WalletAmount", wallte.WalletAmount);
            cmd.Parameters.AddWithValue("AddDate", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("PayMethodId", wallte.PayMethodId);
            cmd.Parameters.AddWithValue("ss_Image", wallte.ss_ImageUrl);

            SaveChangeCommit();
        }
        public void WalletApprove(int walletid, int UsersId, decimal WalletAmount, int ApproveUserId)
        {
            cmd = new MySqlCommand(query.WalletApprove(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("WalletId", walletid);
            cmd.Parameters.AddWithValue("UsersId", UsersId);
            cmd.Parameters.AddWithValue("WalletAmount", WalletAmount);
            cmd.Parameters.AddWithValue("ApproveUserId", ApproveUserId);
            cmd.Parameters.AddWithValue("ApproveDate", DateTime.Now.Date);
            SaveChangeCommit();
        }

        //for customer
        public List<WalletModel> GetWalletByUsersID(int UsersId)
        {
            List<WalletModel> wallets = new List<WalletModel>();

            cmd = new MySqlCommand(query.Select(UsersId), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersId", UsersId);
            WalletModel wallet;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    wallet = new WalletModel();

                    wallet.WalletId = Convert.ToInt32(rdr["WalletId"]);
                    wallet.UsersId = Convert.ToInt32(rdr["UsersId"]);

                    wallet.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    wallet.AddDate = Convert.ToDateTime(rdr["AddDate"]);
                    wallet.ApproveUserId = Convert.ToInt32(rdr["ApproveUserId"]);
                    wallet.UsersName = rdr["UsersName"].ToString();
                    wallet.PayMethodName = rdr["PayMethodName"].ToString();
                    wallet.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    wallet.Status = rdr["Status"].ToString();
                    wallet.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    wallets.Add(wallet);
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
            return wallets;
        }

        //for admin
        public List<WalletModel> GetNewWallet()
        {
            List<WalletModel> wallets = new List<WalletModel>();

            cmd = new MySqlCommand(query.SelectNewWallet(), con);
            cmd.CommandType = CommandType.Text;
            WalletModel wallet;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    wallet = new WalletModel();

                    wallet.WalletId = Convert.ToInt32(rdr["WalletId"]);
                    wallet.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    wallet.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    wallet.AddDate = Convert.ToDateTime(rdr["AddDate"]);
                    wallet.ApproveUserId = Convert.ToInt32(rdr["ApproveUserId"]);
                    wallet.UsersName = rdr["UsersName"].ToString();
                    wallet.PayMethodName = rdr["PayMethodName"].ToString();
                    //wallet.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    wallet.Status = rdr["Status"].ToString();
                    wallet.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    wallets.Add(wallet);
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
            return wallets;
        }
        //for admin
        public List<WalletModel> GetApproveWallet()
        {
            List<WalletModel> wallets = new List<WalletModel>();

            cmd = new MySqlCommand(query.SelectApproveWallet(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            WalletModel wallet;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    wallet = new WalletModel();

                    wallet.WalletId = Convert.ToInt32(rdr["WalletId"]);
                    wallet.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    wallet.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    wallet.AddDate = Convert.ToDateTime(rdr["AddDate"]);
                    wallet.ApproveUserId = Convert.ToInt32(rdr["ApproveUserId"]);
                    wallet.UsersName = rdr["UsersName"].ToString();
                    wallet.PayMethodName = rdr["PayMethodName"].ToString();
                    wallet.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    wallet.Status = rdr["Status"].ToString();
                    wallet.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    wallets.Add(wallet);
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
            return wallets;
        }

        public WalletModel GetWalletById(int walletid)
        {
            cmd = new MySqlCommand(query.SelectByWalletId(), con);
            cmd.CommandType = CommandType.Text;
            WalletModel wallet = new WalletModel();
            try
            {
                cmd.Parameters.AddWithValue("WalletId", walletid);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    wallet.WalletId = Convert.ToInt32(rdr["WalletId"]);
                    wallet.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    wallet.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    wallet.AddDate = Convert.ToDateTime(rdr["AddDate"]);
                    //wallet.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    wallet.Status = rdr["Status"].ToString();
                    wallet.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return wallet;
        }

    }
}
