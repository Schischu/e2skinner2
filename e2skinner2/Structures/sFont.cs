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
            Name = name; 
            Path = path;
            Filename = Path.Substring(Path.LastIndexOf('/')>0?Path.LastIndexOf('/'):0);
            Scale = scale;
            Replacement = replacement;

            pfc = new PrivateFontCollection();
                try
                {
                    pfc.AddFontFile(cProperties.getProperty("path_fonts") + "/" + Path);
                }
                catch (FileNotFoundException error)
                {
                    String errormessage = error.Message + ":\n\n";
                    errormessage += cProperties.getProperty("path_fonts") + "/" + Path + "\n";
                    errormessage += error.Message;

                    MessageBox.Show(errormessage,
                        error.Message,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);

                    return;
                }
                //pfc.Families.GetValue(0);

                //FontFamily = pfc.Families[0];
                FontFamily = pfc.Families[0];
                String name2 = FontFamily.GetName(0);
                //name2 = FontFamily.Name;
                FontStyle = System.Drawing.FontStyle.Regular;
                if (FontFamily.IsStyleAvailable(System.Drawing.FontStyle.Regular))
                    FontStyle = System.Drawing.FontStyle.Regular;
                else
                    FontStyle = System.Drawing.FontStyle.Bold;

                int t1 = FontFamily.GetCellAscent(FontStyle);
                int t2 = FontFamily.GetCellDescent(FontStyle);
                int t3 = FontFamily.GetEmHeight(FontStyle);
                int t4 = FontFamily.GetLineSpacing(FontStyle);

                //Font = new System.Drawing.Font(FontFamily, 20, FontStyle, System.Drawing.GraphicsUnit.Pixel);
        }
    }
}
