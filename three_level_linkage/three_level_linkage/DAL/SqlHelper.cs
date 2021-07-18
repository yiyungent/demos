using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace three_level_linkage.DAL
{
    public class SqlHelper
    {
        private static string GetConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, GetConnStr);
            adapter.SelectCommand.Parameters.AddRange(parameters);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}