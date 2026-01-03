using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.Common
{
    public static class Common
    {
        public static DataTable CreateDataTableFromModel<T>(this IEnumerable<T> list, ref List<string> ColumnName)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                ColumnName.Add(info.Name);
                dataTable.Columns.Add(new DataColumn(info.Name.ToLower(), Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
