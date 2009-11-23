using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using e2skinner2.Logic;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace e2skinner2.Structures
{
    class sGraphicFont : sGraphicElement
    {
        protected String pText;
        protected float pSize;
        protected sFont pFont;
        protected Color pColor;
        protected bool pTranparent;
        protected Color pBackColor;
        protected e2skinner2.Structures.cProperty.eHAlign pAlignment;

        //protected sAttribute pAttr;

        public sGraphicFont(sAttribute attr, UInt32 x, UInt32 y, String text, float size, sFont font, Color color, e2skinner2.Structures.cProperty.eHAlign alignment)
            : base(attr)
        {
            pAttr = attr;

            pX = x;
            pY = y;

            pText = text;
            pSize = size;
            pFont = font;
            pColor = color;
            pAlignment = alignment;

            pTranparent = true;
        }

        public sGraphicFont(sAttribute attr, UInt32 x, UInt32 y, String text, float size, sFont font, Color color, Color backcolor, e2skinner2.Structures.cProperty.eHAlign alignment)
            : base(attr)
        {
            pAttr = attr;

            pX = x;
            pY = y;

            pText = text;
            pSize = size;
            pFont = font;
            pColor = color;

            if (backcolor != null)
            {
                pTranparent = false;
                pBackColor = backcolor;
            }
            else pTranparent = true;

            pAlignment = alignment;
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
                Graphics g = e.Graphics;
                System.Drawing.Font font;
                String name = "";
                try
                {
                    //this crashes, but why?
                   name = pFont.FontFamily.GetName(0);

                   font = new System.Drawing.Font(pFont.FontFamily, pSize, pFont.FontStyle, GraphicsUnit.Pixel);
                }
                catch (Exception error)
                {
                    String errormessage = error.Message + ":\n\n";
                    errormessage += error.StackTrace + "\n\n";
                    errormessage += error.Source + "\n\n";
                    errormessage += error.TargetSite + "\n\n";
                    errormessage += name + "\n\n";

                    MessageBox.Show(errormessage,
                        error.Message,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);

                    return;
                }
            
                if (!pTranparent)
                    new sGraphicRectangel(pAttr, true, 1.0F, pBackColor).paint(sender,e);

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                if (pAlignment == e2skinner2.Structures.cProperty.eHAlign.Left)
                    format.Alignment = StringAlignment.Near;
                else if (pAlignment == e2skinner2.Structures.cProperty.eHAlign.Center)
                    format.Alignment = StringAlignment.Center;
                else
                    format.Alignment = StringAlignment.Far;

                SizeF StringSize = g.MeasureString(pText, font);

                g.DrawString(pText, font, new SolidBrush(pColor), new RectangleF(pX, pY, pWidth, StringSize.Height < pHeight ? StringSize.Height : pHeight), format);
                
        }
    }
}
