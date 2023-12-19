using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        InvoiceDAL _invoice;
        public OrdersController()
        {
            _invoice = new InvoiceDAL();
        }
        [HttpPost("GetOrderHistory")]
        public BalanceOrderModel GetOrderHistory(OrderRequestModel orderRequest)
        {
            var invoices = _invoice.GetBalanceOrderByUserID(orderRequest.UsersID);
            return invoices;
        }
    }
}
