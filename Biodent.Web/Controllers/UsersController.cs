using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Biodent.Web.Controllers
{
    public class UsersController : Controller
    {
        UsersDAL _users;
        public UsersController()
        {
            _users = new UsersDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var user = _users.GetAllClinic();
                return View(user);
            }
           
        }
        public IActionResult Create()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(UsersModel users)
        {
            UsersModel user = _users.CheckUser(users.PhoneNo);
            if (user.PhoneNo != null)
            {
                return RedirectToAction("UserExists", "Register");
            }
            users.CreatedDate = DateTime.Now.Date;
            int status;
            string VerificationCode = Guid.NewGuid().ToString();

            var UserSalt = SystemConstants.GenerateSalt();
            var hmac = SystemConstants.ComputerMAC256(data: Encoding.UTF8.GetBytes(users.Password), UserSalt);

            users.AccountLevel_Id = 0;
            users.Password = Convert.ToBase64String(hmac);
            users.UserSalt = Convert.ToBase64String(UserSalt);
            users.VerificationCode = VerificationCode;
            users.IsVerified = false;

            status = _users.InsertFromAdmin(users);

            if (status == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("EmailError", "Register");
            }
        }

        public IActionResult Edit(int id)
        {
            var users = _users.GetUsersInfo(id);
            return View(users);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(UsersModel users)
        {
            int status = 0;
            status = _users.UpdateFromAdmin(users);
            if (status == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error","Home");
            }
        }
        public IActionResult Delete(int id)
        {
            var users = _users.GetUsersInfo(id);
            return View(users);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(UsersModel users)
        {
            int status = 0;
            status = _users.DeleteFromAdmin(users);
            if (status == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
