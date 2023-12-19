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
    public class GenerateNoDAL:DataControllerBase
    {
        GenerateNoQuery query;
        public GenerateNoDAL() 
        {
            query = new GenerateNoQuery();
        }
        public void Insert(GenerateNoModel generate)
        {
            cmd = new MySqlCommand(query.Insert(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("GenerateDate", generate.GenerateDate);
            cmd.Parameters.AddWithValue("FirstSymbol", generate.FirstSymbol);
            cmd.Parameters.AddWithValue("LastValue", generate.LastValue);
            cmd.Parameters.AddWithValue("GenerateType", generate.GenerateType);

            SaveChangeCommit();
        }
        public void Update(GenerateNoModel generate)
        {
            cmd = new MySqlCommand(query.Update(), con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("GenerateID", generate.GenerateID);
            cmd.Parameters.AddWithValue("GenerateDate", generate.GenerateDate);
            cmd.Parameters.AddWithValue("FirstSymbol", generate.FirstSymbol);
            cmd.Parameters.AddWithValue("LastValue", generate.LastValue);
            cmd.Parameters.AddWithValue("GenerateType", generate.GenerateType);

            SaveChangeCommit();
        }
        public void Delete(int GenerateID)
        {
            cmd = new MySqlCommand(query.Delete(), con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("GenerateID", GenerateID);
            SaveChangeCommit();
        }
        public List<GenerateNoModel> GetAll()
        {
            List<GenerateNoModel> generates = new List<GenerateNoModel>();

            cmd = new MySqlCommand(query.GetAll(), con);
            cmd.CommandType = CommandType.Text;
            GenerateNoModel generate;
            try
            {
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    generate = new GenerateNoModel();

                    generate.GenerateID = Convert.ToInt32(rdr["GenerateID"]);
                    generate.GenerateDate = Convert.ToDateTime(rdr["GenerateDate"]);
                    generate.FirstSymbol = rdr["FirstSymbol"].ToString();
                    generate.LastValue = Convert.ToInt32(rdr["LastValue"]);
                    generate.GenerateType = rdr["GenerateType"].ToString();
                    generates.Add(generate);
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
            return generates;
        }
        public GenerateNoModel GetById(int GenerateId)
        {
            cmd = new MySqlCommand(query.GetById(), con);
            cmd.CommandType = CommandType.Text;
            GenerateNoModel generate = new GenerateNoModel();
            try
            {
                cmd.Parameters.AddWithValue("GenerateId", GenerateId);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    generate.GenerateID = Convert.ToInt32(rdr["GenerateID"]);
                    generate.GenerateDate = Convert.ToDateTime(rdr["GenerateDate"]);
                    generate.FirstSymbol = rdr["FirstSymbol"].ToString();
                    generate.LastValue = Convert.ToInt32(rdr["LastValue"]);
                    generate.GenerateType = rdr["GenerateType"].ToString();
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
            return generate;
        }
        public GenerateNoModel GetByGenerateType(string GenerateType)
        {
            cmd = new MySqlCommand(query.GetByGenerateType(), con);
            cmd.CommandType = CommandType.Text;
            GenerateNoModel generate = new GenerateNoModel();
            try
            {
                cmd.Parameters.AddWithValue("GenerateType", GenerateType);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    generate.GenerateID = Convert.ToInt32(rdr["GenerateID"]);
                    generate.GenerateDate = Convert.ToDateTime(rdr["GenerateDate"]);
                    generate.FirstSymbol = rdr["FirstSymbol"].ToString();
                    generate.LastValue = Convert.ToInt32(rdr["LastValue"]);
                    generate.GenerateType = rdr["GenerateType"].ToString();
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
            return generate;
        }
    }
}
