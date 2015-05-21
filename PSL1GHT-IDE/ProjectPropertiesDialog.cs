using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSL1GHT_IDE
{
    public partial class ProjectPropertiesDialog : Form
    {
        public Project ret = null;

        private bool input = false;

        public ProjectPropertiesDialog()
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
            bool isValid = true;
            char c;

            //Check App ID
            if (appid_tb.Text.Length != 9)
                isValid &= false;

            for (int x = 0; x < ((appid_tb.Text.Length < 9) ? appid_tb.Text.Length : 9); x++)
            {
                c = appid_tb.Text[x].ToString().ToLower()[0];
                if (!(c <= 'z' && c >= 'a') && !(c >= '0' && c <= '9'))
                    isValid &= false;
            }

            if (!isValid)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_PROPERTY_APPID_INVALID, "Error");
                return;
            }


            //Check version
            isValid = true;

            if (ver_tb.Text.Length != 5)
                isValid &= false;

            for (int x = 0; x < ((ver_tb.Text.Length < 5) ? ver_tb.Text.Length : 5); x++)
            {
                c = ver_tb.Text[x].ToString().ToLower()[0];
                if (c == '.' && x == 2)
                { }
                else if (!(c >= '0' && c <= '9'))
                    isValid &= false;
            }

            if (!isValid)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_PROPERTY_VERSION_INVALID, "Error");
                return;
            }

            if (!isValid)
            {
                MessageBox.Show(Globals.ERROR_PROJECT_PROPERTY_VERSION_INVALID, "Error");
                return;
            }

            Close();
        }

        private void ProjectPropertiesDialog_Shown(object sender, EventArgs e)
        {
            if (ret == null)
                Close();

            icon0_tb.Text = ret.ProjectIcon0;
            pic1_tb.Text = ret.ProjectPic1;
            title_tb.Text = ret.ProjectTitle;
            appid_tb.Text = ret.ProjectAppID;
            libs_tb.Text = ret.ProjectLibs;
            incs_tb.Text = ret.ProjectIncludes;
            srcs_tb.Text = ret.ProjectSources;
            ver_tb.Text = ret.ProjectVersion;
            license_tb.Text = ret.ProjectLicense;

            input = true;
        }

        private void icon0_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Browse for ICON0.PNG";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.InitialDirectory = ret.ProjectPath;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                icon0_tb.Text = ofd.FileName;
            }
        }

        private void pic1_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Browse for PIC1.PNG";
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            ofd.InitialDirectory = ret.ProjectPath;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pic1_tb.Text = ofd.FileName;
            }
        }

        private void icon0_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectIcon0 = (sender as TextBox).Text;
        }

        private void pic1_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectPic1 = (sender as TextBox).Text;
        }

        private void title_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectTitle = (sender as TextBox).Text;
        }

        private void appid_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectAppID = (sender as TextBox).Text;
        }

        private void libs_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectLibs = (sender as TextBox).Text;
        }

        private void incs_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectIncludes = (sender as TextBox).Text;
        }

        private void srcs_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectSources = (sender as TextBox).Text;
        }

        private void ver_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectVersion = (sender as TextBox).Text;
        }

        private void license_tb_TextChanged(object sender, EventArgs e)
        {
            ret.ProjectLicense = (sender as TextBox).Text;
        }
    }
}
