using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class PrizesDAL:DataControllerBase
    {
        PrizeQuery query;
        public PrizesDAL()
        {
            query = new PrizeQuery();
        }
        public PrizesModel GetActivePrizesOfPKAmount(int UsersId)
        {
            PrizesModel prize = new PrizesModel();
            cmd = new MySqlCommand(query.GetActivePrize(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersId", UsersId);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prize.PrizeId = Convert.ToInt32(rdr["UsersId"]);
                    prize.PKAmount = Convert.ToDecimal(rdr["PKAmount"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return prize;
        }

        public void UpdatePrizeForOpen(int PrizeId, int UsersId, decimal UseAmount)
        {
            cmd = new MySqlCommand(query.UpdatePrize(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PrizeId", PrizeId);
            cmd.Parameters.AddWithValue("UsersId", UsersId);
            cmd.Parameters.AddWithValue("UseAmount", UseAmount);

            SaveChangeCommit();
        }
        public void OpenPrize(int PrizeId, decimal PKAmount)
        {
            cmd = new MySqlCommand(query.OpenPrize(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PrizeId", PrizeId);
            cmd.Parameters.AddWithValue("PKAmount", PKAmount);
            SaveChangeCommit();
        }
    }
}
