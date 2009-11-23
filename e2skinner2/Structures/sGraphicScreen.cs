using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using e2skinner2.Logic;
using System.Drawing;

namespace e2skinner2.Structures
{
    class sGraphicScreen : sGraphicElement
    {
        //protected sAttributeScreen pAttr;
        

        public sGraphicScreen(sAttributeScreen attr)
            : base(attr)
        {
            pAttr = attr;
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!cProperties.getPropertyBool("skinned_screen"))
            {
                new sGraphicRectangel(pAttr, false, (float)1.0, Color.Blue).paint(sender, e);
            }
            else
            {

                switch (((sAttributeScreen)pAttr).pFlags)
                {
                    case sAttributeScreen.eFlags.wfNoBorder:

                        new sGraphicRectangel(pAttr, true, (float)1.0, ((sAttributeScreen)pAttr).pBackgroundColor.pColor).paint(sender, e);
                        //new sGraphicFont(pAttr, pAttr.pTitle, (float)16, "Arial", pAttr.pLabelForegroundColor.pColor).paint(sender, e);
                        break;

                    case sAttributeScreen.eFlags.wfBorder:
                        //Background
                        Int32 tx = (Int32)pAttr.pAbsolutX;
                        Int32 ty = (Int32)pAttr.pAbsolutY;
                        Int32 tw = (Int32)pAttr.pWidth;
                        Int32 th = (Int32)pAttr.pHeight;

                        /*if (pAttr.pbpTop != null)
                            ty += (Int32)pAttr.pbpTop.Height; th -= (Int32)pAttr.pbpTop.Height;

                        if (pAttr.pbpBottom != null)
                            th -= (Int32)pAttr.pbpBottom.Height;

                        if (pAttr.pbpLeft != null)
                            tx += (Int32)pAttr.pbpLeft.Width; tw -= (Int32)pAttr.pbpLeft.Width;

                        if (pAttr.pbpRight != null)
                            tw -= (Int32)pAttr.pbpRight.Width;*/

                        new sGraphicRectangel((UInt32)(tx > 0 ? tx : 0), (UInt32)(ty > 0 ? ty : 0), (UInt32)(tw > 0 ? tw : 0), (UInt32)(th > 0 ? th : 0), true, (float)1.0, ((sAttributeScreen)pAttr).pBackgroundColor.pColor).paint(sender, e);

                        //BorderLayout

                        UInt32 x = pAttr.pAbsolutX, xm = pAttr.pAbsolutX + pAttr.pWidth;

                        if (((sAttributeScreen)pAttr).pbpTopLeftName != null)
	                    {
                            new sGraphicImage(pAttr,
                                ((sAttributeScreen)pAttr).pbpTopLeftName,
                                x - (UInt32)(((sAttributeScreen)pAttr).pbpLeft != null ? ((sAttributeScreen)pAttr).pbpLeft.Width : 0),
                                pAttr.pAbsolutY - (UInt32)((sAttributeScreen)pAttr).pbpTopLeft.Height
                                ).paint(sender, e);
		                    //painter.blit(tl, ePoint(x, pos.top()));
                            //x += (UInt32)pAttr.pbpTopLeft.Width;
	                    }

                        if (((sAttributeScreen)pAttr).pbpTopRightName != null)
	                    {
                            //xm -= (UInt32)pAttr.pbpTopRight.Width;
                            new sGraphicImage(pAttr,
                                ((sAttributeScreen)pAttr).pbpTopRightName,
                                xm + (UInt32)(((sAttributeScreen)pAttr).pbpRight != null ? ((sAttributeScreen)pAttr).pbpRight.Width : 0) - (UInt32)((sAttributeScreen)pAttr).pbpTopRight.Width,
                                pAttr.pAbsolutY - (UInt32)((sAttributeScreen)pAttr).pbpTopRight.Height
                                ).paint(sender, e);
		                    //painter.blit(tr, ePoint(xm, pos.top()), pos);
	                    }

                        if (((sAttributeScreen)pAttr).pbpTopName != null)
	                    {
                            x += (UInt32)(((sAttributeScreen)pAttr).pbpTopLeft != null ? ((sAttributeScreen)pAttr).pbpTopLeft.Width : 0) - (UInt32)(((sAttributeScreen)pAttr).pbpLeft != null ? ((sAttributeScreen)pAttr).pbpLeft.Width : 0);
                            int diff = (((sAttributeScreen)pAttr).pbpRight != null ? ((sAttributeScreen)pAttr).pbpRight.Width : 0) - (((sAttributeScreen)pAttr).pbpTopRight != null ? ((sAttributeScreen)pAttr).pbpTopRight.Width : 0);
                            xm -= (UInt32)(diff > 0 ? diff : -diff);
		                    while (x < xm)
		                    {
                                new sGraphicImage(pAttr,
                                    ((sAttributeScreen)pAttr).pbpTopName,
                                    x,
                                    pAttr.pAbsolutY - (UInt32)((sAttributeScreen)pAttr).pbpTop.Height,
                                    xm - x,
                                    (UInt32)((sAttributeScreen)pAttr).pbpTop.Height
                                    ).paint(sender, e);
			                    //painter.blit(t, ePoint(x, pos.top()), eRect(x, pos.top(), xm - x, pos.height()));
                                x += (UInt32)((sAttributeScreen)pAttr).pbpTop.Width;
		                    }
	                    }

                        x = pAttr.pAbsolutX;
                        xm = pAttr.pAbsolutX + pAttr.pWidth;

                        if (((sAttributeScreen)pAttr).pbpBottomLeftName != null)
	                    {
                            new sGraphicImage(pAttr,
                                ((sAttributeScreen)pAttr).pbpBottomLeftName,
                                x - (UInt32)(((sAttributeScreen)pAttr).pbpLeft != null ? ((sAttributeScreen)pAttr).pbpLeft.Width : 0),
                                pAttr.pAbsolutY + pAttr.pHeight
                                ).paint(sender, e);
		                    //painter.blit(bl, ePoint(pos.left(), pos.bottom()-bl->size().height()));
                            //x += (UInt32)pAttr.pbpBottomLeft.Width;
	                    }

                        if (((sAttributeScreen)pAttr).pbpBottomRightName != null)
	                    {
                            //xm -= (UInt32)pAttr.pbpBottomRight.Width;
                            new sGraphicImage(pAttr,
                                ((sAttributeScreen)pAttr).pbpBottomRightName,
                                xm + (UInt32)(((sAttributeScreen)pAttr).pbpRight != null ? ((sAttributeScreen)pAttr).pbpRight.Width : 0) - (UInt32)((sAttributeScreen)pAttr).pbpBottomRight.Width,
                                pAttr.pAbsolutY + pAttr.pHeight
                                ).paint(sender, e);
		                    //painter.blit(br, ePoint(xm, pos.bottom()-br->size().height()), eRect(x, pos.bottom()-br->size().height(), pos.width() - x, bl->size().height()));
	                    }

                        if (((sAttributeScreen)pAttr).pbpBottomName != null)
	                    {
                            x += (UInt32)(((sAttributeScreen)pAttr).pbpBottomLeft != null ? ((sAttributeScreen)pAttr).pbpBottomLeft.Width : 0) - (UInt32)(((sAttributeScreen)pAttr).pbpLeft != null ? ((sAttributeScreen)pAttr).pbpLeft.Width : 0);
                            int diff = (((sAttributeScreen)pAttr).pbpRight != null ? ((sAttributeScreen)pAttr).pbpRight.Width : 0) - (((sAttributeScreen)pAttr).pbpBottomRight != null ? ((sAttributeScreen)pAttr).pbpBottomRight.Width : 0);
                            xm -= (UInt32)(diff > 0 ? diff : -diff);
		                    while (x < xm)
		                    {
                                new sGraphicImage(pAttr,
                                    ((sAttributeScreen)pAttr).pbpBottomName,
                                    x,
                                    pAttr.pAbsolutY + pAttr.pHeight,
                                    xm - x, 
                                    (UInt32)((sAttributeScreen)pAttr).pbpBottom.Height
                                    ).paint(sender, e);
			                    //painter.blit(b, ePoint(x, pos.bottom()-b->size().height()), eRect(x, pos.bottom()-b->size().height(), xm - x, pos.height()));
                                x += (UInt32)((sAttributeScreen)pAttr).pbpBottom.Width;
		                    }
	                    }

                        UInt32 y = 0;
                        //if (pAttr.pbpTopLeft != null)
                        //    y = (UInt32)pAttr.pbpTopLeft.Height;

                        y += pAttr.pAbsolutY;

                        UInt32 ym = pAttr.pAbsolutY + pAttr.pHeight;
                        //if (pAttr.pbpBottomLeft != null)
                        //    ym -= (UInt32)pAttr.pbpBottomLeft.Height;

                        if (((sAttributeScreen)pAttr).pbpLeftName != null)
	                    {
		                    while (y < ym)
		                    {
                                new sGraphicImage(pAttr,
                                    ((sAttributeScreen)pAttr).pbpLeftName,
                                    pAttr.pAbsolutX - (UInt32)((sAttributeScreen)pAttr).pbpLeft.Width, 
                                    y, 
                                    (UInt32)((sAttributeScreen)pAttr).pbpLeft.Width, 
                                    ym - y
                                    ).paint(sender, e);
			                    //painter.blit(l, ePoint(pos.left(), y), eRect(pos.left(), y, pos.width(), ym - y));
                                y += (UInt32)((sAttributeScreen)pAttr).pbpLeft.Height;
		                    }
	                    }
                    	
	                    y = 0;

                        //if (pAttr.pbpTopRight != null)
                        //    y = (UInt32)pAttr.pbpTopRight.Height;

                        y += pAttr.pAbsolutY;

                        ym = pAttr.pAbsolutY + pAttr.pHeight;
                        //if (pAttr.pbpBottomRight != null)
                        //    ym -= (UInt32)pAttr.pbpBottomRight.Height;

                        if (((sAttributeScreen)pAttr).pbpRightName != null)
	                    {
		                    while (y < ym)
		                    {
                                new sGraphicImage(pAttr,
                                    ((sAttributeScreen)pAttr).pbpRightName,
                                    pAttr.pAbsolutX + pAttr.pWidth, 
                                    y, 
                                    (UInt32)((sAttributeScreen)pAttr).pbpRight.Width, 
                                    ym - y
                                    ).paint(sender, e);
			                    //painter.blit(r, ePoint(pos.right() - r->size().width(), y), eRect(pos.right()-r->size().width(), y, r->size().width(), ym - y));
                                y += (UInt32)((sAttributeScreen)pAttr).pbpRight.Height;
		                    }
	                    }

                        //Title
                        if (pAttr.pAbsolutX != 0 && pAttr.pAbsolutY != 0) //did the user set the wrong borderstyle?
                        {
                            UInt32 Xoff = pAttr.pAbsolutX + ((sAttributeScreen)pAttr).pTitleXOff - (UInt32)(((sAttributeScreen)pAttr).pbpLeft != null ? ((sAttributeScreen)pAttr).pbpLeft.Width : 0);
                            UInt32 Yoff = pAttr.pAbsolutY + ((sAttributeScreen)pAttr).pTitleYOff - (UInt32)(((sAttributeScreen)pAttr).pbpTop != null ? ((sAttributeScreen)pAttr).pbpTop.Height : 0);

                            new sGraphicFont(pAttr, Xoff, Yoff, ((sAttributeScreen)pAttr).pTitle, ((sAttributeScreen)pAttr).pTitleSize, ((sAttributeScreen)pAttr).pTitleFont, ((sAttributeScreen)pAttr).pLabelForegroundColor.pColor, cProperty.eHAlign.Left).paint(sender, e);
                        }
                        break;
                }
            }
        }
    }
}
