using Biodent.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index(ShowMessageModel message)
        {
            ViewBag.Title = message.Title;
            message.Message = message.Message;
            return View(message);
        }
        public IActionResult Successful()
        {
            return View();
        }
        public IActionResult Unsuccessful()
        {
            return View();
        }
    }
}
