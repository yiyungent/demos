using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace three_level_linkage.DAL
{
    public delegate T DelDataRowToObject<T>(DataRow row);

    public class CityDal
    {
        /// <summary>
        /// 获取指定省中的城市
        /// </summary>
        /// <param name="provincialId">省ID</param>
        /// <returns></returns>
        public List<Models.City> GetList(int provincialId)
        {
            string sql = "select cityID, cityName from city where provincialID = @provincialID";
            SqlParameter parOfProvincialID = new SqlParameter("@provincialID", provincialId);
            DataTable dt = SqlHelper.GetDataTable(sql, parOfProvincialID);
            List<Models.City> list = DataTableToList<Models.City>(dt, (row) =>
              {
                  return new Models.City { CityId = Convert.ToInt32(row["cityId"]), CityName = row["cityName"].ToString(), ProvincialId = provincialId };
              });
            return list;
        }

        private List<T> DataTableToList<T>(DataTable dt, DelDataRowToObject<T> del)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(del(row));
            }
            return list;
        }
    }
}