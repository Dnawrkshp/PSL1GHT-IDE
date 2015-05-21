namespace PSL1GHT_IDE
{
    partial class PropertiesEditDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.propSDKPath = new System.Windows.Forms.TextBox();
            this.propTheme = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(325, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(244, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Finish";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(325, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // propSDKPath
            // 
            this.propSDKPath.Location = new System.Drawing.Point(12, 12);
            this.propSDKPath.Name = "propSDKPath";
            this.propSDKPath.Size = new System.Drawing.Size(307, 20);
            this.propSDKPath.TabIndex = 6;
            this.propSDKPath.TextChanged += new System.EventHandler(this.propSDKPath_TextChanged);
            // 
            // propTheme
            // 
            this.propTheme.FormattingEnabled = true;
            this.propTheme.Location = new System.Drawing.Point(61, 41);
            this.propTheme.Name = "propTheme";
            this.propTheme.Size = new System.Drawing.Size(182, 21);
            this.propTheme.TabIndex = 8;
            this.propTheme.SelectedIndexChanged += new System.EventHandler(this.propTheme_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Theme:";
            // 
            // PropertiesEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 115);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.propTheme);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.propSDKPath);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "PropertiesEditDialog";
            this.Text = "PSL1DE Properties";
            this.Shown += new System.EventHandler(this.PropertiesEditDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox propSDKPath;
        private System.Windows.Forms.ComboBox propTheme;
        private System.Windows.Forms.Label label1;
    }
}