namespace gl_005_test_udp
{
    partial class MainF
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
            this.tb_send = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.tb_main = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_send
            // 
            this.tb_send.Location = new System.Drawing.Point(60, 243);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(348, 20);
            this.tb_send.TabIndex = 11;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(414, 243);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 10;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(414, 27);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 9;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(141, 27);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 8;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // tb_main
            // 
            this.tb_main.Location = new System.Drawing.Point(60, 67);
            this.tb_main.Multiline = true;
            this.tb_main.Name = "tb_main";
            this.tb_main.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_main.Size = new System.Drawing.Size(429, 168);
            this.tb_main.TabIndex = 7;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(60, 27);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 6;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 293);
            this.Controls.Add(this.tb_send);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.tb_main);
            this.Controls.Add(this.btn_start);
            this.Name = "MainF";
            this.Text = "Test UDP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_send;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TextBox tb_main;
        private System.Windows.Forms.Button btn_start;
    }
}

