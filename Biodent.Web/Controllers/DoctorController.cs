using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Biodent.Web.Controllers
{
    public class DoctorController : Controller
    {
        DoctorDAL _doctor;
        public DoctorController()
        {
            _doctor = new DoctorDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            var doctors = _doctor.SelectByUsersID(currentUser.UsersID);
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorModel doctor)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            doctor.UsersID = currentUser.UsersID;
            _doctor.Insert(doctor);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var doctor = _doctor.SelectByID(id);
            return View(doctor);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(DoctorModel doctor)
        {
            _doctor.Edit(doctor);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var doctor = _doctor.SelectByID(id);
            return View(doctor);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(DoctorModel doctor)
        {
            _doctor.Delete(doctor.DoctorID);
            return RedirectToAction("Index");
        }
    }
}
