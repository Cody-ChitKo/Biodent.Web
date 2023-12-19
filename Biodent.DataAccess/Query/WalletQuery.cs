using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class WalletQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT INTO tbl_wallet (UsersId, WalletAmount, AddDate, ApproveUserId, ApproveDate, PayMethodId, ss_Image, Status, IsActive) ";
            query += " VALUES (@UsersId, @WalletAmount, @AddDate, 0, null, @PayMethodId, @ss_Image, 'Add', 1);";
            return query;
        }
        public string WalletApprove()
        {
            query = "UPDATE tbl_wallet SET ApproveUserId = @ApproveUserId, ";
            query += " ApproveDate = @ApproveDate, Status = 'Approve' WHERE WalletId = @WalletId;";

            query += "UPDATE tbl_users SET WalletAmount = WalletAmount + @WalletAmount WHERE tbl_users.UsersID = @UsersId";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_services SET ServiceTypeId = @ServiceTypeId, ServiceHeader = @ServiceHeader,";
            query += " ServiceDescription = @ServiceDescription, ServicePrice = @ServicePrice, ServiceImage = @ServiceImage";
            query += " WHERE ServiceId = @ServiceId";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_services SET IsActive = 0 WHERE ServiceId = @ServiceId";
            return query;
        }
        public string Select(int UsersId)
        {
            if (UsersId == 0)
            {
                query = "SELECT tbl_wallet.*, UsersName, PayMethodName FROM tbl_wallet ";
                query += " INNER JOIN tbl_users ON tbl_users.UsersID = tbl_wallet.UsersId";
                query += " INNER JOIN tbl_paymethod ON tbl_paymethod.PayMethodId = tbl_wallet.PayMethodId";
                query += " WHERE tbl_wallet.IsActive = 1";
            }
            else
            {
                query = "SELECT tbl_wallet.*, UsersName, PayMethodName FROM tbl_wallet ";
                query += " INNER JOIN tbl_users ON tbl_users.UsersID = tbl_wallet.UsersId";
                query += " INNER JOIN tbl_paymethod ON tbl_paymethod.PayMethodId = tbl_wallet.PayMethodId";
                query += " WHERE tbl_wallet.IsActive = 1 AND tbl_wallet.UsersId = @UsersId AND tbl_wallet.Status = 'Approve'";
            }
            return query;
        }
        public string SelectApproveWallet()
        {
            query = "SELECT tbl_wallet.*, UsersName, PayMethodName FROM tbl_wallet ";
            query += " INNER JOIN tbl_users ON tbl_users.UsersID = tbl_wallet.UsersId";
            query += " INNER JOIN tbl_paymethod ON tbl_paymethod.PayMethodId = tbl_wallet.PayMethodId";
            query += " WHERE tbl_wallet.IsActive = 1 AND tbl_wallet.Status = 'Approve'";
            return query;
        }
        public string SelectNewWallet()
        {
            query = "SELECT tbl_wallet.*, UsersName, PayMethodName FROM tbl_wallet ";
            query += " INNER JOIN tbl_users ON tbl_users.UsersID = tbl_wallet.UsersId";
            query += " INNER JOIN tbl_paymethod ON tbl_paymethod.PayMethodId = tbl_wallet.PayMethodId";
            query += " WHERE tbl_wallet.IsActive = 1 AND tbl_wallet.Status = 'Add'";
            return query;
        }
        public string SelectByWalletId()
        {
            query = "SELECT tbl_wallet.* FROM tbl_wallet ";
            query += " WHERE IsActive = 1 AND WalletId = @WalletId";
            return query;
        }
    }
}
