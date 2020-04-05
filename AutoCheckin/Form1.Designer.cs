namespace AutoCheckin
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_ver = new System.Windows.Forms.Label();
            this.button_stopAll = new System.Windows.Forms.Button();
            this.button_startAll = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cookieDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.webClientDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scheduleDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userInfoDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.studentBS = new System.Windows.Forms.BindingSource(this.components);
            this.cookie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonStart = new System.Windows.Forms.DataGridViewButtonColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRules = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBS)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Посещение пар в фоновом режиме!";
            this.notifyIcon1.BalloonTipTitle = "AutoCheckin";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "AutoCheckin";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1800000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_ver
            // 
            this.label_ver.AutoSize = true;
            this.label_ver.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label_ver.Location = new System.Drawing.Point(12, 194);
            this.label_ver.Name = "label_ver";
            this.label_ver.Size = new System.Drawing.Size(22, 13);
            this.label_ver.TabIndex = 4;
            this.label_ver.Text = "1.0";
            // 
            // button_stopAll
            // 
            this.button_stopAll.Location = new System.Drawing.Point(362, 184);
            this.button_stopAll.Name = "button_stopAll";
            this.button_stopAll.Size = new System.Drawing.Size(102, 23);
            this.button_stopAll.TabIndex = 6;
            this.button_stopAll.Text = "Остановить всех";
            this.button_stopAll.UseVisualStyleBackColor = true;
            this.button_stopAll.Click += new System.EventHandler(this.button_stopAll_Click);
            // 
            // button_startAll
            // 
            this.button_startAll.Location = new System.Drawing.Point(253, 184);
            this.button_startAll.Name = "button_startAll";
            this.button_startAll.Size = new System.Drawing.Size(103, 23);
            this.button_startAll.TabIndex = 7;
            this.button_startAll.Text = "Стартовать всех";
            this.button_startAll.UseVisualStyleBackColor = true;
            this.button_startAll.Click += new System.EventHandler(this.button_startAll_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 43200000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cookie,
            this.name,
            this.buttonStart,
            this.status,
            this.buttonRules});
            this.dgv.DataSource = this.studentBS;
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(576, 150);
            this.dgv.TabIndex = 5;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_UserAddedRow);
            this.dgv.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_UserDeletedRow);
            // 
            // cookieDataGridViewTextBoxColumn1
            // 
            this.cookieDataGridViewTextBoxColumn1.DataPropertyName = "Cookie";
            this.cookieDataGridViewTextBoxColumn1.HeaderText = "Cookie";
            this.cookieDataGridViewTextBoxColumn1.Name = "cookieDataGridViewTextBoxColumn1";
            // 
            // webClientDataGridViewTextBoxColumn1
            // 
            this.webClientDataGridViewTextBoxColumn1.DataPropertyName = "WebClient";
            this.webClientDataGridViewTextBoxColumn1.HeaderText = "WebClient";
            this.webClientDataGridViewTextBoxColumn1.Name = "webClientDataGridViewTextBoxColumn1";
            this.webClientDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // scheduleDataGridViewTextBoxColumn1
            // 
            this.scheduleDataGridViewTextBoxColumn1.DataPropertyName = "Schedule";
            this.scheduleDataGridViewTextBoxColumn1.HeaderText = "Schedule";
            this.scheduleDataGridViewTextBoxColumn1.Name = "scheduleDataGridViewTextBoxColumn1";
            // 
            // userInfoDataGridViewTextBoxColumn1
            // 
            this.userInfoDataGridViewTextBoxColumn1.DataPropertyName = "UserInfo";
            this.userInfoDataGridViewTextBoxColumn1.HeaderText = "UserInfo";
            this.userInfoDataGridViewTextBoxColumn1.Name = "userInfoDataGridViewTextBoxColumn1";
            // 
            // studentBS
            // 
            this.studentBS.AllowNew = true;
            this.studentBS.DataSource = typeof(AutoCheckin.Student);
            // 
            // cookie
            // 
            this.cookie.DataPropertyName = "Cookie";
            this.cookie.HeaderText = ".AspNet.ApplicationCookie";
            this.cookie.Name = "cookie";
            this.cookie.Width = 150;
            // 
            // name
            // 
            this.name.DataPropertyName = "Name";
            this.name.HeaderText = "ФИО, группа";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 150;
            // 
            // buttonStart
            // 
            this.buttonStart.HeaderText = "Старт";
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Text = "";
            this.buttonStart.Width = 63;
            // 
            // status
            // 
            this.status.HeaderText = "Статус";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.status.Width = 70;
            // 
            // buttonRules
            // 
            this.buttonRules.HeaderText = "Сообщения";
            this.buttonRules.Name = "buttonRules";
            this.buttonRules.Text = "Настроить";
            this.buttonRules.UseColumnTextForButtonValue = true;
            this.buttonRules.Width = 80;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 217);
            this.Controls.Add(this.button_startAll);
            this.Controls.Add(this.button_stopAll);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label_ver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "AutoCheckin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_ver;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button button_stopAll;
        private System.Windows.Forms.Button button_startAll;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.BindingSource studentBS;
        private System.Windows.Forms.DataGridViewTextBoxColumn scheduleDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn userInfoDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cookieDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn webClientDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cookie;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewButtonColumn buttonStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewButtonColumn buttonRules;
    }
}

