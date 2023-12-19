using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotiController : ControllerBase
    {
        NotiDAL _noti;

        public NotiController() 
        {
            _noti = new NotiDAL();
        }
        //GetNotis
        [HttpPost("GetNotis")]
        public IEnumerable<NotiModel> GetNotis([FromBody] NotiRequestModel request)
        {
            var notis = _noti.GetNotis(request.UsersID);
            return notis;
        }
    }
}
