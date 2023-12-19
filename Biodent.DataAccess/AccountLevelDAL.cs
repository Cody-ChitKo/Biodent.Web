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
    public class AccountLevelDAL:DataControllerBase
    {
        AccountLevelQuery query;
        public AccountLevelDAL()
        {
            query = new AccountLevelQuery();
        }
        public void Insert(AccountLevelModel accountlevel)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("AccountLevel", accountlevel.AccountLevel);
            SaveChangeCommit();
        }
        public void Update()
        {

        }
        public void Delete()
        {

        }
        public List<AccountLevelModel> GetAll()
        {
            List<AccountLevelModel> accountLevels = new List<AccountLevelModel>();

            cmd = new MySqlCommand(query.Select(), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                AccountLevelModel accountLevel;
                while (rdr.Read())
                {
                    accountLevel = new AccountLevelModel();
                    accountLevel.AccountLevel_Id = Convert.ToInt32(rdr["AccountLevel_Id"]);
                    accountLevel.AccountLevel = rdr["AccountLevel"].ToString();
                    accountLevel.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    accountLevels.Add(accountLevel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return accountLevels;
        }
    }
}
