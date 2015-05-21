namespace PSL1GHT_IDE
{
    partial class ProjectMain
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
            this.components = new System.ComponentModel.Container();
            this.projectMainView = new System.Windows.Forms.TreeView();
            this.projectMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebuildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revisionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectMainViewCMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addClassToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importClassToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importHeaderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addHeaderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshViewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editPropertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.packageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buildLogger1 = new PSL1GHT_IDE.BuildLogger();
            this.projectMainMenuStrip.SuspendLayout();
            this.projectMainViewCMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectMainView
            // 
            this.projectMainView.FullRowSelect = true;
            this.projectMainView.HideSelection = false;
            this.projectMainView.Location = new System.Drawing.Point(5, 5);
            this.projectMainView.Name = "projectMainView";
            this.projectMainView.Size = new System.Drawing.Size(194, 334);
            this.projectMainView.TabIndex = 1;
            this.projectMainView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.projectMainView_MouseDoubleClick);
            this.projectMainView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.projectMainView_MouseUp);
            // 
            // projectMainMenuStrip
            // 
            this.projectMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.projectMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.projectMainMenuStrip.Name = "projectMainMenuStrip";
            this.projectMainMenuStrip.Size = new System.Drawing.Size(695, 24);
            this.projectMainMenuStrip.TabIndex = 3;
            this.projectMainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem,
            this.saveAllToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadProjectToolStripMenuItem.Text = "Load Project";
            this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.loadProjectToolStripMenuItem_Click);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveAllToolStripMenuItem.Text = "Save Project";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.findToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem1,
            this.gotoToolStripMenuItem,
            this.replaceToolStripMenuItem});
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // findToolStripMenuItem1
            // 
            this.findToolStripMenuItem1.Name = "findToolStripMenuItem1";
            this.findToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.findToolStripMenuItem1.Text = "Find";
            this.findToolStripMenuItem1.Click += new System.EventHandler(this.findToolStripMenuItem1_Click);
            // 
            // gotoToolStripMenuItem
            // 
            this.gotoToolStripMenuItem.Name = "gotoToolStripMenuItem";
            this.gotoToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.gotoToolStripMenuItem.Text = "Goto";
            this.gotoToolStripMenuItem.Click += new System.EventHandler(this.gotoToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.replaceToolStripMenuItem.Text = "Replace";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addClassToolStripMenuItem,
            this.addHeaderToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.rebuildToolStripMenuItem,
            this.cleanToolStripMenuItem,
            this.packageToolStripMenuItem,
            this.runToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // addClassToolStripMenuItem
            // 
            this.addClassToolStripMenuItem.Name = "addClassToolStripMenuItem";
            this.addClassToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.addClassToolStripMenuItem.Text = "Add Class";
            this.addClassToolStripMenuItem.Click += new System.EventHandler(this.addClassToolStripMenuItem_Click);
            // 
            // addHeaderToolStripMenuItem
            // 
            this.addHeaderToolStripMenuItem.Name = "addHeaderToolStripMenuItem";
            this.addHeaderToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.addHeaderToolStripMenuItem.Text = "Add Header";
            this.addHeaderToolStripMenuItem.Click += new System.EventHandler(this.addHeaderToolStripMenuItem_Click);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.buildToolStripMenuItem.Text = "Build";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // rebuildToolStripMenuItem
            // 
            this.rebuildToolStripMenuItem.Name = "rebuildToolStripMenuItem";
            this.rebuildToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.rebuildToolStripMenuItem.Text = "Rebuild";
            this.rebuildToolStripMenuItem.Click += new System.EventHandler(this.rebuildToolStripMenuItem_Click);
            // 
            // cleanToolStripMenuItem
            // 
            this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
            this.cleanToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.cleanToolStripMenuItem.Text = "Clean";
            this.cleanToolStripMenuItem.Click += new System.EventHandler(this.cleanToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.runToolStripMenuItem.Text = "Run (PS3Load)";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.revisionsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // revisionsToolStripMenuItem
            // 
            this.revisionsToolStripMenuItem.Name = "revisionsToolStripMenuItem";
            this.revisionsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.revisionsToolStripMenuItem.Text = "Revisions";
            this.revisionsToolStripMenuItem.Click += new System.EventHandler(this.revisionsToolStripMenuItem_Click);
            // 
            // projectMainViewCMenuStrip
            // 
            this.projectMainViewCMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.closeToolStripMenuItem1,
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.addClassToolStripMenuItem1,
            this.importClassToolStripMenuItem1,
            this.importHeaderToolStripMenuItem1,
            this.addHeaderToolStripMenuItem1,
            this.addFileToolStripMenuItem1,
            this.refreshViewToolStripMenuItem1,
            this.editPropertiesToolStripMenuItem1});
            this.projectMainViewCMenuStrip.Name = "projectMainViewCMenuStrip";
            this.projectMainViewCMenuStrip.Size = new System.Drawing.Size(152, 246);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.closeToolStripMenuItem1.Text = "Close";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.closeToolStripMenuItem1_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // addClassToolStripMenuItem1
            // 
            this.addClassToolStripMenuItem1.Name = "addClassToolStripMenuItem1";
            this.addClassToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.addClassToolStripMenuItem1.Text = "Add Class";
            this.addClassToolStripMenuItem1.Click += new System.EventHandler(this.addClassToolStripMenuItem1_Click);
            // 
            // importClassToolStripMenuItem1
            // 
            this.importClassToolStripMenuItem1.Name = "importClassToolStripMenuItem1";
            this.importClassToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.importClassToolStripMenuItem1.Text = "Import Class";
            this.importClassToolStripMenuItem1.Click += new System.EventHandler(this.importClassToolStripMenuItem1_Click);
            // 
            // importHeaderToolStripMenuItem1
            // 
            this.importHeaderToolStripMenuItem1.Name = "importHeaderToolStripMenuItem1";
            this.importHeaderToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.importHeaderToolStripMenuItem1.Text = "Import Header";
            this.importHeaderToolStripMenuItem1.Click += new System.EventHandler(this.importHeaderToolStripMenuItem1_Click);
            // 
            // addHeaderToolStripMenuItem1
            // 
            this.addHeaderToolStripMenuItem1.Name = "addHeaderToolStripMenuItem1";
            this.addHeaderToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.addHeaderToolStripMenuItem1.Text = "Add Header";
            this.addHeaderToolStripMenuItem1.Click += new System.EventHandler(this.addHeaderToolStripMenuItem1_Click);
            // 
            // addFileToolStripMenuItem1
            // 
            this.addFileToolStripMenuItem1.Name = "addFileToolStripMenuItem1";
            this.addFileToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.addFileToolStripMenuItem1.Text = "Add File";
            this.addFileToolStripMenuItem1.Click += new System.EventHandler(this.addFileToolStripMenuItem1_Click);
            // 
            // refreshViewToolStripMenuItem1
            // 
            this.refreshViewToolStripMenuItem1.Name = "refreshViewToolStripMenuItem1";
            this.refreshViewToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.refreshViewToolStripMenuItem1.Text = "Refresh View";
            this.refreshViewToolStripMenuItem1.Click += new System.EventHandler(this.refreshViewToolStripMenuItem1_Click);
            // 
            // editPropertiesToolStripMenuItem1
            // 
            this.editPropertiesToolStripMenuItem1.Name = "editPropertiesToolStripMenuItem1";
            this.editPropertiesToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.editPropertiesToolStripMenuItem1.Text = "Edit Properties";
            this.editPropertiesToolStripMenuItem1.Click += new System.EventHandler(this.editPropertiesToolStripMenuItem1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(5, 34);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.projectMainView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buildLogger1);
            this.splitContainer1.Size = new System.Drawing.Size(695, 480);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(205, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(473, 334);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Resize += new System.EventHandler(this.tabControl1_Resize);
            // 
            // packageToolStripMenuItem
            // 
            this.packageToolStripMenuItem.Name = "packageToolStripMenuItem";
            this.packageToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.packageToolStripMenuItem.Text = "Package";
            this.packageToolStripMenuItem.Click += new System.EventHandler(this.packageToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(465, 308);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buildLogger1
            // 
            this.buildLogger1.Location = new System.Drawing.Point(5, 5);
            this.buildLogger1.Name = "buildLogger1";
            this.buildLogger1.Size = new System.Drawing.Size(690, 133);
            this.buildLogger1.TabIndex = 2;
            // 
            // ProjectMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 514);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.projectMainMenuStrip);
            this.Name = "ProjectMain";
            this.Text = "PSL1DE by Dnawrkshp";
            this.Load += new System.EventHandler(this.ProjectMain_Load);
            this.Resize += new System.EventHandler(this.ProjectMain_Resize);
            this.projectMainMenuStrip.ResumeLayout(false);
            this.projectMainMenuStrip.PerformLayout();
            this.projectMainViewCMenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView projectMainView;
        private BuildLogger buildLogger1;
        private System.Windows.Forms.MenuStrip projectMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip projectMainViewCMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addClassToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addHeaderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editPropertiesToolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gotoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem rebuildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshViewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importClassToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importHeaderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem revisionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem packageToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
    }
}