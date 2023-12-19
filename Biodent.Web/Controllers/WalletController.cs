using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace Biodent.Web.Controllers
{
    public class WalletController : Controller
    {
        WalletDAL _wallet;
        public WalletController()
        {
            _wallet = new WalletDAL();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddWallet()
        {
            WalletModel wallte = new WalletModel();
            PayMethodDAL _paymethod = new PayMethodDAL();
            wallte.PayMethods = _paymethod.Select();
            return View(wallte);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWallet(WalletModel wallet)
        {
            var user = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            string imageUrl = await UploadImage(wallet.ss_Image);
            byte[] imageArray = System.IO.File.ReadAllBytes(imageUrl);
            //byte[] imageArray = System.IO.File.ReadAllBytes(@"image file path");
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            wallet.ss_ImageUrl = base64ImageRepresentation;

            wallet.UsersId = user.UsersID;
            _wallet.Save(wallet);
            return RedirectToAction("WalletHistory");
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"WalletImages", special + '-' + file.FileName);

            using (MagickImage image = new MagickImage(file.OpenReadStream()))
            {
                // Resize the image to a specific width and height
                image.Resize(300, 350);
                image.Quality = 50;
                // Save the resized image to the specified file path
                image.Write(filePath);
            }

            return Path.Combine(@"WalletImages", special + '-' + file.FileName);


            //using (FileStream ms = new FileStream(filePath, FileMode.Create))
            //{

            //    await file.CopyToAsync(ms);
            //}
            //var filename = special + "-" + file.FileName;

            ////return $"{filename}";
            //return Path.Combine(@"WalletImages", filename);
        }
        public byte[] ReduceImageSize(byte[] imageBytes, int jpegQuality)
        {
            using var inputStream = new MemoryStream(imageBytes);
            var image = System.Drawing.Image.FromStream(inputStream);
            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (int)jpegQuality);

            using var outputStream = new MemoryStream();
            image.Save(outputStream, jpegEncoder, encoderParameters);
            return outputStream.ToArray();
        }

        public IActionResult WalletHistory()
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            var wallets = _wallet.GetWalletByUsersID(currentUser.UsersID);
            return View(wallets);
        }

        public IActionResult WalletApprove(int WalletId, int usersId)
        {
            var user = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

            WalletModel wallet = new WalletModel();
            wallet = _wallet.GetWalletById(WalletId);

            if (user != null)
            {
                _wallet.WalletApprove(WalletId, usersId, wallet.WalletAmount, user.UsersID);

                //Insert Hostory
                TransactionHistoryDAL _transaction = new TransactionHistoryDAL();
                HistoryModel history = new HistoryModel();
                history.TranDesc = wallet.WalletAmount.ToString() + " - Approved from Admin";
                history.TranDate = DateTime.Now.Date;
                history.UsersId = wallet.UsersId;

                _transaction.Insert(history);

            }
            return RedirectToAction("WalletNewList");
        }
        public IActionResult WalletNewList()
        {
            var wallets = _wallet.GetNewWallet();
            return View(wallets);
        }
    }
}
