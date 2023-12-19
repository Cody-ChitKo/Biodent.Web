using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess
{
    public class DataControllerBase
    {
        protected MySqlConnection con;
        protected MySqlCommand cmd;
        protected MySqlTransaction Transaction;
        protected MySqlDataReader rdr;
        protected bool useTransaction;

        string server = "localhost";
        string database = "dbname";
        string uid = "root";
        string password = "test";

        private string DbConnection;
        string sqlexten = ";Pooling=true;Allow User Variables=True; Convert Zero Datetime=True;ConnectionTimeout=1000;TreatTinyAsBoolean=false;";

        public DataControllerBase()
        {
            DbConnection = "Server=" + server + "; database=" + database + "; uid=" + uid + "; pwd=" + password + sqlexten;
            //con = new MySqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ToString())
            con = new MySqlConnection(DbConnection);
        }
        public void SaveChangeCommit()//SaveChange
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }
        public int SaveChangeCommit(int value)//SaveChange
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        public void CommitTransaction()
        {
            // Check that transaction exists and connection opens
            if ((this.Transaction != null) && (this.con.State == ConnectionState.Open))
            //if ((this.con.State == ConnectionState.Open))
            {
                // Commit transaction
                this.Transaction.Commit();

                // Check that connection open
                if (this.con.State == ConnectionState.Open)
                {
                    // Close connection
                    this.con.Close();
                }

                // Remove transaction
                this.useTransaction = false;
            }
        }


        /// <summary>
        /// Rollback Transaction
        /// </summary>
        public void RollbackTransaction()
        {
            // Check that transaction exists and connection opens
            if ((this.Transaction != null) && (this.con.State == ConnectionState.Open))
            {
                // Rollback transaction
                this.Transaction.Rollback();

                // Check that connection open
                if (this.con.State == ConnectionState.Open)
                {
                    // Close connection
                    this.con.Close();
                }

                // Remove transaction
                this.useTransaction = false;
            }
        }

        /// <summary>
        /// Create transaction
        /// </summary>
        public void StartTransaction()
        {
            // Check that connection is close
            if (this.con.State == ConnectionState.Closed)
            {
                // Open connection
                this.con.Open();
            }

            // Create transaction
            this.Transaction = this.con.BeginTransaction();
            this.useTransaction = true;
        }


        protected virtual object GetNull(object obj)
        {
            // Check that object is "string" and object's value is empty string
            if ((obj is string) && (obj.ToString() == string.Empty))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "DateTime" and object's value is minimum datetime value
            if ((obj is DateTime) && (((DateTime)obj) == DateTime.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "int" and object's value is minimum integer value
            if ((obj is int) && (((int)obj) == -2147483648))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "float" and object's value is minimum float value
            if ((obj is float) && (((float)obj) == float.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "decimal" and object's value is minimum decimal value
            if ((obj is decimal) && (((decimal)obj) == -79228162514264337593543950335M))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "double" and object's value is minimum double value
            if ((obj is double) && (((double)obj) == double.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
            }

            return obj;
        }

        public string CommitScalar(string key)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                key = (string)cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                con.Close();
            }
            return key;
        }
    }
}
