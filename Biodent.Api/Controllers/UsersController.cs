using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UsersDAL _users;
        public UsersController()
        {
            _users = new UsersDAL();
        }
        // GET: api/<MemberController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("GetUserInfo")]
        public UsersModel GetUserInfo([FromBody]UsersRequestModel request)
        {
            try
            {
                var users = _users.GetUsersInfo(request.UsersID);
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        // GET api/<MemberController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
