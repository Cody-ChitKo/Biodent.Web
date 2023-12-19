using Biodent.Api.Models;
using Biodent.DataAccess;
using Biodent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biodent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        TransactionHistoryDAL _transaction;
        public HistoriesController() 
        {
            _transaction = new TransactionHistoryDAL();
        }
        [HttpPost("GetHistories")]
        public List<HistoryModel> GetHistories( HistoryRequestModel obj)
        {
            var histories = _transaction.SelectByUsersID(obj.UsersID);
            return histories;
        }
    }
}
