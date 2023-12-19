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
    public class ControlDAL:DataControllerBase
    {
        ControlQuery query;
        public ControlDAL()
        {
            query = new ControlQuery();
        }
        public void Insert(ControlModel control)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("Control_Id", Guid.NewGuid());
            cmd.Parameters.AddWithValue("Control_Name", control.Control_Name);
            cmd.Parameters.AddWithValue("Control_URL", control.Control_URL);

            SaveChangeCommit();
        }
        public void Update(ControlModel control)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("Control_Id", control.Control_Id);
            cmd.Parameters.AddWithValue("Control_Name", control.Control_Name);
            cmd.Parameters.AddWithValue("Control_URL", control.Control_URL);

            SaveChangeCommit();
        }
        public void Delete(int id)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("Control_Id", id);

            SaveChangeCommit();
        }
        public ControlModel GetControl(string Control_Name)
        {
            cmd = new MySqlCommand(query.SelectByName(), con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Control_Name", Control_Name);

            ControlModel control = new ControlModel();

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    control.Control_Id = rdr["Control_Id"].ToString();
                    control.Control_Name = rdr["Control_Name"].ToString();
                    control.Control_URL = rdr["Control_URL"].ToString();
                    control.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return control;
        }

        public List<ControlModel> Select()
        {
            List<ControlModel> controlList = new List<ControlModel>();

            cmd = new MySqlCommand(query.Select(), con);
            cmd.CommandType = CommandType.Text;
            ControlModel control;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    control = new ControlModel();

                    control.Control_Id = rdr["Control_Id"].ToString();
                    control.Control_Name = rdr["Control_Name"].ToString();
                    control.Control_URL = rdr["Control_URL"].ToString();
                    control.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    controlList.Add(control);
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
            return controlList;
        }
    }
}
