using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class UserPermissionController : Controller
    {
        UsersDAL _users;
        UserPermissionDAL _permission;
        ControlDAL _control;
        public UserPermissionController()
        {
            _users = new UsersDAL();
            _permission = new UserPermissionDAL();
            _control = new ControlDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var users = _users.GetAdminList();
            return View(users);
        }

        public IActionResult ControlMenu(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            UsersPermissionModel permission = new UsersPermissionModel();
            permission.Controls = _control.Select();
            permission.Users = _users.GetUsersInfo(id);
            return View(permission);
        }

        [HttpPost]
        public IActionResult Update(UsersPermissionModel permission)
        {

            return RedirectToAction("Index");
        }
        public IActionResult PermissionDetails(string control_id, int usersId)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            UsersPermissionModel permission = new UsersPermissionModel();

            permission = _permission.SelectByControlID(control_id, usersId);

            return View(permission);
        }
    }
}
