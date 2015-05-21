using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;

namespace PSL1GHT_IDE
{
    public class CSyntaxHighlighter : FastColoredTextBox
    {
        public enum HighlightTheme
        {
            Light,
            Dark
        };

        private HighlightTheme _curTheme = HighlightTheme.Light;
        public HighlightTheme CurrentTheme
        {
            get { return _curTheme; }
            set
            {
                _curTheme = value;

                switch (_curTheme)
                {
                    case HighlightTheme.Light:
                        BlueStyle =         light_BlueStyle;
                        BoldStyle =         light_BoldStyle;
                        GrayStyle =         light_GrayStyle;
                        MagentaStyle =      light_MagentaStyle;
                        GreenStyle =        light_GreenStyle;
                        BrownStyle =        light_BrownStyle;
                        MaroonStyle =       light_MaroonStyle;
                        SameWordsStyle =    light_SameWordsStyle;

                        this.BackColor = Color.FromArgb(235, 235, 235);
                        this.ForeColor = Color.FromArgb(20, 20, 20);
                        break;
                    case HighlightTheme.Dark:
                        BlueStyle =         dark_BlueStyle;
                        BoldStyle =         dark_BoldStyle;
                        GrayStyle =         dark_GrayStyle;
                        MagentaStyle =      dark_MagentaStyle;
                        GreenStyle =        dark_GreenStyle;
                        BrownStyle =        dark_BrownStyle;
                        MaroonStyle =       dark_MaroonStyle;
                        SameWordsStyle =    dark_SameWordsStyle;

                        this.BackColor = Color.FromArgb(20, 20, 20);
                        this.ForeColor = Color.FromArgb(235, 235, 235);

                        this.LineNumberColor = ForeColor;
                        this.IndentBackColor = BackColor;
                        break;
                }
            }
        }

        public List<AutocompleteItem> AutoCompleteWords = new List<AutocompleteItem>();
        public List<AutocompleteItem> AutoCompleteIncludes = new List<AutocompleteItem>();
        public AutocompleteMenu AutoComplete = null;

        private string[] oldLines = null;


        public CSyntaxHighlighter()
        {
            this.TextChanged += new EventHandler<TextChangedEventArgs>(this_TextChanged);
            this.LineInserted += CSyntaxHighlighter_LineInserted;
            this.LineRemoved += CSyntaxHighlighter_LineRemoved;

            this.ShowFoldingLines = true;
            this.SelectionHighlightingForLineBreaksEnabled = true;
            this.AutoIndent = true;
            this.LineNumberColor = ForeColor;
            this.IndentBackColor = BackColor;
            this.CurrentLineColor = Color.DarkGray;
            this.ChangedLineColor = Color.Purple;
            this.BookmarkColor = Color.Blue;
            this.FoldingIndicatorColor = Color.Gray;

            this.HighlightFoldingIndicator = true;

            AutoComplete = new AutocompleteMenu(this);
            AutoComplete.Width = 240;

            SetAutoCompleteItems();
        }

