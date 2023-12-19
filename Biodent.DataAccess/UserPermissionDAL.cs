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
    public class UserPermissionDAL:DataControllerBase
    {
        UsersPermissionQuery query;
        public UserPermissionDAL() 
        {
            query = new UsersPermissionQuery();
        }

        public List<UsersPermissionModel> Select(int UsersID)
        {
            List<UsersPermissionModel> permissionList = new List<UsersPermissionModel>();

            cmd = new MySqlCommand(query.Select(UsersID), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersID);

            UsersPermissionModel permission;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    permission = new UsersPermissionModel();

                    permission.PermissionId = Convert.ToInt32(rdr["PermissionId"]);
                    permission.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    permission.Control_Id = rdr["Control_Id"].ToString();
                    permission.Control_URL = rdr["Control_URL"].ToString();
                    permission.Full_Access = Convert.ToBoolean(rdr["Full_Access"]);
                    permission.List_Access = Convert.ToBoolean(rdr["List_Access"]);
                    permission.Create_Access = Convert.ToBoolean(rdr["Create_Access"]);
                    permission.Edit_Access = Convert.ToBoolean(rdr["Edit_Access"]);
                    permission.Delete_Access = Convert.ToBoolean(rdr["Delete_Access"]);
                    permission.Approve_Access = Convert.ToBoolean(rdr["Approve"]);

                    permissionList.Add(permission);
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
            return permissionList;
        }

        public List<UsersPermissionModel> SelectPermission(int UsersID)
        {
            List<UsersPermissionModel> permissionList = new List<UsersPermissionModel>();

            cmd = new MySqlCommand(query.SelectPermission(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersID);

            UsersPermissionModel permission;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    permission = new UsersPermissionModel();

                    if (rdr["PermissionId"] != DBNull.Value)
                    {
                        permission.PermissionId = Convert.ToInt32(rdr["PermissionId"]);
                    }
                    if (rdr["UsersID"] != DBNull.Value)
                    {
                        permission.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    }
                    if (rdr["Control_Id"] != DBNull.Value)
                    {
                        permission.Control_Id = rdr["Control_Id"].ToString();
                    }
                    
                    permission.Control_URL = rdr["Control_URL"].ToString();
                    if (rdr["Full_Access"] != DBNull.Value)
                    {
                        permission.Full_Access = Convert.ToBoolean(rdr["Full_Access"]);
                    }
                    if (rdr["List_Access"] != DBNull.Value)
                    {
                        permission.List_Access = Convert.ToBoolean(rdr["List_Access"]);
                    }
                    if (rdr["List_Access"] !=DBNull.Value)
                    {
                        permission.List_Access = Convert.ToBoolean(rdr["List_Access"]);
                    }
                    if(rdr["Create_Access"] != DBNull.Value)
                    {
                        permission.Create_Access = Convert.ToBoolean(rdr["Create_Access"]);
                    }
                    if (rdr["Edit_Access"] != DBNull.Value)
                    {
                        permission.Edit_Access = Convert.ToBoolean(rdr["Edit_Access"]);
                    }
                    if (rdr["Delete_Access"] != DBNull.Value)
                    {
                        permission.Delete_Access = Convert.ToBoolean(rdr["Delete_Access"]);
                    }
                    if (rdr["Approve_Access"] != DBNull.Value)
                    {
                        permission.Approve_Access = Convert.ToBoolean(rdr["Approve_Access"]);
                    }
                    permissionList.Add(permission);
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
            return permissionList;
        }
        public UsersPermissionModel SelectByControlID(string id, int UsersId)
        {
            cmd = new MySqlCommand(query.SelectPermissionByControlId(), con);
            cmd.CommandType = CommandType.Text;
            UsersPermissionModel permission = new UsersPermissionModel();
            cmd.Parameters.AddWithValue("Control_Id", id);
            cmd.Parameters.AddWithValue("UsersID", UsersId);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    permission.PermissionId = Convert.ToInt32(rdr["PermissionId"]);
                    permission.Control_Id = rdr["Control_Id"].ToString();
                    permission.Control_URL = rdr["Control_URL"].ToString();

                    permission.Full_Access = Convert.ToBoolean(rdr["Full_Access"]);
                    permission.List_Access = Convert.ToBoolean(rdr["List_Access"]);
                    permission.Create_Access = Convert.ToBoolean(rdr["Create_Access"]);

                    permission.Edit_Access = Convert.ToBoolean(rdr["Edit_Access"]);
                    permission.Delete_Access = Convert.ToBoolean(rdr["Delete_Access"]);
                    permission.Approve_Access = Convert.ToBoolean(rdr["Approve_Access"]);
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
            return permission;
        }
    }
}
