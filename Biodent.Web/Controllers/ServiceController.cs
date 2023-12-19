using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class ServiceController : Controller
    {
        ServiceDAL _service;
        public ServiceController()
        {
            _service = new ServiceDAL();
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
                var services = _service.Select(0).ToList();
                return View(services);
            }
        }
        public IActionResult ServiceView()
        {
            var services = _service.Select(0).ToList();
            return View(services);
        }
        public IActionResult Create()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ServiceModel service = new ServiceModel();
                ServiceTypeDAL _serviceType = new ServiceTypeDAL();
                service.ServiceTypes = _serviceType.Select(0).ToList();
                return View(service);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceModel service)
        {
            string imagepath = await UploadImage(service.ServiceImage);

            byte[] imageArray = System.IO.File.ReadAllBytes(imagepath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            service.ImagePathUrl = base64ImageRepresentation;
            _service.Insert(service);

            //NotiModel noti = new NotiModel();

            //noti.NotiTitleEng = service.ServiceType;
            //noti.NotiMsgEng = service.ServiceDescription;
            //noti.NotiType = 1;
            //noti.IsSeen = false;

            return RedirectToAction("Index");
        }
        public IActionResult Update()
        {
            return View();
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"ServiceImages", special + '-' + file.FileName);

            using (MagickImage image = new MagickImage(file.OpenReadStream()))
            {
                image.Resize(300, 350);
                image.Quality = 50;
                image.Write(filePath);
            }

            return Path.Combine(@"ServiceImages", special + '-' + file.FileName);
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
                ServiceModel service = new ServiceModel();
                service = _service.SelectByID(id);
                ServiceTypeDAL _serviceType = new ServiceTypeDAL();
                service.ServiceTypes = _serviceType.Select(0).ToList();

                return View(service);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(ServiceModel service)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string imagepath = await UploadImage(service.ServiceImage);

                byte[] imageArray = System.IO.File.ReadAllBytes(imagepath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                service.ImagePathUrl = base64ImageRepresentation;

                _service.Update(service);
                return RedirectToAction("Index");
            }
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
                ServiceModel service = new ServiceModel();
                service = _service.SelectByID(id);
                ServiceTypeDAL _serviceType = new ServiceTypeDAL();
                service.ServiceTypes = _serviceType.Select(0).ToList();

                return View(service);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(ServiceModel service)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                _service.Delete(service.ServiceId);
                return RedirectToAction("Index");
            }
        }
    }
}
