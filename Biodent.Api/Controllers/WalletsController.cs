using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private WalletDAL _wallets;
        public WalletsController()
        {
            _wallets = new WalletDAL();
        }

        [HttpPost("GetWallets")]
        public IEnumerable<WalletModel> GetWallets(int UsersId)
        {
            var walltes = _wallets.GetWalletByUsersID(UsersId);
            return walltes;
        }

        [HttpPost("AddWallets")]
        public string AddWallets(WalletRequestModel wallet)
        {
            WalletModel walletdt = new WalletModel();

            walletdt.UsersId = wallet.UsersID;
            walletdt.WalletAmount = wallet.WalletAmount;
            walletdt.AddDate = DateTime.Now.Date;
            walletdt.PayMethodId = wallet.PayMethodId;
            walletdt.ss_ImageUrl = wallet.ss_Image;

            try
            {
                //Insert Hostory
                TransactionHistoryDAL _transaction = new TransactionHistoryDAL();
                HistoryModel history = new HistoryModel();
                history.TranDesc = wallet.WalletAmount.ToString() + " - Add from User "+ wallet.UsersID;
                history.TranDate = DateTime.Now.Date;
                history.UsersId = wallet.UsersID;

                _transaction.Insert(history);

                _wallets.Save(walletdt);
                return "Successful";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
