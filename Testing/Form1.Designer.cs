namespace Testing
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_display_messageBox = new System.Windows.Forms.CheckBox();
            this.label_response = new System.Windows.Forms.Label();
            this.textBox_receivePort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_submit = new System.Windows.Forms.Button();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_display_messageBox);
            this.groupBox1.Controls.Add(this.label_response);
            this.groupBox1.Controls.Add(this.textBox_receivePort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button_submit);
            this.groupBox1.Controls.Add(this.textBox_message);
            this.groupBox1.Controls.Add(this.textBox_port);
            this.groupBox1.Controls.Add(this.textBox_ip);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Testing Information";
            // 
            // checkBox_display_messageBox
            // 
            this.checkBox_display_messageBox.AutoSize = true;
            this.checkBox_display_messageBox.Location = new System.Drawing.Point(430, 66);
            this.checkBox_display_messageBox.Name = "checkBox_display_messageBox";
            this.checkBox_display_messageBox.Size = new System.Drawing.Size(140, 19);
            this.checkBox_display_messageBox.TabIndex = 11;
            this.checkBox_display_messageBox.Text = "Result In MessageBox";
            this.checkBox_display_messageBox.UseVisualStyleBackColor = true;
            // 
            // label_response
            // 
            this.label_response.AutoSize = true;
            this.label_response.Location = new System.Drawing.Point(490, 18);
            this.label_response.Name = "label_response";
            this.label_response.Size = new System.Drawing.Size(0, 15);
            this.label_response.TabIndex = 10;
            // 
            // textBox_receivePort
            // 
            this.textBox_receivePort.Location = new System.Drawing.Point(324, 36);
            this.textBox_receivePort.Name = "textBox_receivePort";
            this.textBox_receivePort.Size = new System.Drawing.Size(100, 23);
            this.textBox_receivePort.TabIndex = 4;
            this.textBox_receivePort.Text = "4001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(324, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Receie Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Response:";
            // 
            // button_submit
            // 
            this.button_submit.Location = new System.Drawing.Point(430, 37);
            this.button_submit.Name = "button_submit";
            this.button_submit.Size = new System.Drawing.Size(117, 23);
            this.button_submit.TabIndex = 6;
            this.button_submit.Text = "Submit";
            this.button_submit.UseVisualStyleBackColor = true;
            this.button_submit.Click += new System.EventHandler(this.button_submit_Click);
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(218, 37);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(100, 23);
            this.textBox_message.TabIndex = 3;
            this.textBox_message.Text = "getplayercount";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(112, 37);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 23);
            this.textBox_port.TabIndex = 2;
            this.textBox_port.Text = "4000";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(6, 37);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(100, 23);
            this.textBox_ip.TabIndex = 1;
            this.textBox_ip.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Send Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 125);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Testing";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Label label_response;
        private TextBox textBox_receivePort;
        private Label label5;
        private Label label4;
        private Button button_submit;
        private TextBox textBox_message;
        private TextBox textBox_port;
        private TextBox textBox_ip;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox checkBox_display_messageBox;
    }
}