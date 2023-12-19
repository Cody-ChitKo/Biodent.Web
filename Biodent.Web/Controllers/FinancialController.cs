using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class FinancialController : Controller
    {
        FinancialDAL _financial;
        public FinancialController()
        {
            _financial = new FinancialDAL();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IncomeByEachDepartment()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DateTime FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);

            var deptincomeList = _financial.IncomeByEachDepartment(FromDate, ToDate);
            return View(deptincomeList);
        }
        public IActionResult EachCaseType()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DateTime FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);

            var deptincomeList = _financial.EachCaseType(FromDate, ToDate);
            return View(deptincomeList);
        }

        [HttpPost]
        public IActionResult EachCaseType(DateTime FromDate, DateTime ToDate)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var deptincomeList = _financial.EachCaseType(FromDate, ToDate);
            return View(deptincomeList);
        }
        public IActionResult BalanceOrder()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DateTime FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);

            var balanceOrder = _financial.GetBalanceOrder(FromDate, ToDate);

            return View(balanceOrder);
        }
        [HttpPost]
        public IActionResult BalanceOrder(DateTime FromDate, DateTime ToDate)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var balanceOrder = _financial.GetBalanceOrder(FromDate, ToDate);
            return View(balanceOrder);
        }
        public IActionResult PaymentReceipt()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var paymentList = _financial.PaymentReceipt();
            return View(paymentList);
        }
    }
}
