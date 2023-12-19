using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class RegionController : Controller
    {
        RegionDAL _region;
        public RegionController()
        {
            _region = new RegionDAL();
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
                var regions = _region.Select().ToList();
                return View(regions);
            }
            
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
                return View();
            }
        }
        [HttpPost]
        public IActionResult Create(RegionModel region)
        {
            try
            {
                _region.Add(region);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
                var regions = _region.SelectById(id);
                return View(regions);
            }
            
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(RegionModel region)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                _region.Edit(region);
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
                var regions = _region.Select().ToList();
                return View(regions);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(RegionModel region)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                _region.Delete(region.RegionId);
                return RedirectToAction("Index");
            }
        }
    }
}
