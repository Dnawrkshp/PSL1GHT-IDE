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
    public partial class AddNewDialog : Form
    {
        public string thisTitle = "";
        public string thisResult = "";
        public string thisDefault = "";
        public string thisExt = "";
        public List<string> thisReserved = new List<string>();

        public AddNewDialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string txt = textBox1.Text.Replace(thisExt, "");

            //Check if in use
            for (int x = 0; x < thisReserved.Count; x++)
            {
                if ((txt + thisExt).ToLower() == thisReserved[x].ToLower())
                {
                    MessageBox.Show("File already exists!", "Error");
                    return;
                }
            }

            //Check if valid characters
            if (!Globals.IsValidFileName(txt))
            {
                MessageBox.Show("Invalid file name!", "Error");
                return;
            }

            //Add extension
            txt += thisExt;

            thisResult = txt;

            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thisResult = null;
            Close();
        }

        private void AddNewDialog_Shown(object sender, EventArgs e)
        {
            this.Text = thisTitle;
            this.textBox1.Text = thisDefault;
        }
    }
}
