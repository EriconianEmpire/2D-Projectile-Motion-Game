namespace _2D_game_engine_v1
{
    partial class Display
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.Projectile_Selection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.World_Selection = new System.Windows.Forms.ComboBox();
            this.Start = new System.Windows.Forms.Button();
            this.Angle = new System.Windows.Forms.TextBox();
            this.Initial_Velocity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Visualiser = new System.Windows.Forms.Panel();
            this.P2Win = new System.Windows.Forms.Label();
            this.P1Win = new System.Windows.Forms.Label();
            this.LAN = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.Visualiser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.LAN);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Projectile_Selection);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.World_Selection);
            this.panel1.Controls.Add(this.Start);
            this.panel1.Controls.Add(this.Angle);
            this.panel1.Controls.Add(this.Initial_Velocity);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1904, 64);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(553, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Projectile";
            // 
            // Projectile_Selection
            // 
            this.Projectile_Selection.FormattingEnabled = true;
            this.Projectile_Selection.Location = new System.Drawing.Point(615, 22);
            this.Projectile_Selection.Name = "Projectile_Selection";
            this.Projectile_Selection.Size = new System.Drawing.Size(121, 23);
            this.Projectile_Selection.TabIndex = 9;
            this.Projectile_Selection.SelectedIndexChanged += new System.EventHandler(this.Projectile_Selection_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "World";
            // 
            // World_Selection
            // 
            this.World_Selection.FormattingEnabled = true;
            this.World_Selection.Items.AddRange(new object[] {
            "Mercury",
            "Venus",
            "Earth",
            "Mars",
            "Weird Gravity"});
            this.World_Selection.Location = new System.Drawing.Point(409, 22);
            this.World_Selection.Name = "World_Selection";
            this.World_Selection.Size = new System.Drawing.Size(121, 23);
            this.World_Selection.TabIndex = 7;
            this.World_Selection.SelectedIndexChanged += new System.EventHandler(this.World_Selection_SelectedIndexChanged);
            // 
            // Start
            // 
            this.Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Start.Location = new System.Drawing.Point(1756, 19);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(94, 26);
            this.Start.TabIndex = 6;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click_1);
            // 
            // Angle
            // 
            this.Angle.Location = new System.Drawing.Point(261, 22);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(65, 23);
            this.Angle.TabIndex = 4;
            this.Angle.Text = "45";
            this.Angle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Initial_Velocity
            // 
            this.Initial_Velocity.Location = new System.Drawing.Point(90, 22);
            this.Initial_Velocity.Name = "Initial_Velocity";
            this.Initial_Velocity.Size = new System.Drawing.Size(65, 23);
            this.Initial_Velocity.TabIndex = 3;
            this.Initial_Velocity.Text = "100";
            this.Initial_Velocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Angle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Velocity";
            // 
            // Visualiser
            // 
            this.Visualiser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Visualiser.Controls.Add(this.P2Win);
            this.Visualiser.Controls.Add(this.P1Win);
            this.Visualiser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Visualiser.Location = new System.Drawing.Point(0, 64);
            this.Visualiser.Name = "Visualiser";
            this.Visualiser.Size = new System.Drawing.Size(1904, 897);
            this.Visualiser.TabIndex = 1;
            // 
            // P2Win
            // 
            this.P2Win.AutoSize = true;
            this.P2Win.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.P2Win.Location = new System.Drawing.Point(736, 346);
            this.P2Win.Name = "P2Win";
            this.P2Win.Size = new System.Drawing.Size(349, 72);
            this.P2Win.TabIndex = 1;
            this.P2Win.Text = "Player 2 Wins";
            this.P2Win.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.P2Win.Visible = false;
            // 
            // P1Win
            // 
            this.P1Win.AutoSize = true;
            this.P1Win.Font = new System.Drawing.Font("Segoe UI", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.P1Win.Location = new System.Drawing.Point(736, 418);
            this.P1Win.Name = "P1Win";
            this.P1Win.Size = new System.Drawing.Size(349, 72);
            this.P1Win.TabIndex = 0;
            this.P1Win.Text = "Player 1 Wins";
            this.P1Win.Visible = false;
            // 
            // LAN
            // 
            this.LAN.Location = new System.Drawing.Point(1675, 19);
            this.LAN.Name = "LAN";
            this.LAN.Size = new System.Drawing.Size(59, 25);
            this.LAN.TabIndex = 11;
            this.LAN.Text = "LAN";
            this.LAN.UseVisualStyleBackColor = true;
            this.LAN.Click += new System.EventHandler(this.LAN_Start);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1904, 961);
            this.Controls.Add(this.Visualiser);
            this.Controls.Add(this.panel1);
            this.Name = "Display";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Size_Changed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.display_visual);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Visualiser.ResumeLayout(false);
            this.Visualiser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label2;
        private Panel Visualiser;
        private Label label1;
        private TextBox Initial_Velocity;
        private Button Start;
        private TextBox Angle;
        private Label label3;
        private ComboBox World_Selection;
        private Label label4;
        private ComboBox Projectile_Selection;
        private Label P1Win;
        private Label P2Win;
        private Button LAN;
    }
}