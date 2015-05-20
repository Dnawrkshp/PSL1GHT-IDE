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
    public partial class InputDialog : Form
    {
        public string thisDefault = "";
        public string thisTitle = "";
        public string thisReturn = "";

        public InputDialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thisReturn = null;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thisReturn = textBox1.Text;
            Close();
        }

        private void InputDialog_Shown(object sender, EventArgs e)
        {
            this.Text = thisTitle;
            this.textBox1.Text = thisDefault;
        }
    }
}
