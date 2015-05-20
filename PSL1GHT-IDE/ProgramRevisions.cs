using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSL1GHT_IDE
{
    public partial class ProgramRevisions : Form
    {
        private List<ProjectMain.RevisionEntry> revisions = null;

        public ProgramRevisions(List<ProjectMain.RevisionEntry> revs)
        {
            InitializeComponent();

            revisions = revs;
            if (revs != null)
            {
                for (int x = revs.Count - 1; x >= 0; x--)
                {
                    if (revs[x].revision <= Globals.REVISION) //Hehe, no secret stuff for you
                    {
                        listBox1.Items.Add("Revision " + revs[x].revision.ToString());

                        if (revs[x].revision == Globals.REVISION)
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (revisions != null && listBox1.SelectedItem != null)
            {
                int v = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[1]);
                ProjectMain.RevisionEntry rev = revisions.Where(z => z.revision == v).FirstOrDefault();

                if (rev.revision == v)
                    textBox1.Text = String.Join("\r\n", rev.revisions);
            }
        }
    }
}
