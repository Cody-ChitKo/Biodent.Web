using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class ControlController : Controller
    {
        private readonly ControlDAL _control;
        public ControlController()
        {
            _control = new ControlDAL();
        }
        public IActionResult Index()
        {
            var controls = _control.Select().ToList();
            return View(controls);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ControlModel control)
        {
            _control.Insert(control);
            return View(control);
        }
        public IActionResult Edit(string id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ControlModel control)
        {
            return View();
        }
    }
}
