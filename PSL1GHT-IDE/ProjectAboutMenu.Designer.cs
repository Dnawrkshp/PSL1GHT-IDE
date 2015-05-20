namespace PSL1GHT_IDE
{
    partial class ProjectAboutMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.btDonate = new System.Windows.Forms.Button();
            this.btGithub = new System.Windows.Forms.Button();
            this.btPs3hax = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 179);
            this.label1.TabIndex = 0;
            this.label1.Text = "sdv";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btDonate
            // 
            this.btDonate.Location = new System.Drawing.Point(197, 226);
            this.btDonate.Name = "btDonate";
            this.btDonate.Size = new System.Drawing.Size(75, 23);
            this.btDonate.TabIndex = 1;
            this.btDonate.Text = "Donate";
            this.btDonate.UseVisualStyleBackColor = true;
            this.btDonate.Click += new System.EventHandler(this.btDonate_Click);
            // 
            // btGithub
            // 
            this.btGithub.Location = new System.Drawing.Point(93, 226);
            this.btGithub.Name = "btGithub";
            this.btGithub.Size = new System.Drawing.Size(98, 23);
            this.btGithub.TabIndex = 2;
            this.btGithub.Text = "Github";
            this.btGithub.UseVisualStyleBackColor = true;
            this.btGithub.Click += new System.EventHandler(this.btGithub_Click);
            // 
            // btPs3hax
            // 
            this.btPs3hax.Location = new System.Drawing.Point(12, 226);
            this.btPs3hax.Name = "btPs3hax";
            this.btPs3hax.Size = new System.Drawing.Size(75, 23);
            this.btPs3hax.TabIndex = 3;
            this.btPs3hax.Text = "PS3Hax";
            this.btPs3hax.UseVisualStyleBackColor = true;
            this.btPs3hax.Click += new System.EventHandler(this.btPs3hax_Click);
            // 
            // ProjectAboutMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btPs3hax);
            this.Controls.Add(this.btGithub);
            this.Controls.Add(this.btDonate);
            this.Controls.Add(this.label1);
            this.Name = "ProjectAboutMenu";
            this.Text = "About PSL1DE";
            this.Shown += new System.EventHandler(this.ProjectAboutMenu_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btDonate;
        private System.Windows.Forms.Button btGithub;
        private System.Windows.Forms.Button btPs3hax;
    }
}