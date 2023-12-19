using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Web.Controllers
{
    public class GiftCardController : Controller
    {
        GiftCardDAL _giftcard;
        public GiftCardController()
        {
            _giftcard = new GiftCardDAL();
        }
        public IActionResult Index()
        {
            var giftcards = _giftcard.GetAll();
            return View(giftcards);
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
                GiftCardModel giftCard = new GiftCardModel();
                PackageDAL _package = new PackageDAL();
                giftCard.Packages = _package.Select();
                return View(giftCard);
            }            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GiftCardModel giftCard)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (giftCard == null)
            {
                return BadRequest();
            }
            else
            {
                string imagepath = await UploadImage(giftCard.GiftCardImage);
                byte[] imageArray = System.IO.File.ReadAllBytes(imagepath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                giftCard.GiftCardImageUrl = base64ImageRepresentation;

                _giftcard.Insert(giftCard);
                return RedirectToAction("Index");
            }
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"GiftCardImages", special + '-' + file.FileName);

            using (MagickImage image = new MagickImage(file.OpenReadStream()))
            {
                image.Resize(300, 350);
                image.Quality = 50;
                image.Write(filePath);
            }

            return Path.Combine(@"GiftCardImages", special + '-' + file.FileName);
        }

        public IActionResult GiftCardView()
        {
            var giftcards = _giftcard.GetAll();
            return View(giftcards);
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
                GiftCardModel giftCard = new GiftCardModel();
                PackageDAL _package = new PackageDAL();
                giftCard = _giftcard.GetById(id);
                giftCard.Packages = _package.Select().ToList();
                return View(giftCard);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(GiftCardModel giftCard)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string imagepath = await UploadImage(giftCard.GiftCardImage);
                byte[] imageArray = System.IO.File.ReadAllBytes(imagepath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                giftCard.GiftCardImageUrl = base64ImageRepresentation;
                _giftcard.Update(giftCard);
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
                var giftcard = _giftcard.GetById(id);
                return View(giftcard);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(GiftCardModel giftCard)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);
            if (currentUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            _giftcard.Delete(giftCard.GiftCardId);
            return RedirectToAction("Index");
        }
    }
}
