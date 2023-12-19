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
    public class UsersDAL:DataControllerBase
    {
        UsersQuery query;
        public UsersDAL() 
        {
            query = new UsersQuery();
        }
        public int Insert(UsersModel user)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersName", user.UsersName);
            cmd.Parameters.AddWithValue("Password", user.Password);
            cmd.Parameters.AddWithValue("PhoneNo", user.PhoneNo);
            cmd.Parameters.AddWithValue("Email", user.Email);

            cmd.Parameters.AddWithValue("UserLevel", user.UserLevel);
            cmd.Parameters.AddWithValue("AccountLevel_Id", user.AccountLevel_Id);
            cmd.Parameters.AddWithValue("UserSalt", user.UserSalt);
            cmd.Parameters.AddWithValue("VerificationCode", user.VerificationCode);
            cmd.Parameters.AddWithValue("IsVerified", user.IsVerified);
            cmd.Parameters.AddWithValue("CreatedDate", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("RegionID", user.RegionID);
            cmd.Parameters.AddWithValue("Address", user.Address);
            return SaveChangeCommit(0);
        }
        public int InsertFromAdmin(UsersModel user)
        {
            cmd = new MySqlCommand(query.InsertFromAdmin(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersName", user.UsersName);
            cmd.Parameters.AddWithValue("Password", user.Password);
            cmd.Parameters.AddWithValue("PhoneNo", user.PhoneNo);
            cmd.Parameters.AddWithValue("Email", user.Email);
            cmd.Parameters.AddWithValue("UserLevel", user.UserLevel);
            cmd.Parameters.AddWithValue("AccountLevel_Id", user.AccountLevel_Id);
            cmd.Parameters.AddWithValue("UserSalt", user.UserSalt);
            cmd.Parameters.AddWithValue("VerificationCode", user.VerificationCode);
            cmd.Parameters.AddWithValue("IsVerified", user.IsVerified);
            cmd.Parameters.AddWithValue("CreatedDate", DateTime.Now.Date);

            return SaveChangeCommit(0);
        }

        public int Update(UsersModel user)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", user.UsersID);
            cmd.Parameters.AddWithValue("UsersName", user.UsersName);
            cmd.Parameters.AddWithValue("PhoneNo", user.PhoneNo);
            cmd.Parameters.AddWithValue("Email", user.Email);
            cmd.Parameters.AddWithValue("RegionID", user.RegionID);
            cmd.Parameters.AddWithValue("Address", user.Address);
            return SaveChangeCommit(0);
        }
        public int UpdateFromAdmin(UsersModel user)
        {
            cmd = new MySqlCommand(query.UpdateForAdmin(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", user.UsersID);
            cmd.Parameters.AddWithValue("UsersName", user.UsersName);
            cmd.Parameters.AddWithValue("PhoneNo", user.PhoneNo);
            cmd.Parameters.AddWithValue("Email", user.Email);

            return SaveChangeCommit(0);
        }

        public int DeleteFromAdmin(UsersModel user)
        {
            cmd = new MySqlCommand(query.DeleteFromAdmin(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", user.UsersID);

            return SaveChangeCommit(0);
        }
        public int BenFromAdmin(UsersModel user)
        {
            cmd = new MySqlCommand(query.DeleteFromAdmin(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", user.UsersID);

            return SaveChangeCommit(0);
        }
        public UsersModel CheckUser(string PhoneNo)
        {
            UsersModel user = new UsersModel();
            cmd = new MySqlCommand(query.CheckUsers(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("PhoneNo", PhoneNo);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    user.UsersName = rdr["UsersName"].ToString();
                    user.Password = rdr["Password"].ToString();
                    user.PhoneNo = rdr["PhoneNo"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.UserLevel = rdr["UserLevel"].ToString();
                    user.AccountLevel_Id = Convert.ToInt32(rdr["AccountLevel_Id"]);
                    user.UserSalt = rdr["UserSalt"].ToString();
                    user.VerificationCode = rdr["VerificationCode"].ToString();
                    user.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    user.PackageAmount = Convert.ToDecimal(rdr["PackageAmount"]);
                    if (rdr["PackageExpire"] != DBNull.Value)
                        user.PackageExpire = Convert.ToDateTime(rdr["PackageExpire"]);
                    else
                        user.PackageExpire = null; // Set to null if the database value is DBNull

                    user.IsVerified = Convert.ToBoolean(rdr["IsActive"]);
                    user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    if (rdr["RegionID"] != DBNull.Value)
                        user.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    else user.RegionID = 0;
                    if (rdr["Address"] != DBNull.Value)
                        user.Address = rdr["Address"].ToString();
                    else user.Address = "";
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
            return user;
        }

        public UsersModel LoginWithPhone(string PhoneNo)
        {
            UsersModel user = new UsersModel();
            cmd = new MySqlCommand(query.UsersLogin_WithPhone(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PhoneNo", PhoneNo);
            //cmd.Parameters.AddWithValue("Password", Password);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    user.UsersName = rdr["UsersName"].ToString();
                    user.Password = rdr["Password"].ToString();
                    user.PhoneNo = rdr["PhoneNo"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.UserLevel = rdr["UserLevel"].ToString();
                    user.AccountLevel_Id = Convert.ToInt32(rdr["AccountLevel_Id"]);
                    user.UserSalt = rdr["UserSalt"].ToString();
                    user.VerificationCode = rdr["VerificationCode"].ToString();
                    user.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    user.PackageAmount = Convert.ToDecimal(rdr["PackageAmount"]);
                    if (rdr["PackageExpire"] != DBNull.Value)
                        user.PackageExpire = Convert.ToDateTime(rdr["PackageExpire"]);
                    else
                        user.PackageExpire = null; // Set to null if the database value is DBNull
                    user.IsVerified = Convert.ToBoolean(rdr["IsActive"]);
                    user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    if (rdr["RegionID"] != DBNull.Value)
                        user.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    else user.RegionID = 0;
                    if (rdr["Address"] != DBNull.Value)
                        user.Address = rdr["Address"].ToString();
                    else user.Address = "";
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
            return user;
        }

        public UsersModel GetUserDataWithEmail(string Email)
        {
            UsersModel user = new UsersModel();
            cmd = new MySqlCommand(query.GetUserByEmail(), con);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.Parameters.AddWithValue("Email", Email);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    user.UsersName = rdr["UsersName"].ToString();
                    user.Password = rdr["Password"].ToString();
                    user.PhoneNo = rdr["PhoneNo"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.UserLevel = rdr["UserLevel"].ToString();
                    user.AccountLevel_Id = Convert.ToInt32(rdr["AccountLevel_Id"]);
                    user.UserSalt = rdr["UserSalt"].ToString();
                    user.VerificationCode = rdr["VerificationCode"].ToString();
                    user.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    user.PackageAmount = Convert.ToDecimal(rdr["PackageAmount"]);
                    if (rdr["PackageExpire"] != DBNull.Value)
                        user.PackageExpire = Convert.ToDateTime(rdr["PackageExpire"]);
                    else
                        user.PackageExpire = null; // Set to null if the database value is DBNull
                    user.IsVerified = Convert.ToBoolean(rdr["IsActive"]);
                    user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    if (rdr["RegionID"] != DBNull.Value)
                        user.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    else user.RegionID = 0;
                    if (rdr["Address"] != DBNull.Value)
                        user.Address = rdr["Address"].ToString();
                    else user.Address = "";
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
            return user;
        }
        public UsersModel GetUsersInfo(int usersID)
        {
            UsersModel user = new UsersModel();
            cmd = new MySqlCommand(query.GetUsersByID(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", usersID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    user.UsersName = rdr["UsersName"].ToString();
                    user.Password = rdr["Password"].ToString();
                    user.PhoneNo = rdr["PhoneNo"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.UserLevel = rdr["UserLevel"].ToString();
                    user.AccountLevel_Id = Convert.ToInt32(rdr["AccountLevel_Id"]);
                    user.UserSalt = rdr["UserSalt"].ToString();
                    user.VerificationCode = rdr["VerificationCode"].ToString();
                    user.WalletAmount = Convert.ToDecimal(rdr["WalletAmount"]);
                    user.PackageAmount = Convert.ToDecimal(rdr["PackageAmount"]);
                    if (rdr["PackageExpire"] != DBNull.Value)
                        user.PackageExpire = Convert.ToDateTime(rdr["PackageExpire"]);
                    else
                        user.PackageExpire = null; // Set to null if the database value is DBNull

                    user.IsVerified = Convert.ToBoolean(rdr["IsActive"]);
                    user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    if (rdr["RegionID"] != DBNull.Value)
                        user.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    else user.RegionID = 0;
                    if (rdr["Address"] != DBNull.Value)
                        user.Address = rdr["Address"].ToString();
                    else user.Address = "";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return user;
         }

        public List<UsersModel> GetNewClinic()
        {
            List<UsersModel> cusList = new List<UsersModel>();

            cmd = new MySqlCommand(query.SelectNewClinic(), con);
            cmd.CommandType = CommandType.Text;
            UsersModel clinic;
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    clinic = new UsersModel();

                    clinic.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    clinic.UsersName = rdr["UsersName"].ToString();
                    clinic.Email = rdr["Email"].ToString();
                    clinic.PhoneNo = rdr["PhoneNo"].ToString();
                    clinic.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    clinic.RegionName = rdr["RegionName"].ToString();
                    clinic.Address = rdr["Address"].ToString();
                    clinic.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    clinic.Status = rdr["Status"].ToString();
                    clinic.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    cusList.Add(clinic);
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
            return cusList;
        }
        public List<UsersModel> GetAllClinic()
        {
            List<UsersModel> cusList = new List<UsersModel>();

            cmd = new MySqlCommand(query.SelectAllClinic(), con);
            cmd.CommandType = CommandType.Text;
            UsersModel clinic;
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    clinic = new UsersModel();

                    clinic.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    clinic.UsersName = rdr["UsersName"].ToString();
                    clinic.Email = rdr["Email"].ToString();
                    clinic.PhoneNo = rdr["PhoneNo"].ToString();
                    clinic.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    clinic.RegionName = rdr["RegionName"].ToString();
                    clinic.Address = rdr["Address"].ToString();
                    clinic.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    clinic.Status = rdr["Status"].ToString();
                    clinic.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    cusList.Add(clinic);
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
            return cusList;
        }
        public List<UsersModel> GetAdminList()
        {
            List<UsersModel> userList = new List<UsersModel>();

            cmd = new MySqlCommand(query.SelectAdminUser(), con);
            cmd.CommandType = CommandType.Text;
            UsersModel user;
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user = new UsersModel();

                    user.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    user.UsersName = rdr["UsersName"].ToString();
                    user.UserLevel = rdr["UserLevel"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.PhoneNo = rdr["PhoneNo"].ToString();
                    //user.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    //user.RegionName = rdr["RegionName"].ToString();
                    user.Address = rdr["Address"].ToString();
                    //user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    user.Status = rdr["Status"].ToString();
                    user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    userList.Add(user);
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
            return userList;
        }
        public List<PrizesModel> GetPrizes(int usersID)
        {
            List<PrizesModel> prizes = new List<PrizesModel>();

            cmd = new MySqlCommand(query.SelectPrizes(usersID), con);
            cmd.CommandType = CommandType.Text;
            PrizesModel prize;
            try
            {
                cmd.Parameters.AddWithValue("usersID", usersID);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    prize = new PrizesModel();
                    prize.PrizeId = Convert.ToInt32(rdr["PrizeId"]);
                    prize.PackageId = Convert.ToInt32(rdr["PackageId"]);
                    prize.GiftCardCode = rdr["GiftCardCode"].ToString();
                    prize.GiftCardName = rdr["GiftCardName"].ToString();
                    prize.IsWithdrawString = rdr["IsWithdraw"].ToString();
                    prize.CanWithdrawString = rdr["CanWithdraw"].ToString();
                    prize.GiftCardImage = rdr["GiftCardImage"].ToString();

                    prizes.Add(prize);
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
            return prizes;
        }
        public int WithDrawPrizes(PrizesModel prizes)
        {
            cmd = new MySqlCommand(query.WithDrawPrize(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PrizeId", prizes.PrizeId);
            cmd.Parameters.AddWithValue("UsersId", prizes.UsersId);
            cmd.Parameters.AddWithValue("WithdrawDate", prizes.WithdrawDate);
            
            return SaveChangeCommit(0);
        }
        public int UnActivePrize(PrizesModel prizes)
        {
            cmd = new MySqlCommand(query.UnActivePrize(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("PrizeId", prizes.PrizeId);
            cmd.Parameters.AddWithValue("UsersId", prizes.UsersId);

            return SaveChangeCommit(0);
        }
        public List<UsersModel> GetStaffUser()
        {
            List<UsersModel> userList = new List<UsersModel>();

            cmd = new MySqlCommand(query.SelectAdminUser(), con);
            cmd.CommandType = CommandType.Text;
            UsersModel user;
            try
            {
                //CommitReader();
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user = new UsersModel();

                    user.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    user.UsersName = rdr["UsersName"].ToString();
                    user.UserLevel = rdr["UserLevel"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.PhoneNo = rdr["PhoneNo"].ToString();
                    //user.RegionID = Convert.ToInt32(rdr["RegionID"]);
                    //user.RegionName = rdr["RegionName"].ToString();
                    user.Address = rdr["Address"].ToString();
                    //user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                    user.Status = rdr["Status"].ToString();
                    user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    userList.Add(user);
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
            return userList;
        }

        public void Approve(int UsersId)
        {
            cmd = new MySqlCommand(query.Approve(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersId);
            SaveChangeCommit();
        }
        public void Reject(int UsersId)
        {
            cmd = new MySqlCommand(query.RejectUser(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("UsersID", UsersId);
            SaveChangeCommit();
        }
    }
}
