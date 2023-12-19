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
    public class PackageDAL:DataControllerBase
    {
        PackageQuery query;
        public PackageDAL()
        {
            query = new PackageQuery();
        }
        public void Save(PackageModel package)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PackageName", package.PackageName);
            cmd.Parameters.AddWithValue("Description", package.Description);
            cmd.Parameters.AddWithValue("PKPrice", package.PKPrice);
            cmd.Parameters.AddWithValue("GetPKAmount", package.GetPKAmount);
            cmd.Parameters.AddWithValue("PKImage", package.PKImageUrl);

            SaveChangeCommit();
        }
        public void Update(PackageModel package)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PackageId", package.PackageId);
            cmd.Parameters.AddWithValue("PackageName", package.PackageName);
            cmd.Parameters.AddWithValue("Description", package.Description);
            cmd.Parameters.AddWithValue("PKPrice", package.PKPrice);
            cmd.Parameters.AddWithValue("GetPKAmount", package.GetPKAmount);
            cmd.Parameters.AddWithValue("PKImage", package.PKImage);

            SaveChangeCommit();
        }
        public void Delete(int PackageId)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PackageId", PackageId);

            SaveChangeCommit();
        }
        public List<PackageModel> Select()
        {
            List<PackageModel> packageList = new List<PackageModel>();

            cmd = new MySqlCommand(query.Select(0), con);
            cmd.CommandType = CommandType.Text;
            PackageModel package;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    package = new PackageModel();

                    package.PackageId = (int)Convert.ToInt64(rdr["PackageId"]);
                    package.PackageName = rdr["PackageName"].ToString();
                    package.Description = rdr["Description"].ToString();
                    package.GetPKAmount = Convert.ToDecimal(rdr["GetPKAmount"]);
                    package.PKPrice = Convert.ToDecimal(rdr["PKPrice"]);
                    package.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    package.PKImageUrl = rdr["PKImage"].ToString();

                    packageList.Add(package);
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
            return packageList;
        }

        public PackageModel SelectByID(int PackageId)
        {
            PackageModel package = new PackageModel();
            cmd = new MySqlCommand(query.Select(PackageId), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("PackageId", PackageId);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    package.PackageId = (int)Convert.ToInt64(rdr["PackageId"]);
                    package.PackageName = rdr["PackageName"].ToString();
                    package.Description = rdr["Description"].ToString();
                    package.GetPKAmount = Convert.ToDecimal(rdr["GetPKAmount"]);
                    package.PKPrice = Convert.ToDecimal(rdr["PKPrice"]);
                    package.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    package.PKImageUrl = rdr["PKImage"].ToString();
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
            return package;
        }

        public void BuyPackage(int usersId, int packageId, decimal PKPrice, decimal PackageAmount, DateTime PackageExpire)
        {
            int GiftCardCount = GetGiftCardCount(packageId);
            cmd.CommandText = "";

            cmd = new MySqlCommand(query.BuyPackage(), con);
            cmd.CommandType = CommandType.Text;
            
            cmd.Parameters.AddWithValue("UsersID", usersId);
            cmd.Parameters.AddWithValue("PackageAmount", PackageAmount);
            cmd.Parameters.AddWithValue("PackageExpire", PackageExpire);
            cmd.Parameters.AddWithValue("PackageId", packageId);
            cmd.Parameters.AddWithValue("PKPrice", PKPrice);
            cmd.Parameters.AddWithValue("GiftCardCount", GiftCardCount);

            SaveChangeCommit();
        }
        public int GetGiftCardCount(int pid)
        {
            int GiftcardCount = 0;
            cmd = new MySqlCommand(query.GetGiftcardCount(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("pid", pid);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    GiftcardCount = Convert.ToInt32(rdr["Count"]);
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
            return GiftcardCount;
        }
    }
}
