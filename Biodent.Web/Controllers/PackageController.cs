using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class PackageController : Controller
    {
        PackageDAL _package;
        TransactionHistoryDAL _transaction;
        public PackageController()
        {
            _package = new PackageDAL();

        }
        public IActionResult Index()
        {
            var packages = _package.Select();
            return View(packages);
        }
        public IActionResult Create()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if(currentUser == null)
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
        public async Task<IActionResult> Create(PackageModel package)
        {

            if (package == null)
            {
                return BadRequest();
            }
            else
            {
                var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

                _transaction = new TransactionHistoryDAL();

                string imagepath = await UploadImage(package.PKImage);
                byte[] imageArray = System.IO.File.ReadAllBytes(imagepath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                package.PKImageUrl = base64ImageRepresentation;

                HistoryModel history = new HistoryModel();
                history.TranDesc = package.PKPrice.ToString() + " - " + package.Description;
                history.TranDate = DateTime.Now.Date;
                history.UsersId = currentUser.UsersID;

                _package.Save(package);
                _transaction.Insert(history);

                return RedirectToAction("Index");
            }
            
        }

        public IActionResult BuyPackage(int PackageId)
        {
            if (PackageId == 0)
            {
                return BadRequest();
            }
            else
            {
                _transaction = new TransactionHistoryDAL();
                var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
                UsersDAL _users = new UsersDAL();
                var user = _users.GetUsersInfo(currentUser.UsersID);
                var pacakgeInfo = _package.SelectByID(PackageId);

                if (user.WalletAmount < pacakgeInfo.PKPrice)
                {
                    return RedirectToAction("PackageError");
                }
                else
                {
                    
                    DateTime expiredate = DateTime.Now.AddMonths(+3);
                    _package.BuyPackage(user.UsersID, PackageId, pacakgeInfo.PKPrice, pacakgeInfo.GetPKAmount, expiredate);
                    
                    HistoryModel history = new HistoryModel();
                    history.TranDesc = pacakgeInfo.PKPrice.ToString() + " - " + pacakgeInfo.Description;
                    history.TranDate = DateTime.Now.Date;
                    history.UsersId = currentUser.UsersID;

                    _transaction.Insert(history);


                    return RedirectToAction("Successful", "Message");
                }
            }
            
        }
        public IActionResult PackageError()
        {
            return View();
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"PackageImages", special + '-' + file.FileName);

            using (MagickImage image = new MagickImage(file.OpenReadStream()))
            {
                image.Resize(300, 350);
                image.Quality = 50;
                image.Write(filePath);
            }

            return Path.Combine(@"PackageImages", special + '-' + file.FileName);
        }

        public IActionResult PackageView()
        {
            var packages = _package.Select();
            return View(packages);
        }
        public IActionResult Edit(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var package = _package.SelectByID(id);
                return View(package);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(PackageModel package)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

            _transaction = new TransactionHistoryDAL();

            string imagepath = await UploadImage(package.PKImage);
            byte[] imageArray = System.IO.File.ReadAllBytes(imagepath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            package.PKImageUrl = base64ImageRepresentation;

            _package.Update(package);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var package = _package.SelectByID(id);
                return View(package);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(PackageModel package)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                _package.Delete(package.PackageId);
                return RedirectToAction("Index");
            }
        }
    }
}
