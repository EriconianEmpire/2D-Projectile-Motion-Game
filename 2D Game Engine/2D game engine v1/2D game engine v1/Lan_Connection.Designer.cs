namespace _2D_game_engine_v1
{
    partial class Lan_Connection
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
            this.Name_Box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Connect_To_Server = new System.Windows.Forms.Button();
            this.Available_User_List = new System.Windows.Forms.ComboBox();
            this.Join_Game = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            this.Incoming_Request_Text = new System.Windows.Forms.Label();
            this.Accept = new System.Windows.Forms.Button();
            this.Reject = new System.Windows.Forms.Button();
            this.Incoming_Player = new System.Windows.Forms.TextBox();
            this.Disconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Name_Box
            // 
            this.Name_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Name_Box.Location = new System.Drawing.Point(94, 12);
            this.Name_Box.Name = "Name_Box";
            this.Name_Box.Size = new System.Drawing.Size(100, 23);
            this.Name_Box.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // Connect_To_Server
            // 
            this.Connect_To_Server.Location = new System.Drawing.Point(12, 41);
            this.Connect_To_Server.Name = "Connect_To_Server";
            this.Connect_To_Server.Size = new System.Drawing.Size(124, 23);
            this.Connect_To_Server.TabIndex = 2;
            this.Connect_To_Server.Text = "Connect to Server";
            this.Connect_To_Server.UseVisualStyleBackColor = true;
            this.Connect_To_Server.Click += new System.EventHandler(this.Connect_To_Server_Click);
            // 
            // Available_User_List
            // 
            this.Available_User_List.FormattingEnabled = true;
            this.Available_User_List.Location = new System.Drawing.Point(12, 84);
            this.Available_User_List.Name = "Available_User_List";
            this.Available_User_List.Size = new System.Drawing.Size(121, 23);
            this.Available_User_List.TabIndex = 3;
            // 
            // Join_Game
            // 
            this.Join_Game.Location = new System.Drawing.Point(12, 123);
            this.Join_Game.Name = "Join_Game";
            this.Join_Game.Size = new System.Drawing.Size(86, 23);
            this.Join_Game.TabIndex = 4;
            this.Join_Game.Text = "Join Game";
            this.Join_Game.UseVisualStyleBackColor = true;
            this.Join_Game.Click += new System.EventHandler(this.Join_Game_Click);
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(104, 308);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(90, 23);
            this.Refresh.TabIndex = 5;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Incoming_Request_Text
            // 
            this.Incoming_Request_Text.AutoSize = true;
            this.Incoming_Request_Text.Location = new System.Drawing.Point(12, 194);
            this.Incoming_Request_Text.Name = "Incoming_Request_Text";
            this.Incoming_Request_Text.Size = new System.Drawing.Size(87, 15);
            this.Incoming_Request_Text.TabIndex = 6;
            this.Incoming_Request_Text.Text = "Player Request:";
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(12, 221);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 7;
            this.Accept.Text = "Accept";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Visible = false;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // Reject
            // 
            this.Reject.Location = new System.Drawing.Point(119, 221);
            this.Reject.Name = "Reject";
            this.Reject.Size = new System.Drawing.Size(75, 23);
            this.Reject.TabIndex = 8;
            this.Reject.Text = "Reject";
            this.Reject.UseVisualStyleBackColor = true;
            this.Reject.Visible = false;
            this.Reject.Click += new System.EventHandler(this.Reject_Click);
            // 
            // Incoming_Player
            // 
            this.Incoming_Player.Location = new System.Drawing.Point(104, 191);
            this.Incoming_Player.Name = "Incoming_Player";
            this.Incoming_Player.ReadOnly = true;
            this.Incoming_Player.Size = new System.Drawing.Size(90, 23);
            this.Incoming_Player.TabIndex = 9;
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(12, 308);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(75, 23);
            this.Disconnect.TabIndex = 10;
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Visible = false;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // Lan_Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 345);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Incoming_Player);
            this.Controls.Add(this.Reject);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.Incoming_Request_Text);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.Join_Game);
            this.Controls.Add(this.Available_User_List);
            this.Controls.Add(this.Connect_To_Server);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Name_Box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Lan_Connection";
            this.Text = "Lan_Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox Name_Box;
        private Label label1;
        private Button Connect_To_Server;
        private ComboBox Available_User_List;
        private Button Join_Game;
        private Button Refresh;
        private Label Incoming_Request_Text;
        private Button Accept;
        private Button Reject;
        private TextBox Incoming_Player;
        private Button Disconnect;
    }
}