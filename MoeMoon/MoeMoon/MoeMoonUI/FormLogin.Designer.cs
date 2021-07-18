namespace MoeMoonUI
{
    partial class FormLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.txtUserCode = new CCWin.SkinControl.SkinTextBox();
            this.txtUserPwd = new CCWin.SkinControl.SkinTextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.notifyIconOfTaskRight = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripOfTaskRight = new CCWin.SkinControl.SkinContextMenuStrip();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRegisterAcc = new CCWin.SkinControl.SkinLabel();
            this.lblRetrievePwd = new CCWin.SkinControl.SkinLabel();
            this.btnMin = new CCWin.SkinControl.SkinButton();
            this.btnCloseFrm = new CCWin.SkinControl.SkinButton();
            this.panelRightTop = new CCWin.SkinControl.SkinPanel();
            this.panelHeader = new CCWin.SkinControl.SkinPanel();
            this.contextMenuStripOfTaskRight.SuspendLayout();
            this.panelRightTop.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserCode
            // 
            this.txtUserCode.BackColor = System.Drawing.Color.Transparent;
            this.txtUserCode.DownBack = null;
            this.txtUserCode.Icon = null;
            this.txtUserCode.IconIsButton = false;
            this.txtUserCode.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserCode.IsPasswordChat = '\0';
            this.txtUserCode.IsSystemPasswordChar = false;
            this.txtUserCode.Lines = new string[0];
            this.txtUserCode.Location = new System.Drawing.Point(200, 291);
            this.txtUserCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserCode.MaxLength = 32767;
            this.txtUserCode.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtUserCode.MouseBack = null;
            this.txtUserCode.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserCode.Multiline = true;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.NormlBack = null;
            this.txtUserCode.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserCode.ReadOnly = false;
            this.txtUserCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserCode.Size = new System.Drawing.Size(290, 44);
            // 
            // 
            // 
            this.txtUserCode.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserCode.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserCode.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtUserCode.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtUserCode.SkinTxt.Multiline = true;
            this.txtUserCode.SkinTxt.Name = "BaseText";
            this.txtUserCode.SkinTxt.Size = new System.Drawing.Size(280, 34);
            this.txtUserCode.SkinTxt.TabIndex = 0;
            this.txtUserCode.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserCode.SkinTxt.WaterText = "QQ号码/手机/邮箱";
            this.txtUserCode.TabIndex = 0;
            this.txtUserCode.TabStop = true;
            this.txtUserCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUserCode.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserCode.WaterText = "QQ号码/手机/邮箱";
            this.txtUserCode.WordWrap = true;
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.BackColor = System.Drawing.Color.Transparent;
            this.txtUserPwd.DownBack = null;
            this.txtUserPwd.Icon = null;
            this.txtUserPwd.IconIsButton = false;
            this.txtUserPwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserPwd.IsPasswordChat = '·';
            this.txtUserPwd.IsSystemPasswordChar = false;
            this.txtUserPwd.Lines = new string[0];
            this.txtUserPwd.Location = new System.Drawing.Point(200, 336);
            this.txtUserPwd.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserPwd.MaxLength = 32767;
            this.txtUserPwd.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtUserPwd.MouseBack = null;
            this.txtUserPwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtUserPwd.Multiline = true;
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.NormlBack = null;
            this.txtUserPwd.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserPwd.ReadOnly = false;
            this.txtUserPwd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserPwd.Size = new System.Drawing.Size(290, 44);
            // 
            // 
            // 
            this.txtUserPwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserPwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserPwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtUserPwd.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtUserPwd.SkinTxt.Multiline = true;
            this.txtUserPwd.SkinTxt.Name = "BaseText";
            this.txtUserPwd.SkinTxt.PasswordChar = '·';
            this.txtUserPwd.SkinTxt.Size = new System.Drawing.Size(280, 34);
            this.txtUserPwd.SkinTxt.TabIndex = 0;
            this.txtUserPwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserPwd.SkinTxt.WaterText = "密码";
            this.txtUserPwd.TabIndex = 0;
            this.txtUserPwd.TabStop = true;
            this.txtUserPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUserPwd.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtUserPwd.WaterText = "密码";
            this.txtUserPwd.WordWrap = true;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(163)))), ((int)(((byte)(255)))));
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(195)))), ((int)(((byte)(245)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(200, 430);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(292, 45);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.TabStop = false;
            this.btnLogin.Text = "登  录";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // notifyIconOfTaskRight
            // 
            this.notifyIconOfTaskRight.ContextMenuStrip = this.contextMenuStripOfTaskRight;
            this.notifyIconOfTaskRight.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconOfTaskRight.Icon")));
            this.notifyIconOfTaskRight.Text = "MoeMoon";
            this.notifyIconOfTaskRight.Visible = true;
            this.notifyIconOfTaskRight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconOfTaskRight_MouseClick);
            // 
            // contextMenuStripOfTaskRight
            // 
            this.contextMenuStripOfTaskRight.Arrow = System.Drawing.Color.Black;
            this.contextMenuStripOfTaskRight.Back = System.Drawing.Color.White;
            this.contextMenuStripOfTaskRight.BackRadius = 4;
            this.contextMenuStripOfTaskRight.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.contextMenuStripOfTaskRight.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.contextMenuStripOfTaskRight.Fore = System.Drawing.Color.Black;
            this.contextMenuStripOfTaskRight.HoverFore = System.Drawing.Color.White;
            this.contextMenuStripOfTaskRight.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripOfTaskRight.ItemAnamorphosis = true;
            this.contextMenuStripOfTaskRight.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.contextMenuStripOfTaskRight.ItemBorderShow = true;
            this.contextMenuStripOfTaskRight.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.contextMenuStripOfTaskRight.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.contextMenuStripOfTaskRight.ItemRadius = 4;
            this.contextMenuStripOfTaskRight.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.contextMenuStripOfTaskRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem,
            this.hideMenuItem,
            this.showMenuItem});
            this.contextMenuStripOfTaskRight.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.contextMenuStripOfTaskRight.Name = "contextMenuStripOfTaskRight";
            this.contextMenuStripOfTaskRight.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.contextMenuStripOfTaskRight.Size = new System.Drawing.Size(117, 88);
            this.contextMenuStripOfTaskRight.SkinAllColor = true;
            this.contextMenuStripOfTaskRight.TitleAnamorphosis = true;
            this.contextMenuStripOfTaskRight.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.contextMenuStripOfTaskRight.TitleRadius = 4;
            this.contextMenuStripOfTaskRight.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(116, 28);
            this.exitMenuItem.Text = "退出";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // hideMenuItem
            // 
            this.hideMenuItem.Name = "hideMenuItem";
            this.hideMenuItem.Size = new System.Drawing.Size(116, 28);
            this.hideMenuItem.Text = "隐藏";
            this.hideMenuItem.Click += new System.EventHandler(this.hideMenuItem_Click);
            // 
            // showMenuItem
            // 
            this.showMenuItem.Name = "showMenuItem";
            this.showMenuItem.Size = new System.Drawing.Size(116, 28);
            this.showMenuItem.Text = "显示";
            this.showMenuItem.Click += new System.EventHandler(this.showMenuItem_Click);
            // 
            // lblRegisterAcc
            // 
            this.lblRegisterAcc.AutoSize = true;
            this.lblRegisterAcc.BackColor = System.Drawing.Color.Transparent;
            this.lblRegisterAcc.BorderColor = System.Drawing.Color.White;
            this.lblRegisterAcc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegisterAcc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(133)))), ((int)(((byte)(227)))));
            this.lblRegisterAcc.Location = new System.Drawing.Point(524, 291);
            this.lblRegisterAcc.Name = "lblRegisterAcc";
            this.lblRegisterAcc.Size = new System.Drawing.Size(82, 24);
            this.lblRegisterAcc.TabIndex = 4;
            this.lblRegisterAcc.Text = "注册账号";
            this.lblRegisterAcc.Click += new System.EventHandler(this.lblRegisterAcc_Click);
            this.lblRegisterAcc.MouseEnter += new System.EventHandler(this.lblRegisterAcc_MouseEnter);
            this.lblRegisterAcc.MouseLeave += new System.EventHandler(this.lblRegisterAcc_MouseLeave);
            // 
            // lblRetrievePwd
            // 
            this.lblRetrievePwd.AutoSize = true;
            this.lblRetrievePwd.BackColor = System.Drawing.Color.Transparent;
            this.lblRetrievePwd.BorderColor = System.Drawing.Color.White;
            this.lblRetrievePwd.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblRetrievePwd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRetrievePwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(133)))), ((int)(((byte)(227)))));
            this.lblRetrievePwd.Location = new System.Drawing.Point(524, 346);
            this.lblRetrievePwd.Name = "lblRetrievePwd";
            this.lblRetrievePwd.Size = new System.Drawing.Size(82, 24);
            this.lblRetrievePwd.TabIndex = 4;
            this.lblRetrievePwd.Text = "找回密码";
            this.lblRetrievePwd.Click += new System.EventHandler(this.lblRetrievePwd_Click);
            this.lblRetrievePwd.MouseEnter += new System.EventHandler(this.lblRetrievePwd_MouseEnter);
            this.lblRetrievePwd.MouseLeave += new System.EventHandler(this.lblRetrievePwd_MouseLeave);
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BaseColor = System.Drawing.Color.Transparent;
            this.btnMin.BorderColor = System.Drawing.Color.Transparent;
            this.btnMin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnMin.DownBack = null;
            this.btnMin.DownBaseColor = System.Drawing.Color.Transparent;
            this.btnMin.DrawType = CCWin.SkinControl.DrawStyle.None;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.InnerBorderColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(191, 0);
            this.btnMin.MouseBack = null;
            this.btnMin.MouseBaseColor = System.Drawing.Color.Transparent;
            this.btnMin.Name = "btnMin";
            this.btnMin.NormlBack = null;
            this.btnMin.Size = new System.Drawing.Size(40, 40);
            this.btnMin.TabIndex = 1;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            this.btnMin.MouseEnter += new System.EventHandler(this.btnMin_MouseEnter);
            this.btnMin.MouseLeave += new System.EventHandler(this.btnMin_MouseLeave);
            // 
            // btnCloseFrm
            // 
            this.btnCloseFrm.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseFrm.BaseColor = System.Drawing.Color.Transparent;
            this.btnCloseFrm.BorderColor = System.Drawing.Color.Transparent;
            this.btnCloseFrm.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnCloseFrm.DownBack = null;
            this.btnCloseFrm.DownBaseColor = System.Drawing.Color.Transparent;
            this.btnCloseFrm.DrawType = CCWin.SkinControl.DrawStyle.None;
            this.btnCloseFrm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCloseFrm.ForeColor = System.Drawing.Color.White;
            this.btnCloseFrm.InnerBorderColor = System.Drawing.Color.White;
            this.btnCloseFrm.Location = new System.Drawing.Point(237, 0);
            this.btnCloseFrm.MouseBack = null;
            this.btnCloseFrm.MouseBaseColor = System.Drawing.Color.Transparent;
            this.btnCloseFrm.Name = "btnCloseFrm";
            this.btnCloseFrm.NormlBack = null;
            this.btnCloseFrm.Size = new System.Drawing.Size(40, 40);
            this.btnCloseFrm.TabIndex = 1;
            this.btnCloseFrm.Text = "×";
            this.btnCloseFrm.UseVisualStyleBackColor = false;
            this.btnCloseFrm.Click += new System.EventHandler(this.btnCloseFrm_Click);
            this.btnCloseFrm.MouseEnter += new System.EventHandler(this.btnCloseFrm_MouseEnter);
            this.btnCloseFrm.MouseLeave += new System.EventHandler(this.btnCloseFrm_MouseLeave);
            // 
            // panelRightTop
            // 
            this.panelRightTop.BackColor = System.Drawing.Color.Transparent;
            this.panelRightTop.Controls.Add(this.btnMin);
            this.panelRightTop.Controls.Add(this.btnCloseFrm);
            this.panelRightTop.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.panelRightTop.DownBack = null;
            this.panelRightTop.ForeColor = System.Drawing.Color.White;
            this.panelRightTop.Location = new System.Drawing.Point(368, 0);
            this.panelRightTop.MouseBack = null;
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.NormlBack = null;
            this.panelRightTop.Size = new System.Drawing.Size(277, 128);
            this.panelRightTop.TabIndex = 3;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(118)))), ((int)(((byte)(209)))));
            this.panelHeader.Controls.Add(this.panelRightTop);
            this.panelHeader.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.DownBack = null;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.MouseBack = null;
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.NormlBack = null;
            this.panelHeader.Size = new System.Drawing.Size(645, 273);
            this.panelHeader.TabIndex = 5;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHeader_MouseDown);
            // 
            // LoginFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 495);
            this.Controls.Add(this.lblRetrievePwd);
            this.Controls.Add(this.lblRegisterAcc);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUserPwd);
            this.Controls.Add(this.txtUserCode);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗体";
            this.Load += new System.EventHandler(this.LoginFrm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LoginFrm_MouseDown);
            this.contextMenuStripOfTaskRight.ResumeLayout(false);
            this.panelRightTop.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CCWin.SkinControl.SkinTextBox txtUserCode;
        private CCWin.SkinControl.SkinTextBox txtUserPwd;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.NotifyIcon notifyIconOfTaskRight;
        private CCWin.SkinControl.SkinContextMenuStrip contextMenuStripOfTaskRight;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMenuItem;
        private CCWin.SkinControl.SkinLabel lblRegisterAcc;
        private CCWin.SkinControl.SkinLabel lblRetrievePwd;
        private CCWin.SkinControl.SkinButton btnMin;
        private CCWin.SkinControl.SkinButton btnCloseFrm;
        private CCWin.SkinControl.SkinPanel panelRightTop;
        private CCWin.SkinControl.SkinPanel panelHeader;
    }
}

