using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using FastColoredTextBoxNS;

namespace PSL1GHT_IDE
{
    public class ProgramProperties
    {

        private string _sdkPath = "";
        public string SDKPath
        {
            get { return _sdkPath; }
            set { _sdkPath = value; }
        }

        private int _themeSelectedIndex = 0;
        public int ThemeSelectedIndex
        {
            get { return _themeSelectedIndex; }
            set
            {
                if (value >= Themes.Count || value < 0)
                    return;

                _themeSelectedIndex = value;

                if (ProjectMain.Instance != null)
                {
                    ProjectMain.Instance.BackColor = Themes[_themeSelectedIndex].BackColor;
                    ProjectMain.Instance.ForeColor = Themes[_themeSelectedIndex].ForeColor;
                    ProjectMain.HandlePluginControls(ProjectMain.Instance.Controls);

                    foreach (Project p in ProjectMain.Instance.CurrentProjects)
                    {
                        p.SetTheme(Themes[_themeSelectedIndex]);
                    }
                }
            }
        }

        public struct PSL1DETheme
        {
            //Generic
            public Color BackColor;
            public Color ForeColor;

            public string Name;

            //Syntax Highlighting
            public Color CurrentLineColor;
            public Color ChangedLineColor;
            public Color FoldingIndicatorColor;

            public TextStyle Style_Keywords;
            public TextStyle Style_Bold;
            public TextStyle Style_Attributes;
            public TextStyle Style_Numbers;
            public TextStyle Style_Comments;
            public TextStyle Style_Strings;
            public MarkerStyle MarkerStyle_SameWords;
        };

        [XmlIgnore]
        public static List<PSL1DETheme> Themes = new List<PSL1DETheme>();

        public ProgramProperties()
        {
            if (Themes.Count == 0)
            {
                PSL1DETheme theme = new PSL1DETheme();

                theme.Name = "Light";
                theme.BackColor = Color.FromArgb(245, 245, 245);
                theme.ForeColor = Color.FromArgb(10, 10, 10);
                theme.CurrentLineColor = Color.DimGray;
                theme.ChangedLineColor = Color.MediumOrchid;
                theme.FoldingIndicatorColor = Color.Blue;
                theme.Style_Keywords = new TextStyle(Brushes.DarkBlue, null, FontStyle.Regular);
                theme.Style_Bold = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
                theme.Style_Attributes = new TextStyle(Brushes.DimGray, null, FontStyle.Regular);
                theme.Style_Numbers = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
                theme.Style_Comments = new TextStyle(Brushes.Green, null, FontStyle.Italic);
                theme.Style_Strings = new TextStyle(Brushes.SaddleBrown, null, FontStyle.Italic);
                theme.MarkerStyle_SameWords = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
                Themes.Add(theme);

                theme.Name = "Dark";
                theme.BackColor = Color.FromArgb(10, 10, 10);
                theme.ForeColor = Color.FromArgb(245, 245, 245);
                theme.CurrentLineColor = Color.Gray;
                theme.ChangedLineColor = Color.Purple;
                theme.FoldingIndicatorColor = Color.Blue;
                theme.Style_Keywords = new TextStyle(Brushes.LightBlue, null, FontStyle.Regular);
                theme.Style_Bold = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
                theme.Style_Attributes = new TextStyle(Brushes.LightGray, null, FontStyle.Regular);
                theme.Style_Numbers = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
                theme.Style_Comments = new TextStyle(Brushes.LimeGreen, null, FontStyle.Italic);
                theme.Style_Strings = new TextStyle(Brushes.BurlyWood, null, FontStyle.Italic);
                theme.MarkerStyle_SameWords = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
                Themes.Add(theme);
            }
        }
        
        public static ProgramProperties Load(string file)
        {
            ProgramProperties p = null;
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramProperties));

            try
            {
                using (XmlReader reader = XmlReader.Create(file))
                {
                    p = (ProgramProperties)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                //System.Windows.Forms.MessageBox.Show(e.Message, e.Source);
                return null;
            }

            return p;
        }

        public bool Save(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramProperties));

            string res = Path.Combine(path, "Properties.psini");
            if (File.Exists(res))
                File.Delete(res);

            using (XmlWriter writer = XmlWriter.Create(res))
            {
                serializer.Serialize(writer, this);
            }

            return true;
        }

        public static string FindSDKPath()
        {
            ProjectSDKFinder sdkf = new ProjectSDKFinder();
            sdkf.BackColor = ProgramProperties.Themes[Globals.Properties.ThemeSelectedIndex].BackColor;
            sdkf.ForeColor = ProgramProperties.Themes[Globals.Properties.ThemeSelectedIndex].ForeColor;
            ProjectMain.HandlePluginControls(sdkf.Controls);
            sdkf.ShowDialog();
            return sdkf.ret;
        }

        public void ShowEditDialog()
        {
            PropertiesEditDialog p = new PropertiesEditDialog();
            p.ret = this;
            p.BackColor = Themes[ThemeSelectedIndex].BackColor;
            p.ForeColor = Themes[ThemeSelectedIndex].ForeColor;
            ProjectMain.HandlePluginControls(p.Controls);
            p.ShowDialog();

            if (p.ret != null)
            {
                this.SDKPath = p.ret.SDKPath;
            }
        }

    }
}
