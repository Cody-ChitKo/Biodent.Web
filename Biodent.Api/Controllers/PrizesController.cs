using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrizesController : ControllerBase
    {
        UsersDAL _users;
        public PrizesController()
        {
            _users = new UsersDAL();
        }
        [HttpPost("GetPrizes")]
        public List<PrizesModel> GetPrizes(PrizesRequestModel request)
        {
            var prizes = _users.GetPrizes(request.UsersID);
            return prizes;
        }
        [HttpPost("DrawPrize")]
        public string DrawPrize(PrizesRequestModel prizesRequest)
        {
            return "";
        }

    }
}
