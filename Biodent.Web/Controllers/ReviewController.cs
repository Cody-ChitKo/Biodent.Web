using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class ReviewController : Controller
    {
        ReviewDAL _review;
        public ReviewController()
        {
            _review = new ReviewDAL();
        }
        public IActionResult Index()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var reviewList = _review.SelectByUsersID(currentUser.UsersID);
                return View(reviewList);
            }
        }
        public IActionResult Create(string InvoiceID, string InvNo)
        {
            ReviewModel review = new ReviewModel();
            review.InvoiceID = InvoiceID;
            review.InvNo = InvNo;
            return View(review);
        }
        [HttpPost]

        public IActionResult Create(ReviewModel review)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);

            review.UsersID = currentUser.UsersID;
            review.ReviewDate = DateTime.Now.Date;
            _review.Insert(review);
            return RedirectToAction("Index");
        }
    
        public IActionResult ReviewList()
        {
            var reviewList = _review.Select();
            return View(reviewList);
        }
    }
}
