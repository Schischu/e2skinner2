using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using e2skinner2.Logic;
using System.Drawing.Imaging;
using System.IO;

namespace e2skinner2.Structures
{
    class sGraphicImage : sGraphicElement
    {
        protected Image pImage;

        //protected sAttribute pAttr;

        public sGraphicImage(sAttribute attr, String image, UInt32 x, UInt32 y, UInt32 w, UInt32 h)
            : base(attr)
        {
            pAttr = attr;

            try
            {
                pImage = Image.FromFile(cDataBase.getPath(image));
            }
            catch (FileNotFoundException ex)
            {
                return;
            }
            pX = x;
            pY = y;
            pWidth = w < (UInt32)pImage.Width ? w : (UInt32)pImage.Width;
            pHeight = h < (UInt32)pImage.Height ? h : (UInt32)pImage.Height; ;
        }

        public sGraphicImage(sAttribute attr, String image, UInt32 x, UInt32 y)
            : base(attr)
        {
            pAttr = attr;
            if (image == null || image.Length == 0)
                return;
            try
            {
                pImage = Image.FromFile(cDataBase.getPath(image));
            }
            catch (FileNotFoundException ex)
            {
                return;
            }
            pX = x;
            pY = y;
            pWidth = (UInt32)pImage.Width;
            pHeight = (UInt32)pImage.Height;
        }

        public sGraphicImage(sAttribute attr, String image)
            : base(attr)
        {
            pAttr = attr;
            if (image == null || image.Length == 0)
                return;
            try
            {
                pImage = Image.FromFile(cDataBase.getPath(image));
            }
            catch (FileNotFoundException ex)
            {
                return;
            }
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //if (!cProperties.getPropertyBool("skinned"))
            //{
            //    new sGraphicRectangel(pAttr, false, (float)1.0, Color.Red).paint(sender, e);
            //}
            //else
            if(pImage != null)
            {
                Graphics g = e.Graphics;
                g.DrawImageUnscaledAndClipped(pImage, new Rectangle((int)pX, (int)pY, pWidth < pImage.Width ? (int)pWidth : pImage.Width, pHeight < pImage.Height ? (int)pHeight : pImage.Height));
            }
        }
    }
}
