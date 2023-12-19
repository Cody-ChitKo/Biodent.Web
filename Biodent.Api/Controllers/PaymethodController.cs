using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymethodController : ControllerBase
    {
        PayMethodDAL _paymethod;
        public PaymethodController()
        {
            _paymethod = new PayMethodDAL();
        }

        [HttpGet("GetPaymethods")]
        public IEnumerable<PayMethodModel> GetPaymethods()
        {
            var paymethods = _paymethod.Select();
            return paymethods;
        }
    }
}
