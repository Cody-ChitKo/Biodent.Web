using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class FinancialQuery
    {
        private string query;
        public string IncomeByEachDepartment()
        {
            query = "SELECT DepartmentName, SUM(Qty) as Qty,  SUM(Amount) as NetAmount ";
            query += "FROM tbl_invdetail invd INNER JOIN tbl_invoice inv ON inv.InvoiceID = invd.InvoiceID ";
            query += "INNER JOIN tbl_prothesis P ON P.ProthesisID = invd.ProthesisID ";
            query += "INNER JOIN tbl_department D ON D.DepartmentID = P.DepartmentID ";
            query += "WHERE DATE(InvDate) BETWEEN DATE(@FromDate) AND DATE(@ToDate) GROUP BY DepartmentName;";
            return query;
        }
        public string EachCaseTypeByDate()
        {
            query = "SELECT ProthesisName AS DepartmentName, SUM(Qty) AS Qty,  CaseType FROM tbl_invdetail ";
            query += "INNER JOIN tbl_invoice inv ON inv.InvoiceID = tbl_invdetail.InvoiceID ";
            query += "INNER JOIN tbl_prothesis P ON P.ProthesisID = tbl_invdetail.ProthesisID ";
            query += "WHERE DATE(InvDate) BETWEEN DATE(@FromDate) AND DATE(@ToDate) GROUP BY CaseType, ProthesisName;";
            return query;
        }
        public  string BalanceOrdersForAdmin()
        {
            query = "SELECT tbl_invoice.InvNo, InvDate, IssueDate,  DeliverdDate, ";
            query += "UsersName, DoctorName, PatientName, SUM(tbl_invdetail.Qty) AS Qty, NetAmount ";
            query += "FROM tbl_invoice INNER JOIN tbl_users C ON C.UsersID = tbl_invoice.UsersID ";
            query += "INNER JOIN tbl_invdetail ON tbl_invdetail.InvoiceID = tbl_invoice.InvoiceID ";
            query += "LEFT OUTER JOIN tbl_review ON tbl_review.InvoiceID = tbl_invdetail.InvoiceID ";
            query += " WHERE OrderStatus = 'Close' AND Balance <> 0 AND DATE(InvDate) BETWEEN DATE(@FromDate) AND DATE(@ToDate) ";
            query += "GROUP BY tbl_invoice.InvNo, InvDate, IssueDate, DeliverdDate, UsersName, DoctorName, PatientName, NetAmount;";
            return query;
        }

        public string PaymentReceipt()
        {
            query = "SELECT tbl_payment.*, InvNo, TotalAmount, NetAmount, Discount, Balance, UsersName ";
            query += "FROM tbl_payment INNER JOIN tbl_invoice ON tbl_payment.InvoiceID = tbl_invoice.InvoiceID ";
            query += "INNER JOIN tbl_users ON tbl_payment.UsersID = tbl_users.UsersID ";
            query += "WHERE tbl_payment.IsActive = 1 ORDER BY tbl_payment.PayDate DESC; ";
            return query;
        }

    }
}
