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
    public class NotiDAL:DataControllerBase
    {
        NotiQuery query;
        public NotiDAL() 
        {
            query = new NotiQuery();
        }
        public void Insert(NotiModel noti)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;
           
            cmd.Parameters.AddWithValue("UsersID", noti.UsersID);
            cmd.Parameters.AddWithValue("NotiDate", noti.NotiDate);
            cmd.Parameters.AddWithValue("NotiTitleEng", noti.NotiTitleEng);
            cmd.Parameters.AddWithValue("NotiTitleMyan", noti.NotiTitleMyan);
            cmd.Parameters.AddWithValue("NotiType", noti.NotiType);
            cmd.Parameters.AddWithValue("NotiMsgEng", noti.NotiMsgEng);
            cmd.Parameters.AddWithValue("NotiMsgMyan", noti.NotiMsgMyan);
            cmd.Parameters.AddWithValue("IsSeen", noti.IsSeen);
            SaveChangeCommit();
        }
        public List<NotiModel> GetNotis(int UsersID)
        {
            List<NotiModel> Noties = new List<NotiModel>();

            cmd = new MySqlCommand(query.GetNotis(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    NotiModel noti = new NotiModel();
                    noti.NotiID = Convert.ToInt32(rdr["NotiID"]);
                    noti.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    noti.NotiTitleEng = rdr["NotiTitleEng"].ToString();
                    noti.NotiTitleMyan = rdr["NotiTitleMyan"].ToString();
                    noti.NotiType = Convert.ToInt32(rdr["NotiType"]);
                    noti.NotiMsgEng = rdr["NotiMsgEng"].ToString();
                    noti.NotiMsgMyan = rdr["NotiMsgMyan"].ToString();
                    noti.IsSeen = Convert.ToBoolean(rdr["IsActive"]);
                    noti.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    Noties.Add(noti);
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
            return Noties;
        }

        public List<NotiModel> GetNotis()
        {
            List<NotiModel> Noties = new List<NotiModel>();

            cmd = new MySqlCommand(query.GetNotis(), con);
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    NotiModel noti = new NotiModel();
                    noti.NotiID = Convert.ToInt32(rdr["NotiID"]);
                    noti.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    noti.NotiTitleEng = rdr["NotiTitleEng"].ToString();
                    noti.NotiTitleMyan = rdr["NotiTitleMyan"].ToString();
                    noti.NotiType = Convert.ToInt32(rdr["NotiType"]);
                    noti.NotiMsgEng = rdr["NotiMsgEng"].ToString();
                    noti.NotiMsgMyan = rdr["NotiMsgMyan"].ToString();
                    noti.IsSeen = Convert.ToBoolean(rdr["IsActive"]);
                    noti.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    Noties.Add(noti);
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
            return Noties;
        }
    }
}