        //Generic -- Defaulted to light
        TextStyle BlueStyle = new TextStyle(Brushes.LightBlue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.LightGray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.LimeGreen, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.BurlyWood, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.LightYellow, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        //Light
        TextStyle light_BlueStyle = new TextStyle(Brushes.DarkBlue, null, FontStyle.Regular);
        TextStyle light_BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle light_GrayStyle = new TextStyle(Brushes.DarkGray, null, FontStyle.Regular);
        TextStyle light_MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle light_GreenStyle = new TextStyle(Brushes.DarkGreen, null, FontStyle.Italic);
        TextStyle light_BrownStyle = new TextStyle(Brushes.BurlyWood, null, FontStyle.Italic);
        TextStyle light_MaroonStyle = new TextStyle(Brushes.Gold, null, FontStyle.Regular);
        MarkerStyle light_SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        //Dark
        TextStyle dark_BlueStyle = new TextStyle(Brushes.LightBlue, null, FontStyle.Regular);
        TextStyle dark_BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle dark_GrayStyle = new TextStyle(Brushes.LightGray, null, FontStyle.Regular);
        TextStyle dark_MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle dark_GreenStyle = new TextStyle(Brushes.LimeGreen, null, FontStyle.Italic);
        TextStyle dark_BrownStyle = new TextStyle(Brushes.BurlyWood, null, FontStyle.Italic);
        TextStyle dark_MaroonStyle = new TextStyle(Brushes.LightYellow, null, FontStyle.Regular);
        MarkerStyle dark_SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        private void this_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            CSyntaxHighlight(e);

            if (e.ChangedRange.Start.iLine == e.ChangedRange.End.iLine)
            {
                string oldText = oldLines[e.ChangedRange.Start.iLine];
                string lineText = this.GetLine(e.ChangedRange.Start.iLine).Text;

                if (isLineDeclaration(oldText, oldLines, e.ChangedRange.Start.iLine)) //remove old
                {
                    List<AutocompleteItem> items = parseLine(oldText);
                    if (items != null)
                    {
                        foreach (AutocompleteItem item in items)
                        {
                            //Find item
                            for (int x = 0; x < AutoCompleteWords.Count; x++)
                            {
                                if (AutoCompleteWords[x].MenuText == item.MenuText && AutoCompleteWords[x].Text == item.Text)
                                {
                                    AutoCompleteWords.RemoveAt(x);
                                    x--;
                                }
                            }
                        }
                    }
                }

                if (isLineDeclaration(lineText, this.Lines.ToArray(), e.ChangedRange.Start.iLine))
                {
                    List<AutocompleteItem> items = parseLine(lineText, e.ChangedRange.Start.iLine+1, new System.IO.FileInfo(this.Name).Name);
                    if (items != null)
                    {
                        foreach (AutocompleteItem item in items)
                        {
                            for (int a = 0; a < AutoCompleteWords.Count; a++)
                                if (AutoCompleteWords[a].MenuText == item.MenuText && AutoCompleteWords[a].Text == item.Text)
                                    goto skip;

                            AutoCompleteWords.Add(item);
                        skip: ;
                        }
                    }
                }

                SetAutoCompleteItems();
            }

            oldLines = this.Lines.ToArray();
        }

        void CSyntaxHighlighter_LineRemoved(object sender, LineRemovedEventArgs e)
        {
            if (oldLines != null && e.Index < (oldLines.Length - 1))
            {
                string lineText = oldLines[e.Index+1];
                if (isLineDeclaration(lineText, this.Lines.ToArray(), e.Index + 1))
                {
                    List<AutocompleteItem> items = parseLine(lineText, e.Index + 1);
                    if (items != null)
                    {
                        foreach (AutocompleteItem item in items)
                        {

                            //Find item
                            for (int x = 0; x < AutoCompleteWords.Count; x++)
                            {
                                if (AutoCompleteWords[x].MenuText == item.MenuText && AutoCompleteWords[x].Text == item.Text)
                                {
                                    AutoCompleteWords.RemoveAt(x);
                                    x--;
                                }
                            }
                        }
                    }

                    SetAutoCompleteItems();
                }
            }

            oldLines = this.Lines.ToArray();
        }

