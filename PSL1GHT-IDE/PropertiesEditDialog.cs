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
    public partial class PropertiesEditDialog : Form
    {
        public ProgramProperties ret = null;

        public PropertiesEditDialog()
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
            if (ProjectSDKFinder.VerifySDKPath(propSDKPath.Text))
            {
                ret.SDKPath = propSDKPath.Text;
            }
            else
            {
                MessageBox.Show(Globals.ERROR_SDK_PATH_INVALID, "Error");
                return;
            }

            ret.Save(Application.StartupPath);
            Close();
        }

        private void PropertiesEditDialog_Shown(object sender, EventArgs e)
        {
            if (ret == null)
                Close();

            propSDKPath.Text = ret.SDKPath;
            foreach (ProgramProperties.PSL1DETheme theme in ProgramProperties.Themes)
            {
                propTheme.Items.Add(theme.Name);
            }
            propTheme.SelectedIndex = Globals.Properties.ThemeSelectedIndex;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Browsing for PSL1GHT root directory...";
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                propSDKPath.Text = fb.SelectedPath;
            }
        }

        private void propSDKPath_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void propTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (propTheme.SelectedIndex < 0)
                return;

            Globals.Properties.ThemeSelectedIndex = propTheme.SelectedIndex;
        }
    }
}
