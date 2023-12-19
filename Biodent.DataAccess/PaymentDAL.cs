using Biodent.DataAccess.Common;
using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class PaymentDAL:DataControllerBase
    {
        PaymentQuery query;
        public PaymentDAL()
        {
            query = new PaymentQuery();
        }
        public void Insert(PaymentModel payment)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("InvoiceID", payment.InvoiceID);
            cmd.Parameters.AddWithValue("PayNo", payment.PayNo);
            cmd.Parameters.AddWithValue("PayDate", payment.PayDate);
            cmd.Parameters.AddWithValue("PayAmount", payment.PayAmount);
            cmd.Parameters.AddWithValue("Remark", payment.Remark);
            cmd.Parameters.AddWithValue("UsersID", payment.UsersID);

            SaveChangeCommit();
        }
        //for mobile
        public void InsertFromMobile(PaymentModel payment)
        {
            cmd = new MySqlCommand("PaymentInsert", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("InvoiceID", payment.InvoiceID);
            cmd.Parameters.AddWithValue("PayNo", payment.PayNo);
            cmd.Parameters.AddWithValue("PayDate", payment.PayDate);
            cmd.Parameters.AddWithValue("PayAmount", payment.PayAmount);
            cmd.Parameters.AddWithValue("Remark", payment.Remark);
            cmd.Parameters.AddWithValue("UsersID", payment.UsersID);
            SaveChangeCommit();
        }
        //for mobile
        public void OtherPayment(PaymentModel payment)
        {
            cmd = new MySqlCommand(query.OtherPayment(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PayNo", payment.PayNo);
            cmd.Parameters.AddWithValue("PayDate", payment.PayDate);
            cmd.Parameters.AddWithValue("PayAmount", payment.PayAmount);
            cmd.Parameters.AddWithValue("Remark", payment.Remark);
            cmd.Parameters.AddWithValue("UsersID", payment.UsersID);
            SaveChangeCommit();
        }

        public void UpdatePrizesForOpen(int UsersId, decimal PayAmount)
        {

            List<PrizesModel> prizesList = GetPrizeOfUser(UsersId);
            decimal pkamount = GetPKAmountOfPrize(UsersId).PKAmount;
            
            DataTable dt = GlobalFunction.ConvertToDataTable(prizesList);

            DataRow[] rows = dt.Select($"UsersId = {UsersId} AND UseAmount < {pkamount} AND IsActive = 1");

            decimal balancePAmount = PayAmount;
            if (rows.Length > 0)
            {
                if (balancePAmount < pkamount)
                {
                    //rows[0]["UseAmount"] = balancePAmount;
                    //balancePAmount = balancePAmount - PayAmount;

                    foreach (DataRow row in rows)
                    {
                        // Update the current row with the balancePAmount
                        row["UseAmount"] = pkamount - (Convert.ToDecimal(row["UseAmount"])+balancePAmount);
                        balancePAmount = 0; // Set balancePAmount to 0 since it's fully utilized
                    }
                }
                else
                {
                    foreach (DataRow row in rows)
                    {
                        // Update the current row with the pkamount
                        row["UseAmount"] = pkamount;
                        balancePAmount -= pkamount;

                        // If balancePAmount is less than 0, exit the loop
                        if (balancePAmount <= 0)
                            break;
                    }
                }
                //if (balancePAmount == 0)
                //{
                //    if (PayAmount < pkamount)
                //    {
                //        rows[0]["UseAmount"] = PayAmount;
                //        balancePAmount = 0;
                //    }
                //    else
                //    {
                //        balancePAmount = PayAmount - pkamount;
                //        rows[0]["UseAmount"] = balancePAmount;
                //    }
                //}
                //else
                //{
                //    if (pkuseamount == 0)
                //    {
                //        pkuseamount = balancePAmount - pkamount;
                //        rows[0]["UseAmount"] = pkamount;
                //    }
                //    else if (pkuseamount != 0 || pkuseamount < pkamount)
                //    {
                //        rows[0]["UseAmount"] = pkuseamount;
                //    }

                //}

                SaveDataTableChanges(dt, UsersId);
            }

        }
        public void SaveDataTableChanges(DataTable dt, int userId)
        {
            // Iterate through the DataTable and update the database
            foreach (DataRow row in dt.Rows)
            {
                int prizeId = Convert.ToInt32(row["PrizeId"]);
                decimal useAmount = Convert.ToDecimal(row["UseAmount"]);
                decimal pkAmount = Convert.ToDecimal(row["PKAmount"]);
                PrizesDAL _prizes = new PrizesDAL();
                _prizes.UpdatePrizeForOpen(prizeId, userId, useAmount);
                _prizes.OpenPrize(prizeId, pkAmount);
            }
        }

        //Get Prizes of user
        public List<PrizesModel> GetPrizeOfUser(int UsersId)
        {
            PrizeQuery prizeQuery = new PrizeQuery();
            List<PrizesModel> prizes = new List<PrizesModel>();

            cmd = new MySqlCommand(prizeQuery.GetActivePrize(), con);
            cmd.CommandType = CommandType.Text;
            PrizesModel prize;
            try
            {
                cmd.Parameters.AddWithValue("UsersId", UsersId);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prize = new PrizesModel();
                    prize.UsersId = Convert.ToInt32(rdr["UsersId"]);
                    prize.PrizeId = Convert.ToInt32(rdr["PrizeId"]);
                    prize.PackageId = Convert.ToInt32(rdr["PackageId"]);
                    //prize.GiftCardCode = rdr["GiftCardCode"].ToString();
                    //prize.GiftCardName = rdr["GiftCardName"].ToString();
                    prize.IsWithdraw = Convert.ToBoolean(rdr["IsWithdraw"]);
                    prize.CanWithdraw = Convert.ToBoolean(rdr["CanWithdraw"]);
                    prize.PKAmount = Convert.ToDecimal(rdr["PKAmount"]);
                    prize.UseAmount = Convert.ToDecimal(rdr["UseAmount"]);
                    //prize.GiftCardImage = rdr["GiftCardImage"].ToString();
                    prize.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    prizes.Add(prize);
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
            return prizes;
        }
        public PrizesModel GetPKAmountOfPrize(int UsersId)
        {
            PrizeQuery prizeQuery = new PrizeQuery();
            cmd = new MySqlCommand(prizeQuery.GetActivePrize(), con);
            cmd.CommandType = CommandType.Text;
            PrizesModel prize = new PrizesModel();
            try
            {
                cmd.Parameters.AddWithValue("UsersId", UsersId);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prize.PKAmount = Convert.ToDecimal(rdr["PKAmount"]);
                    prize.UseAmount = Convert.ToDecimal(rdr["UseAmount"]);
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
            return prize;
        }
        public List<PaymentModel> Select()
        {
            List<PaymentModel> paymentList = new List<PaymentModel>();

            cmd = new MySqlCommand("PaymentSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            PaymentModel payment;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    payment = new PaymentModel();
                    payment.PaymentID = Convert.ToInt32(rdr["PaymentID"]);
                    payment.InvoiceID = rdr["InvoiceID"].ToString();
                    payment.InvNo = rdr["InvNo"].ToString();
                    payment.PayNo = rdr["PayNo"].ToString();
                    payment.PayDate = Convert.ToDateTime(rdr["PayDate"]);
                    payment.PayAmount = Convert.ToDecimal(rdr["PayAmount"]);
                    payment.Remark = Convert.ToString(rdr["Remark"]);
                    payment.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    payment.UserName = Convert.ToString(rdr["UserName"]);
                    payment.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    paymentList.Add(payment);
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
            return paymentList;
        }
        public PaymentModel SelectByID(string id)
        {

            cmd = new MySqlCommand("PaymentSelectByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PaymentID", id);
            PaymentModel payment = new PaymentModel();
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    payment.PaymentID = Convert.ToInt32(rdr["PaymentID"]);
                    payment.InvoiceID = rdr["InvoiceID"].ToString();
                    payment.InvNo = rdr["InvNo"].ToString();
                    payment.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    payment.Discount = Convert.ToDecimal(rdr["Discount"]);
                    payment.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    payment.PayNo = rdr["PayNo"].ToString();
                    payment.PayDate = Convert.ToDateTime(rdr["PayDate"]);
                    payment.PayAmount = Convert.ToDecimal(rdr["PayAmount"]);
                    payment.Remark = Convert.ToString(rdr["Remark"]);
                    payment.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    payment.UserName = Convert.ToString(rdr["UserName"]);
                    payment.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return payment;
        }
        public List<PaymentModel> SelectByUserID(int UsersID)
        {
            List<PaymentModel> paymentList = new List<PaymentModel>();

            cmd = new MySqlCommand(query.SelectByUsersID(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersID);
            PaymentModel payment;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    payment = new PaymentModel();
                    payment.PaymentID = Convert.ToInt32(rdr["PaymentID"]);
                    payment.InvoiceID = rdr["InvoiceID"].ToString();
                    payment.InvNo = rdr["InvNo"].ToString();
                    payment.PayNo = rdr["PayNo"].ToString();
                    payment.PayDate = Convert.ToDateTime(rdr["PayDate"]);
                    payment.PayAmount = Convert.ToDecimal(rdr["PayAmount"]);
                    payment.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    payment.Balance = Convert.ToDecimal(rdr["Balance"]);
                    payment.Discount = Convert.ToDecimal(rdr["Discount"]);
                    payment.Remark = Convert.ToString(rdr["Remark"]);
                    //payment.UsersID = Convert.ToString(rdr["UsersID"]);
                    //payment.UserName = Convert.ToString(rdr["UserName"]);
                    payment.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    paymentList.Add(payment);
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
            return paymentList;
        }
    }
}
