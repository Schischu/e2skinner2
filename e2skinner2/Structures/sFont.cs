using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing.Text;
using System.IO;
using e2skinner2.Logic;
using System.Windows.Forms;

namespace e2skinner2.Structures
{
    public class sFont
    {
        public const int DEFAULT_SCALE = 100;
        public const bool DEFAULT_REPLACEMENT = false;

        public String Name = "";
        public String Filename = "";
        public String Path = "";
        public System.Drawing.FontFamily FontFamily;
        public System.Drawing.FontStyle FontStyle;

        //if this isnt defined as global public, mono will lose the reference and crash!!!
        public PrivateFontCollection pfc;

        public System.Drawing.Font Font;
        public int Scale = 0;
        public bool Replacement = false;

        public sFont(String name, String path, int scale, bool replacement)
        {
            String fontPath = cProperties.getProperty("path_fonts");
            Name = name; 
            Path = path;
            //This way we have only the file name, but what happens if the fonts are in the skin directory ?
            //Lets check all posibilities
            Filename = Path.Substring(Path.LastIndexOf('/')>0?Path.LastIndexOf('/')+1:0);
            if (!File.Exists(fontPath + "/" + Filename))
            {
                Filename = Path;
                Filename = Filename.Replace("enigma2", "");
                Filename = Filename.Replace("usr", "");
                Filename = Filename.Replace("local", "");
                Filename = Filename.Replace("share", "");
                Filename = Filename.Replace("var", "");
                fontPath = fontPath.Replace("fonts", "");
            }
            Scale = scale;
            Replacement = replacement;

            pfc = new PrivateFontCollection();
                try
                {
                    pfc.AddFontFile(fontPath + "/" + Filename);
                }
                catch (FileNotFoundException error)
                {
                    String errormessage = error.Message + ":\n\n";
                    errormessage += fontPath + "/" + Filename + "\n";
                    errormessage += error.Message;

                    MessageBox.Show(errormessage,
                        error.Message,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);

                    return;
                }

                FontFamily = pfc.Families[0];
                String name2 = FontFamily.GetName(0);
                FontStyle = System.Drawing.FontStyle.Regular;
                if (FontFamily.IsStyleAvailable(System.Drawing.FontStyle.Regular))
                    FontStyle = System.Drawing.FontStyle.Regular;
                else
                    FontStyle = System.Drawing.FontStyle.Bold;

                int t1 = FontFamily.GetCellAscent(FontStyle);
                int t2 = FontFamily.GetCellDescent(FontStyle);
                int t3 = FontFamily.GetEmHeight(FontStyle);
                int t4 = FontFamily.GetLineSpacing(FontStyle);
        }
    }
}
