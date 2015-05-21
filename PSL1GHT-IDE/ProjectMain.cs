using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FastColoredTextBoxNS;

using PSL1GHT_IDE;

namespace PSL1GHT_IDE
{
    public partial class ProjectMain : Form
    {
        public struct TreeNodeCollectionHistory
        {
            public string Text;
            public List<TreeNodeCollectionHistory> Nodes;
            public bool isExpanded;
        }

        public struct RevisionEntry
        {
            public int revision;
            public List<string> revisions;
        }

        public List<Project> CurrentProjects = new List<Project>();

        public Project GetCurrentProject(Control selectedControl)
        {
            if (CurrentProjects.Count > 0)
                return CurrentProjects[0];

            
            return null;
        }

        public static ProjectMain Instance = null;

        public ProjectMain()
        {
            InitializeComponent();

            buildLogger1.blBuildStatusCallback = callback_buildlog_build_status;
            buildLogger1.blErrorCallback = callback_buildlog_item_doubleclick;
            buildLogger1.blHideStatusCallback = callback_buildlog_hide_status;

            this.FormClosing += ProjectMain_Closing;

            //Little hack that lets you right click select
            projectMainView.NodeMouseClick += (sender, args) => projectMainView.SelectedNode = args.Node;
            projectMainView.ImageList = new ImageList();
            projectMainView.ImageList.Images.AddRange(
                new Image[] { 
                    Properties.Resources.tree_folder, 
                    Properties.Resources.tree_file,
                    Properties.Resources.tree_class,
                    Properties.Resources.tree_header
                });

            tabControl1.TabIndexChanged += tabControl1_SelectedIndexChanged;
            tabControl1.Visible = false;
            tabControl1.TabPages.Clear();
            tabControl1.SelectedIndex = -1;

            WindowState = FormWindowState.Maximized;

            this.Text = "PSL1DE r" + Globals.REVISION.ToString() + " by Dnawrkshp";

            Instance = this;
        }

        #region Project Main TreeView CMenu Strip Events

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (projectMainView.SelectedNode == null)
                return;

            TreeNode SelectedNode = projectMainView.SelectedNode;
            int x = 0;

            if (SelectedNode.Name.StartsWith("ROOT"))
            {
                //Close project
                for (x = 0; x < CurrentProjects.Count; x++)
                {
                    if (CurrentProjects[x].ProjectName == SelectedNode.Text)
                    {
                        if (CurrentProjects[x].Close())
                        {
                            CurrentProjects.RemoveAt(x);
                            x--;
                        }
                    }
                }

                UpdateProjectTreeView();
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (projectMainView.SelectedNode == null)
                return;

            TreeNode SelectedNode = projectMainView.SelectedNode;

            if (SelectedNode.Name == "")
            {
                string name = SelectedNode.Text;
                Project p = GetCurrentProject(sender as Control);
                if (p == null)
                {
                    MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                    return;
                }

                //Find parent folder
                SelectedNode = SelectedNode.Parent;
                string backPath = "";
                while (SelectedNode != null && SelectedNode.Name.IndexOf("ROOT") < 0)
                {
                    backPath = Path.Combine(SelectedNode.Text, backPath);
                    SelectedNode = SelectedNode.Parent;
                }

                if (SelectedNode.Name == "")
                {
                    MessageBox.Show("Unable to determine the location of the selected file.. This shouldn't happen...");
                    return;
                }
                string path = Path.Combine(p.ProjectPath, backPath, name);
                
                OpenFileTB(sender as Control, path);
            }
        }

        private void addClassToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addClassToolStripMenuItem_Click(sender, e);
        }

