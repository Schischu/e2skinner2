using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using e2skinner2.Logic;
using System.Drawing;

namespace e2skinner2.Structures
{
    class sGraphicPixmap : sGraphicElement
    {
        //protected sAttributePixmap pAttr;
        
        public sGraphicPixmap(sAttributePixmap attr)
            : base(attr)
        {
            pAttr = attr;
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!cProperties.getPropertyBool("skinned_pixmap"))
            {
                new sGraphicRectangel(pAttr, false, (float)1.0, new sColor(Color.Blue)).paint(sender, e);
            }
            else
            {
                if (((sAttributePixmap)pAttr).pPixmapName != null)
                    new sGraphicImage(pAttr, ((sAttributePixmap)pAttr).pPixmapName).paint(sender, e);
                /*else
                 * 
                 * Show missing icon ?
                 */
            }
        }
    }
}
