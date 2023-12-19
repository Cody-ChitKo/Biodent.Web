using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Relational;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Biodent.Web.Controllers
{
    public class InvoiceController : Controller
    {
        InvoiceDAL _invoice;
        public InvoiceController() 
        {
            _invoice = new InvoiceDAL();
        }
        public IActionResult Index()
        {
            //var user = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            //var invList = _invoice.GetInvoicesByUserID(user.UsersID);
            //return View(invList);
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult NewOrders()
        {
            var orders=_invoice.GetNewOrders();
            return View(orders);
        }
        public IActionResult CreateAdmin()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ProthesisDAL _prothesis = new ProthesisDAL();
                SubProthesisDAL _subprothesis = new SubProthesisDAL();
                DoctorDAL _doctor = new DoctorDAL();
                UsersDAL _users = new UsersDAL();

                InvoiceModel invoice = new InvoiceModel();
                invoice.Users = _users.GetAllClinic();
                invoice.Doctors = _doctor.Select();
                invoice.invDetail = new List<InvoiceDetailModel>();
                invoice.ProthesisLiv = _prothesis.Select().ToList();
                invoice.SubProthesisLiv = _subprothesis.Select().ToList();
                
                invoice.invDetail.Add(new InvoiceDetailModel() { InvDetailID = 1 });
                return View(invoice);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateAdmin(InvoiceModel invoice)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
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
        public IActionResult Details(string id)
        {
            //ControllerBase controller = new ControllerBase();
            //CurrentControl = controller.GetControl(Url_Name);

            //CurrentUser = (Users)Session[SystemConstants.Session_CurrentUser];
            //UserPermissionList = (List<UserPermission>)Session[SystemConstants.Session_UserAccessibleMainModules];

            //if (CurrentUser == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else
            //{
            //    #region Full Access
            //    if (UserPermissionList == null)
            //    {
            //        return RedirectToAction("Index", "AccessDenied");
            //    }
            //    var PageAccess = UserPermissionList.Find(x => x.Control_Id == CurrentControl.Control_Id && x.UserLevel_Id == CurrentUser.UserLevel_ID);
            //    if (PageAccess == null)
            //    {
            //        return RedirectToAction("Index", "AccessDenied");
            //    }
            //    if ((PageAccess.Approve == false || PageAccess.Approve == null) && (PageAccess.Full_Access == false || PageAccess.Full_Access == null))
            //    {
            //        return RedirectToAction("Index", "AccessDenied");
            //    }
            //    #endregion
                InvoiceModel invoice = new InvoiceModel();
                invoice = _invoice.SelectByInvId(id);
                invoice.invDetail = _invoice.SelectDetailsByInvID(id);

                return View(invoice);
            //}
        }
        #region Approve Pending Reject
        [HttpPost]
        public IActionResult Details(InvoiceModel invoice)
        {
            //ControllerBase controller = new ControllerBase();
            //CurrentControl = controller.GetControl(Url_Name);

            //CurrentUser = (Users)Session[SystemConstants.Session_CurrentUser];
            //UserPermissionList = (List<UserPermission>)Session[SystemConstants.Session_UserAccessibleMainModules];

            //if (CurrentUser == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else
            //{
            //    #region Full Access
            //    var PageAccess = UserPermissionList.Find(x => x.Control_Id == CurrentControl.Control_Id && x.UserLevel_Id == CurrentUser.UserLevel_ID);
            //    if (PageAccess == null || UserPermissionList == null)
            //    {
            //        return RedirectToAction("Index", "AccessDenied");
            //    }
            //    if ((PageAccess.Approve == false || PageAccess.Approve == null) && (PageAccess.Full_Access == false || PageAccess.Full_Access == null))
            //    {
            //        return RedirectToAction("Index", "AccessDenied");
            //    }
            //    #endregion

            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

            invoice.ApproveUserID = currentUser.UsersID;
            invoice.ApproveDate = DateTime.Now.Date;
            _invoice.Approve(invoice);
            return RedirectToAction("NewOrders", "Invoice");
            //}
        }
        
        [HttpGet]
        public IActionResult Approve(InvoiceModel invoice)
        {
            //CurrentUser = (Users)Session[SystemConstants.Session_CurrentUser];
            //UserPermissionList = (List<UserPermission>)Session[SystemConstants.Session_UserAccessibleMainModules];
            //if (CurrentUser == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else
            //{
            //    invoice.ApproveUserID = CurrentUser.UsersID;
            //    invoice.ApproveDate = DateTime.Now.Date;

            //    data.Approve(invoice);
            //    return RedirectToAction("NewOrder", "Invoice");
            //}
            return RedirectToAction("NewOrders", "Invoice");
        }
        [HttpGet]
        public ActionResult Pending(InvoiceModel invoice)
        {
            return RedirectToAction("NewOrder", "Invoice");
            //CurrentUser = (Users)Session[SystemConstants.Session_CurrentUser];
            //UserPermissionList = (List<UserPermission>)Session[SystemConstants.Session_UserAccessibleMainModules];
            //if (CurrentUser == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else
            //{
            //    invoice.ApproveUserID = CurrentUser.UsersID;
            //    invoice.ApproveDate = DateTime.Now.Date;

            //    data.Pending(invoice);
            //    return RedirectToAction("NewOrder", "Invoice");
            //}
        }
        [HttpGet]
        public ActionResult Reject(InvoiceModel invoice)
        {
            return RedirectToAction("NewOrders", "Invoice");
            //CurrentUser = (Users)Session[SystemConstants.Session_CurrentUser];
            //UserPermissionList = (List<UserPermission>)Session[SystemConstants.Session_UserAccessibleMainModules];
            //if (CurrentUser == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else
            //{
            //    invoice.ApproveUserID = CurrentUser.UsersID;
            //    invoice.ApproveDate = DateTime.Now.Date;

            //    data.Reject(invoice);
            //    return RedirectToAction("NewOrder", "Invoice");
            //}
        }
        #endregion


        public IActionResult ProcessOrder()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<InvoiceModel> invList = new List<InvoiceModel>();
            invList = _invoice.GetProcessOrders();
            return View(invList);
        }
        
        public IActionResult Delivered(string id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DateTime deliDate = DateTime.Now;
            _invoice.Delivered(id, deliDate);

            return RedirectToAction("ProcessOrder");
        }

        public IActionResult DeliOrders()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<InvoiceModel> invList = new List<InvoiceModel>();
            invList = _invoice.GetDeliveringOrders();
            return View(invList);
        }

        public IActionResult ReceivedOrders()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var invList = _invoice.GetReceivedOrders();
            return View(invList);
        }

        public IActionResult Received(string id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DateTime ReceiveDate = DateTime.Now;
            _invoice.Received(id, ReceiveDate);
            return RedirectToAction("DeliOrders");
        }
        public IActionResult CloseOrders()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var invList = _invoice.GetClosedOrders();
            return View(invList);
        }
        public IActionResult Closed(string id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DateTime CloseDate = DateTime.Now;
            _invoice.Closed(id, CloseDate);
            return RedirectToAction("ReceivedOrders");
        }
        public IActionResult CompletedOrders()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            DateTime FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ToDate = FromDate.AddMonths(1).AddDays(-1);
            var data = _invoice.GetCompletedOrders(FromDate, ToDate);
            return View(data);
        }

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        public IActionResult ToothImage(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "Files/"));

            //Copy File names to Model collection.
            List<ToothSimpleModel> files = new List<ToothSimpleModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ToothSimpleModel { ImageName = Path.GetFileName(filePath) });
            }

            return View(files);

            //string[] filePaths = Directory.GetFiles(Server.MapPath("~/ToothSimpleFile/"));

            ////Copy File names to Model collection.
            //List<ToothSimple> files = new List<ToothSimple>();
            //foreach (string filePath in filePaths)
            //{
            //    files.Add(new ToothSimple { ImageName = Path.GetFileName(filePath) });
            //}

            //return View(files);
        }
        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
