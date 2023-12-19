using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        PurchaseDAL _purchase;
        public PurchaseController()
        {
            _purchase = new PurchaseDAL();
        }


        [HttpPost("GetPurchases")]
        public IEnumerable<PurchaseModel> GetPurchases([FromBody] PurchaseRequestModel purchaseRequest)
        {
            var purchases = _purchase.GetByUsersID(purchaseRequest.UsersId);
            return purchases;
        }
        [HttpPost("GetPurchasesByType")]
        public IEnumerable<PurchaseModel> GetPurchasesByType(string PurchaseType)
        {
            var purchases = _purchase.GetByType(PurchaseType);
            return purchases;
        }
    }
}
