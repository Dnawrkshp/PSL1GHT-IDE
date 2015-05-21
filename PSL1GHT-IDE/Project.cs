using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace PSL1GHT_IDE
{
    public class Project
    {
        public struct FileHandler
        {
            public string name;
            public CSyntaxHighlighter tb;
        }

        [XmlIgnore]
        public List<FileHandler> fileHandles = new List<FileHandler>();
        

        #region Project Properties

        private string _path = null;
        public string ProjectPath
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _name = null;
        public string ProjectName
        {
            get { return _name; }
            set { _name = value; }
        }

        private List<string> _classes = null;
        public List<string> ProjectClasses
        {
            get { return _classes; }
            set { _classes = value; }
        }

        private List<string> _headers = null;
        public List<string> ProjectHeaders
        {
            get { return _headers; }
            set { _headers = value; }
        }


        //Makefile
        private string _icon0 = "";
        public string ProjectIcon0
        {
            get { return _icon0; }
            set { _icon0 = value; }
        }

        private string _icon1 = "";
        public string ProjectIcon1
        {
            get { return _icon1; }
            set { _icon1 = value; }
        }

        private string _pic1 = "";
        public string ProjectPic1
        {
            get { return _pic1; }
            set { _pic1 = value; }
        }

        private string _title = "";
        public string ProjectTitle
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _appid = "PSL1DE001";
        public string ProjectAppID
        {
            get { return _appid; }
            set { _appid = value; }
        }

        private string _libs = "-lc -lio";
        public string ProjectLibs
        {
            get { return _libs; }
            set { _libs = value; }
        }

        private string _includes = "";
        public string ProjectIncludes
        {
            get { return _includes; }
            set { _includes = value; }
        }

        private string _sources = "";
        public string ProjectSources
        {
            get { return _sources; }
            set { _sources = value; }
        }

        //SFO
        private string _version = "01.00";
        public string ProjectVersion
        {
            get { return _version; }
            set { _version = value; }
        }

        private string _license = "This application was created with the official non-official SDK called PSL1GHT, for more information visit http://www.psl1ght.com/ . This is in no way associated with Sony Computer Entertainment Inc., please do not contact them for help, they will not be able to provide it.";
        public string ProjectLicense
        {
            get { return _license; }
            set { _license = value; }
        }

        #endregion

        #region Instance Functions

        private Process p;
        public void Build(BuildLogger bl, string makeCmd)
        {
            if (bl.isBuilding)
            {
                return;
            }

            SaveAll();

            GenerateMakefile();
            GenerateSFO();

            string batch = "@echo off\r\n";
            batch += Globals.Properties.SDKPath.Split(':')[0] + ":\r\n"; //move to new drive (if new drive, safe to be safe)
            batch += "cd \"" + Path.Combine(Globals.Properties.SDKPath, "MinGW/msys/1.0/bin").Replace("\\", "/") + "\"\r\n";
            batch += "sh --login -i\r\n";
            File.WriteAllText(Path.Combine(System.Windows.Forms.Application.StartupPath, "temp.bat"), batch);

            try { p.Kill(); } catch { }
            p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                ErrorDialog = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                FileName = "CMD.exe",
                Arguments = "/c \"" + Path.Combine(System.Windows.Forms.Application.StartupPath, "temp.bat").Replace("\\", "/") + "\""
            };

            bl.Clear();
            p.OutputDataReceived += new DataReceivedEventHandler(bl.buildCallback);
            p.ErrorDataReceived += new DataReceivedEventHandler(bl.errorCallback);
            bl.process = p;
            bl.isBuilding = true;
            if (bl.blBuildStatusCallback != null)
                bl.blBuildStatusCallback(1);
            if (bl.blHideStatusCallback != null)
                bl.blHideStatusCallback(0);

            p.Start();

            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            p.StandardInput.WriteLine("cd \"" + ProjectPath + "\"");
            p.StandardInput.WriteLine(makeCmd);
            p.StandardInput.WriteLine("exit");
        }

        public void Clean(BuildLogger bl)
        {
            Build(bl, "make clean");
        }

        public void Run(BuildLogger bl)
        {
            Build(bl, "make exec");
        }

        public void Package(BuildLogger bl)
        {
            string pkg = Path.Combine(ProjectPath, ProjectName.Replace(" ", "_") + ".pkg");
            if (File.Exists(pkg))
                File.Delete(pkg);
            Build(bl, "make pkg");
        }

        public bool Close()
        {
            DialogResult dr = DialogResult.Yes;

            if (this.isChanged())
                dr = MessageBox.Show(Globals.WARNING_PROJECT_NOT_SAVED, "Warning - " + this.ProjectName, MessageBoxButtons.YesNoCancel);

            if (dr == DialogResult.Yes)
                this.SaveAll();
            else if (dr == DialogResult.Cancel)
                return false;
            
            //Close all file handles (and pages)
            for (int z = 0; z < fileHandles.Count; z++)
            {
                if (fileHandles[z].tb != null && fileHandles[z].tb.Parent != null && fileHandles[z].tb.Parent.Parent != null)
                {
                    (fileHandles[z].tb.Parent.Parent as TabControl).TabPages.Remove(fileHandles[z].tb.Parent as TabPage);
                    fileHandles[z].tb.CloseBindingFile();
                }
            }



            return true;
        }

        public bool isChanged()
        {
            for (int x = 0; x < fileHandles.Count; x++)
            {
                if (fileHandles[x].tb.IsChanged)
                {
                    return true;
                }
            }

            return false;
        }

        public void SaveAll()
        {
            Save();

            for (int x = 0; x < fileHandles.Count; x++)
            {
                if (fileHandles[x].tb.IsChanged)
                {
                    //changes isChanged to false and saves
                    fileHandles[x].tb.SaveToFile(fileHandles[x].name, Encoding.ASCII);
                    fileHandles[x].tb.Refresh();
                }
            }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Project));
            using (XmlWriter writer = XmlWriter.Create(Path.Combine(ProjectPath, "Project.psxml")))
            {
                serializer.Serialize(writer, this);
            }
        }

        public void GenerateMakefile()
        {
            string makefile = PSL1GHT_IDE.Properties.Resources.makefile;

            if (File.Exists(ProjectIcon0) && ProjectIcon0 != Path.Combine(ProjectPath, "ICON0.PNG"))
            {
                Image icon0 = Image.FromFile(ProjectIcon0);
                icon0.Save(Path.Combine(ProjectPath, "ICON0.PNG"), System.Drawing.Imaging.ImageFormat.Png);
                icon0.Save(Path.Combine(ProjectPath, "pkgfiles", "ICON0.PNG"), System.Drawing.Imaging.ImageFormat.Png);
            }
            else if (!File.Exists(ProjectIcon0)) //use default ICON0.png
                PSL1GHT_IDE.Properties.Resources.ICON0.Save(Path.Combine(ProjectPath, "ICON0.PNG"), System.Drawing.Imaging.ImageFormat.Png);

            if (File.Exists(ProjectPic1) && ProjectPic1 != Path.Combine(ProjectPath, "PIC1.PNG"))
            {
                Image pic1 = Image.FromFile(ProjectPic1);
                pic1.Save(Path.Combine(ProjectPath, "PIC1.PNG"), System.Drawing.Imaging.ImageFormat.Png);
            }

            makefile = makefile.Replace("%ICON0%", File.Exists(Path.Combine(ProjectPath, "ICON0.PNG")) ? "ICON0		:=	$(CURDIR)/ICON0.PNG" : "");
            makefile = makefile.Replace("%ICON1%", ""); //unsupported
            makefile = makefile.Replace("%PIC1%", File.Exists(Path.Combine(ProjectPath, "PIC1.PNG")) ? "PIC1		:=	$(CURDIR)/PIC1.PNG" : "");
            makefile = makefile.Replace("%TITLE%", ProjectTitle);
            makefile = makefile.Replace("%APPID%", ProjectAppID);
            makefile = makefile.Replace("%LIBS%", ProjectLibs);
            makefile = makefile.Replace("%SOURCES%", ProjectSources);
            makefile = makefile.Replace("%INCLUDES%", ProjectIncludes);

            File.WriteAllText(Path.Combine(ProjectPath, "makefile"), makefile);
        }

        public void GenerateSFO()
        {
            string sfo = PSL1GHT_IDE.Properties.Resources.sfo;

            sfo = sfo.Replace("%VER%", ProjectVersion);
            sfo = sfo.Replace("%LICENSE%", ProjectLicense);
            sfo = sfo.Replace("%TITLE%", ProjectTitle);
            sfo = sfo.Replace("%APPID%", ProjectAppID);

            File.WriteAllText(Path.Combine(ProjectPath, "sfo.xml"), sfo);
        }

        public void SetTheme(ProgramProperties.PSL1DETheme theme)
        {
            foreach (FileHandler fh in fileHandles)
            {
                string oldT = fh.tb.Text;
                int len = fh.tb.SelectionLength;
                int start = fh.tb.SelectionStart;

                fh.tb.BeginUpdate();

                fh.tb.CurrentTheme = theme;
                fh.tb.Text = "";
                fh.tb.Text = oldT;
                fh.tb.SelectionStart = start;
                fh.tb.SelectionLength = len;
                fh.tb.Refresh();

                fh.tb.EndUpdate();
            }

            SaveAll();
        }

        #endregion

        #region Static Functions

        public static Project CreateProjectDialog()
        {
            ProjectDialog pd = new ProjectDialog();
            pd.ShowDialog();

            return pd.ret;
        }

        public static Project LoadProject(string file)
        {
            Project p = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Project));

            try
            {
                using (XmlReader reader = XmlReader.Create(file))
                {
                    p = (Project)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, e.Source);
            }

            return p;
        }

        public static void SaveProject(Project p, string path)
        {
            p.Save();
        }

        #endregion

    }
}
