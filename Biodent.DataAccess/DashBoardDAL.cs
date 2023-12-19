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
    public class DashBoardDAL:DataControllerBase
    {
        DashBoardQuery query;
        public DashBoardDAL()
        {
            query = new DashBoardQuery();
        }
        public DashBoardModel DashView(DateTime FromDate, DateTime ToDate)
        {
            DashBoardModel dashBoardView = new DashBoardModel();
            cmd = new MySqlCommand(query.OverView(), con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("FromDate", FromDate);
            cmd.Parameters.AddWithValue("ToDate", ToDate);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    dashBoardView.NewCustomer = Convert.ToInt32(rdr["NewCustomer"]);
                    dashBoardView.TotalCustomer = Convert.ToInt32(rdr["TotalCustomer"]);
                    dashBoardView.NewOrder = Convert.ToInt32(rdr["NewOrder"]);
                    dashBoardView.ProcessOrder = Convert.ToInt32(rdr["ProcessOrder"]);
                    dashBoardView.PendingOrder = Convert.ToInt32(rdr["PendingOrder"]);
                    dashBoardView.DeliveringOrder = Convert.ToInt32(rdr["DeliveringOrder"]);
                    dashBoardView.ToDeliveryOrder = Convert.ToInt32(rdr["ToDeliveryOrder"]);
                    dashBoardView.BalanceOrder = Convert.ToInt32(rdr["BalanceOrder"]);
                    dashBoardView.CloseOrder = Convert.ToInt32(rdr["CloseOrder"]);
                    dashBoardView.TotalProcessingOrderItem = Convert.ToInt32(rdr["TotalProcessingOrderItem"]);
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
            return dashBoardView;
        }
    }
}
