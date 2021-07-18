using MoeMoonCommon;
using MoeMoonDal;
using MoeMoonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeMoonBll
{
    public partial class UserInfoBll
    {
        /// <summary>
        /// 数据层对象
        /// </summary>
        private UserInfoDal uiDal = new UserInfoDal();

        /// <summary>
        /// 登录,根据账户和密码判断是否能登录
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userPwd"></param>
        /// <returns>返回登录状态</returns>
        public LoginState Login(string userCode, string userPwd)
        {
            UserInfo user = uiDal.GetByUCode(userCode);
            if (user == null || user.UDelFlag == 1)
            {
                // 无此账户, UDelFlag为1 表示被删除
                return LoginState.WithoutAcc;
            }
            else if (user.UPassword.Equals(Md5Helper.EncryptString(userPwd)))
            {
                // 密码正确,登录成功
                return LoginState.Success;
            }
            else
            {
                // 账户或密码错误
                // 填写的密码与此账户的密码不一致
                return LoginState.Failure;
            }
        }

        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userPwd"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public RegisterState Register(string userCode, string userPwd, string userName)
        {
            if (uiDal.GetByUCode(userCode) != null)
            {
                // 如果说已经有此uCode的用户，则说明此账户已经被注册
                return RegisterState.Registed;
            }
            UserInfo regUser = new UserInfo() { UCode = userCode, UPassword = userPwd, UName = userName };
            int count = uiDal.Insert(regUser);
            if (count == 1)
            {
                return RegisterState.Success;
            }
            else
            {
                return RegisterState.Failure;
            }
        }
    }
}
