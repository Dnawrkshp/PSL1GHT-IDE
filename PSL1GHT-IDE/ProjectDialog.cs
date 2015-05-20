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

namespace PSL1GHT_IDE
{
    public partial class ProjectDialog : Form
    {
        public Project ret = new Project();

        private int menu = 0;

        public ProjectDialog()
        {
            InitializeComponent();
        }

        private void ProjectDialog_Load(object sender, EventArgs e)
        {
            string curProjectName = "newPSProject";
            string curProjectPath = Path.Combine(Globals.WorkingDirectory, curProjectName);
            int index = 1;

            string[] dirs = Directory.GetDirectories(Globals.WorkingDirectory);

            while (dirs.Contains(curProjectPath + index.ToString()))
                index++;

            TBName.Text = curProjectName + index.ToString();
        }

        private void buttCancel_Click(object sender, EventArgs e)
        {
            ret = null;
            Close();
        }

        private void buttNext_Click(object sender, EventArgs e)
        {
            menu++;
            switch (menu)
            {
                case 1:
                    ret.ProjectName = TBName.Text;
                    TBName.Text = Path.Combine(Globals.WorkingDirectory, ret.ProjectName);

                    label1.Text = "Please enter a directory to work in...";

                    buttBrowse.Visible = true;
                    TBName.Width -= 80;
                    buttPrevious.Visible = true;
                    break;
                case 2:
                    //Check if directory isn't in use
                    if (Directory.Exists(TBName.Text))
                    {
                        MessageBox.Show("Project directory already exists!", "Error");
                        break;
                    }

                    ret.ProjectPath = TBName.Text;
                    TBName.Text = "";

                    buttBrowse.Visible = false;
                    TBName.Width += 80;
                    buttPrevious.Visible = true;
                    TBName.Visible = false;
                    label1.Visible = false;
                    buttNext.Text = "Finish";
                    break;
                case 3:
                    MessageBox.Show("Remember, you can change the Projects settings from the Project->Properties menu.");

                    Close();
                    break;
            }
        }

        private void buttPrevious_Click(object sender, EventArgs e)
        {
            menu--;
            switch (menu)
            {
                case 0:
                    TBName.Text = ret.ProjectName;

                    label1.Text = "Please enter a name...";

                    buttBrowse.Visible = false;
                    TBName.Width += 80;
                    buttPrevious.Visible = false;
                    break;
                case 1:
                    TBName.Text = ret.ProjectPath;

                    label1.Text = "Please enter a directory to work in...";

                    buttBrowse.Visible = true;
                    TBName.Width -= 80;
                    buttPrevious.Visible = true;
                    TBName.Visible = true;
                    label1.Visible = true;
                    buttNext.Text = "Next";
                    break;
            }

           
        }

        private void TBName_TextChanged(object sender, EventArgs e)
        {
            if (menu != 0)
                return;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                TBName.Text = TBName.Text.Replace(c.ToString(), "");
            }
        }

        private void buttBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.SelectedPath = TBName.Text;
            f.Description = "Project Directory";
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TBName.Text = f.SelectedPath;
            }
        }


    }
}
