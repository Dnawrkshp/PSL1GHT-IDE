using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;


namespace PSL1GHT_IDE
{
    public partial class BuildLogger : UserControl
    {
        public struct BuildLog
        {
            public int type;
            public string description;
            public string file;
            public int line;
            public int index;
            public DateTime time;
        };

        public List<BuildLog> buildLogs = new List<BuildLog>();
        public List<BuildLog> errorLogs = new List<BuildLog>();
        public List<BuildLog> warniLogs = new List<BuildLog>();

        public delegate void ErrorCallback(BuildLog log);
        public delegate void BuildStatusCallback(int status);

        public ErrorCallback blErrorCallback = null;
        public BuildStatusCallback blBuildStatusCallback = null;
        public BuildStatusCallback blHideStatusCallback = null;

        public Process process;
        public bool isBuilding = false;

        public BuildLogger()
        {
            InitializeComponent();

            filterBuild.CheckedChanged += new EventHandler(filterCallback);
            filterError.CheckedChanged += new EventHandler(filterCallback);
            filterWarni.CheckedChanged += new EventHandler(filterCallback);

            
        }

        #region Public Functions

        public void Clear()
        {
            logView.Items.Clear();
            buildLogs.Clear();
            errorLogs.Clear();
            warniLogs.Clear();
        }

        public BuildLog ParseBuildLog(string data)
        {
            BuildLog ret = new BuildLog();
            ret.type = -1;
            ret.time = DateTime.Now;

            if (data == null || data == "")
                return ret;

            string[] words = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= 0)
                return ret;

            if (words[0].EndsWith(":") && words[0][1] == ':' && words.Length > 2)
            {
                string path = words[0].Substring(0, words[0].IndexOf(':', 3));

                string[] subWords0 = words[0].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (subWords0.Length > 3)
                {
                    ret.line = int.Parse(subWords0[2]);
                    ret.index = int.Parse(subWords0[3]);
                }

                ret.file = path;

                if (words[1].IndexOf("warning") >= 0)
                {
                    ret.type = 2;

                    ret.description = String.Join(" ", words, 2, words.Length - 2);
                }
                else if (words[1].IndexOf("error") >= 0)
                {
                    ret.type = 1;
                    ret.description = String.Join(" ", words, 2, words.Length - 2);
                }
            }
            else if (words[0] == "$")
            {
                //ret.description = data.Replace("$ ", "");
                //ret.index = -1;
                //ret.line = -1;
                //ret.type = 0;
            }
            else
            {
                if (words.Length > 1 && words[0][0] > 0x20 && words[0][0] < 0x7E)
                {

                    ret.description = data.Replace("$ ", "");
                    ret.index = -1;
                    ret.line = -1;
                    ret.type = 0;
                }
            }

            return ret;
        }

        #endregion

        #region Build Callbacks

        public void errorCallback(object sender, DataReceivedEventArgs dre)
        {
            BuildLog bl = ParseBuildLog(dre.Data);
            Invoke((MethodInvoker)delegate
            {
                switch (bl.type)
                {
                    case 0:
                        buildLogs.Add(bl);
                        break;
                    case 1:
                        errorLogs.Add(bl);
                        break;
                    case 2:
                        warniLogs.Add(bl);
                        break;
                }

                filterCallback(null, null);
            });
        }

        public void buildCallback(object sender, DataReceivedEventArgs dre)
        {
            if (dre.Data == "")
                return;

            BuildLog bl;

            if (dre.Data == null)
            {
                if (process != null)
                    process.Close();
                process = null;
                if (blBuildStatusCallback != null)
                    blBuildStatusCallback(0);
                isBuilding = false;

                bl = new BuildLog() { type = 0, description = "done", line = -1, index = -1, file = "null", time = DateTime.Now };
                Invoke((MethodInvoker)delegate
                {
                    buildLogs.Add(bl);
                    filterCallback(null, null);
                });
                return;
            }

            bl = new BuildLog() { type = 0, description = dre.Data, line = -1, index = -1, file = "null", time = DateTime.Now };
            Invoke((MethodInvoker)delegate
            {
                buildLogs.Add(bl);
                filterCallback(null, null);
            });
        }

        void filterCallback(object sender, EventArgs e)
        {
            logView.Items.Clear();

            logView.BeginUpdate();

            int x = 0;

            List<BuildLog> total = new List<BuildLog>();

            if (filterBuild.Checked)
                total.AddRange(buildLogs);
            if (filterError.Checked)
                total.AddRange(errorLogs);
            if (filterWarni.Checked)
                total.AddRange(warniLogs);

            //sort
            total.Sort((a, b) => a.time.Ticks.CompareTo(b.time.Ticks));

            for (x = 0; x < total.Count; x++)
            {
                BuildLog b = total[x];
                string t = "", l = "", i = "", f = "";
                switch (b.type)
                {
                    case 0: t = "B"; l = ""; i = ""; f = ""; break;
                    case 1: t = "E"; l = b.line.ToString(); i = b.index.ToString(); f = b.file; break;
                    case 2: t = "W"; l = b.line.ToString(); i = b.index.ToString(); f = b.file; break;
                }

                if (f != "" && File.Exists(f))
                    f = new FileInfo(f).Name;

                ListViewItem lvi = new ListViewItem(new string[] { t, b.description, f, l, i });
                lvi.Tag = b.file;
                logView.Items.Add(lvi);
            }

            logView.EndUpdate();

            if (logView.SelectedIndices.Count == 0 && logView.Items.Count > 0) //Autoscroll
            {
                logView.Items[logView.Items.Count - 1].EnsureVisible();
            }

        }

        #endregion

        #region User Control Events

        private void btHide_Click(object sender, EventArgs e)
        {
            if (blHideStatusCallback != null)
                blHideStatusCallback(1);
        }

        private void BuildLogger_Resize(object sender, EventArgs e)
        {
            logView.Size = new Size(this.Size.Width - 6, this.Size.Height - 36);
            btHide.Left = this.Size.Width - 22;
        }

        private void logView_Resize(object sender, EventArgs e)
        {
            colDescription.Width = logView.Width - colType.Width - colFile.Width - colLine.Width - colIndex.Width - 23;
        }

        private void logView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (logView.SelectedItems.Count <= 0)
                return;

            string strType = logView.SelectedItems[0].SubItems[0].Text;
            string desc = logView.SelectedItems[0].SubItems[1].Text;
            string file = logView.SelectedItems[0].Tag as string;
            int line = int.Parse(logView.SelectedItems[0].SubItems[3].Text);
            int index = int.Parse(logView.SelectedItems[0].SubItems[4].Text);

            int type = 0;
            if (strType == "W")
                type = 2;
            else if (strType == "E")
                type = 1;

            BuildLog log = new BuildLog() { type = type, description = desc, file = file, index = index, line = line };

            if (blErrorCallback != null)
                blErrorCallback(log);
        }

        #endregion

    }
}
