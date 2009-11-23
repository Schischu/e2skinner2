using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using e2skinner2.Logic;
using System.Drawing;

namespace e2skinner2.Structures
{
    class sGraphicLabel : sGraphicElement
    {
        //protected sAttributeLabel pAttr;

        public sGraphicLabel(sAttributeLabel attr)
            : base(attr)
        {
            pAttr = attr;
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!cProperties.getPropertyBool("skinned_label") || cProperties.getPropertyBool("label_test"))
            {
                new sGraphicRectangel(pAttr, false, (float)1.0, Color.Green).paint(sender, e);
            }

            if (cProperties.getPropertyBool("skinned_label"))
            {
                if (((sAttributeLabel)pAttr).pText != null || ((sAttributeLabel)pAttr).pPreviewText != null)
                {
                    String text = "";
                    if (((sAttributeLabel)pAttr).pText != null)
                        text = ((sAttributeLabel)pAttr).pText;
                    else
                        text = ((sAttributeLabel)pAttr).pPreviewText;

                    //Hack
                    if (((sAttributeLabel)pAttr).pPreviewText == "MAGIC#TRUE") { }
                    //pLabel.pText = "";
                    else if (((sAttributeLabel)pAttr).pPreviewText == "MAGIC#FALSE")
                        text = "";

                    if (pAttr.pTransparent)
                        new sGraphicFont(pAttr, pAttr.pAbsolutX, pAttr.pAbsolutY, text, ((sAttributeLabel)pAttr).pFontSize * (((float)((sAttributeLabel)pAttr).pFont.Scale) / 100.0F), ((sAttributeLabel)pAttr).pFont, ((sAttributeLabel)pAttr).pForegroundColor.pColor, ((sAttributeLabel)pAttr).pHalign).paint(sender, e);
                    else
                        new sGraphicFont(pAttr, pAttr.pAbsolutX, pAttr.pAbsolutY, text, ((sAttributeLabel)pAttr).pFontSize * (((float)((sAttributeLabel)pAttr).pFont.Scale) / 100.0F), ((sAttributeLabel)pAttr).pFont, ((sAttributeLabel)pAttr).pForegroundColor.pColor, ((sAttributeLabel)pAttr).pBackgroundColor == null ? Color.Black : ((sAttributeLabel)pAttr).pBackgroundColor.pColor, ((sAttributeLabel)pAttr).pHalign).paint(sender, e);
                }
                else
                {
                    if (!pAttr.pTransparent)
                        new sGraphicRectangel(pAttr, true, 1.0F, ((sAttributeLabel)pAttr).pBackgroundColor.pColor).paint(sender, e);
                }

                if(pAttr.pBorder)
                    new sGraphicRectangel(pAttr, false, (float)pAttr.pBorderWidth, pAttr.pBorderColor.pColor).paint(sender, e);

            }
        }
    }
}
