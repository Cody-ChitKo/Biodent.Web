using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        PackageDAL _package;
        TransactionHistoryDAL _transaction;
        public PackagesController()
        {
            _package = new PackageDAL();
            _transaction = new TransactionHistoryDAL();
        }


        [HttpGet("GetPackages")]
        public IEnumerable<PackageModel> GetPackages()
        {
            var packages = _package.Select();
            return packages;
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("BuyPackage")]
        public void BuyPackage([FromBody] PackageRequestModel package)
        {
            UsersDAL _users = new UsersDAL();
            var user = _users.GetUsersInfo(package.UsersID);
            var pacakgeInfo = _package.SelectByID(package.PackageId);
            DateTime expiredate = DateTime.Now.AddMonths(+3);
            _package.BuyPackage(user.UsersID, package.PackageId, pacakgeInfo.PKPrice, pacakgeInfo.GetPKAmount, expiredate);

            //Insert Hostory
            HistoryModel history = new HistoryModel();
            history.TranDesc = pacakgeInfo.PKPrice.ToString() + " - " + pacakgeInfo.PackageName + " Purchase Success";
            history.TranDate = DateTime.Now.Date;
            history.UsersId = package.UsersID;

            _transaction.Insert(history);

        }
    }
}
