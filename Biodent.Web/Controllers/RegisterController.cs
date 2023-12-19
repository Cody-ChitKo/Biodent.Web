using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Biodent.Web.Controllers
{
    public class RegisterController : Controller
    {
        UsersDAL _user;
        public RegisterController()
        {
            _user = new UsersDAL();
        }
        public IActionResult Index()
        {

            UsersModel user = new UsersModel();
            RegionDAL _region = new RegionDAL();
            user.RegionList = _region.Select().ToList();

            return View(user);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UsersModel user)
        {
            UsersModel users = _user.CheckUser(user.PhoneNo);
            if(users.PhoneNo != null)
            {
                return RedirectToAction("UserExists", "Register");
            }
            else
            {
                user.CreatedDate = DateTime.Now.Date;
                int status;
                string VerificationCode = Guid.NewGuid().ToString();

                var UserSalt = SystemConstants.GenerateSalt();
                var hmac = SystemConstants.ComputerMAC256(data: Encoding.UTF8.GetBytes(user.Password), UserSalt);

                user.UserLevel = "Customer";
                user.AccountLevel_Id = 0;
                user.Password = Convert.ToBase64String(hmac);
                user.UserSalt = Convert.ToBase64String(UserSalt);
                user.VerificationCode = VerificationCode;
                user.IsVerified = false;

                status = _user.Insert(user);

                if (status == 1)
                {
                    //var activateURL = "Register/ActivateAccount/" + VerificationCode;
                    //var activateURL = "";
                    //var activateLink = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{activateURL}";
                    //EmailVerification.SendVerificationLink(customer.Email, activateLink);

                    return RedirectToAction("Successful", "Register");
                }
                else
                {
                    return RedirectToAction("EmailError", "Register");
                }
            }
            
        }

        public IActionResult UserExists()
        {
            return View();
        }
        public IActionResult Successful()
        {
            return View();
        }
        public IActionResult EmailError()
        {
            return View();
        }
    }
}
