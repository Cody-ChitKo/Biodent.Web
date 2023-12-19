using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Biodent.Web.Controllers
{
    public class LoginController : Controller
    {
        UsersDAL _user;
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _user = new UsersDAL();
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UsersModel users)
        {
            UsersModel user = new UsersModel();
            try
            {

                user = Authenticate(users);
                if (user != null)
                {
                    if (user.UserLevel == "Customer") return RedirectToAction("UserProfile", "Customer");
                    else return RedirectToAction("Index", "DashBoard");

                    //var token = GenerateToken(user);
                    //if (token != null)
                    //{
                    //    if (user.UserLevel == "Customer") return RedirectToAction("UserProfile", "Customer");
                    //    else return RedirectToAction("Index", "DashBoard");
                    //}
                    //else
                    //{
                    //    return RedirectToAction("TokenAccess", "Home");
                    //}

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //[HttpPost("token")]
        //private string GenerateToken(UsersModel user)
        //{
        //    try
        //    {

        //        var keySize = 256; // Key size in bits
        //        var key = new byte[keySize / 8]; // Key size in bytes

        //        using (var rng = RandomNumberGenerator.Create())
        //        {
        //            rng.GetBytes(key);
        //        }

        //        //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //        var securityKey = new SymmetricSecurityKey(key);

        //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //        var claims = new[]
        //        {
        //        new Claim(ClaimTypes.NameIdentifier,user.UsersName),
        //        new Claim(ClaimTypes.Role,user.UserLevel)
        //        };
        //        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //            _config["Jwt:Audience"],
        //            claims,
        //            expires: DateTime.Now.AddMinutes(15),
        //            signingCredentials: credentials);
        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //To authenticate user
        private UsersModel Authenticate(UsersModel user)
        {
            var loginuser = _user.GetUserDataWithEmail(user.UsersName);


            byte[] salt = Convert.FromBase64String(loginuser.UserSalt);
            //users.Password = "4BhWQYc9hxavq3UWpAAEZjWrFFkyvmTabWLGZuvlt0E=";
            byte[] bytePassword = Encoding.ASCII.GetBytes(user.Password);

            byte[] passwordConversion = SystemConstants.ComputerMAC256(bytePassword, salt);
            string actualPassword = Convert.ToBase64String(passwordConversion);

            if (loginuser.Password != actualPassword)
            {
                return null;
            }
            //if (user.Status == "Registered" || !user.IsVerified)
            if (loginuser.IsVerified == false)
            {
                return null;
            }
            if (loginuser.Email == user.UsersName && loginuser.Password == actualPassword)
            {
                List<UsersPermissionModel> permissionList = GetUserPermissionByUsersID(user.UsersID);

                if (loginuser.UserLevel == "Customer")
                {
                    HttpContext.Session.SetObject(SystemConstants.Session_PublicCurrentUser, loginuser);
                    HttpContext.Session.SetObject(SystemConstants.Session_PublicUserAccessibleAllModules, permissionList);
                }
                else
                {
                    HttpContext.Session.SetObject(SystemConstants.Session_CurrentUser, loginuser);
                    HttpContext.Session.SetObject(SystemConstants.Session_UserAccessibleMainModules, permissionList);
                }
            }

            var currentUser = loginuser;

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        private List<UsersPermissionModel> GetUserPermissionByUsersID(int UsersID)
        {
            UserPermissionDAL pdata = new UserPermissionDAL();

            List<UsersPermissionModel> permissionList = new List<UsersPermissionModel>();
            permissionList = pdata.Select(UsersID);
            return permissionList;
        }

        #region User Account LogOut
        public async Task<ActionResult> LogOut()
        {
            //await HttpContext.SignOutAsync();

            // Clear session if needed
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        #endregion
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult NotApprove()
        {
            return View();
        }
        public ActionResult WorngPassword()
        {
            return View();
        }
    }
}
