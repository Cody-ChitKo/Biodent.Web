using Biodent.Models;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Biodent.Web.Common
{
    public class SystemConstants
    {
        /*Session  for Admin Current User*/
        public static readonly string Session_CurrentUser = "CurrentUser";   // get current user data (eg.userid,name,etc.)

        /*Session  for Public Current User*/
        public static readonly string Session_PublicCurrentUser = "PublicCurrentUser"; // get current user data (eg.userid,name,etc.)

        public static readonly string Session_UserAccessibleMainModules = "UserAccessibleModules";   // get permission data of current user
        public static readonly string Session_PublicUserAccessibleAllModules = "PublicUserAccessibleAllModules";         // get permission data of current user

        /*Encrypt String for Biodent User Password */
        public static readonly string BIOEncryptString = "$$BuserE&crypted$tRingV@!ue";


        private const int SaltSize = 32;

        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[SaltSize];
                rng.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] ComputerMAC256(byte[] data, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                return hmac.ComputeHash(data);
            }
        }

        public static bool isAccessOrAllownd(UsersModel CurrentUser, List<UsersPermissionModel> userPermissions, ControlModel CurrentControl)
        {
            if (CurrentUser == null)
            {
                return false;
            }
            else
            {
                #region Full Access
                var PageAccess = userPermissions.Find(x => x.Control_Id == CurrentControl.Control_Id && x.UsersID == CurrentUser.UsersID);
                if (PageAccess == null || userPermissions == null)
                {
                    return false;
                }
                if ((PageAccess.Create_Access == false || PageAccess.Create_Access == null) && (PageAccess.Full_Access == false || PageAccess.Full_Access == null))
                {
                    return false;
                }
                #endregion
            }
            return true;
        }

    }
}
