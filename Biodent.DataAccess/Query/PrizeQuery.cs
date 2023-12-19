using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class PrizeQuery
    {
        private string query;
        public string GetActivePrize()
        {
            query = "SELECT * FROM tbl_prizes WHERE IsActive = 1 AND UsersId = @UsersId";
            return query;
        }
        public string UpdatePrize()
        {
            query = "UPDATE tbl_prizes SET UseAmount = UseAmount+@UseAmount WHERE PrizeId = @PrizeId AND UsersId = @UsersId AND IsActive = 1";
            return query;
        }
        public string OpenPrize()
        {
            query = "UPDATE tbl_prizes SET CanWithdraw = 1 WHERE PrizeId = @PrizeId AND UseAmount = @PKAmount;";
            return query;
        }
    }
}
