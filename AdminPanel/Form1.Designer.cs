namespace AdminPanel
{
    partial class AdminCenter
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
            this.button_connect = new System.Windows.Forms.Button();
            this.textBox_sql_bot_token = new System.Windows.Forms.TextBox();
            this.label_sql_bot_token = new System.Windows.Forms.Label();
            this.label_sql_bot_status = new System.Windows.Forms.Label();
            this.textBox_sql_bot_status = new System.Windows.Forms.TextBox();
            this.label_sql_bot_game = new System.Windows.Forms.Label();
            this.textBox_sql_bot_game = new System.Windows.Forms.TextBox();
            this.label_sql_bot_displayName = new System.Windows.Forms.Label();
            this.textBox_sql_bot_displayName = new System.Windows.Forms.TextBox();
            this.button_sql_bot_update = new System.Windows.Forms.Button();
            this.textBox_sql_bot_id = new System.Windows.Forms.TextBox();
            this.button_sql_game_delete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGrid_sql_output = new System.Windows.Forms.DataGridView();
            this.label_logger = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_sql_output)).BeginInit();
            this.SuspendLayout();
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(6, 34);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(75, 23);
            this.button_connect.TabIndex = 0;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // textBox_sql_bot_token
            // 
            this.textBox_sql_bot_token.Location = new System.Drawing.Point(6, 35);
            this.textBox_sql_bot_token.Name = "textBox_sql_bot_token";
            this.textBox_sql_bot_token.Size = new System.Drawing.Size(187, 23);
            this.textBox_sql_bot_token.TabIndex = 1;
            // 
            // label_sql_bot_token
            // 
            this.label_sql_bot_token.AutoSize = true;
            this.label_sql_bot_token.Location = new System.Drawing.Point(6, 17);
            this.label_sql_bot_token.Name = "label_sql_bot_token";
            this.label_sql_bot_token.Size = new System.Drawing.Size(38, 15);
            this.label_sql_bot_token.TabIndex = 2;
            this.label_sql_bot_token.Text = "Token";
            // 
            // label_sql_bot_status
            // 
            this.label_sql_bot_status.AutoSize = true;
            this.label_sql_bot_status.Location = new System.Drawing.Point(199, 17);
            this.label_sql_bot_status.Name = "label_sql_bot_status";
            this.label_sql_bot_status.Size = new System.Drawing.Size(39, 15);
            this.label_sql_bot_status.TabIndex = 4;
            this.label_sql_bot_status.Text = "Status";
            // 
            // textBox_sql_bot_status
            // 
            this.textBox_sql_bot_status.Location = new System.Drawing.Point(199, 35);
            this.textBox_sql_bot_status.Name = "textBox_sql_bot_status";
            this.textBox_sql_bot_status.Size = new System.Drawing.Size(164, 23);
            this.textBox_sql_bot_status.TabIndex = 3;
            // 
            // label_sql_bot_game
            // 
            this.label_sql_bot_game.AutoSize = true;
            this.label_sql_bot_game.Location = new System.Drawing.Point(369, 19);
            this.label_sql_bot_game.Name = "label_sql_bot_game";
            this.label_sql_bot_game.Size = new System.Drawing.Size(38, 15);
            this.label_sql_bot_game.TabIndex = 6;
            this.label_sql_bot_game.Text = "Game";
            // 
            // textBox_sql_bot_game
            // 
            this.textBox_sql_bot_game.Location = new System.Drawing.Point(369, 36);
            this.textBox_sql_bot_game.Name = "textBox_sql_bot_game";
            this.textBox_sql_bot_game.Size = new System.Drawing.Size(133, 23);
            this.textBox_sql_bot_game.TabIndex = 5;
            // 
            // label_sql_bot_displayName
            // 
            this.label_sql_bot_displayName.AutoSize = true;
            this.label_sql_bot_displayName.Location = new System.Drawing.Point(508, 17);
            this.label_sql_bot_displayName.Name = "label_sql_bot_displayName";
            this.label_sql_bot_displayName.Size = new System.Drawing.Size(80, 15);
            this.label_sql_bot_displayName.TabIndex = 8;
            this.label_sql_bot_displayName.Text = "Display Name";
            // 
            // textBox_sql_bot_displayName
            // 
            this.textBox_sql_bot_displayName.Location = new System.Drawing.Point(508, 35);
            this.textBox_sql_bot_displayName.Name = "textBox_sql_bot_displayName";
            this.textBox_sql_bot_displayName.Size = new System.Drawing.Size(133, 23);
            this.textBox_sql_bot_displayName.TabIndex = 7;
            // 
            // button_sql_bot_update
            // 
            this.button_sql_bot_update.Location = new System.Drawing.Point(647, 35);
            this.button_sql_bot_update.Name = "button_sql_bot_update";
            this.button_sql_bot_update.Size = new System.Drawing.Size(84, 23);
            this.button_sql_bot_update.TabIndex = 9;
            this.button_sql_bot_update.Text = "Update Bot";
            this.button_sql_bot_update.UseVisualStyleBackColor = true;
            this.button_sql_bot_update.Click += new System.EventHandler(this.button_sql_bot_update_Click);
            // 
            // textBox_sql_bot_id
            // 
            this.textBox_sql_bot_id.Location = new System.Drawing.Point(650, 14);
            this.textBox_sql_bot_id.Name = "textBox_sql_bot_id";
            this.textBox_sql_bot_id.ReadOnly = true;
            this.textBox_sql_bot_id.Size = new System.Drawing.Size(100, 23);
            this.textBox_sql_bot_id.TabIndex = 10;
            this.textBox_sql_bot_id.TabStop = false;
            this.textBox_sql_bot_id.Visible = false;
            // 
            // button_sql_game_delete
            // 
            this.button_sql_game_delete.Location = new System.Drawing.Point(6, 34);
            this.button_sql_game_delete.Name = "button_sql_game_delete";
            this.button_sql_game_delete.Size = new System.Drawing.Size(84, 23);
            this.button_sql_game_delete.TabIndex = 11;
            this.button_sql_game_delete.Text = "Delete Game";
            this.button_sql_game_delete.UseVisualStyleBackColor = true;
            this.button_sql_game_delete.Click += new System.EventHandler(this.button_sql_game_delete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_sql_game_delete);
            this.groupBox1.Location = new System.Drawing.Point(890, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(103, 66);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Controls";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_connect);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(97, 66);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL Database";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_sql_bot_token);
            this.groupBox3.Controls.Add(this.label_sql_bot_token);
            this.groupBox3.Controls.Add(this.textBox_sql_bot_status);
            this.groupBox3.Controls.Add(this.textBox_sql_bot_id);
            this.groupBox3.Controls.Add(this.label_sql_bot_status);
            this.groupBox3.Controls.Add(this.button_sql_bot_update);
            this.groupBox3.Controls.Add(this.label_sql_bot_displayName);
            this.groupBox3.Controls.Add(this.textBox_sql_bot_game);
            this.groupBox3.Controls.Add(this.textBox_sql_bot_displayName);
            this.groupBox3.Controls.Add(this.label_sql_bot_game);
            this.groupBox3.Location = new System.Drawing.Point(115, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(769, 66);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bot Information";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.dataGrid_sql_output);
            this.groupBox4.Location = new System.Drawing.Point(12, 84);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1117, 397);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Game Database";
            // 
            // dataGrid_sql_output
            // 
            this.dataGrid_sql_output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid_sql_output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_sql_output.Location = new System.Drawing.Point(6, 22);
            this.dataGrid_sql_output.Name = "dataGrid_sql_output";
            this.dataGrid_sql_output.ReadOnly = true;
            this.dataGrid_sql_output.RowTemplate.Height = 25;
            this.dataGrid_sql_output.Size = new System.Drawing.Size(1105, 369);
            this.dataGrid_sql_output.TabIndex = 0;
            // 
            // label_logger
            // 
            this.label_logger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_logger.AutoSize = true;
            this.label_logger.Location = new System.Drawing.Point(12, 484);
            this.label_logger.Name = "label_logger";
            this.label_logger.Size = new System.Drawing.Size(91, 15);
            this.label_logger.TabIndex = 1;
            this.label_logger.Text = "Starting Client...";
            this.label_logger.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // AdminCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 497);
            this.Controls.Add(this.label_logger);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(1157, 529);
            this.Name = "AdminCenter";
            this.Text = "Admin Center";
            this.Load += new System.EventHandler(this.AdminCenter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_sql_output)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button_connect;
        private TextBox textBox_sql_bot_token;
        private Label label_sql_bot_token;
        private Label label_sql_bot_status;
        private TextBox textBox_sql_bot_status;
        private Label label_sql_bot_game;
        private TextBox textBox_sql_bot_game;
        private Label label_sql_bot_displayName;
        private TextBox textBox_sql_bot_displayName;
        private Button button_sql_bot_update;
        private TextBox textBox_sql_bot_id;
        private Button button_sql_game_delete;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private DataGridView dataGrid_sql_output;
        private Label label_logger;
    }
}