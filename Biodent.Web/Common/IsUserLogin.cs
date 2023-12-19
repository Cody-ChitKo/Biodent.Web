using Biodent.Models;
using Microsoft.AspNetCore.Http;

namespace Biodent.Web.Common
{
    public class IsUserLogin
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsUserLogin()
        {
            _httpContextAccessor=new HttpContextAccessor();
        }

        public bool IsMemberUser()
        {
            // Access session data
            var user = _httpContextAccessor.HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);

            if (user == null)
            {
                return false;
            }
            return true;
        }
        public bool IsAdminUser()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

            if (user == null)
            {
                return false;
            }
            return true;
        }

        public int GetMemberUserId()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            return user.UsersID;
        }
        public int GetAdminUserId()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            return user.UsersID;
        }
    }
}
