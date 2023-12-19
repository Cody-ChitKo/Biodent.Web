using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class PackageQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "";
            query = "INSERT tbl_package(PackageName, Description, IsActive, PKPrice, GetPKAmount, PKImage) ";
            query += " VALUES(@PackageName, @Description,1, @PKPrice, @GetPKAmount,@PKImage)";
            return query;
        }
        public string Update()
        {
            query = "";
            query = "UPDATE tbl_package SET PackageName = @PackageName, Description = @Description, PKPrice = @PKPrice, ";
            query += " GetPKAmount = @GetPKAmount, PKImage = @PKImage WHERE PackageId = @PackageId";
            return query;
        }
        public string Delete()
        {
            query = "DELETE FROM tbl_package WHERE PackageId = @PackageId";
            return query;
        }
        public string Select(int PackageId)
        {
            query = "";
            if (PackageId == 0)
            {
                query = "SELECT * FROM tbl_package WHERE IsActive = 1";
            }
            else
            {
                query = "SELECT * FROM tbl_package WHERE IsActive = 1";
                query += " AND PackageId = @PackageId";
            }
            return query;
        }
        public string BuyPackage()
        {
            query = "";
            query = "UPDATE tbl_users SET PackageAmount = PackageAmount+@PackageAmount, PackageExpire = @PackageExpire";
            query += " WHERE UsersID = @UsersID;";

            query += " UPDATE tbl_users SET WalletAmount = WalletAmount-@PKPrice ";
            query += " WHERE UsersID = @UsersID ;";

            query += "INSERT tbl_prizes(UsersId, PackageId, GiftCardId, WithdrawDate, IsWithdraw, CanWithdraw, PKAmount, UseAmount, IsActive) ";
            query += " SELECT @UsersId, PackageId, GiftCardId, null, 0, 0, (@PackageAmount/@GiftCardCount), 0, 1 ";
            query += " FROM tbl_giftcard WHERE PackageId = @PackageId;";
            return query;
        }
        public string GetGiftcardCount()
        {
            query = "";
            query = "select count(*) as Count from tbl_giftcard WHERE PackageId = @pid";
            return query;
        }
    }
}
