namespace ht_0102_clock_client
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
            this.btn_GetDate = new System.Windows.Forms.Button();
            this.btn_GetTime = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txb_Port = new System.Windows.Forms.TextBox();
            this.txb_Time = new System.Windows.Forms.TextBox();
            this.txb_Date = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_IP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_GetDate
            // 
            this.btn_GetDate.Location = new System.Drawing.Point(161, 71);
            this.btn_GetDate.Name = "btn_GetDate";
            this.btn_GetDate.Size = new System.Drawing.Size(149, 23);
            this.btn_GetDate.TabIndex = 8;
            this.btn_GetDate.Text = "GetDate";
            this.btn_GetDate.UseVisualStyleBackColor = true;
            this.btn_GetDate.Click += new System.EventHandler(this.btn_GetDate_Click);
            // 
            // btn_GetTime
            // 
            this.btn_GetTime.Location = new System.Drawing.Point(10, 71);
            this.btn_GetTime.Name = "btn_GetTime";
            this.btn_GetTime.Size = new System.Drawing.Size(145, 23);
            this.btn_GetTime.TabIndex = 7;
            this.btn_GetTime.Text = "GetTime";
            this.btn_GetTime.UseVisualStyleBackColor = true;
            this.btn_GetTime.Click += new System.EventHandler(this.btn_GetTime_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(12, 12);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(298, 23);
            this.btn_Connect.TabIndex = 6;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // txb_Port
            // 
            this.txb_Port.Location = new System.Drawing.Point(43, 41);
            this.txb_Port.Name = "txb_Port";
            this.txb_Port.Size = new System.Drawing.Size(75, 20);
            this.txb_Port.TabIndex = 5;
            this.txb_Port.Text = "55555";
            // 
            // txb_Time
            // 
            this.txb_Time.Location = new System.Drawing.Point(10, 100);
            this.txb_Time.Name = "txb_Time";
            this.txb_Time.ReadOnly = true;
            this.txb_Time.Size = new System.Drawing.Size(145, 20);
            this.txb_Time.TabIndex = 5;
            // 
            // txb_Date
            // 
            this.txb_Date.Location = new System.Drawing.Point(161, 100);
            this.txb_Date.Name = "txb_Date";
            this.txb_Date.Size = new System.Drawing.Size(149, 20);
            this.txb_Date.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "port";
            // 
            // txb_IP
            // 
            this.txb_IP.Location = new System.Drawing.Point(189, 41);
            this.txb_IP.Name = "txb_IP";
            this.txb_IP.Size = new System.Drawing.Size(121, 20);
            this.txb_IP.TabIndex = 5;
            this.txb_IP.Text = "default";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "IP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 156);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_GetDate);
            this.Controls.Add(this.btn_GetTime);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txb_Date);
            this.Controls.Add(this.txb_Time);
            this.Controls.Add(this.txb_IP);
            this.Controls.Add(this.txb_Port);
            this.Name = "Form1";
            this.Text = "Get Time Or Date";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_GetDate;
        private System.Windows.Forms.Button btn_GetTime;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txb_Port;
        private System.Windows.Forms.TextBox txb_Time;
        private System.Windows.Forms.TextBox txb_Date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_IP;
        private System.Windows.Forms.Label label2;
    }
}

