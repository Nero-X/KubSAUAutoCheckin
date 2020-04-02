namespace AutoCheckin
{
    partial class FormMsgConf
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.radioButton_Off = new System.Windows.Forms.RadioButton();
            this.radioButton_On = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgv.Enabled = false;
            this.dgv.Location = new System.Drawing.Point(12, 88);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(528, 150);
            this.dgv.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 45F;
            this.Column1.HeaderText = "Предмет";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 35F;
            this.Column2.HeaderText = "Преподаватель";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 20F;
            this.Column3.HeaderText = "Отправлять";
            this.Column3.Name = "Column3";
            // 
            // radioButton_Off
            // 
            this.radioButton_Off.AutoSize = true;
            this.radioButton_Off.Checked = true;
            this.radioButton_Off.Location = new System.Drawing.Point(35, 12);
            this.radioButton_Off.Name = "radioButton_Off";
            this.radioButton_Off.Size = new System.Drawing.Size(83, 17);
            this.radioButton_Off.TabIndex = 1;
            this.radioButton_Off.TabStop = true;
            this.radioButton_Off.Text = "Выключено";
            this.radioButton_Off.UseVisualStyleBackColor = true;
            this.radioButton_Off.CheckedChanged += new System.EventHandler(this.radioButton_Off_CheckedChanged);
            // 
            // radioButton_On
            // 
            this.radioButton_On.AutoSize = true;
            this.radioButton_On.Location = new System.Drawing.Point(35, 50);
            this.radioButton_On.Name = "radioButton_On";
            this.radioButton_On.Size = new System.Drawing.Size(75, 17);
            this.radioButton_On.TabIndex = 2;
            this.radioButton_On.Text = "Включено";
            this.radioButton_On.UseVisualStyleBackColor = true;
            this.radioButton_On.CheckedChanged += new System.EventHandler(this.radioButton_On_CheckedChanged);
            // 
            // FormMsgConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 318);
            this.Controls.Add(this.radioButton_On);
            this.Controls.Add(this.radioButton_Off);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMsgConf";
            this.Text = "FormMsgConf";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.RadioButton radioButton_Off;
        private System.Windows.Forms.RadioButton radioButton_On;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
    }
}