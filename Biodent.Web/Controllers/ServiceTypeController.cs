using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class ServiceTypeController : Controller
    {
        readonly ServiceTypeDAL _serviceTypes;

        public ServiceTypeController()
        {
            _serviceTypes = new ServiceTypeDAL();
        }

        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var servicetypes = _serviceTypes.Select(0);
            return View(servicetypes);
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

        // POST: ServiceType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceTypeModel servicet)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                _serviceTypes.Insert(servicet);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: ServiceType/Edit/5
        public IActionResult Edit(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ServiceTypeModel serviceType = new ServiceTypeModel();
            serviceType = _serviceTypes.SelectByID(id);
            return View(serviceType);
        }

        // POST: ServiceType/Edit/5
        [HttpPost]
        public IActionResult Edit(ServiceTypeModel servicet)
        {
            try
            {
                _serviceTypes.Update(servicet);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceType/Delete/5
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ServiceTypeModel serviceType = new ServiceTypeModel();
            serviceType = _serviceTypes.SelectByID(id);
            return View(serviceType);
        }

        // POST: ServiceType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _serviceTypes.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
