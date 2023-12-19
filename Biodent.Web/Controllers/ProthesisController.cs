using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class ProthesisController : Controller
    {
        ProthesisDAL _prothesis;
        public ProthesisController()
        {
            _prothesis = new ProthesisDAL();
        }

        public IActionResult Index()
        {
            var prothes = _prothesis.Select();
            return View(prothes);
        }
        public IActionResult Create()
        {
            ProthesisModel prothesis = new ProthesisModel();
            DepartmentDAL _dept = new DepartmentDAL();
            prothesis.Departments = _dept.Select();
            return View(prothesis);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProthesisModel prothesis)
        {
            if (prothesis == null)
            {
                return BadRequest();
            }
            else
            {
                _prothesis.Insert(prothesis);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            DepartmentDAL _department = new DepartmentDAL();
            ProthesisModel prothesis = new ProthesisModel();
            prothesis = _prothesis.SelectByID(id);
            prothesis.Departments = _department.Select();
            return View(prothesis);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ProthesisModel prothesis)
        {
            if (prothesis != null)
            {
                _prothesis.Edit(prothesis);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string id)
        {
            DepartmentDAL _department = new DepartmentDAL();
            ProthesisModel prothesis = new ProthesisModel();
            prothesis = _prothesis.SelectByID(id);
            prothesis.Departments = _department.Select();
            return View(prothesis);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(ProthesisModel prothesis)
        {
            _prothesis.Delete(prothesis.ProthesisID);
            return RedirectToAction("Index");
        }
    }
}
