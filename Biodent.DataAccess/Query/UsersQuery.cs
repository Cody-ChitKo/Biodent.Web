using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class UsersQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT tbl_users ";
            query += " (UsersName, Password, PhoneNo, Email, UserLevel, AccountLevel_Id, UserSalt, VerificationCode,";
            query += " WalletAmount, PackageAmount, PackageExpire, IsVerified, IsActive, CreatedDate, RegionID, Address, Status, MobileLoginCode)";
            query += " VALUES(@UsersName, @Password, @PhoneNo, @Email, @UserLevel, @AccountLevel_Id,";
            query += " @UserSalt, @VerificationCode, 0, 0, null, @IsVerified, 1, @CreatedDate, @RegionID, @Address,'Registered',null)";
            return query;
        }
        public string InsertFromAdmin()
        {
            query = "INSERT tbl_users ";
            query += " (UsersName, Password, PhoneNo, Email, UserLevel, AccountLevel_Id, UserSalt, VerificationCode,";
            query += " WalletAmount, PackageAmount, PackageExpire, IsVerified, IsActive, CreatedDate,RegionID, Status, MobileLoginCode)";
            query += " VALUES(@UsersName, @Password, @PhoneNo, @Email, @UserLevel, @AccountLevel_Id,";
            query += " @UserSalt, @VerificationCode, 0, 0, null, @IsVerified, 1, @CreatedDate,1, 'Approved',null)";
            return query;
        }
        public string UpdateForAdmin()
        {
            query = "UPDATE tbl_users SET UsersName = @UsersName, PhoneNo = @PhoneNo, ";
            query += "Email = @Email WHERE UsersID = @UsersID;";
            return query;
        }
        public string DeleteFromAdmin()
        {
            query = "UPDATE tbl_users SET IsActive = 0 WHERE UsersID = @UsersID";
            return query;
        }
        public string RegisterUserAccount()
        {
            query = "";
            query += "";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_users SET UsersName = @UsersName, PhoneNo = @PhoneNo, ";
            query += "Email = @Email, RegionID = @RegionID, Address = @Address WHERE UsersID = @UsersID";
            return this.query;
        }
        public string Delete()
        {
            return query;
        }
        public string Select()
        {
            return query;
        }
        public string SelectPrizes(int UsersId)
        {
            query = "SELECT pz.PrizeId, pz.PackageId, gc.GiftCardCode, gc.GiftCardName, gc.GiftCardImage, ";
            query += " CASE WHEN pz.CanWithdraw = 0 THEN 'NO' ELSE 'YES' END AS CanWithdraw, ";
            query += " CASE WHEN pz.IsWithdraw = 0 THEN 'NO' ELSE 'YES' END AS IsWithdraw ";
            query += " FROM tbl_prizes pz INNER JOIN tbl_giftcard gc ON gc.GiftCardId = pz.GiftCardId ";
            query += " WHERE pz.UsersId = @UsersId;";
            return query;
        }
        public string GetActivePrizes()
        {
            query = "SELECT * FROM tbl_prizes WHERE IsActive = 1";
            return query;
        }
        public string userLogin()
        {
            query = "SELECT TOP(1) tbl_users.*, UserLevelName FROM tbl_users";
            query += " INNER JOIN tbl_accountLevel acl ON acl.AccountLevel_Id = tbl_users.AccountLevel_Id";
            query += " WHERE UserName=@UserName AND [Password]=@Password AND Users.IsActive=1";
            return query;
        }
        public string GetUserByEmail()
        {
            query = "select * from tbl_users where Email = @Email AND IsActive = 1";
            return query;
        }
        public string userLogout()
        {
            return query;
        }
        public string CheckUsers()
        {
            query = "SELECT * FROM tbl_users WHERE PhoneNo = @PhoneNo AND IsActive = 1";
            return query;
        }
        public string UsersLogin_WithPhone()
        {
            query = "SELECT * FROM tbl_users WHERE PhoneNo = @PhoneNo AND IsActive = 1";
            return query;
        }
        public string GetUsersByID()
        {
            query = "SELECT * FROM tbl_users WHERE UsersID = @UsersID AND IsActive = 1";
            return query;
        }
        public string SelectNewClinic()
        {
            query = "SELECT tbl_users.*, tbl_region.RegionName FROM tbl_users";
            query += " INNER JOIN tbl_region ON tbl_region.RegionID = tbl_users.RegionID";
            query += " WHERE tbl_users.IsActive = 1 AND tbl_users.Status = 'Registered';";
            return query;
        }
        public string SelectAllClinic()
        {
            query = "SELECT tbl_users.*, tbl_region.RegionName FROM tbl_users";
            query += " INNER JOIN tbl_region ON tbl_region.RegionID = tbl_users.RegionID";
            query += " WHERE tbl_users.IsActive = 1 AND tbl_users.Status <> 'Registered';";
            return query;
        }
        public string SelectAdminUser()
        {
            query = "SELECT tbl_users.* FROM tbl_users";
            query += " WHERE IsActive = 1 AND UserLevel = 'Users';";
            return query;
        }
        public string WithDrawPrize()
        {
            query = "UPDATE tbl_prizes SET TakenOutDate = @TakenOutDate, IsTakenOut = 1 ";
            query += " WHERE PrizeId = @PrizeId AND UsersId = @UsersId AND CanTaken = 1; ";

            //query += " UPDATE tbl_Prizes SET IsAcitve = 1 WHERE PrizeId <> @PrizeId AND UsersId = @UsersId;  ";
            return query;
        }
        public string UnActivePrize()
        {
            query = " UPDATE tbl_prizes SET IsAcitve = 1 WHERE PrizeId <> @PrizeId AND UsersId = @UsersId;  ";
            return query;
        }
        public string Approve()
        {
            query = "UPDATE tbl_users SET Status='Approved' WHERE UsersID = @UsersID";
            return query;
        }
        public string RejectUser()
        {
            query = "UPDATE tbl_users SET Status='Reject' WHERE UsersID = @UsersID";
            return query;
        }
    }
}
