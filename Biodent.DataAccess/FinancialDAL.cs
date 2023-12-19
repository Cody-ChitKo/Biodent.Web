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
    public class FinancialDAL:DataControllerBase
    {
        FinancialQuery query;
        public FinancialDAL()
        {
            query = new FinancialQuery();
        }
        public List<DepartmentIncomeModel> IncomeByEachDepartment(DateTime FromDate, DateTime ToDate)
        {
            List<DepartmentIncomeModel> deptIncomeList = new List<DepartmentIncomeModel>();

            cmd = new MySqlCommand(query.IncomeByEachDepartment(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("FromDate", FromDate);
            cmd.Parameters.AddWithValue("ToDate", ToDate);
            DepartmentIncomeModel deptincome;
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    deptincome = new DepartmentIncomeModel();
                    deptincome.DepartmentName = rdr["DepartmentName"].ToString();
                    deptincome.Qty = Convert.ToInt32(rdr["Qty"]);
                    deptincome.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);

                    deptIncomeList.Add(deptincome);
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
            return deptIncomeList;
        }

        public List<DepartmentIncomeModel> EachCaseType(DateTime FromDate, DateTime ToDate)
        {
            List<DepartmentIncomeModel> deptIncomeList = new List<DepartmentIncomeModel>();

            cmd = new MySqlCommand(query.EachCaseTypeByDate(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("FromDate", FromDate);
            cmd.Parameters.AddWithValue("ToDate", ToDate);
            DepartmentIncomeModel deptincome;
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    deptincome = new DepartmentIncomeModel();
                    deptincome.DepartmentName = Convert.ToString(rdr["DepartmentName"]);
                    deptincome.CaseType = Convert.ToString(rdr["CaseType"]);
                    deptincome.Qty = Convert.ToInt32(rdr["Qty"]);
                    deptIncomeList.Add(deptincome);
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
            return deptIncomeList;
        }
        public BalanceOrderModel GetBalanceOrder(DateTime FromDate, DateTime ToDate)
        {
            BalanceOrderModel balanceOrder = new BalanceOrderModel();
            balanceOrder.completeViewModels = new List<CompleteViewModel>();
            cmd = new MySqlCommand(query.BalanceOrdersForAdmin(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("FromDate", FromDate);
            cmd.Parameters.AddWithValue("ToDate", ToDate);
            CompleteViewModel completeView;
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    completeView = new CompleteViewModel();
                    completeView.InvNo = Convert.ToString(rdr["InvNo"]);
                    completeView.IssueDate = Convert.ToDateTime(rdr["IssueDate"]);
                    completeView.DeliverdDate = Convert.ToDateTime(rdr["DeliverdDate"]);
                    completeView.UsersName = Convert.ToString(rdr["UsersName"]);
                    completeView.DoctorName = Convert.ToString(rdr["DoctorName"]);
                    completeView.PatientName = Convert.ToString(rdr["PatientName"]);
                    completeView.Qty = Convert.ToInt32(rdr["Qty"]);
                    completeView.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    balanceOrder.TotalNetAmount += Convert.ToDecimal(rdr["NetAmount"]);
                    balanceOrder.completeViewModels.Add(completeView);

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
            return balanceOrder;
        }

        public List<PaymentModel> PaymentReceipt()
        {
            List<PaymentModel> PaymentList = new List<PaymentModel>();

            cmd = new MySqlCommand(query.PaymentReceipt(), con);
            cmd.CommandType = CommandType.Text;
            PaymentModel payment;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    payment = new PaymentModel();
                    payment.InvNo = rdr["InvNo"].ToString();
                    payment.UserName = rdr["UserName"].ToString();
                    payment.TotalAmount = Convert.ToDecimal(rdr["TotalAmount"]);
                    payment.Discount = Convert.ToDecimal(rdr["Discount"]);
                    payment.NetAmount = Convert.ToDecimal(rdr["NetAmount"]);
                    payment.Balance = Convert.ToDecimal(rdr["Balance"]);
                    payment.PayNo = Convert.ToString(rdr["PayNo"]);
                    payment.PayAmount = Convert.ToDecimal(rdr["PayAmount"]);
                    payment.PayDate = Convert.ToDateTime(rdr["PayDate"]);
                    payment.UserName = Convert.ToString(rdr["UserName"]);
                    payment.Remark = Convert.ToString(rdr["Remark"]);
                    PaymentList.Add(payment);
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
            return PaymentList;
        }

    }
}
