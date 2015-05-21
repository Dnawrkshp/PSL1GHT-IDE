namespace PSL1GHT_IDE
{
    partial class BuildLogger
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filterBuild = new System.Windows.Forms.CheckBox();
            this.filterError = new System.Windows.Forms.CheckBox();
            this.filterWarni = new System.Windows.Forms.CheckBox();
            this.logView = new System.Windows.Forms.ListView();
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btHide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filterBuild
            // 
            this.filterBuild.Appearance = System.Windows.Forms.Appearance.Button;
            this.filterBuild.Checked = true;
            this.filterBuild.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterBuild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterBuild.Location = new System.Drawing.Point(3, 3);
            this.filterBuild.Name = "filterBuild";
            this.filterBuild.Size = new System.Drawing.Size(64, 24);
            this.filterBuild.TabIndex = 0;
            this.filterBuild.Text = "Build";
            this.filterBuild.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.filterBuild.UseVisualStyleBackColor = true;
            // 
            // filterError
            // 
            this.filterError.Appearance = System.Windows.Forms.Appearance.Button;
            this.filterError.Checked = true;
            this.filterError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterError.Location = new System.Drawing.Point(73, 3);
            this.filterError.Name = "filterError";
            this.filterError.Size = new System.Drawing.Size(64, 24);
            this.filterError.TabIndex = 1;
            this.filterError.Text = "Error";
            this.filterError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.filterError.UseVisualStyleBackColor = true;
            // 
            // filterWarni
            // 
            this.filterWarni.Appearance = System.Windows.Forms.Appearance.Button;
            this.filterWarni.Checked = true;
            this.filterWarni.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterWarni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterWarni.Location = new System.Drawing.Point(143, 3);
            this.filterWarni.Name = "filterWarni";
            this.filterWarni.Size = new System.Drawing.Size(64, 24);
            this.filterWarni.TabIndex = 2;
            this.filterWarni.Text = "Warning";
            this.filterWarni.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.filterWarni.UseVisualStyleBackColor = true;
            // 
            // logView
            // 
            this.logView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colType,
            this.colDescription,
            this.colFile,
            this.colLine,
            this.colIndex});
            this.logView.FullRowSelect = true;
            this.logView.GridLines = true;
            this.logView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.logView.HideSelection = false;
            this.logView.LabelWrap = false;
            this.logView.Location = new System.Drawing.Point(3, 33);
            this.logView.MultiSelect = false;
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(440, 339);
            this.logView.TabIndex = 3;
            this.logView.UseCompatibleStateImageBehavior = false;
            this.logView.View = System.Windows.Forms.View.Details;
            this.logView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.logView_MouseDoubleClick);
            this.logView.Resize += new System.EventHandler(this.logView_Resize);
            // 
            // colType
            // 
            this.colType.Text = "T";
            this.colType.Width = 20;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 250;
            // 
            // colFile
            // 
            this.colFile.Text = "File";
            // 
            // colLine
            // 
            this.colLine.Text = "Line";
            this.colLine.Width = 50;
            // 
            // colIndex
            // 
            this.colIndex.Text = "Index";
            // 
            // btHide
            // 
            this.btHide.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btHide.Location = new System.Drawing.Point(424, 3);
            this.btHide.Name = "btHide";
            this.btHide.Size = new System.Drawing.Size(19, 20);
            this.btHide.TabIndex = 2;
            this.btHide.Text = "X";
            this.btHide.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btHide.UseVisualStyleBackColor = true;
            this.btHide.Click += new System.EventHandler(this.btHide_Click);
            // 
            // BuildLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btHide);
            this.Controls.Add(this.logView);
            this.Controls.Add(this.filterWarni);
            this.Controls.Add(this.filterError);
            this.Controls.Add(this.filterBuild);
            this.Name = "BuildLogger";
            this.Size = new System.Drawing.Size(446, 375);
            this.Resize += new System.EventHandler(this.BuildLogger_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox filterBuild;
        private System.Windows.Forms.CheckBox filterError;
        private System.Windows.Forms.CheckBox filterWarni;
        private System.Windows.Forms.ListView logView;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colFile;
        private System.Windows.Forms.ColumnHeader colLine;
        private System.Windows.Forms.ColumnHeader colIndex;
        private System.Windows.Forms.Button btHide;

    }
}
