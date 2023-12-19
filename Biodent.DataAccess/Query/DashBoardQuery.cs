using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class DashBoardQuery
    {
        private string query = "";
        public string OverView()
        {
            query = " SELECT SUM(NewCustomer) as NewCustomer,SUM(TotalCustomer) as TotalCustomer, SUM(NewOrder) as NewOrder, SUM(ProcessOrder) as ProcessOrder, ";
            query += " SUM(PendingOrder) as PendingOrder, SUM(DeliveringOrder) as DeliveringOrder, SUM(ToDeliveryOrder) AS ToDeliveryOrder, ";
            query += " SUM(CloseOrder) AS CloseOrder, SUM(BalanceOrder) as BalanceOrder, SUM(TotalProcessingOrderItem) as TotalProcessingOrderItem ";
            query += " FROM( SELECT Count(UsersId) AS NewCustomer, 0 as TotalCustomer, 0 as NewOrder, 0 as ProcessOrder,0 as PendingOrder, 0 as DeliveringOrder, 0 as ToDeliveryOrder, ";
            query += " 0 AS CloseOrder,0 AS BalanceOrder, 0 as TotalProcessingOrderItem FROM tbl_users WHERE Status='Registered' AND IsActive = 1 ";

            query += " UNION ALL SELECT 0 as NewCustomer, Count(UsersId) AS TotalCustomer, 0 as NewOrder, 0 as ProcessOrder,0 as PendingOrder, 0 as DeliveringOrder, ";
            query += " 0 as ToDeliveryOrder, 0 AS CloseOrder, 0 AS BalanceOrder, 0 as TotalProcessingOrderItem FROM tbl_users WHERE Status='Approve' AND IsActive = 1 ";

            query += " UNION ALL SELECT 0 as NewCustomer, 0 AS TotalCustomer, Count(InvoiceID) AS NewOrder, 0 as ProcessOrder,0 as PendingOrder, 0 as DeliveringOrder, ";
            query += " 0 as ToDeliveryOrder, 0 AS CloseOrder, 0 AS BalanceOrder, 0 as TotalProcessingOrderItem FROM tbl_invoice WHERE OrderStatus='New' AND IsActive = 1 ";

            query += " UNION ALL SELECT 0 as NewCustomer, 0 AS TotalCustomer, 0 AS NewOrder, Count(InvoiceID) AS ProcessOrder ,0 as PendingOrder, 0 as DeliveringOrder, ";
            query += " 0 as ToDeliveryOrder, 0 AS CloseOrder, 0 AS BalanceOrder, 0 as TotalProcessingOrderItem FROM tbl_invoice WHERE OrderStatus='Processing' AND IsActive = 1 ";

            query += " UNION ALL SELECT  0 as NewCustomer, 0 AS TotalCustomer, 0 AS NewOrder, 0 AS ProcessOrder, Count(InvoiceID) as PendingOrder, 0 as DeliveringOrder, ";
            query += " 0 as ToDeliveryOrder, 0 AS CloseOrder, 0 AS BalanceOrder, 0 as TotalProcessingOrderItem FROM tbl_invoice WHERE OrderStatus='Pending' AND IsActive = 1 ";

            query += " UNION ALL SELECT 0 as NewCustomer, 0 AS TotalCustomer, 0 AS NewOrder, 0 AS ProcessOrder, 0 as PendingOrder, Count(InvoiceID) as DeliveringOrder, ";
            query += " 0 as ToDeliveryOrder, 0 AS CloseOrder, 0 AS BalanceOrder, 0 as TotalProcessingOrderItem FROM tbl_invoice WHERE OrderStatus='Delivering' AND IsActive = 1 ";

            query += " UNION ALL SELECT  0 as NewCustomer, 0 AS TotalCustomer, 0 AS NewOrder, 0 AS ProcessOrder, 0 as PendingOrder, 0 as DeliveringOrder, ";
            query += " 0 as ToDeliveryOrder, Count(InvoiceID) AS CloseOrder, 0 AS BalanceOrder, 0 as TotalProcessingOrderItem FROM tbl_invoice WHERE OrderStatus='Close' AND IsActive = 1 ";

            query += " UNION ALL SELECT  0 as NewCustomer, 0 AS TotalCustomer, 0 AS NewOrder, 0 AS ProcessOrder, 0 as PendingOrder, 0 as DeliveringOrder, ";
            query += " 0 as ToDeliveryOrder, 0 AS CloseOrder, Count(InvoiceID) AS BalanceOrder, 0 as TotalProcessingOrderItem ";
            query += " FROM tbl_invoice WHERE Balance<>0 AND OrderStatus='Close' AND IsActive = 1 ";

            query += " UNION ALL";
            query += " SELECT  0 as NewCustomer, 0 AS TotalCustomer, 0 AS NewOrder, 0 AS ProcessOrder, 0 as PendingOrder, 0 as DeliveringOrder, 0 as ToDeliveryOrder, ";
            query += " 0 AS CloseOrder, 0 AS BalanceOrder, Count(Qty) as TotalProcessingOrderItem FROM tbl_invdetail ind ";
            query += " INNER JOIN tbl_invoice inv ON inv.InvoiceID = ind.InvoiceID AND inv.IsActive = 1 ";
            query += " WHERE DAY(InvDate)= DAY(@ToDate) AND MONTH(InvDate) = MONTH(@ToDate) AND YEAR(InvDate)=YEAR(@ToDate) AND ";
            query += " OrderStatus='Processing' AND ind.IsActive = 1 ) as OverView";
            return query;
        }
    }
}
