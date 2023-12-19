using Biodent.DataAccess.Query;
using Biodent.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class DoctorDAL:DataControllerBase
    {
        DoctorQuery query;
        public DoctorDAL()
        {
            query = new DoctorQuery();
        }

        public void Insert(DoctorModel doctor)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("UsersID", doctor.UsersID);
            cmd.Parameters.AddWithValue("DoctorName", doctor.DoctorName);

            SaveChangeCommit();
        }
        public void Edit(DoctorModel doctor)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("DoctorID", doctor.DoctorID);
            cmd.Parameters.AddWithValue("UsersID", doctor.UsersID);
            cmd.Parameters.AddWithValue("DoctorName", doctor.DoctorName);

            SaveChangeCommit();
        }
        public void Delete(int ID)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("DoctorID", ID);

            SaveChangeCommit();
        }
        public List<DoctorModel> Select()
        {
            List<DoctorModel> doctorList = new List<DoctorModel>();

            cmd = new MySqlCommand(query.Select(0), con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                DoctorModel docor;
                while (rdr.Read())
                {
                    docor = new DoctorModel();
                    docor.DoctorID = Convert.ToInt32(rdr["DoctorID"]);
                    docor.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    docor.UsersName = rdr["UsersName"].ToString();
                    docor.DoctorName = rdr["DoctorName"].ToString();
                    docor.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    doctorList.Add(docor);
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
            return doctorList;
        }
        public DoctorModel SelectByID(int id)
        {
            cmd = new MySqlCommand(query.SelectById(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("DoctorID", id);
            DoctorModel doctor = new DoctorModel();
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    doctor.DoctorID = Convert.ToInt32(rdr["DoctorID"]);
                    doctor.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    doctor.UsersName = rdr["UsersName"].ToString();
                    doctor.DoctorName = rdr["DoctorName"].ToString();
                    doctor.IsActive = Convert.ToBoolean(rdr["IsActive"]);
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
            return doctor;
        }
        public List<DoctorModel> SelectByUsersID(int UsersID)
        {
            List<DoctorModel> doctorList = new List<DoctorModel>();
            cmd = new MySqlCommand(query.Select(UsersID), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("UsersID", UsersID);
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                DoctorModel docor;
                while (rdr.Read())
                {
                    docor = new DoctorModel();
                    docor.DoctorID = Convert.ToInt32(rdr["DoctorID"]);
                    docor.UsersID = Convert.ToInt32(rdr["UsersID"]);
                    docor.UsersName = rdr["UsersName"].ToString();
                    docor.DoctorName = rdr["DoctorName"].ToString();
                    docor.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    doctorList.Add(docor);
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
            return doctorList;
        }
    }
}
