namespace ре_0401_Server
{
    partial class MainF0401Server
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.gb_Connect = new System.Windows.Forms.GroupBox();
            this.gb_Monitor = new System.Windows.Forms.GroupBox();
            this.lb_Monitor = new System.Windows.Forms.ListBox();
            this.gb_Connect.SuspendLayout();
            this.gb_Monitor.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(13, 13);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Enabled = false;
            this.btn_Stop.Location = new System.Drawing.Point(94, 13);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 0;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            // 
            // tb_ip
            // 
            this.tb_ip.Location = new System.Drawing.Point(45, 19);
            this.tb_ip.Name = "tb_ip";
            this.tb_ip.Size = new System.Drawing.Size(100, 20);
            this.tb_ip.TabIndex = 2;
            this.tb_ip.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(45, 45);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(100, 20);
            this.tb_port.TabIndex = 2;
            this.tb_port.Text = "10000";
            // 
            // gb_Connect
            // 
            this.gb_Connect.Controls.Add(this.tb_ip);
            this.gb_Connect.Controls.Add(this.tb_port);
            this.gb_Connect.Controls.Add(this.label1);
            this.gb_Connect.Controls.Add(this.label2);
            this.gb_Connect.Location = new System.Drawing.Point(13, 42);
            this.gb_Connect.Name = "gb_Connect";
            this.gb_Connect.Size = new System.Drawing.Size(156, 82);
            this.gb_Connect.TabIndex = 3;
            this.gb_Connect.TabStop = false;
            this.gb_Connect.Text = "Connect";
            // 
            // gb_Monitor
            // 
            this.gb_Monitor.Controls.Add(this.lb_Monitor);
            this.gb_Monitor.Location = new System.Drawing.Point(13, 131);
            this.gb_Monitor.Name = "gb_Monitor";
            this.gb_Monitor.Size = new System.Drawing.Size(341, 307);
            this.gb_Monitor.TabIndex = 4;
            this.gb_Monitor.TabStop = false;
            this.gb_Monitor.Text = "Monitor";
            // 
            // lb_Monitor
            // 
            this.lb_Monitor.FormattingEnabled = true;
            this.lb_Monitor.HorizontalScrollbar = true;
            this.lb_Monitor.Location = new System.Drawing.Point(7, 20);
            this.lb_Monitor.Name = "lb_Monitor";
            this.lb_Monitor.ScrollAlwaysVisible = true;
            this.lb_Monitor.Size = new System.Drawing.Size(328, 277);
            this.lb_Monitor.TabIndex = 0;
            // 
            // MainF0401Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 450);
            this.Controls.Add(this.gb_Monitor);
            this.Controls.Add(this.gb_Connect);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Name = "MainF0401Server";
            this.Text = "Form1";
            this.gb_Connect.ResumeLayout(false);
            this.gb_Connect.PerformLayout();
            this.gb_Monitor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.GroupBox gb_Connect;
        private System.Windows.Forms.GroupBox gb_Monitor;
        private System.Windows.Forms.ListBox lb_Monitor;
    }
}

