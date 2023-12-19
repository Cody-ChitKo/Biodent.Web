using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class OrderController : Controller
    {
        UsersDAL _users;
        InvoiceDAL _invoice;
        public OrderController()
        {
            _users = new UsersDAL();
            _invoice = new InvoiceDAL();
        }

        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            var user = _users.GetUsersInfo(currentUser.UsersID);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
               var invList = _invoice.GetInvoicesByUserID(user.UsersID);
                return View(invList);
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
                ProthesisDAL _prothesis = new ProthesisDAL();
                SubProthesisDAL _subprothesis = new SubProthesisDAL();
                DoctorDAL _doctor = new DoctorDAL();

                InvoiceModel invoice = new InvoiceModel();
                invoice.Doctors = _doctor.SelectByUsersID(currentUser.UsersID);
                invoice.invDetail = new List<InvoiceDetailModel>();
                invoice.ProthesisLiv = _prothesis.Select().ToList();
                invoice.SubProthesisLiv = _subprothesis.Select().ToList();

                invoice.invDetail.Add(new InvoiceDetailModel() { InvDetailID = 1 });
                return View(invoice);
            }
        }

        [HttpPost]
        public IActionResult Create(InvoiceModel invoice)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            var user = _users.GetUsersInfo(currentUser.UsersID);
            string InvID = Guid.NewGuid().ToString();
            AutoGenerateNo generateNo = new AutoGenerateNo();
            string InvNo = generateNo.GET_InvoiceNo();
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                try
                {
                    invoice.InvoiceID = InvID;
                    invoice.UsersID = currentUser.UsersID;
                    invoice.InvNo = InvNo;
                    invoice.InvDate = DateTime.Now;
                    invoice.Discount = 0;
                    if (string.IsNullOrEmpty(invoice.Remark))
                    {
                        invoice.Remark = "non";
                    }
                    if (string.IsNullOrEmpty(invoice.Materials))
                    {
                        invoice.Materials = "non";
                    }
                    //data.StartTransaction();
                    _invoice.Insert(invoice);
                    foreach (var invDetails in invoice.invDetail)
                    {
                        invDetails.InvoiceID = InvID;
                        invDetails.Discount = 0;
                        invDetails.CaseType = "New";

                        _invoice.InsertDetail(invDetails);
                    }
                    //data.CommitTransaction();
                    return RedirectToAction("Index");
                }
                catch
                {
                    // data.RollbackTransaction();
                    return RedirectToAction("Index");
                }
            }
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"ToothSimpleFile", special + '-' + file.FileName);

            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            var filename = special + "-" + file.FileName;
            //return $"{filename}";
            return Path.Combine(@"ToothSimpleFile", filename);
        }
        public IActionResult BalanceOrder()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            var user = _users.GetUsersInfo(currentUser.UsersID);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var invList = _invoice.GetBalanceOrderByUserID(user.UsersID);
                return View(invList);
            }
        }
        public ActionResult Details(string id)
        {
            var invoice = _invoice.SelectByInvId(id);
            invoice.invDetail = _invoice.SelectDetailsByInvID(id);
            return View(invoice);
        }

        
    }
}
