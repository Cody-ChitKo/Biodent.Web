using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class CustomerController : Controller
    {
        UsersDAL _user;
        public CustomerController()
        {
            _user = new UsersDAL();
        }
        public IActionResult Index()
        {
            var newclinic = _user.GetAllClinic();
            return View(newclinic);
        }
        public IActionResult UserProfile()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            var user = _user.GetUsersInfo(currentUser.UsersID);
            return View(user);
        }
        public IActionResult PrizeOfCustomer()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            var prizes = _user.GetPrizes(currentUser.UsersID);
            return View(prizes);
        }

        public IActionResult DrawPrize(int PrizeId)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            PrizesModel prize = new PrizesModel();
            prize.UsersId = currentUser.UsersID;
            prize.PrizeId = PrizeId;
            prize.WithdrawDate = DateTime.Now.Date;
            int isSuccess = 0;
            isSuccess = _user.WithDrawPrizes(prize);
            if(isSuccess == 1)
            {
                _user.UnActivePrize(prize);
                return RedirectToAction("WithdreawSuccess");
            }
            else
            {
                return RedirectToAction("WithdrawUnsuccess");
            }
            
        }
        public IActionResult WithdrawUnsuccess()
        {
            return View();
        }
        public IActionResult WithdreawSuccess()
        {
            return View();
        }
        public IActionResult NewClinic()
        {
            var newclinic = _user.GetNewClinic();
            return View(newclinic);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ChangePassword(UsersModel users)
        {
            if (users == null)
            {

            }
            else
            {
                
            }
            return View("UserProfile");
        }

        public IActionResult Edit(int id)
        {
            RegionDAL _region = new RegionDAL();
            UsersModel users = new UsersModel();
            users = _user.GetUsersInfo(id);
            users.RegionList = _region.Select().ToList();

            return View(users);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(UsersModel users)
        {
            _user.UpdateFromAdmin(users);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var users= _user.GetUsersInfo(id);
            return View(users);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(UsersModel users)
        {
            _user.DeleteFromAdmin(users);
            return RedirectToAction("Index");
        }

        public IActionResult Approve(int id, string Email)
        {
            _user.Approve(id);
            return RedirectToAction("NewClinic");
        }
        public IActionResult Reject(int id)
        {
            _user.Reject(id);
            return View("NewClinic");
        }

    }
}
