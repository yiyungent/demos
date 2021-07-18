using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace three_level_linkage.DAL
{
    public class ProvincialDal
    {

        public List<Models.Provincial> GetList()
        {
            string sql = "select provincialID, provincialName from provincial";
            DataTable dt = SqlHelper.GetDataTable(sql);
            List<Models.Provincial> list = DataTableToList(dt, delegate (DataRow row)
            {
                return new Models.Provincial { ProvincialId = Convert.ToInt32(row["provincialId"]), ProvincialName = row["provincialName"].ToString() };
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