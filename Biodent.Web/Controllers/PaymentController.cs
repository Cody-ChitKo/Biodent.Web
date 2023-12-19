using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class PaymentController : Controller
    {
        PaymentDAL _payment;
        public PaymentController() 
        {
            _payment = new PaymentDAL();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(string id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            InvoiceDAL _invoice = new InvoiceDAL();
            AutoGenerateNo generateNo = new AutoGenerateNo();
            var invoices = _invoice.SelectByInvId(id);
            PaymentModel payment = new PaymentModel();
            payment.InvoiceID = invoices.InvoiceID;
            payment.InvNo = invoices.InvNo;
            payment.NetAmount = invoices.NetAmount;
            payment.PayNo = generateNo.GET_PaymentNo();
            return View(payment);
        }

        [HttpPost]
        public IActionResult Create(PaymentModel payment)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _payment.Insert(payment);
            return RedirectToAction("");
        }


        public IActionResult PaymentHistory()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            //var user = _users.GetUsersInfo(currentUser.UsersID);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                
                var payList = _payment.SelectByUserID(currentUser.UsersID);
                return View(payList);
            }
        }
    }
}
