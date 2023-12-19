using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class PurchaseDAL:DataControllerBase
    {
        PurchaseQuery query;
        public PurchaseDAL()
        {
            query = new PurchaseQuery();
        }
        public string Insert(PurchaseModel purchase)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("UsersId", purchase.UsersId);
            cmd.Parameters.AddWithValue("InvNo", purchase.InvNo);
            cmd.Parameters.AddWithValue("PurchaseType", purchase.PurchaseType);
            cmd.Parameters.AddWithValue("Pur_Desc", purchase.Pur_Desc);
            cmd.Parameters.AddWithValue("Pur_Date", purchase.Pur_Date);
            cmd.Parameters.AddWithValue("Pur_Amount", purchase.Pur_Amount);
            SaveChangeCommit();
            return "Success";
        }
        public List<PurchaseModel> GetAll(string PurchaseType)
        {
            List<PurchaseModel> purchases = new List<PurchaseModel>();

            cmd = new MySqlCommand(query.Select(PurchaseType), con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("PurchaseType", PurchaseType);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                PurchaseModel purchase;
                while (rdr.Read())
                {
                    purchase = new PurchaseModel();
                    purchase.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    purchase.PurchaseId = Convert.ToInt32(rdr["PurchaseId"]);
                    purchase.PurchaseType = rdr["PurchaseType"].ToString();
                    purchase.Pur_Desc = rdr["Pur_Desc"].ToString();
                    purchase.InvNo = rdr["InvNo"].ToString();
                    purchase.Pur_Date = Convert.ToDateTime(rdr["Pur_Date"]);
                    purchase.Pur_Amount = Convert.ToDecimal(rdr["Pur_Amount"]);
                    purchase.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    purchases.Add(purchase);
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
            return purchases;
        }

        public List<PurchaseModel> GetByType(string PurchaseType)
        {
            List<PurchaseModel> purchases = new List<PurchaseModel>();

            cmd = new MySqlCommand(query.Select(PurchaseType), con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("PurchaseType", PurchaseType);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                PurchaseModel purchase;
                while (rdr.Read())
                {
                    purchase = new PurchaseModel();
                    purchase.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    purchase.PurchaseId = Convert.ToInt32(rdr["PurchaseId"]);
                    purchase.PurchaseType = rdr["PurchaseType"].ToString();
                    purchase.Pur_Desc = rdr["Pur_Desc"].ToString();
                    purchase.InvNo = rdr["InvNo"].ToString();
                    purchase.Pur_Date = Convert.ToDateTime(rdr["Pur_Date"]);
                    purchase.Pur_Amount = Convert.ToDecimal(rdr["Pur_Amount"]);
                    purchase.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    purchases.Add(purchase);
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
            return purchases;
        }
        public List<PurchaseModel> GetByUsersID(int usersid)
        {
            List<PurchaseModel> purchases = new List<PurchaseModel>();

            cmd = new MySqlCommand(query.SelectByUserID(), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.Parameters.AddWithValue("UsersId", usersid);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                PurchaseModel purchase;
                while (rdr.Read())
                {
                    purchase = new PurchaseModel();
                    purchase.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    purchase.PurchaseId = Convert.ToInt32(rdr["PurchaseId"]);
                    purchase.PurchaseType = rdr["PurchaseType"].ToString();
                    purchase.Pur_Desc = rdr["Pur_Desc"].ToString();
                    purchase.InvNo = rdr["InvNo"].ToString();
                    purchase.Pur_Date = Convert.ToDateTime(rdr["Pur_Date"]);
                    purchase.Pur_Amount = Convert.ToDecimal(rdr["Pur_Amount"]);
                    purchase.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    purchases.Add(purchase);
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
            return purchases;
        }

    }
}
