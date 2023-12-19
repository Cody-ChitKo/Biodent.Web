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
    public class MessageDAL:DataControllerBase
    {
        MessageQuery query;
        public MessageDAL()
        {
            query = new MessageQuery();
        }
        public void Insert(MessageModel message)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("MessageText", message.MessageText);
            cmd.Parameters.AddWithValue("TextDate", message.TextDate);
            cmd.Parameters.AddWithValue("ToUsersId", message.ToUsersId);
            cmd.Parameters.AddWithValue("FromUsersId", message.FromUsersId);
            SaveChangeCommit();
        }
        public List<MessageModel> GetByUsersId(int usersId)
        {
            List<MessageModel> messages = new List<MessageModel>();

            cmd = new MySqlCommand(query.Select(usersId), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.Parameters.AddWithValue("ToUsersId", usersId);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                MessageModel msg;
                while (rdr.Read())
                {
                    msg = new MessageModel();
                    msg.MessageId = Convert.ToInt32(rdr["MessageId"]);
                    msg.ToUsersId = Convert.ToInt32(rdr["ToUsersId"]);
                    msg.FromUsersId = Convert.ToInt32(rdr["FromUsersId"]);
                    msg.UserName = rdr["UsersName"].ToString();
                    msg.MessageText = rdr["MessageText"].ToString();
                    msg.TextDate = Convert.ToDateTime(rdr["TextDate"]);
                    messages.Add(msg);
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
            return messages;
        }
    }
}
