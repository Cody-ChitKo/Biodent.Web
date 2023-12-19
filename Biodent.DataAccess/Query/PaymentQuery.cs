using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class PaymentQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT INTO tbl_payment (InvoiceID, PayNo, PayDate, PayAmount, Remark, UsersID, IsActive)";
            query += "VALUES (@InvoiceID, @PayNo, @PayDate, @PayAmount, @Remark, @UsersID, 1);";

            query += " UPDATE tbl_invoice SET Balance = NetAmount - @PayAmount";
            query += " WHERE InvoiceID = @InvoiceID;";
            return query;
        }
        public string InsertFormMobile()
        {
            query += " UPDATE tbl_invoice SET Balance = NetAmount - @PayAmount";
            query += " WHERE InvoiceID = @InvoiceID;";

            query = "INSERT INTO tbl_payment (InvoiceID, PayNo, PayDate, PayAmount, Remark, UsersID, IsActive)";
            query += "VALUES (null, @PayNo, @PayDate, @PayAmount, @Remark, @UsersID, 1);";
            return query;
        }
        public string OtherPayment()
        {
            query = " UPDATE tbl_users SET PackageAmount = PackageAmount - @PayAmount WHERE UsersID = @UsersID; ";

            query += "INSERT INTO tbl_payment (PayNo, PayDate, PayAmount, Remark, UsersID, IsActive) ";
            query += "VALUES (@PayNo, @PayDate, @PayAmount, @Remark, @UsersID, 1);";
            return query;
        }
        public string SelectByUsersID()
        {
            query = " SELECT tbl_payment.*, inv.NetAmount, inv.Discount, inv.Balance, inv.InvNo";
            query += " FROM tbl_payment INNER JOIN tbl_Invoice inv ON tbl_payment.InvoiceID = inv.InvoiceID ";
            query += " WHERE tbl_payment.IsActive = 1 AND inv.UsersID = @UsersID ORDER BY PayDate DESC;";
            return query;
        }


    }
}