        private void addHeaderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addHeaderToolStripMenuItem_Click(sender, e);
        }

        private void importHeaderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = projectMainView.SelectedNode;
            if (SelectedNode == null)
                return;

            Project p = GetCurrentProject(sender as Control);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Header Files (*.h)|*.h|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo orig = new FileInfo(ofd.FileName);
                string copyto = Path.Combine(p.ProjectPath, "include", orig.Name);

                //Don't want to copy a file to the same location..
                if (new FileInfo(copyto).FullName != orig.FullName)
                    File.Copy(ofd.FileName, copyto);

                p.ProjectHeaders.Add("include/" + orig.Name);
                UpdateProjectTreeView();
            }
        }

        private void importClassToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = projectMainView.SelectedNode;
            if (SelectedNode == null)
                return;

            Project p = GetCurrentProject(sender as Control);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Class Files (*.c)|*.c|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo orig = new FileInfo(ofd.FileName);
                string copyto = Path.Combine(p.ProjectPath, "src", orig.Name);
                
                //Don't want to copy a file to the same location..
                if (new FileInfo(copyto).FullName != orig.FullName)
                    File.Copy(ofd.FileName, copyto);

                p.ProjectClasses.Add("src/" + orig.Name);
                UpdateProjectTreeView();
            }
        }

        private void addFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = projectMainView.SelectedNode;
            if (SelectedNode == null)
                return;

            Project p = GetCurrentProject(sender as Control);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            //Find root
            string name = SelectedNode.Text;
            SelectedNode = SelectedNode.Parent;
            string backPath = "";
            while (SelectedNode != null && SelectedNode.Name.IndexOf("ROOT") < 0)
            {
                backPath = Path.Combine(SelectedNode.Text, backPath);
                SelectedNode = SelectedNode.Parent;
            }

            if (SelectedNode.Name == "")
            {
                MessageBox.Show("Unable to determine the location of the selected file.. This shouldn't happen...");
                return;
            }

            string path = Path.Combine(p.ProjectPath, backPath, name);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string copyto = Path.Combine(path, new FileInfo(ofd.FileName).Name);
                File.Copy(ofd.FileName, copyto);

                UpdateProjectTreeView();
            }
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (projectMainView.SelectedNode == null)
                return;

            Project p = GetCurrentProject(sender as Control);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            TreeNode SelectedNode = projectMainView.SelectedNode;

            if (SelectedNode.Name == "")
            {
                int x = 0;
                string name = SelectedNode.Text;
                string ext = new FileInfo(name).Extension;

                InputDialog id = new InputDialog();
                id.thisDefault = name;
                id.thisTitle = "Rename File";

                id.ShowDialog();
                if (id.thisReturn == null || id.thisReturn == "")
                    return;

                if (id.thisReturn.IndexOf(".") >= 0)
                    id.thisReturn = id.thisReturn.Substring(0, id.thisReturn.IndexOf("."));
                //Check if valid characters
                if (!Globals.IsValidFileName(id.thisReturn))
                {
                    MessageBox.Show("Invalid file name!", "Error");
                    return;
                }

                //Find root
                SelectedNode = SelectedNode.Parent;
                string backPath = "";
                while (SelectedNode != null && SelectedNode.Name.IndexOf("ROOT") < 0)
                {
                    backPath = Path.Combine(SelectedNode.Text, backPath);
                    SelectedNode = SelectedNode.Parent;
                }

                if (SelectedNode.Name == "")
                {
                    MessageBox.Show("Unable to determine the location of the selected file.. This shouldn't happen...");
                    return;
                }

                string path = Path.Combine(p.ProjectPath, backPath, name);
                string store = Path.Combine(p.ProjectPath, backPath, id.thisReturn + ext);
                if (File.Exists(store))
                {
                    DialogResult dr = MessageBox.Show(Globals.WARNING_FILE_EXISTS_YESNOCANCEL, "Error", MessageBoxButtons.YesNo);

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        File.Delete(store);
                        File.Move(path, store);
                    }
                    else if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else
                    File.Move(path, store);

                //Check classes first
                for (x = 0; x < p.ProjectClasses.Count; x++)
                {
                    string cmpPath = Path.Combine(p.ProjectPath, p.ProjectClasses[x]).Replace("/", "\\");
                    if (cmpPath == path)
                    {
                        //Rename class
                        p.ProjectClasses[x] = "src/" + id.thisReturn + ext;
                        break;
                    }
                }

                //Check headers next
                for (x = 0; x < p.ProjectHeaders.Count; x++)
                {
                    string cmpPath = Path.Combine(p.ProjectPath, p.ProjectHeaders[x]).Replace("/", "\\");
                    if (cmpPath == path)
                    {
                        //Rename header
                        p.ProjectHeaders[x] = "include/" + id.thisReturn + ext;
                        break;
                    }
                }

                //Finally update any open instances of the file
                for (x = 0; x < p.fileHandles.Count; x++)
                {
                    if (p.fileHandles[x].name.ToLower() == path.ToLower())
                    {
                        Project.FileHandler fh = p.fileHandles[x];
                        fh.name = store;
                        fh.tb.Name = store;
                        p.fileHandles[x] = fh;

                        for (int t = 0; t < tabControl1.TabPages.Count; t++)
                        {
                            if (tabControl1.TabPages[t].Name.ToLower() == path.ToLower())
                            {
                                tabControl1.TabPages[t].Name = store;
                                tabControl1.TabPages[t].Text = id.thisReturn + ext;
                                break;
                            }
                        }

                        break;
                    }
                }

                saveAllToolStripMenuItem_Click(sender, e);
                UpdateProjectTreeView();
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (projectMainView.SelectedNode == null)
                return;

            Project p = GetCurrentProject(sender as Control);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            TreeNode SelectedNode = projectMainView.SelectedNode;

            if (SelectedNode.Name == "")
            {
                string name = SelectedNode.Text;

                //Find root
                SelectedNode = SelectedNode.Parent;
                string backPath = "";
                while (SelectedNode != null && SelectedNode.Name.IndexOf("ROOT") < 0)
                {
                    backPath = Path.Combine(SelectedNode.Text, backPath);
                    SelectedNode = SelectedNode.Parent;
                }

                if (SelectedNode.Name == "")
                {
                    MessageBox.Show("Unable to determine the location of the selected file.. This shouldn't happen...");
                    return;
                }

                string path = Path.Combine(p.ProjectPath, backPath, name);

                //Delete file
                if (File.Exists(path))
                    File.Delete(path);

                //Remove file handler (if one exists)
                int x = 0;
                for (x = 0; x < p.ProjectClasses.Count; x++)
                {
                    string cmpPath = Path.Combine(p.ProjectPath, p.ProjectClasses[x]).Replace("/", "\\");
                    if (cmpPath == path)
                    {
                        //delete class
                        p.ProjectClasses.RemoveAt(x);
                        break;
                    }
                }

                //Check headers next
                for (x = 0; x < p.ProjectHeaders.Count; x++)
                {
                    string cmpPath = Path.Combine(p.ProjectPath, p.ProjectHeaders[x]).Replace("/", "\\");
                    if (cmpPath == path)
                    {
                        //delete header
                        p.ProjectHeaders.RemoveAt(x);
                        break;
                    }
                }

                //Finally remove any open instances of the file
                for (int t = 0; t < tabControl1.TabPages.Count; t++)
                {
                    if (tabControl1.TabPages[t].Name.ToLower() == path.ToLower())
                    {
                        tabControl1.TabPages.RemoveAt(t);
                        break;
                    }
                }

                saveAllToolStripMenuItem_Click(null, null);
                UpdateProjectTreeView();
            }
            else if (SelectedNode.Name == "DIRECTORY")
            {
                string name = SelectedNode.Text;

                //Find root
                SelectedNode = SelectedNode.Parent;
                string backPath = "";
                while (SelectedNode != null && SelectedNode.Name.IndexOf("ROOT") < 0)
                {
                    backPath = Path.Combine(SelectedNode.Text, backPath);
                    SelectedNode = SelectedNode.Parent;
                }

                if (SelectedNode.Name == "")
                {
                    MessageBox.Show("Unable to determine the location of the selected file.. This shouldn't happen...");
                    return;
                }

                string path = Path.Combine(p.ProjectPath, backPath, name);

                //Delete directory
                if (Directory.Exists(path))
                    Directory.Delete(path, true);

                //Remove file handler (if one exists)
                int x = 0;
                for (x = 0; x < p.ProjectClasses.Count; x++)
                {
                    string cmpPath = Path.Combine(p.ProjectPath, p.ProjectClasses[x]).Replace("/", "\\");
                    if (cmpPath.IndexOf(path) == 0)
                    {
                        //delete class
                        p.ProjectClasses.RemoveAt(x);
                    }
                }

                //Check headers next
                for (x = 0; x < p.ProjectHeaders.Count; x++)
                {
                    string cmpPath = Path.Combine(p.ProjectPath, p.ProjectHeaders[x]).Replace("/", "\\");
                    if (cmpPath.IndexOf(path) == 0)
                    {
                        //delete header
                        p.ProjectHeaders.RemoveAt(x);
                    }
                }

                //Finally remove any open instances of the file
                for (int t = 0; t < tabControl1.TabPages.Count; t++)
                {
                    if (tabControl1.TabPages[t].Name.ToLower().IndexOf(path.ToLower()) == 0)
                    {
                        tabControl1.TabPages.RemoveAt(t);
                    }
                }

                saveAllToolStripMenuItem_Click(null, null);
                UpdateProjectTreeView();
            }

        }

        private void refreshViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateProjectTreeView();
        }

        private void editPropertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Project p = GetCurrentProject(sender as Control);

            if (p == null)
                return;

            ProjectPropertiesDialog pp = new ProjectPropertiesDialog();
            pp.ret = p;
            pp.ShowDialog();

            if (pp.ret != null)
            {
                for (int x = 0; x < CurrentProjects.Count; x++)
                {
                    if (CurrentProjects[x].ProjectPath == pp.ret.ProjectPath)
                    {
                        CurrentProjects[x] = pp.ret;
                        CurrentProjects[x].SaveAll();
                        break;
                    }
                }
            }
        }

        #endregion

        #region Project Main Menu Strip Events

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.Properties != null)
                Globals.Properties.ShowEditDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ProjectAboutMenu().Show();
        }

        private void revisionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<RevisionEntry> revisions = new List<RevisionEntry>();
            

            string[] revs = Properties.Resources.revisions.Split(new string[] { "###" }, StringSplitOptions.RemoveEmptyEntries);

            for (int x = 0; x < revs.Length; x++)
            {
                RevisionEntry re = new RevisionEntry();
                re.revisions = new List<string>();

                string[] lines = revs[x].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                        re.revision = int.Parse(lines[i].Split(' ')[0]);
                    else
                    {
                        re.revisions.Add(lines[i]);
                    }
                }

                if (re.revision > 0)
                    revisions.Add(re);
            }

            new ProgramRevisions(revisions).Show();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0)
                return;

            CSyntaxHighlighter tb = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0] as CSyntaxHighlighter;
            tb.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0)
                return;

            CSyntaxHighlighter tb = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0] as CSyntaxHighlighter;
            tb.Redo();
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0)
                return;

            CSyntaxHighlighter tb = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0] as CSyntaxHighlighter;
            tb.ShowFindDialog();
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0)
                return;

            CSyntaxHighlighter tb = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0] as CSyntaxHighlighter;
            tb.ShowGoToDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex < 0)
                return;

            CSyntaxHighlighter tb = tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0] as CSyntaxHighlighter;
            tb.ShowReplaceDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project newP = Project.CreateProjectDialog();
            if (newP != null)
            {
                if ((CurrentProjects.Count + 1) > Globals.Max_Projects_Open)
                {
                    CurrentProjects[CurrentProjects.Count - 1].Close();
                    CurrentProjects.RemoveAt(CurrentProjects.Count - 1);
                }

                if ((CurrentProjects.Count + 1) > Globals.Max_Projects_Open)
                    return;

                newP.ProjectClasses = new List<string>();
                newP.ProjectHeaders = new List<string>();

                newP.ProjectClasses.Add("src/main.c");

                if (!Directory.Exists(newP.ProjectPath))
                    Directory.CreateDirectory(newP.ProjectPath);

                if (!Directory.Exists(Path.Combine(newP.ProjectPath, "src")))
                    Directory.CreateDirectory(Path.Combine(newP.ProjectPath, "src"));
                if (!Directory.Exists(Path.Combine(newP.ProjectPath, "include")))
                    Directory.CreateDirectory(Path.Combine(newP.ProjectPath, "include"));
                if (!Directory.Exists(Path.Combine(newP.ProjectPath, "pkgfiles")))
                    Directory.CreateDirectory(Path.Combine(newP.ProjectPath, "pkgfiles"));
                if (!Directory.Exists(Path.Combine(newP.ProjectPath, "data")))
                    Directory.CreateDirectory(Path.Combine(newP.ProjectPath, "data"));

                if (File.Exists(Path.Combine(newP.ProjectPath, "src/main.c")))
                    File.Delete(Path.Combine(newP.ProjectPath, "src/main.c"));

                File.WriteAllText(Path.Combine(newP.ProjectPath, "src/main.c"), PSL1GHT_IDE.Properties.Resources.main);

                newP.SaveAll();

                CurrentProjects.Add(newP);
                UpdateProjectTreeView();
            }
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "PSL1DE Project Files (*.psxml)|*.psxml|All Files (*.*)|*.*";
            
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((CurrentProjects.Count + 1) > Globals.Max_Projects_Open)
                {
                    CurrentProjects[CurrentProjects.Count - 1].Close();
                    CurrentProjects.RemoveAt(CurrentProjects.Count - 1);
                }

                if ((CurrentProjects.Count + 1) > Globals.Max_Projects_Open)
                    return;

                Project loadP = Project.LoadProject(ofd.FileName);
                if (loadP != null)
                    CurrentProjects.Add(loadP);

                UpdateProjectTreeView();

                TreeNode root = projectMainView.Nodes[projectMainView.Nodes.Count - 1];
                root.Expand();
                if (root.Nodes.Count > 0)
                    root.Nodes[0].Expand();
                if (root.Nodes.Count > 1)
                    root.Nodes[1].Expand();
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProjects.Count <= 0)
                return;

            GetCurrentProject(sender as Control).SaveAll();
        }


        //Project Menu
        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProjects.Count <= 0)
                return;

            GetCurrentProject(sender as Control).Build(buildLogger1, "make");
        }

        private void rebuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProjects.Count <= 0)
                return;

            GetCurrentProject(sender as Control).Build(buildLogger1, "make clean\nmake");
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentProject(sender as Control).Clean(buildLogger1);
        }

        private void packageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentProject(sender as Control).Package(buildLogger1);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCurrentProject(sender as Control).Run(buildLogger1);
        }

        private void addClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project p = GetCurrentProject(sender as Control);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            int cIndex = 0;
            AddNewDialog and = new AddNewDialog();



            and.thisDefault = "class";
            while (p.ProjectClasses.Contains("src/" + and.thisDefault + cIndex.ToString() + ".c"))
                cIndex++;
            and.thisDefault += cIndex.ToString();

            and.thisExt = ".c";
            and.thisTitle = "Create New Class";
            for (cIndex = 0; cIndex < p.ProjectClasses.Count; cIndex++)
                and.thisReserved.Add(p.ProjectClasses[cIndex].Replace("src/", ""));

            and.ShowDialog();

            if (and.thisResult != null && and.thisResult != "")
            {
                string tPath = Path.Combine(Path.Combine(p.ProjectPath, "src"), and.thisResult);
                if (File.Exists(tPath))
                {
                    DialogResult dr = MessageBox.Show(Globals.WARNING_FILE_EXISTS_YESNOCANCEL, "Warning", MessageBoxButtons.YesNoCancel);

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        File.Delete(tPath);
                        File.WriteAllText(tPath, "");
                    }
                    else if (dr == System.Windows.Forms.DialogResult.No)
                    {

                    }
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                p.ProjectClasses.Add("src/" + and.thisResult);

                UpdateProjectTreeView();
            }
        }

        private void addHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project p = GetCurrentProject(sender as Control);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            int cIndex = 0;
            AddNewDialog and = new AddNewDialog();



            and.thisDefault = "header";
            while (p.ProjectClasses.Contains("include/" + and.thisDefault + cIndex.ToString() + ".c"))
                cIndex++;
            and.thisDefault += cIndex.ToString();

            and.thisExt = ".c";
            and.thisTitle = "Create New Header";
            for (cIndex = 0; cIndex < p.ProjectClasses.Count; cIndex++)
                and.thisReserved.Add(p.ProjectClasses[cIndex].Replace("include/", ""));

            and.ShowDialog();

            if (and.thisResult != null && and.thisResult != "")
            {
                string tPath = Path.Combine(Path.Combine(p.ProjectPath, "include"), and.thisResult);
                if (File.Exists(tPath))
                {
                    DialogResult dr = MessageBox.Show(Globals.WARNING_FILE_EXISTS_YESNOCANCEL, "Warning", MessageBoxButtons.YesNoCancel);

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        File.Delete(tPath);
                        File.WriteAllText(tPath, "");
                    }
                    else if (dr == System.Windows.Forms.DialogResult.No)
                    {

                    }
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                p.ProjectHeaders.Add("include/" + and.thisResult);

                UpdateProjectTreeView();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project p = GetCurrentProject(sender as Control);

            if (p == null)
                return;

            ProjectPropertiesDialog pp = new ProjectPropertiesDialog();
            pp.ret = p;
            pp.ShowDialog();

            if (pp.ret != null)
            {
                for (int x = 0; x < CurrentProjects.Count; x++)
                {
                    if (CurrentProjects[x].ProjectPath == pp.ret.ProjectPath)
                    {
                        CurrentProjects[x] = pp.ret;
                        CurrentProjects[x].SaveAll();
                        break;
                    }
                }
            }
        }

        #endregion

        #region Project Main Form Events

        private void ProjectMain_Closing(object sender, FormClosingEventArgs e)
        {
            foreach (Project p in CurrentProjects)
            {
                if (!p.Close())
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void ProjectMain_Load(object sender, EventArgs e)
        {
            //Globals.Properties = ProgramProperties.Load(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Properties.psini"));
            if (Globals.Properties == null)
            {
                string sdkPath = ProgramProperties.FindSDKPath();
                if (sdkPath == null)
                {
                    MessageBox.Show(Globals.ERROR_SDK_PATH_REQUIRED, "Error");
                    Close();
                }

                Globals.Properties = new ProgramProperties();
                Globals.Properties.SDKPath = sdkPath;
                Globals.Properties.Save(Application.StartupPath);
            }
            else
            {
                HandlePluginControls(Controls);
                this.BackColor = ProgramProperties.Themes[Globals.Properties.ThemeSelectedIndex].BackColor;
                this.ForeColor = ProgramProperties.Themes[Globals.Properties.ThemeSelectedIndex].ForeColor;
            }
        }

        private void ProjectMain_Resize(object sender, EventArgs e)
        {
            splitContainer1.Size = new Size(this.Width - 20, this.Height - 54);

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer1_Resize(null, null);
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                return;

            //Project View
            projectMainView.Height = splitContainer1.Panel1.Height - 10;

            //Build Logger
            buildLogger1.Size = new Size(splitContainer1.Panel2.Width - 10, splitContainer1.Panel2.Height - 30);

            //Text Viewer
            tabControl1.Width = splitContainer1.Panel1.Width - 198 - 10;
            tabControl1.Height = splitContainer1.Panel1.Height - 10;
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {
            foreach (TabPage tb in tabControl1.TabPages)
            {
                if (tb.Controls.Count > 0)
                    tb.Controls[0].Size = tb.Size;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count == 0)
                tabControl1.Visible = false;
            else
                tabControl1.Visible = true;
        }

        private void projectMainView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (projectMainView.SelectedNode == null)
                return;

            TreeNode SelectedNode = projectMainView.SelectedNode;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (SelectedNode.Name == "")
                {
                    string name = SelectedNode.Text;
                    Project p = GetCurrentProject(sender as Control);
                    if (p == null)
                    {
                        MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                        return;
                    }

                    //Find parent folder
                    SelectedNode = SelectedNode.Parent;
                    string backPath = "";
                    while (SelectedNode != null && SelectedNode.Name.IndexOf("ROOT") < 0)
                    {
                        backPath = Path.Combine(SelectedNode.Text, backPath);
                        SelectedNode = SelectedNode.Parent;
                    }

                    if (SelectedNode.Name == "")
                    {
                        MessageBox.Show("Unable to determine the location of the selected file.. This shouldn't happen...");
                        return;
                    }
                    string path = Path.Combine(p.ProjectPath, backPath, name);

                    OpenFileTB(sender as Control, path);
                }
            }
        }

        private void projectMainView_MouseUp(object sender, MouseEventArgs e)
        {
            if (projectMainView.SelectedNode == null)
                return;

            TreeNode SelectedNode = projectMainView.SelectedNode;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                closeToolStripMenuItem1.Visible = false;
                openToolStripMenuItem1.Visible = false;
                renameToolStripMenuItem1.Visible = false;
                deleteToolStripMenuItem1.Visible = false;
                addClassToolStripMenuItem1.Visible = false;
                importClassToolStripMenuItem1.Visible = false;
                addFileToolStripMenuItem1.Visible = false;
                addHeaderToolStripMenuItem1.Visible = false;
                importHeaderToolStripMenuItem1.Visible = false;
                editPropertiesToolStripMenuItem1.Visible = false;

                string name = SelectedNode.Name;
                if (name.IndexOf("_") > 0)
                    name = name.Split('_')[0];

                switch (name.ToUpper())
                {
                    case "DIRECTORY": //directory
                        renameToolStripMenuItem1.Visible = true;
                        deleteToolStripMenuItem1.Visible = true;
                        addFileToolStripMenuItem1.Visible = true;
                        break;
                    case "": //File
                        openToolStripMenuItem1.Visible = true;
                        renameToolStripMenuItem1.Visible = true;
                        deleteToolStripMenuItem1.Visible = true;
                        break;
                    case "ROOT":
                        closeToolStripMenuItem1.Visible = true;
                        addClassToolStripMenuItem1.Visible = true;
                        addHeaderToolStripMenuItem1.Visible = true;
                        editPropertiesToolStripMenuItem1.Visible = true;
                        break;
                    case "INCLUDE":
                        addHeaderToolStripMenuItem1.Visible = true;
                        importHeaderToolStripMenuItem1.Visible = true;
                        break;
                    case "SRC":
                        addClassToolStripMenuItem1.Visible = true;
                        importClassToolStripMenuItem1.Visible = true;
                        break;
                    case "DATA":
                        addFileToolStripMenuItem1.Visible = true;
                        break;
                    case "PKGFILES":
                        addFileToolStripMenuItem1.Visible = true;
                        break;
                }

                projectMainViewCMenuStrip.Show((sender as TreeView), e.Location);
            }
        }

        #endregion

        #region Project Main GUI Functions

        public void OpenFileTB(Control sender, string path)
        {
            if (path == null || path == "")
                return;

            if (!File.Exists(path))
                File.Create(path).Close();

            Project p = GetCurrentProject(sender);
            if (p == null)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_NO_CURRENT);
                return;
            }

            string ext = new FileInfo(path).Extension.ToLower();
            switch (ext)
            {
                case ".c": //text files
                case ".h":
                case ".txt":
                case ".xml":

                    FileInfo b = new FileInfo(path);

                    //Check if already open
                    for (int c = 0; c < p.fileHandles.Count; c++)
                    {
                        FileInfo a = new FileInfo(p.fileHandles[c].name);
                        if (a.FullName.ToLower() == b.FullName.ToLower())
                        {
                            //Find the tab that relates to it
                            for (int t = 0; t < tabControl1.TabPages.Count; t++ )
                            {
                                if (tabControl1.TabPages[t].Controls.Count > 0 && tabControl1.TabPages[t].Controls[0].Name == path)
                                {
                                    tabControl1.SelectedIndex = t;
                                    break;
                                }
                            }
                            return;
                        }
                    }

                    Project.FileHandler newFH = new Project.FileHandler();
                    newFH.name = path;


                    if (!File.Exists(path))
                        File.Create(path).Close();

                    tabControl1.TabPages.Add(new FileInfo(newFH.name).Name);
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Name = path;
                    tabControl1.Visible = true;

                    newFH.tb = new CSyntaxHighlighter();
                    newFH.tb.CurrentTheme = ProgramProperties.Themes[Globals.Properties.ThemeSelectedIndex];
                    newFH.tb.Name = newFH.name;
                    newFH.tb.Tag = "CEDITOR";
                    newFH.tb.Text = File.ReadAllText(path);
                    newFH.tb.Location = new Point(0, 0);
                    newFH.tb.Size = tabControl1.TabPages[tabControl1.TabPages.Count - 1].Size;
                    //Get rid of changed line when setting text
                    newFH.tb.SaveToFile(newFH.name, Encoding.ASCII);
                    newFH.tb.Refresh();

                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(newFH.tb);
                    p.fileHandles.Add(newFH);


                    tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                    break;
                default: //can't open natively
                    if (MessageBox.Show(Globals.ERROR_INCOMPATIBLE_FILE, path, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                    break;
            }
        }

        public List<TreeNodeCollectionHistory> Copy(TreeView treeview1)
        {
            List<TreeNodeCollectionHistory> ret = new List<TreeNodeCollectionHistory>();

            TreeNodeCollectionHistory newTn;
            foreach (TreeNode tn in treeview1.Nodes)
            {
                newTn = new TreeNodeCollectionHistory() { isExpanded = tn.IsExpanded, Text = tn.Text };
                if (newTn.Nodes == null)
                    newTn.Nodes = new List<TreeNodeCollectionHistory>();

                newTn.Nodes.AddRange(CopyChilds(tn));
                ret.Add(newTn);
            }

            return ret;
        }

        public List<TreeNodeCollectionHistory> CopyChilds(TreeNode willCopied)
        {
            List<TreeNodeCollectionHistory> ret = new List<TreeNodeCollectionHistory>();

            TreeNodeCollectionHistory newTn;
            foreach (TreeNode tn in willCopied.Nodes)
            {
                newTn = new TreeNodeCollectionHistory() { isExpanded = tn.IsExpanded, Text = tn.Text };
                ret.Add(newTn);
            }

            return ret;
        }

        List<TreeNode> ReadDirectoryTreeView(string dir, int dirImageIndex, int fileImageIndex)
        {
            List<TreeNode> ret = new List<TreeNode>();

            if (!Directory.Exists(dir))
                return ret;

            string[] dirs = Directory.GetDirectories(dir);
            string[] files = Directory.GetFiles(dir);

            int x = 0;


            for (x = 0; x < dirs.Length; x++ )
            {
                TreeNode d = new TreeNode(new DirectoryInfo(dirs[x]).Name, dirImageIndex, dirImageIndex);
                d.Name = "DIRECTORY";
                d.Nodes.AddRange(ReadDirectoryTreeView(dirs[x], dirImageIndex, fileImageIndex).ToArray());
                ret.Add(d);
            }
            for (x = 0; x < files.Length; x++)
            {
                string text = new FileInfo(files[x]).Name;
                if (!Globals.HIDDEN_FILES.Contains(text.ToLower()))
                {
                    TreeNode f = new TreeNode(text, fileImageIndex, fileImageIndex);
                    ret.Add(f);
                }
            }

            return ret;
        }

        void SetTheme(ProgramProperties.PSL1DETheme theme)
        {
            
        }

        //From NetCheatPS3
        public static void HandlePluginControls(Control.ControlCollection plgCtrl)
        {
            if (Globals.Properties == null)
                return;

            Color bc = ProgramProperties.Themes[Globals.Properties.ThemeSelectedIndex].BackColor;
            Color fc = ProgramProperties.Themes[Globals.Properties.ThemeSelectedIndex].ForeColor;

            foreach (Control ctrl in plgCtrl)
            {
                //if (ctrl is GroupBox || ctrl is Panel || ctrl is TabControl || ctrl is TabPage||
                //    ctrl is UserControl || ctrl is ListBox || ctrl is ListView)
                if (ctrl.Controls != null && ctrl.Controls.Count > 0)
                {
                    HandlePluginControls(ctrl.Controls);
                }

                if (ctrl is ListView)
                {
                    foreach (ListViewItem ctrlLVI in (ctrl as ListView).Items)
                    {
                        ctrlLVI.BackColor = bc;
                        ctrlLVI.ForeColor = fc;
                    }
                }
                else if (ctrl is SplitContainer)
                {
                    (ctrl as SplitContainer).Panel1.BackColor = bc;
                    (ctrl as SplitContainer).Panel1.ForeColor = fc;
                    (ctrl as SplitContainer).Panel2.BackColor = bc;
                    (ctrl as SplitContainer).Panel2.ForeColor = fc;
                    
                    if ((ctrl as SplitContainer).Panel1.Controls != null)
                        HandlePluginControls((ctrl as SplitContainer).Panel1.Controls);
                    if ((ctrl as SplitContainer).Panel2.Controls != null)
                        HandlePluginControls((ctrl as SplitContainer).Panel2.Controls);
                }
                else if (ctrl is TabControl)
                {
                    foreach (TabPage tp in (ctrl as TabControl).TabPages)
                    {
                        tp.BackColor = bc;
                        tp.ForeColor = fc;
                        HandlePluginControls(tp.Controls);
                    }
                }

                ctrl.BackColor = bc;
                ctrl.ForeColor = fc;
            }
        }

        void UpdateProjectTreeView()
        {

            //Basically copy the (to be) old tree view to maintain the open state
            List<TreeNodeCollectionHistory> tnc = Copy(projectMainView);

            
            projectMainView.BeginUpdate();

            projectMainView.Nodes.Clear();

            int z = 0, y = 0;

            for (int x = 0; x < CurrentProjects.Count; x++)
            {
                if (CurrentProjects[x].ProjectClasses == null)
                    CurrentProjects[x].ProjectClasses = new List<string>();
                if (CurrentProjects[x].ProjectHeaders == null)
                    CurrentProjects[x].ProjectHeaders = new List<string>();

                //Root project node
                TreeNode root = new TreeNode(CurrentProjects[x].ProjectName);
                TreeNodeCollectionHistory oldRoot = new TreeNodeCollectionHistory();
                root.Name = "ROOT_" + x.ToString();

                //Find old root
                for (y = 0; y < tnc.Count; y++)
                {
                    if (tnc[y].Text == root.Text)
                    {
                        oldRoot = tnc[y];
                        break;
                    }
                }

                if (oldRoot.Text != null && oldRoot.isExpanded)
                    root.Expand();

                //Add directories
                TreeNode include = new TreeNode("include", 0, 0);
                include.Name = "INCLUDE_" + x.ToString();
                for (z = 0; z < CurrentProjects[x].ProjectHeaders.Count; z++)
                    include.Nodes.Add(new TreeNode(CurrentProjects[x].ProjectHeaders[z].Replace("include/", ""), 3, 3));

                if (oldRoot.Text != null && oldRoot.Nodes[0].isExpanded)
                    include.Expand();

                TreeNode src = new TreeNode("src", 0, 0);
                src.Name = "SRC_" + x.ToString();
                for (z = 0; z < CurrentProjects[x].ProjectClasses.Count; z++)
                    src.Nodes.Add(new TreeNode(CurrentProjects[x].ProjectClasses[z].Replace("src/", ""), 2, 2));

                if (oldRoot.Text != null && oldRoot.Nodes[1].isExpanded)
                    src.Expand();

                TreeNode data = new TreeNode("data", 0, 0);
                data.Name = "DATA_" + x.ToString();
                data.Nodes.AddRange(ReadDirectoryTreeView(Path.Combine(CurrentProjects[x].ProjectPath, "data"), 0, 1).ToArray());

                if (oldRoot.Text != null && oldRoot.Nodes[2].isExpanded)
                    data.Expand();

                TreeNode pkgfiles = new TreeNode("pkgfiles", 0, 0);
                pkgfiles.Name = "PKGFILES_" + x.ToString();
                pkgfiles.Nodes.AddRange(ReadDirectoryTreeView(Path.Combine(CurrentProjects[x].ProjectPath, "pkgfiles"), 0, 1).ToArray());

                if (oldRoot.Text != null && oldRoot.Nodes[3].isExpanded)
                    pkgfiles.Expand();

                root.Nodes.Add(include);
                root.Nodes.Add(src);
                root.Nodes.Add(data);
                root.Nodes.Add(pkgfiles);

                projectMainView.Nodes.Add(root);
            }

            

            projectMainView.EndUpdate();
        }

        #endregion

        #region Project Main Callbacks

        void callback_buildlog_item_doubleclick(BuildLogger.BuildLog log)
        {
            OpenFileTB(null, log.file);

            if (tabControl1.SelectedIndex < 0)
                return;

            CSyntaxHighlighter tb = tabControl1.SelectedTab.Controls[0] as CSyntaxHighlighter;

            tb.Focus();
            tb.Navigate(log.line - 1);

            //Add index and account for tabs
            string t = tb.GetLine(log.line - 1).Text.Substring(0, log.index - 1);
            tb.SelectionStart += t.Length;
        }

        void callback_buildlog_build_status(int status)
        {

        }

        void callback_buildlog_hide_status(int status)
        {
            if (status == 1)
                splitContainer1.Panel2Collapsed = true;
            else if (status == 0)
                splitContainer1.Panel2Collapsed = false;

            splitContainer1.Size = new Size(splitContainer1.Width - 1, splitContainer1.Height - 1);
            splitContainer1.Size = new Size(splitContainer1.Width + 1, splitContainer1.Height + 1);
        }

        #endregion


    }
}
