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
    public partial class FormLogin : Form
    {
        /// <summary>
        /// 业务逻辑层对象
        /// </summary>
        private UserInfoBll uiBll = new UserInfoBll();

        public FormLogin()
        {
            InitializeComponent();
            // 注意：一定要在初始化控件后才能 执行InitializeSettings()
            InitializeSettings();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {

        }

        #region 初始化设置，控件等
        private void InitializeSettings()
        {
            #region 界面初始化
            // 将panel父控件设置为背景图片控件
            //this.panelRightTop.Parent = this.panelHeader;

            // 现在已经没有必要，因为
            // 已经在设计器this.Controls.Add(this.panelHeader);
            // 而this.panelHeader.Controls.Add(this.panelRightTop);
            // 这样panelRigthTop的父容器就是panelHeader
            #endregion
        }
        #endregion

        #region 右侧任务栏通知图标
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIconOfTaskRight.Visible = false;
            this.Close();
            this.Dispose();
            Application.Exit();
        }

        private void hideMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void notifyIconOfTaskRight_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // 如果是右键点击不做任何操作
            {
                return;
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }
        #endregion

        #region 登录框顶部最小化，关闭按钮组
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

        #region Label注册账号
        /// <summary>
        /// 点击注册账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRegisterAcc_Click(object sender, EventArgs e)
        {
            FormRegisterAcc registerAccFrm = new FormRegisterAcc();
            registerAccFrm.Show();
        }

        private void lblRegisterAcc_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(200, 38, 133, 227);
            // 鼠标移入改变Cursor
            LblCursorToHand(sender);
        }

        private void lblRegisterAcc_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(255, 38, 133, 227);
        }
        #endregion

        #region Label找回密码
        /// <summary>
        /// 点击找回密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRetrievePwd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("找回密码");
        }

        private void lblRetrievePwd_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(200, 38, 133, 227);
            // 鼠标移入改变Cursor
            LblCursorToHand(sender);
        }

        private void lblRetrievePwd_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(255, 38, 133, 227);
        }
        #endregion

        #region 将Label控件的Cursor属性设置成自定义图标
        /// <summary>
        /// 将Label控件的Cursor属性设置成自定义图标
        /// </summary>
        /// <param name="sender">Label控件对象</param>
        private void LblCursorToHand(object sender)
        {
            // 记录：不知道为何离开后会自动恢复为默认
            Cursor lbl_cur_hand = new Cursor("res/cursor_enter.cur");
            ((Label)sender).Cursor = lbl_cur_hand;
        }
        #endregion

        #region 为了使窗体能够随意拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void LoginFrm_MouseDown(object sender, MouseEventArgs e)
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

        #region 登录账号
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userCode = this.txtUserCode.Text.Trim();
            string userPwd = this.txtUserPwd.Text.Trim();

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
            #endregion

            #region 判断是否登录成功
            LoginState loginState = uiBll.Login(userCode, userPwd);
            if (loginState == LoginState.WithoutAcc)
            {
                MessageBox.Show("无此账户");
            }
            else if (loginState == LoginState.Failure)
            {
                MessageBox.Show("账号或密码输入错误");
            }
            else if (loginState == LoginState.Success)
            {
                FormMain main = new FormMain();
                this.Hide();
                main.Show();
            } 
            #endregion
        }
        #endregion
    }
}
