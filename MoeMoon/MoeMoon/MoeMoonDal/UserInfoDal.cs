using MoeMoonCommon;
using MoeMoonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MoeMoonDal
{
    public partial class UserInfoDal
    {
        /// <summary>
        /// 通过账户(UCode)查找对象
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public UserInfo GetByUCode(string userCode)
        {
            UserInfo user = null;
            string sql = "select UID, UCode, UPassword, UName, UPhone, UEmail, UDelFlag from UserInfo where UCode = @UCode";
            SqlParameter parOfUCode = new SqlParameter() { ParameterName = "@UCode", SqlValue = userCode };
            DataTable dt = SqlHelper.GetDataTable(sql, parOfUCode);
            if (dt.Rows.Count > 0)
            {
                // 如果有此用户(UCode)则将其返回
                user = new UserInfo()
                {
                    UID = Convert.ToInt32(dt.Rows[0]["UID"]),
                    UCode = userCode,
                    UPassword = dt.Rows[0]["UPassword"].ToString(),
                    UName = dt.Rows[0]["UName"].ToString(),
                    UPhone = dt.Rows[0]["UPhone"].ToString(),
                    UEmail = dt.Rows[0]["UEmail"].ToString(),
                    UDelFlag = short.Parse(dt.Rows[0]["UDelFlag"].ToString())
                };
            }

            return user;
        }

        /// <summary>
        /// 插入新账户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(UserInfo user)
        {
            string sql = "insert into UserInfo(uCode, uPassword, uName) values(@uCode, @uPassword, @uName)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter() { ParameterName = "@UCode", SqlValue = user.UCode });
            parameters.Add(new SqlParameter() { ParameterName = "@UName", SqlValue = user.UName });
            parameters.Add(new SqlParameter() { ParameterName = "@UPassword", SqlValue = Md5Helper.EncryptString(user.UPassword) });

            return SqlHelper.ExecuteNonQuery(sql, parameters.ToArray());
        }
    }
}
