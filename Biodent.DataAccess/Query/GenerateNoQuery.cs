using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class GenerateNoQuery
    {
        private string query = "";
        public string Insert()
        {
            query = "INSERT INTO tbl_generateno (GenerateDate, FirstSymbol, LastValue, GenerateType) ";
            query += " VALUES (@GenerateDate, @FirstSymbol, @LastValue, @GenerateType)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_generateno SET FirstSymbol = @FirstSymbol, GenerateDate = @GenerateDate, ";
            query += "LastValue = @LastValue, GenerateType = @GenerateType WHERE GenerateID = @GenerateID";
            return query;
        }
        public string Delete()
        {
            query = "DELETE FROM tbl_generateno WHERE GenerateID = @GenerateID";
            return query;
        }
        public string GetById()
        {
            query = "SELECT * FROM tbl_generateno WHERE GenerateID = @GenerateID";
            return query;
        }
        public string GetAll()
        {
            query = "SELECT * FROM tbl_generateno";
            return query;
        }
        public string GetByGenerateType()
        {
            query = "SELECT * FROM tbl_generateno WHERE GenerateType = @GenerateType";
            return query;
        }
    }
}
