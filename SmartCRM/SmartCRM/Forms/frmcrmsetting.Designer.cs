namespace SmartCRM.Forms
{
    partial class frmcrmsetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmcrmsetting));
            this.tbrMain = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlmain = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabsms = new System.Windows.Forms.TabPage();
            this.tabemail = new System.Windows.Forms.TabPage();
            this.cmbapiprovider = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsmsusername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtsmspassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsmssender = new System.Windows.Forms.TextBox();
            this.tbrMain.SuspendLayout();
            this.pnlmain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabsms.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbrMain
            // 
            this.tbrMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(243)))), ((int)(((byte)(254)))));
            this.tbrMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbrMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.ToolStripSeparator1,
            this.btnClose});
            this.tbrMain.Location = new System.Drawing.Point(0, 0);
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.Size = new System.Drawing.Size(590, 60);
            this.tbrMain.TabIndex = 22;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(44, 57);
            this.btnSave.Text = "&Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 60);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 57);
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlmain
            // 
            this.pnlmain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(224)))), ((int)(((byte)(245)))));
            this.pnlmain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlmain.Controls.Add(this.tabControl1);
            this.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlmain.Location = new System.Drawing.Point(0, 60);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(590, 263);
            this.pnlmain.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabsms);
            this.tabControl1.Controls.Add(this.tabemail);
            this.tabControl1.Location = new System.Drawing.Point(5, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(580, 250);
            this.tabControl1.TabIndex = 0;
            // 
            // tabsms
            // 
            this.tabsms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(224)))), ((int)(((byte)(245)))));
            this.tabsms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabsms.Controls.Add(this.txtsmssender);
            this.tabsms.Controls.Add(this.label4);
            this.tabsms.Controls.Add(this.txtsmspassword);
            this.tabsms.Controls.Add(this.label3);
            this.tabsms.Controls.Add(this.txtsmsusername);
            this.tabsms.Controls.Add(this.label2);
            this.tabsms.Controls.Add(this.label1);
            this.tabsms.Controls.Add(this.cmbapiprovider);
            this.tabsms.Location = new System.Drawing.Point(4, 22);
            this.tabsms.Name = "tabsms";
            this.tabsms.Padding = new System.Windows.Forms.Padding(3);
            this.tabsms.Size = new System.Drawing.Size(572, 224);
            this.tabsms.TabIndex = 0;
            this.tabsms.Text = "SMS";
            // 
            // tabemail
            // 
            this.tabemail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(224)))), ((int)(((byte)(245)))));
            this.tabemail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabemail.Location = new System.Drawing.Point(4, 22);
            this.tabemail.Name = "tabemail";
            this.tabemail.Padding = new System.Windows.Forms.Padding(3);
            this.tabemail.Size = new System.Drawing.Size(572, 224);
            this.tabemail.TabIndex = 1;
            this.tabemail.Text = "E-Mail";
            // 
            // cmbapiprovider
            // 
            this.cmbapiprovider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbapiprovider.FormattingEnabled = true;
            this.cmbapiprovider.Location = new System.Drawing.Point(83, 15);
            this.cmbapiprovider.Name = "cmbapiprovider";
            this.cmbapiprovider.Size = new System.Drawing.Size(194, 21);
            this.cmbapiprovider.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Provider";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User name";
            // 
            // txtsmsusername
            // 
            this.txtsmsusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsmsusername.Location = new System.Drawing.Point(83, 39);
            this.txtsmsusername.Name = "txtsmsusername";
            this.txtsmsusername.Size = new System.Drawing.Size(194, 20);
            this.txtsmsusername.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            // 
            // txtsmspassword
            // 
            this.txtsmspassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsmspassword.Location = new System.Drawing.Point(83, 62);
            this.txtsmspassword.Name = "txtsmspassword";
            this.txtsmspassword.Size = new System.Drawing.Size(194, 20);
            this.txtsmspassword.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Sender Name";
            // 
            // txtsmssender
            // 
            this.txtsmssender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsmssender.Location = new System.Drawing.Point(83, 85);
            this.txtsmssender.Name = "txtsmssender";
            this.txtsmssender.Size = new System.Drawing.Size(194, 20);
            this.txtsmssender.TabIndex = 2;
            // 
            // frmcrmsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 323);
            this.Controls.Add(this.pnlmain);
            this.Controls.Add(this.tbrMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmcrmsetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CRM SETTINGS";
            this.tbrMain.ResumeLayout(false);
            this.tbrMain.PerformLayout();
            this.pnlmain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabsms.ResumeLayout(false);
            this.tabsms.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip tbrMain;
        internal System.Windows.Forms.ToolStripButton btnSave;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.Panel pnlmain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabsms;
        private System.Windows.Forms.TabPage tabemail;
        private System.Windows.Forms.TextBox txtsmssender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtsmspassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtsmsusername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbapiprovider;
    }
}