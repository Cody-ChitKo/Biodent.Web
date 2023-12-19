using Biodent.Api.Common;
using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        PaymentDAL _dataAccess;
        public PaymentController()
        {
            _dataAccess = new PaymentDAL();
        }

        // GET: api/<PaymentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("Insert")]
        public void Insert([FromBody] PaymentModel payment)
        {
            _dataAccess.InsertFromMobile(payment);
            //return "Success";
        }

        [HttpPost("OtherPayment")]
        public void OtherPayment([FromBody] PaymentRequestModel request)
        {
            PaymentModel paymentModel = new PaymentModel();
            AutoGenerateNo autoGenerate = new AutoGenerateNo();
            paymentModel.PayNo = autoGenerate.GET_PaymentNo();
            paymentModel.PayDate = request.PayDate;
            paymentModel.PayAmount = request.PayAmount;
            paymentModel.UsersID = request.UsersID;

            if (request.PKAmount != 0)
            {
                _dataAccess.UpdatePrizesForOpen(request.UsersID, request.PayAmount);
            }
            _dataAccess.OtherPayment(paymentModel);
        }

        [HttpPost("GetAll")]
        public IEnumerable<PaymentModel> GetAll(int userid)
        {
            var payments = _dataAccess.SelectByUserID(userid);
            return payments;
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
