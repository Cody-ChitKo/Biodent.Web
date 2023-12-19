using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        ServiceDAL _service;
        public ServicesController()
        {
            _service = new ServiceDAL();
        }


        [HttpGet("GetServices")]
        public IEnumerable<ServiceModel> GetServices()
        {
            var services = _service.Select(0);
            return services;
        }

        [HttpPost("BuyServices")]
        public string BuyServices([FromBody] ServiceRequestModel service)
        {
            PurchaseDAL _purchase = new PurchaseDAL();
            PurchaseModel purchase = new PurchaseModel();

            purchase.PurchaseType = "Service";
            purchase.Pur_Amount = service.ServicePrice;
            purchase.Pur_Desc = "Buy " + service.ServiceHeader;
            purchase.Pur_Date = DateTime.Now.Date;
            purchase.UsersId = service.UsersId;
            purchase.InvNo = service.InvNo;

            return _purchase.Insert(purchase);
        }

        
    }
}
