using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class ReviewDAL:DataControllerBase
    {
        ReviewQuery query;
        public ReviewDAL() 
        {
            query = new ReviewQuery();
        }
        public void Insert(ReviewModel review)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("InvoiceID", review.InvoiceID);
            cmd.Parameters.AddWithValue("InvNo", review.InvNo);
            cmd.Parameters.AddWithValue("ReviewDesp", review.ReviewDesp);
            cmd.Parameters.AddWithValue("Rating", review.Rating);
            cmd.Parameters.AddWithValue("ReviewDate", review.ReviewDate);
            cmd.Parameters.AddWithValue("UsersID", review.UsersID);

            SaveChangeCommit();
        }

        public void Delete(string ID)
        {
            cmd = new MySqlCommand("ProthesisDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ProthesisID", ID);

            SaveChangeCommit();
        }

        public List<ReviewModel> Select()
        {
            List<ReviewModel> reviewList = new List<ReviewModel>();

            cmd = new MySqlCommand(query.Select(0), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                ReviewModel review;
                while (rdr.Read())
                {
                    review = new ReviewModel();
                    review.ReviewID = Convert.ToInt32(rdr["ReviewID"]);
                    review.InvoiceID = rdr["InvoiceID"].ToString();
                    review.InvNo = rdr["InvNo"].ToString();
                    review.ReviewDesp = rdr["ReviewDesp"].ToString();
                    review.Rating = Convert.ToInt32(rdr["Rating"]);
                    review.ReviewDate = Convert.ToDateTime(rdr["ReviewDate"]);
                    review.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    review.UsersName = rdr["UsersName"].ToString();
                    review.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    reviewList.Add(review);
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
            return reviewList;
        }
        public List<ReviewModel> SelectByUsersID(int usersId)
        {
            List<ReviewModel> reviewList = new List<ReviewModel>();

            cmd = new MySqlCommand(query.Select(usersId), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", usersId);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                ReviewModel review;
                while (rdr.Read())
                {
                    review = new ReviewModel();
                    review.ReviewID = Convert.ToInt32(rdr["ReviewID"]);
                    review.InvoiceID = rdr["InvoiceID"].ToString();
                    review.InvNo = rdr["InvNo"].ToString();
                    review.ReviewDesp = rdr["ReviewDesp"].ToString();
                    review.Rating = Convert.ToInt32(rdr["Rating"]);
                    review.ReviewDate = Convert.ToDateTime(rdr["ReviewDate"]);
                    review.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    review.UsersName = rdr["UsersName"].ToString();
                    review.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    reviewList.Add(review);
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
            return reviewList;
        }
    }
}
