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
    public class GiftCardDAL:DataControllerBase
    {
        GiftCardQuery query;
        public GiftCardDAL()
        {
            query = new GiftCardQuery();
        }
        public void Insert(GiftCardModel giftCard)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PackageId", giftCard.PackageId);
            cmd.Parameters.AddWithValue("GiftCardCode", giftCard.GiftCardCode);
            cmd.Parameters.AddWithValue("GiftCardName", giftCard.GiftCardName);
            cmd.Parameters.AddWithValue("GiftCardImage", giftCard.GiftCardImageUrl);
            cmd.Parameters.AddWithValue("GiftCardLevel", giftCard.GiftCardLevel);

            SaveChangeCommit();
        }
        public void Update(GiftCardModel giftCard)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("GiftCardId", giftCard.GiftCardId);
            cmd.Parameters.AddWithValue("PackageId", giftCard.PackageId);
            cmd.Parameters.AddWithValue("GiftCardCode", giftCard.GiftCardCode);
            cmd.Parameters.AddWithValue("GiftCardName", giftCard.GiftCardName);
            cmd.Parameters.AddWithValue("GiftCardImage", giftCard.GiftCardImageUrl);
            cmd.Parameters.AddWithValue("GiftCardLevel", giftCard.GiftCardLevel);

            SaveChangeCommit();
        }
        public void Delete(int GiftcardId)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("GiftCardId", GiftcardId);
            SaveChangeCommit();
        }
        public List<GiftCardModel> GetAll()
        {
            List<GiftCardModel> giftCards = new List<GiftCardModel>();

            cmd = new MySqlCommand(query.Select(0), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    GiftCardModel giftCard = new GiftCardModel();
                    giftCard.GiftCardId = Convert.ToInt32(rdr["GiftCardId"]);
                    giftCard.PackageId = Convert.ToInt32(rdr["PackageId"]);
                    giftCard.PackageName = rdr["PackageName"].ToString();
                    giftCard.GiftCardCode = rdr["GiftCardCode"].ToString();
                    giftCard.GiftCardName = rdr["GiftCardName"].ToString();
                    giftCard.GiftCardLevel = Convert.ToInt32(rdr["GiftCardLevel"]);
                    giftCard.GiftCardImageUrl = rdr["GiftCardImage"].ToString();
                    giftCard.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    giftCards.Add(giftCard);
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
            return giftCards;
        }
        public GiftCardModel GetById(int GiftCardId)
        {
            GiftCardModel giftCard = new GiftCardModel();

            cmd = new MySqlCommand(query.Select(GiftCardId), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("GiftCardId", GiftCardId);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    
                    giftCard.GiftCardId = Convert.ToInt32(rdr["GiftCardId"]);
                    giftCard.PackageId = Convert.ToInt32(rdr["PackageId"]);
                    giftCard.PackageName = rdr["PackageName"].ToString();
                    giftCard.GiftCardCode = rdr["GiftCardCode"].ToString();
                    giftCard.GiftCardName = rdr["GiftCardCode"].ToString();
                    giftCard.GiftCardLevel = Convert.ToInt32(rdr["GiftCardLevel"]);
                    giftCard.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return giftCard;
        }
        public List<GiftCardModel> GetByPackageId(int pacakgeId)
        {
            List<GiftCardModel> giftCards = new List<GiftCardModel>();

            cmd = new MySqlCommand("GiftCard_SelectByPackageId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_PackageId", pacakgeId);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    GiftCardModel giftCard = new GiftCardModel();
                    giftCard.GiftCardId = Convert.ToInt32(rdr["GiftCardId"]);
                    giftCard.PackageId = Convert.ToInt32(rdr["PackageId"]);
                    giftCard.PackageName = rdr["PackageName"].ToString();
                    giftCard.GiftCardCode = rdr["GiftCardCode"].ToString();
                    giftCard.GiftCardName = rdr["GiftCardCode"].ToString();
                    giftCard.GiftCardLevel = Convert.ToInt32(rdr["GiftCardLevel"]);
                    giftCard.GiftCardImageUrl = rdr["GiftCardImage"].ToString();
                    giftCard.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    giftCards.Add(giftCard);
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
            return giftCards;
        }
    }
}
