using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PSL1GHT_IDE
{
    public static class Globals
    {
        public const int REVISION = 3;
        public static string ABOUT_STRING =  "-----  PSL1DE REVISION " + REVISION.ToString() + " -----\n" +
                                            "---- Built " + new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime.ToShortDateString() + " at " + new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime.ToShortTimeString() + " ----\n" +
                                            "\n--- Authors ---\n" + 
                                            "--- Dnawrkshp :: Core Developer ---\n" +
                                            "" + //Anyone else
                                            "\n\nPSL1DE was started as a fun side project by Dnawrkshp. The intention was to create a practical, though not preferable, tool to write homebrew for the PSL1GHT SDK."
                                            ;

        public const string CKeywords = "auto|break|case|char|const|continue|default|do|double|else|enum|extern|float|for|goto|if|int|long|register|return|short|signed|sizeof|static|struct|switch|typedef|union|unsigned|void|volatile|while";
        public const string CTypes = "char|const|double|enum|extern|float|int|long|short|signed|static|struct|typedef|union|unsigned|void|volatile";

        public static string WorkingDirectory = "";

        public const string ERROR_PROJECT_NO_CURRENT = "No project open or unable to determine current project.";
        public const string ERROR_INCOMPATIBLE_FILE = "PSL1DE is unable to open this kind of file.\nWould you like Windows to try?";
        public const string ERROR_SDK_PATH_REQUIRED = "PSL1DE requires a valid PSL1GHT SDK Installation to operate.\nPlease install it before using PSL1DE.";
        public const string ERROR_SDK_PATH_INVALID = "Invalid installation! Make sure it's the root directory (named PSDK3v2 typically)\nIf this is a valid installation please contact Dnawrkshp with info about your PSL1GHT SDK setup. Sorry.";
        public const string ERROR_PROJECT_PROPERTY_APPID_INVALID = "Invalid App ID! It must be nine characters (only letters and numbers)!";
        public const string ERROR_PROJECT_PROPERTY_VERSION_INVALID = "Invalid Version! It must be 5 characters (numbers only) in the format XX.XX!";

        public const string WARNING_FILE_EXISTS_YESNOCANCEL = "File already exists!\nWould you like to replace it?";

        public static string[] HIDDEN_FILES = { "thumbs.db" };

        public static ProgramProperties Properties = ProgramProperties.Load(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Properties.psini"));

        public static bool IsValidFileName(string name)
        {
            //Check if valid characters
            for (int c = 0; c < name.Length; c++)
            {
                char ch = name[c].ToString().ToLower()[0];
                if (ch < 'a' || ch > 'z')
                {
                    if (ch < '0' || ch > '9')
                    {
                        if (ch != '_' && ch != '-')
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

    }
}
