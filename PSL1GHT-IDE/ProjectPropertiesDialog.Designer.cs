namespace PSL1GHT_IDE
{
    partial class ProjectPropertiesDialog
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
            this.icon0_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.icon0_browse = new System.Windows.Forms.Button();
            this.pic1_browse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pic1_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.title_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.appid_tb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.libs_tb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.incs_tb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.srcs_tb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ver_tb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.license_tb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // icon0_tb
            // 
            this.icon0_tb.Location = new System.Drawing.Point(15, 25);
            this.icon0_tb.Name = "icon0_tb";
            this.icon0_tb.Size = new System.Drawing.Size(467, 20);
            this.icon0_tb.TabIndex = 0;
            this.icon0_tb.TextChanged += new System.EventHandler(this.icon0_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ICON0.PNG (Icon on XMB)";
            // 
            // icon0_browse
            // 
            this.icon0_browse.Location = new System.Drawing.Point(488, 23);
            this.icon0_browse.Name = "icon0_browse";
            this.icon0_browse.Size = new System.Drawing.Size(75, 23);
            this.icon0_browse.TabIndex = 2;
            this.icon0_browse.Text = "Browse";
            this.icon0_browse.UseVisualStyleBackColor = true;
            this.icon0_browse.Click += new System.EventHandler(this.icon0_browse_Click);
            // 
            // pic1_browse
            // 
            this.pic1_browse.Location = new System.Drawing.Point(488, 62);
            this.pic1_browse.Name = "pic1_browse";
            this.pic1_browse.Size = new System.Drawing.Size(75, 23);
            this.pic1_browse.TabIndex = 5;
            this.pic1_browse.Text = "Browse";
            this.pic1_browse.UseVisualStyleBackColor = true;
            this.pic1_browse.Click += new System.EventHandler(this.pic1_browse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "PIC1.PNG (Background when selected on XMB)";
            // 
            // pic1_tb
            // 
            this.pic1_tb.Location = new System.Drawing.Point(15, 64);
            this.pic1_tb.Name = "pic1_tb";
            this.pic1_tb.Size = new System.Drawing.Size(467, 20);
            this.pic1_tb.TabIndex = 3;
            this.pic1_tb.TextChanged += new System.EventHandler(this.pic1_tb_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Title (Title on XMB)";
            // 
            // title_tb
            // 
            this.title_tb.Location = new System.Drawing.Point(15, 103);
            this.title_tb.Name = "title_tb";
            this.title_tb.Size = new System.Drawing.Size(548, 20);
            this.title_tb.TabIndex = 6;
            this.title_tb.TextChanged += new System.EventHandler(this.title_tb_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "* App ID (9 characters, letters and numbers)";
            // 
            // appid_tb
            // 
            this.appid_tb.Location = new System.Drawing.Point(15, 142);
            this.appid_tb.Name = "appid_tb";
            this.appid_tb.Size = new System.Drawing.Size(548, 20);
            this.appid_tb.TabIndex = 8;
            this.appid_tb.TextChanged += new System.EventHandler(this.appid_tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Libraries (as appears in makefile)";
            // 
            // libs_tb
            // 
            this.libs_tb.Location = new System.Drawing.Point(15, 181);
            this.libs_tb.Name = "libs_tb";
            this.libs_tb.Size = new System.Drawing.Size(548, 20);
            this.libs_tb.TabIndex = 10;
            this.libs_tb.TextChanged += new System.EventHandler(this.libs_tb_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Includes (seperate paths with spaces)";
            // 
            // incs_tb
            // 
            this.incs_tb.Location = new System.Drawing.Point(15, 220);
            this.incs_tb.Name = "incs_tb";
            this.incs_tb.Size = new System.Drawing.Size(548, 20);
            this.incs_tb.TabIndex = 12;
            this.incs_tb.TextChanged += new System.EventHandler(this.incs_tb_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Sources (seperate paths with spaces)";
            // 
            // srcs_tb
            // 
            this.srcs_tb.Location = new System.Drawing.Point(15, 259);
            this.srcs_tb.Name = "srcs_tb";
            this.srcs_tb.Size = new System.Drawing.Size(548, 20);
            this.srcs_tb.TabIndex = 14;
            this.srcs_tb.TextChanged += new System.EventHandler(this.srcs_tb_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 282);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(261, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "* Version (4 numbers with a \'.\' in the middle. Ex: 01.00)";
            // 
            // ver_tb
            // 
            this.ver_tb.Location = new System.Drawing.Point(16, 298);
            this.ver_tb.Name = "ver_tb";
            this.ver_tb.Size = new System.Drawing.Size(548, 20);
            this.ver_tb.TabIndex = 16;
            this.ver_tb.TextChanged += new System.EventHandler(this.ver_tb_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 321);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(198, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "License (uses default PSL1GHT license)";
            // 
            // license_tb
            // 
            this.license_tb.Location = new System.Drawing.Point(15, 337);
            this.license_tb.Name = "license_tb";
            this.license_tb.Size = new System.Drawing.Size(548, 20);
            this.license_tb.TabIndex = 18;
            this.license_tb.TextChanged += new System.EventHandler(this.license_tb_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(408, 379);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Finish";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ProjectPropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 414);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.license_tb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ver_tb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.srcs_tb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.incs_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.libs_tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.appid_tb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.title_tb);
            this.Controls.Add(this.pic1_browse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pic1_tb);
            this.Controls.Add(this.icon0_browse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.icon0_tb);
            this.Name = "ProjectPropertiesDialog";
            this.Text = "Project Properties";
            this.Shown += new System.EventHandler(this.ProjectPropertiesDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox icon0_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button icon0_browse;
        private System.Windows.Forms.Button pic1_browse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pic1_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox title_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox appid_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox libs_tb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox incs_tb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox srcs_tb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ver_tb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox license_tb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}