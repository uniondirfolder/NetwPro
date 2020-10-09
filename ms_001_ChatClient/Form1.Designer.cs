namespace ms_001_ChatClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_What = new System.Windows.Forms.Label();
            this.txb_ChatName = new System.Windows.Forms.TextBox();
            this.btn_ConnectToServer = new System.Windows.Forms.Button();
            this.txb_ChatMonitor = new System.Windows.Forms.TextBox();
            this.txb_SendMsg = new System.Windows.Forms.TextBox();
            this.btn_SendMsg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_What
            // 
            this.lbl_What.AutoSize = true;
            this.lbl_What.Location = new System.Drawing.Point(13, 13);
            this.lbl_What.Name = "lbl_What";
            this.lbl_What.Size = new System.Drawing.Size(129, 15);
            this.lbl_What.TabIndex = 0;
            this.lbl_What.Text = "Enter your chat name : ";
            // 
            // txb_ChatName
            // 
            this.txb_ChatName.Location = new System.Drawing.Point(148, 10);
            this.txb_ChatName.Name = "txb_ChatName";
            this.txb_ChatName.Size = new System.Drawing.Size(186, 23);
            this.txb_ChatName.TabIndex = 1;
            this.txb_ChatName.TextChanged += new System.EventHandler(this.txb_ChatName_TextChanged);
            // 
            // btn_ConnectToServer
            // 
            this.btn_ConnectToServer.Location = new System.Drawing.Point(148, 39);
            this.btn_ConnectToServer.Name = "btn_ConnectToServer";
            this.btn_ConnectToServer.Size = new System.Drawing.Size(185, 25);
            this.btn_ConnectToServer.TabIndex = 2;
            this.btn_ConnectToServer.Text = "Connect to Server";
            this.btn_ConnectToServer.UseVisualStyleBackColor = true;
            this.btn_ConnectToServer.Click += new System.EventHandler(this.btn_ConnectToServer_Click);
            // 
            // txb_ChatMonitor
            // 
            this.txb_ChatMonitor.Location = new System.Drawing.Point(13, 70);
            this.txb_ChatMonitor.Multiline = true;
            this.txb_ChatMonitor.Name = "txb_ChatMonitor";
            this.txb_ChatMonitor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txb_ChatMonitor.Size = new System.Drawing.Size(320, 247);
            this.txb_ChatMonitor.TabIndex = 3;
            // 
            // txb_SendMsg
            // 
            this.txb_SendMsg.Location = new System.Drawing.Point(13, 324);
            this.txb_SendMsg.Name = "txb_SendMsg";
            this.txb_SendMsg.Size = new System.Drawing.Size(320, 23);
            this.txb_SendMsg.TabIndex = 4;
            this.txb_SendMsg.TextChanged += new System.EventHandler(this.txb_SendMsg_TextChanged);
            // 
            // btn_SendMsg
            // 
            this.btn_SendMsg.Location = new System.Drawing.Point(148, 354);
            this.btn_SendMsg.Name = "btn_SendMsg";
            this.btn_SendMsg.Size = new System.Drawing.Size(185, 23);
            this.btn_SendMsg.TabIndex = 5;
            this.btn_SendMsg.Text = "Send Message";
            this.btn_SendMsg.UseVisualStyleBackColor = true;
            this.btn_SendMsg.Click += new System.EventHandler(this.btn_SendMsg_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 398);
            this.Controls.Add(this.btn_SendMsg);
            this.Controls.Add(this.txb_SendMsg);
            this.Controls.Add(this.txb_ChatMonitor);
            this.Controls.Add(this.btn_ConnectToServer);
            this.Controls.Add(this.txb_ChatName);
            this.Controls.Add(this.lbl_What);
            this.Name = "Form1";
            this.Text = "Chat Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_What;
        private System.Windows.Forms.TextBox txb_ChatName;
        private System.Windows.Forms.Button btn_ConnectToServer;
        private System.Windows.Forms.TextBox txb_ChatMonitor;
        private System.Windows.Forms.TextBox txb_SendMsg;
        private System.Windows.Forms.Button btn_SendMsg;
    }
}

