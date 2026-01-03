using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries
{
    public class DBQueryParam
    {
        public class dbqParam
        {
            public string Key { get; set; }
            public SqlParameter sqlparam { get; set; }
        }
    }
}
