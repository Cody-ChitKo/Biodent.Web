using Biodent.Api.Common;
using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UsersDAL _user;
        public LoginController()
        {
            _user = new UsersDAL();
        }

        [HttpPost("LoginWithPhone")]
        public UsersModel LoginWithPhone(LoginRequestModel obj)
        {
            var user = Authenticate(obj);

            //var users = _user.LoginWithPhone(obj.PhoneNo);
            return user;
        }
        [HttpGet("userId")]
        public UsersModel GetUserDataByUserId(int userId)
        {
            var users = _user.GetUsersInfo(userId);
            return users;
        }

        private UsersModel Authenticate(LoginRequestModel obj)
        {
            var loginuser = _user.LoginWithPhone(obj.PhoneNo);


            byte[] salt = Convert.FromBase64String(loginuser.UserSalt);
            //users.Password = "4BhWQYc9hxavq3UWpAAEZjWrFFkyvmTabWLGZuvlt0E=";
            byte[] bytePassword = Encoding.ASCII.GetBytes(obj.Password);

            byte[] passwordConversion = GeneralFunction.ComputerMAC256(bytePassword, salt);
            string actualPassword = Convert.ToBase64String(passwordConversion);

            if (loginuser.Password != actualPassword)
            {
                return null;
            }
            //if (user.Status == "Registered" || !user.IsVerified)
            if (loginuser.Status == "Registered")
            {
                return null;
            }

            var currentUser = loginuser;

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        [HttpPost("ConfrimPassword")]
        public ConfirmPasswordModel ConfrimPassword(LoginRequestModel obj)
        {
            ConfirmPasswordModel confirm = new ConfirmPasswordModel();
            var users = Authenticate(obj);
            if(users != null)
            {
                confirm.Status = true;
                confirm.Message = "Success";
            }
            else
            {
                confirm.Status = true;
                confirm.Message = "Success";
            }
            return confirm;
        }
    }
}
