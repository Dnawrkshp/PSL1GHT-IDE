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
    public partial class ProjectAboutMenu : Form
    {
        public ProjectAboutMenu()
        {
            InitializeComponent();
        }

        private void ProjectAboutMenu_Shown(object sender, EventArgs e)
        {
            label1.Text = Globals.ABOUT_STRING;
        }

        private void btDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=werewu45%40yahoo%2ecom&lc=US&item_name=Dnawrkshp%27s%20effort&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted");
        }

        private void btGithub_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.github.com/Dnawrkshp/");
        }

        private void btPs3hax_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ps3hax.net/member.php?u=280821");
        }
    }
}
