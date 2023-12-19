using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class InvoiceQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_invoice (InvoiceID, InvNo, InvDate, IssueDate, PatientName, P_Age, Gender, TeethShade, UsersID,";
            query += " DoctorName, TotalAmount, Discount, NetAmount, Balance, Remark, ToothSimpleFile, Materials,";
            query += " ApproveUserID, ApproveDate, OrderStatus, DeliverdDate, ReceivedDate, CloseDate, IsActive)";
            query += " VALUES(@InvoiceID, @InvNo, @InvDate, @IssueDate, @PatientName, @P_Age,";
            query += " @Gender, @TeethShade, @UsersID, @DoctorName, @TotalAmount, @Discount, @TotalAmount, ";
            query += " @TotalAmount, @Remark, null , @Materials, null, null, @OrderStatus, null, null, null, 1);";

            //update tbl_GenerateNo table
            query += " UPDATE tbl_generateno SET LastValue = LastValue+1 WHERE GenerateType = @GenerateType;";

            return query;
        }

        public string InvoiceApprove()
        {
            query = "UPDATE tbl_invoice SET IssueDate = @IssueDate, TotalAmount = @TotalAmount, Discount = @Discount, ";
            query += "NetAmount = @NetAmount, Remark = @Remark, ApproveUserID = @ApproveUserID, ApproveDate = @ApproveDate, ";
            query += "OrderStatus = @OrderStatus WHERE InvoiceID = @InvID";
            return query;
        }
        public string InvoicePending()
        {
            query = "UPDATE tbl_invoice SET ApproveUserID = @ApproveUserID, ApproveDate = @ApproveDate,OrderStatus = @OrderStatus";
            query += " WHERE InvoiceID = @InvID";
            return query;
        }
        public string InvoiceReject()
        {
            query = "UPDATE tbl_invoice SET ApproveUserID = @ApproveUserID, ApproveDate = @ApproveDate,OrderStatus = @OrderStatus";
            query += " WHERE InvoiceID = @InvID";
            return query;
        }
        public string InvoiceDelivered()
        {
            query = "UPDATE tbl_invoice SET DeliverdDate=@DeliverdDate, OrderStatus = @OrderStatus WHERE InvoiceID = @InvID";
            return query;
        }
        public string InvoiceReceived()
        {
            query = "UPDATE tbl_invoice SET ReceivedDate = @ReceivedDate, OrderStatus = 'Received' WHERE InvoiceID = @InvID";
            return query;
        }
        public string InvoiceClosed()
        {
            query = "UPDATE tbl_invoice SET CloseDate = @CloseDate, OrderStatus = 'Closed' WHERE InvoiceID = @InvID";
            return query;
        }
        public string InsertDetail()
        {
            query = "INSERT tbl_invdetail (InvoiceID, ToothNo, ProthesisID, SubProID, Qty, Price, Discount, Amount, CaseType, IsActive)";
            query += " VALUES(@InvoiceID, @ToothNo, @ProthesisID, @SubProID, @Qty, @Price, @Discount, @Amount, @CaseType, 1)";
            return query;
        }
        public string Select(string InvID)
        {
            if (string.IsNullOrEmpty(InvID))
            {
                query = "SELECT tbl_invoice.*, UsersName FROM tbl_invoice ";
                query += "INNER JOIN tbl_users ON tbl_users.UsersID = tbl_invoice.UsersID ";
                query += "WHERE tbl_invoice.IsActive=1";
            }
            else
            {
                query = "SELECT tbl_invoice.*, UsersName FROM tbl_invoice ";
                query += "INNER JOIN tbl_users ON tbl_users.UsersID = tbl_invoice.UsersID ";
                query += "WHERE tbl_invoice.IsActive=1 AND InvoiceID = @InvID";
            }
            return query;
        }
        public string SelectDetails(string InvID)
        {
            if(string.IsNullOrEmpty(InvID))
            {
                query = "SELECT tbl_InvDetail.*, ProthesisName, SubProthesisName FROM tbl_InvDetail ";
                query += "INNER JOIN tbl_Invoice ON tbl_Invoice.InvoiceID = tbl_InvDetail.InvoiceID ";
                query += "INNER JOIN tbl_Prothesis ON tbl_Prothesis.ProthesisID = tbl_InvDetail.ProthesisID ";
                query += "INNER JOIN tbl_Subprothesis ON tbl_Subprothesis.SubProID = tbl_InvDetail.SubProID ";
                query += "WHERE tbl_InvDetail.IsActive = 1";
            }
            else
            {
                query = "SELECT tbl_InvDetail.*, ProthesisName, SubProthesisName FROM tbl_InvDetail ";
                query += "INNER JOIN tbl_Invoice ON tbl_Invoice.InvoiceID = tbl_InvDetail.InvoiceID ";
                query += "INNER JOIN tbl_Prothesis ON tbl_Prothesis.ProthesisID = tbl_InvDetail.ProthesisID ";
                query += "INNER JOIN tbl_Subprothesis ON tbl_Subprothesis.SubProID = tbl_InvDetail.SubProID ";
                query += "WHERE tbl_InvDetail.IsActive = 1 AND tbl_InvDetail.InvoiceID = @InvID";
            }
            return query;
        }
        public string SelectByUsersID()
        {
            query = "SELECT inv.*,  DATEDIFF(inv.IssueDate, CURDATE()) as RemainingDay, UsersName ";
            query += "FROM tbl_Invoice AS inv INNER JOIN tbl_Users us ON us.UsersID = inv.UsersID ";
            query += " WHERE inv.IsActive = 1 AND inv.UsersID = @UsersID";
            return query;
        }
        public string SelectBalanceOrderByUserID()
        {
            query = "SELECT inv.* FROM tbl_invoice AS inv INNER JOIN tbl_users us ON us.UsersID = inv.UsersID ";
            query += "WHERE inv.IsActive = 1 AND OrderStatus='Close' AND Balance<>0 ";
            query += " AND inv.UsersID = @UsersID";
            return query;
        }
        public string SelectPending()
        {
            query = "SELECT tbl_invoice.*,  DATEDIFF(Invoice.IssueDate, CURDATE()) as RemainingDay";
            query += "FROM tbl_invoice AS Invoice WHERE Invoice.IsActive = 1 AND ";
            query += "CustomerID = @UsersID";
            return query;
        }
        public string SelectNewOrders()
        {
            query = "SELECT inv.*,  DATEDIFF(inv.IssueDate, CURDATE()) as RemainingDay, UsersName ";
            query += "FROM tbl_invoice AS inv INNER JOIN tbl_users us ON us.UsersID = inv.UsersID ";
            query += " WHERE inv.IsActive = 1 AND inv.OrderStatus = 'New'";
            
            return query;
        }
        public string SelectProcessOrders()
        {
            query = "SELECT inv.*,  DATEDIFF(inv.IssueDate, CURDATE()) as RemainingDay, UsersName ";
            query += "FROM tbl_invoice AS inv INNER JOIN tbl_users us ON us.UsersID = inv.UsersID ";
            query += " WHERE inv.IsActive = 1 AND inv.OrderStatus = 'Processing'";

            return query;
        }
        public string SelectDeliveringOrders()
        {
            query = "SELECT inv.*,  DATEDIFF(inv.IssueDate, CURDATE()) as RemainingDay, UsersName ";
            query += "FROM tbl_invoice AS inv INNER JOIN tbl_users us ON us.UsersID = inv.UsersID ";
            query += " WHERE inv.IsActive = 1 AND inv.OrderStatus = 'Delivering'";

            return query;
        }
        public string SelectReceivedOrders()
        {
            query = "SELECT tbl_invoice.*, UsersName FROM tbl_invoice ";
            query += "INNER JOIN tbl_users ON tbl_invoice.UsersID = tbl_users.UsersID ";
            query += "WHERE tbl_invoice.IsActive=1 AND tbl_invoice.OrderStatus = 'Received'";
            return query;
        }
        public string SelectClosedOrders()
        {
            query = "SELECT tbl_invoice.*, UsersName FROM tbl_invoice ";
            query += "INNER JOIN tbl_users ON tbl_invoice.UsersID = tbl_users.UsersID ";
            query += "WHERE tbl_invoice.IsActive=1 AND tbl_invoice.OrderStatus = 'Closed' AND Balance <> 0";
            return query;
        }
        public string SelectCompletedOrders()
        {
            query = "SELECT inv.InvNo, UsersName, DoctorName, PatientName, SUM(invd.Qty) AS Qty, NetAmount, ";
            query += "0 AS TotalNetAmount, IFNULL(ReviewDesp, '') AS ReviewDesp, IFNULL(Rating, 0) AS Rating ";
            query += "FROM tbl_invoice inv INNER JOIN tbl_users C ON C.UsersID = inv.UsersID ";
            query += "INNER JOIN tbl_invdetail invd ON invd.InvoiceID = inv.InvoiceID ";
            query += "LEFT OUTER JOIN tbl_review ON tbl_review.InvoiceID = invd.InvoiceID ";
            query += "WHERE OrderStatus = 'Close' AND DATE(InvDate) BETWEEN DATE(@FromDate) AND DATE(@ToDate) ";
            query += "GROUP BY inv.InvNo, UsersName, DoctorName, PatientName, NetAmount, ReviewDesp, Rating ";

            query += " UNION ALL SELECT '' AS InvNo, '' AS UsersName, '' AS DoctorName, '' AS PatientName, 0 AS Qty, ";
            query += "0 AS NetAmount, SUM(NetAmount) AS TotalNetAmount, '' AS ReviewDesp, 0 AS Rating ";
            query += " FROM tbl_invoice WHERE OrderStatus = 'Close' AND ";
            query += " DATE(InvDate) BETWEEN CAST(@FromDate AS DATE) AND CAST(@ToDate AS DATE) ";
            query += "GROUP BY InvNo, DoctorName, PatientName;";

            return query;
        }
        public string InvoicePrint()
        {
            query = "SELECT inv.*, ind.ToothNo, ind.Qty, ind.Price, ind.Amount, ind.CaseType, ProthesisName, SubProthesisName, UsersName ";
            query += "FROM tbl_Invoice inv INNER JOIN tbl_InvDetail ind ON inv.InvoiceID = ind.InvoiceID ";
            query += "INNER JOIN tbl_Prothesis ON ind.ProthesisID = tbl_Prothesis.ProthesisID ";
            query += "INNER JOIN tbl_Subprothesis ON ind.SubProID = tbl_Subprothesis.SubProID ";
            query += "INNER JOIN tbl_Users ON tbl_Users.UsersID = inv.UsersID WHERE inv.InvoiceID = @InvID AND inv.IsActive = 1";
            return query;
        }
    }
}
