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

        public CSyntaxHighlighter()
        {
            this.TextChanged += new EventHandler<TextChangedEventArgs>(this_TextChanged);

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
            e.ChangedRange.SetStyle(BlueStyle, @"\b(auto|break|case|char|const|continue|default|do|double|else|enum|extern|float|for|goto|if|int|long|register|return|short|signed|sizeof|static|struct|switch|typedef|union|unsigned|void|volatile|while)\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            //e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        
    }
}
