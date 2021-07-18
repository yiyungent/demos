using MoeMoonBll;
using MoeMoonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MoeMoonUI
{
    public partial class FormRegisterAcc : Form
    {
        /// <summary>
        /// 业务逻辑层对象
        /// </summary>
        private UserInfoBll uiBll = new UserInfoBll();

        public FormRegisterAcc()
        {
            InitializeComponent();
        }

        #region 为了使窗体能够随意拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void RegisterAccFrm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            // 使窗体能够随意拖动
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        private void RegisterAccFrm_Load(object sender, EventArgs e)
        {

        }

        #region 注册框顶部最小化，关闭按钮组
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseFrm_MouseEnter(object sender, EventArgs e)
        {
            this.btnCloseFrm.BackColor = Color.FromArgb(230, 211, 64, 39);
        }

        private void btnCloseFrm_MouseLeave(object sender, EventArgs e)
        {
            this.btnCloseFrm.BackColor = Color.Transparent;
        }

        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            this.btnMin.BackColor = Color.FromArgb(50, 255, 255, 255);
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            this.btnMin.BackColor = Color.Transparent;
        }
        #endregion

        #region 注册账号
        private void btnRegAcc_Click(object sender, EventArgs e)
        {
            string userCode = this.txtUserCode.Text.Trim();
            string userPwd = this.txtUserPwd.Text.Trim();
            string userName = this.txtUserName.Text.Trim();

            #region 检查输入
            if (string.IsNullOrEmpty(userCode))
            {
                MessageBox.Show("账号不能为空");
                return;
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("昵称不能为空");
                return;
            }
            #endregion

            #region 判断注册
            RegisterState registerState = uiBll.Register(userCode, userPwd, userName);
            if (registerState == RegisterState.Success)
            {
                MessageBox.Show("注册成功\r\n你的账号为: " + this.txtUserCode.Text.Trim());
            }
            else if (registerState == RegisterState.Registed)
            {
                MessageBox.Show("已经存在此账户，请更换账户名，或则用此账户登录");
            }
            else
            {
                MessageBox.Show("注册失败，请稍后再试");
            }
            #endregion
        }
        #endregion
    }
}
