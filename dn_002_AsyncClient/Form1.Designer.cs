namespace dn_002_AsyncClient
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_GetTime = new System.Windows.Forms.Button();
            this.btn_GetDate = new System.Windows.Forms.Button();
            this.lableStatus = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 20);
            this.textBox1.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(185, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // btn_GetTime
            // 
            this.btn_GetTime.Location = new System.Drawing.Point(12, 38);
            this.btn_GetTime.Name = "btn_GetTime";
            this.btn_GetTime.Size = new System.Drawing.Size(75, 23);
            this.btn_GetTime.TabIndex = 3;
            this.btn_GetTime.Text = "GetTime";
            this.btn_GetTime.UseVisualStyleBackColor = true;
            // 
            // btn_GetDate
            // 
            this.btn_GetDate.Location = new System.Drawing.Point(94, 38);
            this.btn_GetDate.Name = "btn_GetDate";
            this.btn_GetDate.Size = new System.Drawing.Size(75, 23);
            this.btn_GetDate.TabIndex = 4;
            this.btn_GetDate.Text = "GetDate";
            this.btn_GetDate.UseVisualStyleBackColor = true;
            // 
            // lableStatus
            // 
            this.lableStatus.Location = new System.Drawing.Point(0, 428);
            this.lableStatus.Name = "lableStatus";
            this.lableStatus.Size = new System.Drawing.Size(800, 22);
            this.lableStatus.TabIndex = 5;
            this.lableStatus.Text = "statusStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lableStatus);
            this.Controls.Add(this.btn_GetDate);
            this.Controls.Add(this.btn_GetTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_GetTime;
        private System.Windows.Forms.Button btn_GetDate;
        private System.Windows.Forms.StatusStrip lableStatus;
    }
}

