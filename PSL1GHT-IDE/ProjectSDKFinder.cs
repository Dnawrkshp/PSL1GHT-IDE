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
    public partial class ProjectSDKFinder : Form
    {
        public string ret = null;

        public ProjectSDKFinder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ret = null;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (VerifySDKPath(ret))
            {

            }
            else
            {
                MessageBox.Show(Globals.ERROR_SDK_PATH_INVALID, "Error");
            }

            Close();
        }

        private void ProjectSDKFinder_Shown(object sender, EventArgs e)
        {
            string[] rootDirs = new string[] { "psdk3v2", "psl1ght", "psl1ght-master" };
            DriveInfo[] drives = DriveInfo.GetDrives();

            for (int dr = 0; dr < drives.Length; dr++)
            {
                try
                {
                    string[] subDirs = Directory.GetDirectories(drives[dr].RootDirectory.FullName);
                    for (int dir = 0; dir < subDirs.Length; dir++)
                    {
                        if (rootDirs.Contains(new DirectoryInfo(subDirs[dir]).Name.ToLower()))
                        {
                            if (VerifySDKPath(subDirs[dir]))
                            {
                                ret = subDirs[dir];
                                button2.Visible = true;
                                button3.Visible = true;
                                textBox1.Visible = true;
                                progressBar1.Visible = false;
                                textBox1.Text = ret;
                                label1.Text = "Found PSL1GHT Directory!";
                                return;
                            }
                        }

                        Application.DoEvents();
                    }
                }
                catch
                {

                }

                Application.DoEvents();
            }

            MessageBox.Show("Unable to find the PSL1GHT SDK Installation.\nPlease the root directory (Ex: C:/PSDK3v2/) manually.", "Error");
            label1.Text = "Please supply the root directory of your PSL1GHT Installation.";
            button3.Visible = true;
            textBox1.Visible = true;
            button2.Visible = true;
            progressBar1.Visible = false;
        }

        public static bool VerifySDKPath(string path)
        {
            if (path == null || path == "")
                return false;

            string p = Path.Combine(path, "MinGW");
            if (Directory.Exists(p))
            {
                p = Path.Combine(p, "msys");
                if (Directory.Exists(p))
                {
                    p = Path.Combine(p, "1.0");
                    if (Directory.Exists(p))
                    {
                        if (File.Exists(Path.Combine(p, "msys.bat")))
                        {
                            p = Path.Combine(p, "bin");
                            if (Directory.Exists(p))
                                return true;
                        }
                    }
                }
            }

            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Browsing for PSL1GHT root directory...";
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = fb.SelectedPath;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ret = textBox1.Text;
        }

    }
}