        void CSyntaxHighlighter_LineInserted(object sender, LineInsertedEventArgs e)
        {
            if (e.Index > 0)
            {
                string lineText = this.GetLine(e.Index - 1).Text;

                if (lineText.ToLower().StartsWith("#include ")) //process include, might take awhile
                {
                    string filep = lineText.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    if (filep.Length > 0)
                    {
                        if (filep[0] == '<') //sdk library
                        {
                            filep = filep.Replace("<", "").Replace(">", "");
                            string inc = System.IO.Path.Combine(Globals.Properties.SDKPath, @"psl1ght\ppu\include", filep);
                            if (!System.IO.File.Exists(inc))
                                inc = System.IO.Path.Combine(Globals.Properties.SDKPath, @"MinGW\include", filep);
                            if (System.IO.File.Exists(inc))
                            {
                                string[] lines = System.IO.File.ReadAllLines(inc);
                                for (int lll = 0; lll < lines.Length; lll++)
                                {
                                    if (isLineDeclaration(lines[lll], lines, lll))
                                    {
                                        List<AutocompleteItem> incs = parseLine(lines[lll], lll + 1, filep);
                                        if (incs != null)
                                        {
                                            foreach (AutocompleteItem incf in incs)
                                            {
                                                for (int a = 0; a < AutoCompleteIncludes.Count; a++)
                                                    if (AutoCompleteIncludes[a].MenuText == incf.MenuText && AutoCompleteIncludes[a].Text == incf.Text)
                                                        goto skip;

                                                AutoCompleteIncludes.Add(incf);
                                            skip: ;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            filep = filep.Replace("\"", "").Replace("\"", "");
                            List<string> includeFolders = new List<string>();
                            includeFolders.Add("include");
                            includeFolders.AddRange(ProjectMain.Instance.GetCurrentProject(this).ProjectIncludes.Split(' '));

                            for (int i = 0; i < includeFolders.Count; i++)
                            {
                                string inc = System.IO.Path.Combine(ProjectMain.Instance.GetCurrentProject(this).ProjectPath, includeFolders[i], filep);
                                if (System.IO.File.Exists(inc))
                                {
                                    string[] lines = System.IO.File.ReadAllLines(inc);
                                    for (int lll = 0; lll < lines.Length; lll++)
                                    {
                                        if (isLineDeclaration(lines[lll], lines, lll))
                                        {
                                            List<AutocompleteItem> incs = parseLine(lines[lll], lll + 1, filep);
                                            if (incs != null)
                                            {
                                                foreach (AutocompleteItem incf in incs)
                                                {
                                                    for (int a = 0; a < AutoCompleteIncludes.Count; a++)
                                                        if (AutoCompleteIncludes[a].MenuText == incf.MenuText && AutoCompleteIncludes[a].Text == incf.Text)
                                                            goto skip;

                                                    AutoCompleteIncludes.Add(incf);
                                                skip: ;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                if (oldLines != null)
                    return;

                if (isLineDeclaration(lineText, this.Lines.ToArray(), e.Index - 1))
                {
                    List<AutocompleteItem> items = parseLine(lineText, e.Index, new System.IO.FileInfo(this.Name).Name);
                    if (items != null)
                    {
                        foreach (AutocompleteItem item in items)
                        {
                            for (int a = 0; a < AutoCompleteWords.Count; a++)
                                if (AutoCompleteWords[a].MenuText == item.MenuText && AutoCompleteWords[a].Text == item.Text)
                                    goto skip;

                            AutoCompleteWords.Add(item);
                        skip: ;
                        }
                    }
                }

                SetAutoCompleteItems();
            }
        }
        
        private void SetAutoCompleteItems()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            items.AddRange(AutoCompleteWords);
            items.AddRange(AutoCompleteIncludes);

            items.Sort((a, b) => a.MenuText.CompareTo(b.MenuText));

            //C keywords
            string[] cwords = Globals.CKeywords.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int x = 0; x < cwords.Length; x++ )
            {
                items.Add(new AutocompleteItem() { MenuText = cwords[x].ToLower(), Text = cwords[x].ToLower(), ToolTipTitle = "C Keyword" });
            }

            AutoComplete.ShowItemToolTips = true;
            AutoComplete.ToolTipDuration = 0x7FFFFFFF;
            AutoComplete.ToolTip.BackColor = this.BackColor;
            AutoComplete.ToolTip.ForeColor = this.ForeColor;
            //popupAutoCom.ImageList = descImageList;
            AutoComplete.BackColor = this.BackColor;
            AutoComplete.ForeColor = this.ForeColor;
            AutoComplete.Font = new Font(FontFamily.GenericMonospace, 9.75f);
            AutoComplete.SelectedColor = Color.Red;
            AutoComplete.Items.MaximumSize = new System.Drawing.Size(300, 300);
            AutoComplete.Items.Width = 300;
            AutoComplete.MinFragmentLength = 0;
            AutoComplete.AppearInterval = 0x7FFFFFFF;
            AutoComplete.Items.SetAutocompleteItems(items);
        }

        private void CSyntaxHighlight(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            this.LeftBracket = '(';
            this.RightBracket = ')';
            this.LeftBracket2 = '{';
            this.RightBracket2 = '}';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);


            //string highlighting
            e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(#define|#include)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(BlueStyle, @"\b(" + Globals.CKeywords + @")\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            //e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }


        #region C Syntax Parsing

        Place isLineDeclaration_FindReverseString(string[] lines, int startLine, int startInd, string find)
        {
            Place ret = new Place();

            if (startLine >= lines.Length)
            {
                startLine = lines.Length - 1;
                startInd = lines[startLine].Length - 1;
            }

            if (startInd >= lines[startLine].Length || startInd < 0)
                startInd = lines[startLine].Length - 1;

            int i = lines[startLine].LastIndexOf(find, startInd);
            while (i < 0)
            {
                startLine--;
                if (startLine < 0)
                    break;
                startInd = lines[startLine].Length - 1;

                i = lines[startLine].LastIndexOf(find, startInd);
            }

            ret.iLine = startLine;
            ret.iChar = i;

            return ret;
        }

        bool isLineDeclaration(string line, string[] lines = null, int ind = -1)
        {
            line = line.Trim();
            Regex trimmer = new Regex(@"\s\s+");
            line = trimmer.Replace(line, " ");
            string firstWord = line.Split(' ')[0].ToLower();

            //Check if it is within a multiline comment
            if (lines != null && ind >= 0)
            {
                Place multiOpen = isLineDeclaration_FindReverseString(lines, ind, -1, "/*");
                Place multiClose = isLineDeclaration_FindReverseString(lines, ind, -1, "*/");

                if (multiOpen.iLine >= 0 && multiClose.iLine < multiOpen.iLine)
                    return false;
                if ((multiOpen.iLine >= 0 && multiClose.iLine <= multiOpen.iLine) && multiClose.iChar > multiOpen.iChar)
                    return false;
            }

            

            //is define
            if (firstWord == "#define")
                return true;

            //is known type
            string[] types = Globals.CTypes.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (types.Contains(firstWord))
                return true;

            //attempt to see if it matches the format at least in case of user defined type

            //Collapse containers down so they don't interfere with the checking
            string tLine = line;
            //Remove comment
            if (tLine.IndexOf("//") >= 0)
                tLine = tLine.Substring(0, tLine.IndexOf("//"));

            List<string> preStrings = new List<string>();
            preStrings.AddRange(parseLine_Collapse(ref tLine, "{", "}", preStrings.Count));
            preStrings.AddRange(parseLine_Collapse(ref tLine, "[", "]", preStrings.Count));
            preStrings.AddRange(parseLine_Collapse(ref tLine, "(", ")", preStrings.Count));
            preStrings.AddRange(parseLine_Collapse(ref tLine, "\"", "\"", preStrings.Count));

            List<string> tWords = tLine.Split(new string[] { "$__", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //Strip out the collapsed pieces
            for (int t = 0; t < tWords.Count; t++ )
            {
                int tFind = tWords[t].IndexOf("__$");
                if (tFind >= 0)
                {
                    //tWords.RemoveAt(t);
                    //t--;
                    tWords[t] = tWords[t].Remove(0, tFind + 3);
                }
            }

            //Check if var first
            int v = tLine.IndexOf(" "), v1 = tLine.IndexOf(" = "), v2 = tLine.IndexOf(" : ");
            if ((v1 > 0 || v2 > 0) && tWords.Count > 2)
            {
                //Check if there are at least 2 words before the =/:
                int v3 = (v1 < v2 || v2 < 0) ? v1 : v2;
                if (v < v3 && tLine.IndexOf(" ", v + 1) < v3)
                    return true;
            }
            else
            {
                //Check if unitialized var
                if (tWords.Count == 2 && tWords[1].EndsWith(";") && tWords[1].Length > 1 && tWords[0].IndexOf(";") < 0)
                    return true;

                //Verify not non-type keyword (ie: if, switch, ect)
                types = Globals.CKeywords.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (types.Contains(firstWord))
                    return false;

                //Check if function
                if (tWords.Count > 1 && preStrings.Count > 0 && preStrings[0].StartsWith("(") && preStrings[0].EndsWith(")") && preStrings[0].IndexOf(" ") > 0 && line.IndexOf("=") <= 0)
                {
                    //Check if there are at least 2 words before the (
                    v1 = line.IndexOf("(");
                    v = line.IndexOf(" ");
                    if (v < v1 && v >= 0)
                    {
                        if (line[v1 - 1] != ' ')
                            return true;
                        else if (line.IndexOf(" ", v + 1) < v1)
                            return true;
                    }
                }
            }

            return false;
        }

        AutocompleteItem _parseLine(string line, int ln = -1, string file = "")
        {
            AutocompleteItem ret = new AutocompleteItem();

            if (line == null || line == "")
                return null;

            try
            {
                line = line.Trim().Trim(';');
                string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                //#define VARNAME VALUE
                if (words[0].ToLower() == "#define")
                {
                    if (words.Length <= 2) //needs a value
                        return null;

                    ret.MenuText = words[1];
                    ret.Text = words[1];
                    ret.ToolTipText = line;
                    ret.ToolTipTitle = words[1];
                    if (ln >= 0)
                        ret.ToolTipTitle += " (File:" + file + ",Line:" + ln.ToString() + ")";
                    return ret;
                }

                if (words[0].ToLower() == "register")
                {
                    //Find the =
                    int regIndex = 0;
                    while (regIndex < words.Length && words[regIndex].IndexOf("=") < 0)
                        regIndex++;

                    if (regIndex >= words.Length)
                        return null;

                    regIndex -= 2;
                    if (regIndex < 0)
                        regIndex = 1;


                    ret.MenuText = words[regIndex];
                    ret.Text = words[regIndex];
                    ret.ToolTipText = line;
                    ret.ToolTipTitle = words[regIndex];
                    if (ln >= 0)
                        ret.ToolTipTitle += " (File:" + file + ",Line:" + ln.ToString() + ")";
                    return ret;
                }

                //Bit definition (part of struct)
                if (line.IndexOf(" : ") > 0 && !(line.IndexOf(" = ") >= 0 && line.IndexOf(" = ") < line.IndexOf(" : ")))
                {

                    int colonIndex = 0;
                    while (words[colonIndex] != ":")
                        colonIndex++;

                    if (!Globals.CKeywords.Contains(words[colonIndex-1].ToLower())) //this means its has a name!!
                    {
                        ret.MenuText = words[colonIndex - 1];
                        ret.Text = words[colonIndex-1];
                        ret.ToolTipText = line;
                        ret.ToolTipTitle = words[colonIndex - 1];
                        if (ln >= 0)
                            ret.ToolTipTitle += " (File:" + file + ",Line:" + ln.ToString() + ")";
                        return ret;
                    }

                    return null;
                }

                //find name (finds the ; or = to denote variable)
                int x = 0;
                for (x = 0; x < words.Length; x++)
                {
                    if (words[x].IndexOf("(") >= 0) //function?
                    {
                        if (words[x].IndexOf("(") == 0) //go back one for name
                        {
                            x--;
                            words[x] += "()";
                            break;
                        }
                        else
                        {
                            words[x] = words[x].Split('(')[0] + "()";
                            break;
                        }
                    }
                    if (words[x].EndsWith(";"))
                        break;
                    if (words[x] == "=")
                    {
                        x--;
                        break;
                    }
                }

                if (x < words.Length) //found name
                {
                    words[x] = words[x].Replace("*", "");
                    ret.MenuText = words[x];
                    ret.Text = words[x];
                    ret.ToolTipText = line;
                    ret.ToolTipTitle = words[x];
                    if (ln >= 0)
                        ret.ToolTipTitle += " (File:" + file + ",Line:" + ln.ToString() + ")";
                    return ret;
                }
            }
            catch (Exception ee)
            {
                return null;
            }

            return null;
        }

        string parseLine_Expand(string line, List<string> strings)
        {
            int q1 = line.IndexOf("$__"), q2 = 0;

            while (q1 >= 0)
            {
                q2 = line.IndexOf("__$", q1 + 1);
                if (q2 >= 0)
                {
                    int v = int.Parse(line.Substring(q1 + 3, q2 - q1 - 3));
                    line = line.Replace("$__" + v.ToString() + "__$", strings[v]);
                }

                q1 = line.IndexOf("$__");
            }

            return line;
        }

        List<string> parseLine_Collapse(ref string line, string open, string close, int ind = 0)
        {
            int quote1 = line.IndexOf(open), quote2 = 0;
            int qCnt = 0, qInd = 0;
            List<string> preStrings = new List<string>();
            while (quote1 >= 0)
            {
                //qInd++;

                int tq = line.IndexOf(open, quote1 + 1);
                quote2 = line.IndexOf(close, quote1 + 1);

                if (tq < quote2 && tq >= 0)
                {
                    qInd++;
                    quote1 = tq;
                    continue;
                }
                else if (quote2 < 0)
                    quote2 = line.Length - 1;
                else
                    qInd--;

                if (qInd <= 0)
                {
                    preStrings.Add(line.Substring(quote1, quote2 - quote1 + 1));
                    line = line.Substring(0, quote1) + "$__" + (qCnt + ind).ToString() + "__$" + line.Substring(quote2 + 1, line.Length - quote2 - 1);
                    qInd = 0;
                    qCnt++;
                }

                if (qInd == 0)
                    quote1 = line.IndexOf(open);
                else
                    quote1 = line.IndexOf(open, quote1 + 1);
            }

            return preStrings;
        }

        List<AutocompleteItem> parseLine(string line, int ln = -1, string file = "")
        {
            List<AutocompleteItem> ret = new List<AutocompleteItem>();

            AutocompleteItem item = null;

            line = line.Trim();
            //Remove comment
            if (line.IndexOf("//") >= 0)
                line = line.Substring(0, line.IndexOf("//"));

            //Check if commas
            if (line.IndexOf(",") >= 0)
            {
                //Remove comment
                string tLine = line;

                //Collapse containers down so they don't interfere with the checking
                List<string> preStrings = new List<string>();
                preStrings.AddRange(parseLine_Collapse(ref tLine, "{", "}", preStrings.Count));
                preStrings.AddRange(parseLine_Collapse(ref tLine, "[", "]", preStrings.Count));
                preStrings.AddRange(parseLine_Collapse(ref tLine, "(", ")", preStrings.Count));
                preStrings.AddRange(parseLine_Collapse(ref tLine, "\"", "\"", preStrings.Count));

                if (tLine.IndexOf(",") >= 0)
                {
                    string[] parts = tLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string type = "";
                    for (int p = 0; p < parts.Length; p++)
                    {
                        string part = parts[p];

                        if (p != 0)
                        {
                            part = parseLine_Expand(part, preStrings);
                            item = _parseLine(type + " " + part.Trim(), ln, file);
                            if (item != null)
                                ret.Add(item);
                        }
                        else
                        {
                            string[] pWords = part.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                            if (part.IndexOf("=") < 0 && part.IndexOf(":") < 0) //everything but last word is the type
                            {
                                type = String.Join(" ", pWords, 0, pWords.Length - 1);
                            }
                            else //account for equal sign
                            {
                                int pInd = 0;
                                while (pInd < pWords.Length && pWords[pInd].IndexOf("=") < 0 && pWords[pInd].IndexOf(":") < 0)
                                    pInd++;
                                
                                if (pWords[pInd].IndexOf("=") == 0)
                                    pInd--;
                                if (pWords[pInd].IndexOf(":") == 0)
                                    pInd--;

                                type = String.Join(" ", pWords, 0, pInd);
                            }

                            part = parseLine_Expand(part, preStrings);
                            item = _parseLine(part, ln, file);
                            if (item != null)
                                ret.Add(item);
                        }
                    }
                }
                else
                {
                    tLine = parseLine_Expand(tLine, preStrings);
                    item = _parseLine(tLine, ln, file);
                    if (item != null)
                        ret.Add(item);
                }
            }
            else
            {
                item = _parseLine(line, ln, file);
                if (item != null)
                    ret.Add(item);
            }


            return ret;
        }

        #endregion

    }
}
