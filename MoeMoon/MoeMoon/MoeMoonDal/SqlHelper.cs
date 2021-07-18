using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MoeMoonDal
{
    public static class SqlHelper
    {
        private static string GetConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        public static int ExecuteNonQuery(string sqlText,params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetDataTable(string sqlText,params SqlParameter[] parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlText, GetConnectionString);
            adapter.SelectCommand.Parameters.AddRange(parameters);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static object ExecuteScalar(string sqlText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
