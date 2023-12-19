using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Biodent.DataAccess
{
    public class InvoiceDAL:DataControllerBase
    {
        InvoiceQuery query;
        public InvoiceDAL()
        {
            query = new InvoiceQuery();
        }
        public void Insert(InvoiceModel inv)
        {
            cmd = new MySqlCommand(query.Insert(), con, Transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("InvoiceID", inv.InvoiceID);
            cmd.Parameters.AddWithValue("InvNo", this.GetNull(inv.InvNo));
            cmd.Parameters.AddWithValue("InvDate", this.GetNull(inv.InvDate));
            cmd.Parameters.AddWithValue("IssueDate", this.GetNull(inv.IssueDate));
            cmd.Parameters.AddWithValue("PatientName", this.GetNull(inv.PatientName));
            cmd.Parameters.AddWithValue("P_Age", this.GetNull(inv.P_Age));
            cmd.Parameters.AddWithValue("Gender", this.GetNull(inv.Gender));
            cmd.Parameters.AddWithValue("TeethShade", this.GetNull(inv.TeethShade));
            cmd.Parameters.AddWithValue("UsersID", this.GetNull(inv.UsersID));
            cmd.Parameters.AddWithValue("UsersName", this.GetNull(inv.UsersName));
            cmd.Parameters.AddWithValue("DoctorName", this.GetNull(inv.DoctorName));
            cmd.Parameters.AddWithValue("TotalAmount", this.GetNull(inv.TotalAmount));
            cmd.Parameters.AddWithValue("Discount", this.GetNull(inv.Discount));
            cmd.Parameters.AddWithValue("Remark", this.GetNull(inv.Remark));
            //cmd.Parameters.AddWithValue("ToothSimpleFile", this.GetNull(inv.ToothSimpleFile));
            cmd.Parameters.AddWithValue("Materials", this.GetNull(inv.Materials));
            cmd.Parameters.AddWithValue("OrderStatus", "New");

            cmd.Parameters.AddWithValue("GenerateType", "Order");
            SaveChangeCommit();
        }
        public void Approve(InvoiceModel inv)
        {
            cmd = new MySqlCommand(query.InvoiceApprove(), con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("InvID", this.GetNull(inv.InvoiceID));
            cmd.Parameters.AddWithValue("IssueDate", this.GetNull(inv.IssueDate));
            cmd.Parameters.AddWithValue("TotalAmount", this.GetNull(inv.TotalAmount));
            cmd.Parameters.AddWithValue("Discount", this.GetNull(inv.Discount));
            cmd.Parameters.AddWithValue("NetAmount", this.GetNull(inv.NetAmount));
            cmd.Parameters.AddWithValue("Remark", this.GetNull(inv.Remark));
            cmd.Parameters.AddWithValue("ApproveUserID", this.GetNull(inv.ApproveUserID));
            cmd.Parameters.AddWithValue("ApproveDate", this.GetNull(inv.ApproveDate));
            cmd.Parameters.AddWithValue("OrderStatus", "Processing");
            SaveChangeCommit();
        }
        public void Pending(InvoiceModel inv)
        {
            cmd = new MySqlCommand(query.InvoicePending(), con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("InvID", this.GetNull(inv.InvoiceID));
            cmd.Parameters.AddWithValue("ApproveUserID", this.GetNull(inv.ApproveUserID));
            cmd.Parameters.AddWithValue("ApproveDate", this.GetNull(inv.ApproveDate));
            cmd.Parameters.AddWithValue("OrderStatus", "Pending");
            SaveChangeCommit();
        }
        public void Reject(InvoiceModel inv)
        {
            cmd = new MySqlCommand(query.InvoiceReject(), con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("InvID", this.GetNull(inv.InvoiceID));
            cmd.Parameters.AddWithValue("ApproveUserID", this.GetNull(inv.ApproveUserID));
            cmd.Parameters.AddWithValue("ApproveDate", this.GetNull(inv.ApproveDate));
            cmd.Parameters.AddWithValue("OrderStatus", "Pending");
            SaveChangeCommit();
        }
        public void Delivered(string InvID, DateTime deliDate)
        {
            cmd = new MySqlCommand(query.InvoiceDelivered(), con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("InvID", this.GetNull(InvID));
            cmd.Parameters.AddWithValue("DeliverdDate", this.GetNull(deliDate));
            cmd.Parameters.AddWithValue("OrderStatus", "Delivering");
            SaveChangeCommit();
        }
        public void Received(string InvID, DateTime receiveDate)
        {
            cmd = new MySqlCommand(query.InvoiceReceived(), con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("InvID", this.GetNull(InvID));
            cmd.Parameters.AddWithValue("ReceivedDate", this.GetNull(receiveDate));
            //cmd.Parameters.AddWithValue("OrderStatus", "Delivering");
            SaveChangeCommit();
        }
        public void Closed(string InvID, DateTime closedDate)
        {
            cmd = new MySqlCommand(query.InvoiceClosed(), con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("InvID", this.GetNull(InvID));
            cmd.Parameters.AddWithValue("CloseDate", this.GetNull(closedDate));
            //cmd.Parameters.AddWithValue("OrderStatus", "Delivering");
            SaveChangeCommit();
        }
        public void InsertDetail(InvoiceDetailModel invdetail)
        {
            cmd = new MySqlCommand(query.InsertDetail(), con, Transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("InvoiceID", this.GetNull(invdetail.InvoiceID));
            cmd.Parameters.AddWithValue("ToothNo", this.GetNull(invdetail.ToothNo));
            cmd.Parameters.AddWithValue("ProthesisID", this.GetNull(invdetail.ProthesisID));
            cmd.Parameters.AddWithValue("SubProID", this.GetNull(invdetail.SubProID));
            cmd.Parameters.AddWithValue("Qty", this.GetNull(invdetail.Qty));
            cmd.Parameters.AddWithValue("Price", this.GetNull(invdetail.Price));
            cmd.Parameters.AddWithValue("Discount", this.GetNull(invdetail.Discount));
            cmd.Parameters.AddWithValue("Amount", this.GetNull(invdetail.Amount));
            cmd.Parameters.AddWithValue("CaseType", invdetail.CaseType);
            SaveChangeCommit();
        }

        public InvoiceModel SelectByInvId(string InvID)
        {
            cmd = new MySqlCommand(query.Select(InvID), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("InvID", InvID);
            InvoiceModel invoice = new InvoiceModel();
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.ToothSimpleFile = Convert.ToString(rdr["ToothSimpleFile"]);
                    //invoice.ApproveUserID = Convert.ToString(rdr["ApproveUserID"]);
                    //invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return invoice;
        }
        public List<InvoiceModel> GetInvoicesByUserID(int UsersID)
        {
            List<InvoiceModel> invList = new List<InvoiceModel>();

            cmd = new MySqlCommand(query.SelectByUsersID(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersID);
            InvoiceModel invoice;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.Materials = Convert.ToString(rdr["Materials"]);
                    invoice.RemainingDay = Convert.ToInt32(rdr["RemainingDay"]);
                    //invoice.ApproveUserID = Convert.ToString(rdr["ApproveUserID"]);
                    //invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    invList.Add(invoice);
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
            return invList;
        }

        public BalanceOrderModel GetBalanceOrderByUserID(int UsersID)
        {
            BalanceOrderModel balalceOrders = new BalanceOrderModel();
            balalceOrders.completeViewModels = new List<CompleteViewModel>();
            cmd = new MySqlCommand(query.SelectBalanceOrderByUserID(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersID);
            CompleteViewModel completeViewModel;
            try
            {

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    completeViewModel = new CompleteViewModel();
                    completeViewModel.InvNo = rdr["InvNo"].ToString();
                    completeViewModel.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    completeViewModel.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    completeViewModel.PatientName = rdr["PatientName"].ToString();
                    completeViewModel.UsersName = rdr["UsersName"].ToString();
                    completeViewModel.DoctorName = rdr["DoctorName"].ToString();
                    completeViewModel.Discount = Convert.ToDecimal(rdr["Discount"]);
                    completeViewModel.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    balalceOrders.TotalNetAmount += Convert.ToDecimal(rdr["NetAmount"]);

                    balalceOrders.completeViewModels.Add(completeViewModel);
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
            return balalceOrders;
        }

        public List<InvoiceModel> GetNewOrders()
        {
            List<InvoiceModel> invList = new List<InvoiceModel>();

            cmd = new MySqlCommand(query.SelectNewOrders(), con);
            cmd.CommandType = CommandType.Text;
            
            InvoiceModel invoice;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.Materials = Convert.ToString(rdr["Materials"]);
                    invoice.RemainingDay = Convert.ToInt32(rdr["RemainingDay"]);
                    //invoice.ApproveUserID = Convert.ToString(rdr["ApproveUserID"]);
                    //invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    invList.Add(invoice);
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
            return invList;
        }
        public List<InvoiceModel> GetProcessOrders()
        {
            List<InvoiceModel> invList = new List<InvoiceModel>();

            cmd = new MySqlCommand(query.SelectProcessOrders(), con);
            cmd.CommandType = CommandType.Text;

            InvoiceModel invoice;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.Materials = Convert.ToString(rdr["Materials"]);
                    invoice.RemainingDay = Convert.ToInt32(rdr["RemainingDay"]);
                    //invoice.ApproveUserID = Convert.ToString(rdr["ApproveUserID"]);
                    //invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    invList.Add(invoice);
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
            return invList;
        }

        public List<InvoiceModel> GetDeliveringOrders()
        {
            List<InvoiceModel> invList = new List<InvoiceModel>();

            cmd = new MySqlCommand(query.SelectDeliveringOrders(), con);
            cmd.CommandType = CommandType.Text;

            InvoiceModel invoice;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.Materials = Convert.ToString(rdr["Materials"]);
                    invoice.RemainingDay = Convert.ToInt32(rdr["RemainingDay"]);
                    //invoice.ApproveUserID = Convert.ToString(rdr["ApproveUserID"]);
                    //invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    invList.Add(invoice);
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
            return invList;
        }
        public List<InvoiceModel> GetReceivedOrders()
        {
            List<InvoiceModel> ProcessInvList = new List<InvoiceModel>();

            cmd = new MySqlCommand(query.SelectReceivedOrders(), con);
            cmd.CommandType = CommandType.Text;
            InvoiceModel invoice;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.DeliverdDate = Convert.ToDateTime(rdr["DeliverdDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.ApproveUserID = Convert.ToInt32(rdr["ApproveUserID"]);
                    invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    ProcessInvList.Add(invoice);
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
            return ProcessInvList;
        }
        public List<InvoiceModel> GetClosedOrders()
        {
            List<InvoiceModel> ProcessInvList = new List<InvoiceModel>();

            cmd = new MySqlCommand(query.SelectClosedOrders(), con);
            cmd.CommandType = CommandType.Text;
            InvoiceModel invoice;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.DeliverdDate = Convert.ToDateTime(rdr["DeliverdDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.ApproveUserID = Convert.ToInt32(rdr["ApproveUserID"]);
                    invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    ProcessInvList.Add(invoice);
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
            return ProcessInvList;
        }
        public List<CompleteViewModel> GetCompletedOrders(DateTime fromDate, DateTime toDate)
        {
            List<CompleteViewModel> completeOrders = new List<CompleteViewModel>();

            cmd = new MySqlCommand(query.SelectCompletedOrders(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("FromDate", fromDate);
            cmd.Parameters.AddWithValue("ToDate", toDate);

            CompleteViewModel complete;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    complete = new CompleteViewModel();

                    complete.InvNo = rdr["InvNo"].ToString();
                    complete.PatientName = rdr["PatientName"].ToString();
                    complete.UsersName = rdr["UsersName"].ToString();
                    complete.DoctorName = rdr["DoctorName"].ToString();
                    complete.Qty = Convert.ToInt16(rdr["Qty"]);
                    complete.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    //complete.TotalNetAmount = Convert.ToDecimal(rdr["TotalNetAmount"]);
                    complete.ReviewDesp = rdr["ReviewDesp"].ToString();
                    complete.Rating = Convert.ToInt16(rdr["Rating"]);
                    completeOrders.Add(complete);
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
            return completeOrders;
        }

        public List<InvoiceModel> GetCompletedOrders()
        {
            List<InvoiceModel> ProcessInvList = new List<InvoiceModel>();

            cmd = new MySqlCommand(query.SelectCompletedOrders(), con);
            cmd.CommandType = CommandType.Text;
            InvoiceModel invoice;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    invoice = new InvoiceModel();
                    invoice.InvoiceID = rdr["InvoiceID"].ToString();
                    invoice.InvNo = rdr["InvNo"].ToString();
                    invoice.InvDate = Convert.ToDateTime(rdr["InvDate"]);
                    invoice.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    invoice.DeliverdDate = Convert.ToDateTime(rdr["DeliverdDate"]);
                    invoice.PatientName = rdr["PatientName"].ToString();
                    invoice.P_Age = Convert.ToInt32(rdr["P_Age"]);
                    invoice.Gender = rdr["Gender"].ToString();
                    invoice.TeethShade = rdr["TeethShade"].ToString();
                    invoice.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    invoice.UsersName = rdr["UsersName"].ToString();
                    invoice.DoctorName = rdr["DoctorName"].ToString();
                    invoice.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    invoice.Discount = Convert.ToDecimal(rdr["Discount"]);
                    invoice.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    invoice.Balance = Convert.ToDecimal(rdr["Balance"]);
                    invoice.Remark = Convert.ToString(rdr["Remark"]);
                    invoice.ApproveUserID = Convert.ToInt32(rdr["ApproveUserID"]);
                    invoice.ApproveDate = Convert.ToDateTime(rdr["ApproveDate"]);
                    invoice.OrderStatus = Convert.ToString(rdr["OrderStatus"]);
                    invoice.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    ProcessInvList.Add(invoice);
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
            return ProcessInvList;
        }
        public List<InvoiceDetailModel> SelectDetailsByInvID(string InvID)
        {
            List<InvoiceDetailModel> invdetailList = new List<InvoiceDetailModel>();

            cmd = new MySqlCommand(query.SelectDetails(InvID), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("InvID", InvID);

            InvoiceDetailModel detail;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    detail = new InvoiceDetailModel();
                    detail.InvoiceID = rdr["InvoiceID"].ToString();
                    detail.InvDetailID = Convert.ToInt32(rdr["InvDetailID"]);
                    detail.ToothNo = (rdr["ToothNo"]).ToString();
                    detail.ProthesisID = rdr["ProthesisID"].ToString();
                    detail.ProthesisName = rdr["ProthesisName"].ToString();
                    detail.SubProID = rdr["SubProID"].ToString();
                    detail.SubProthesisName = rdr["SubProthesisName"].ToString();
                    detail.Qty = Convert.ToInt32(rdr["Qty"]);
                    detail.Price = Convert.ToInt32(rdr["Price"]);
                    detail.Amount = Convert.ToInt32(rdr["Amount"]);
                    detail.CaseType = rdr["CaseType"].ToString();
                    detail.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    invdetailList.Add(detail);
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
            return invdetailList;
        }
    }
}
