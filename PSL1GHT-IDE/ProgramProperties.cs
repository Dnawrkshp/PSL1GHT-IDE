using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

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
            sdkf.ShowDialog();
            return sdkf.ret;
        }

        public void ShowEditDialog()
        {
            PropertiesEditDialog p = new PropertiesEditDialog();
            p.ret = this;
            p.ShowDialog();

            if (p.ret != null)
            {
                this.SDKPath = p.ret.SDKPath;
            }
        }

    }
}
