using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Common
{
    internal static class GlobalFunction
    {
        public static DataTable ConvertToDataTable<T>(List<T> data)
        {
            DataTable table = new DataTable();

            // Get all properties of the type
            var properties = typeof(T).GetProperties();

            // Create columns based on the properties
            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            // Add rows to the table
            foreach (var item in data)
            {
                DataRow row = table.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
