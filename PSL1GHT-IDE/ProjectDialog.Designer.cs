namespace PSL1GHT_IDE
{
    partial class ProjectDialog
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
            this.TBName = new System.Windows.Forms.TextBox();
            this.buttCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttNext = new System.Windows.Forms.Button();
            this.buttPrevious = new System.Windows.Forms.Button();
            this.buttBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TBName
            // 
            this.TBName.Location = new System.Drawing.Point(12, 200);
            this.TBName.Name = "TBName";
            this.TBName.Size = new System.Drawing.Size(363, 20);
            this.TBName.TabIndex = 0;
            this.TBName.TextChanged += new System.EventHandler(this.TBName_TextChanged);
            // 
            // buttCancel
            // 
            this.buttCancel.Location = new System.Drawing.Point(300, 226);
            this.buttCancel.Name = "buttCancel";
            this.buttCancel.Size = new System.Drawing.Size(75, 23);
            this.buttCancel.TabIndex = 1;
            this.buttCancel.Text = "Cancel";
            this.buttCancel.UseVisualStyleBackColor = true;
            this.buttCancel.Click += new System.EventHandler(this.buttCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please enter a name...";
            // 
            // buttNext
            // 
            this.buttNext.Location = new System.Drawing.Point(219, 226);
            this.buttNext.Name = "buttNext";
            this.buttNext.Size = new System.Drawing.Size(75, 23);
            this.buttNext.TabIndex = 3;
            this.buttNext.Text = "Next";
            this.buttNext.UseVisualStyleBackColor = true;
            this.buttNext.Click += new System.EventHandler(this.buttNext_Click);
            // 
            // buttPrevious
            // 
            this.buttPrevious.Location = new System.Drawing.Point(138, 226);
            this.buttPrevious.Name = "buttPrevious";
            this.buttPrevious.Size = new System.Drawing.Size(75, 23);
            this.buttPrevious.TabIndex = 4;
            this.buttPrevious.Text = "Previous";
            this.buttPrevious.UseVisualStyleBackColor = true;
            this.buttPrevious.Visible = false;
            this.buttPrevious.Click += new System.EventHandler(this.buttPrevious_Click);
            // 
            // buttBrowse
            // 
            this.buttBrowse.Location = new System.Drawing.Point(300, 198);
            this.buttBrowse.Name = "buttBrowse";
            this.buttBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttBrowse.TabIndex = 5;
            this.buttBrowse.Text = "Browse";
            this.buttBrowse.UseVisualStyleBackColor = true;
            this.buttBrowse.Visible = false;
            this.buttBrowse.Click += new System.EventHandler(this.buttBrowse_Click);
            // 
            // ProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 261);
            this.Controls.Add(this.buttBrowse);
            this.Controls.Add(this.buttPrevious);
            this.Controls.Add(this.buttNext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttCancel);
            this.Controls.Add(this.TBName);
            this.Name = "ProjectDialog";
            this.Text = "Create New Project";
            this.Load += new System.EventHandler(this.ProjectDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBName;
        private System.Windows.Forms.Button buttCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttNext;
        private System.Windows.Forms.Button buttPrevious;
        private System.Windows.Forms.Button buttBrowse;
    }
}