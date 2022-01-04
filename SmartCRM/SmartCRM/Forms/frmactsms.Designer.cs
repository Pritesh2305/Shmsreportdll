namespace SmartCRM.Forms
{
    partial class frmactsms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmactsms));
            this.tbrMain = new System.Windows.Forms.ToolStrip();
            this.btnSms = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlmain = new System.Windows.Forms.Panel();
            this.PnlEntry = new System.Windows.Forms.Panel();
            this.lblmessage = new System.Windows.Forms.Label();
            this.txtsms = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCellNo = new System.Windows.Forms.Label();
            this.lblsmsstatusinfo = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.tbrMain.SuspendLayout();
            this.PnlEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbrMain
            // 
            this.tbrMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(243)))), ((int)(((byte)(254)))));
            this.tbrMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbrMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSms,
            this.ToolStripSeparator1,
            this.btnClose});
            this.tbrMain.Location = new System.Drawing.Point(0, 0);
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tbrMain.Size = new System.Drawing.Size(446, 60);
            this.tbrMain.TabIndex = 20;
            // 
            // btnSms
            // 
            this.btnSms.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSms.Image = ((System.Drawing.Image)(resources.GetObject("btnSms.Image")));
            this.btnSms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSms.Name = "btnSms";
            this.btnSms.Size = new System.Drawing.Size(44, 57);
            this.btnSms.Text = "&Sms";
            this.btnSms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            this.pnlmain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlmain.Location = new System.Drawing.Point(0, 60);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(446, 309);
            this.pnlmain.TabIndex = 21;
            // 
            // PnlEntry
            // 
            this.PnlEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlEntry.Controls.Add(this.lblmessage);
            this.PnlEntry.Controls.Add(this.txtsms);
            this.PnlEntry.Controls.Add(this.label1);
            this.PnlEntry.Controls.Add(this.lblCellNo);
            this.PnlEntry.Controls.Add(this.lblsmsstatusinfo);
            this.PnlEntry.Controls.Add(this.txtMobileNo);
            this.PnlEntry.Location = new System.Drawing.Point(8, 67);
            this.PnlEntry.Name = "PnlEntry";
            this.PnlEntry.Size = new System.Drawing.Size(429, 294);
            this.PnlEntry.TabIndex = 1;
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessage.Location = new System.Drawing.Point(26, 116);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(58, 14);
            this.lblmessage.TabIndex = 5;
            this.lblmessage.Text = "Message";
            // 
            // txtsms
            // 
            this.txtsms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsms.Location = new System.Drawing.Point(25, 132);
            this.txtsms.Multiline = true;
            this.txtsms.Name = "txtsms";
            this.txtsms.Size = new System.Drawing.Size(377, 134);
            this.txtsms.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "[ Seperate Multiple Mobile Nos. With Comma. ]";
            // 
            // lblCellNo
            // 
            this.lblCellNo.AutoSize = true;
            this.lblCellNo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCellNo.Location = new System.Drawing.Point(25, 31);
            this.lblCellNo.Name = "lblCellNo";
            this.lblCellNo.Size = new System.Drawing.Size(71, 14);
            this.lblCellNo.TabIndex = 2;
            this.lblCellNo.Text = "Mobile Nos.";
            // 
            // lblsmsstatusinfo
            // 
            this.lblsmsstatusinfo.AutoSize = true;
            this.lblsmsstatusinfo.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsmsstatusinfo.Location = new System.Drawing.Point(127, 6);
            this.lblsmsstatusinfo.Name = "lblsmsstatusinfo";
            this.lblsmsstatusinfo.Size = new System.Drawing.Size(177, 16);
            this.lblsmsstatusinfo.TabIndex = 1;
            this.lblsmsstatusinfo.Text = "ENTER SMS INFORMATION";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNo.Location = new System.Drawing.Point(25, 65);
            this.txtMobileNo.Multiline = true;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(377, 46);
            this.txtMobileNo.TabIndex = 0;
            // 
            // frmactsms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 369);
            this.Controls.Add(this.PnlEntry);
            this.Controls.Add(this.pnlmain);
            this.Controls.Add(this.tbrMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmactsms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MANUAL SMS ACTIVITY";
            this.tbrMain.ResumeLayout(false);
            this.tbrMain.PerformLayout();
            this.PnlEntry.ResumeLayout(false);
            this.PnlEntry.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip tbrMain;
        internal System.Windows.Forms.ToolStripButton btnSms;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.Panel pnlmain;
        internal System.Windows.Forms.Panel PnlEntry;
        internal System.Windows.Forms.Label lblmessage;
        internal System.Windows.Forms.TextBox txtsms;
        internal System.Windows.Forms.Label lblCellNo;
        internal System.Windows.Forms.Label lblsmsstatusinfo;
        internal System.Windows.Forms.TextBox txtMobileNo;
        internal System.Windows.Forms.Label label1;
    }
}