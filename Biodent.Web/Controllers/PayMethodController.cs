using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class PayMethodController : Controller
    {
        PayMethodDAL _paymethod;
        public PayMethodController()
        {
            _paymethod = new PayMethodDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var paymethods = _paymethod.Select();
            return View(paymethods);
        }
        public IActionResult Create()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PayMethodModel payMethod)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _paymethod.Insert(payMethod);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var paymethod=_paymethod.SelectById(id);
            return View(paymethod);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PayMethodModel payMethod)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _paymethod.Update(payMethod);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var paymethod = _paymethod.SelectById(id);
            return View(paymethod);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PayMethodModel payMethod)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _paymethod.Delete(payMethod.PayMethodId);
            return RedirectToAction("Index");
        }
    }
}
