using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace e2skinner2.Structures
{
    class sGraphicListbox : sGraphicElement
    {
        //protected sAttributeListbox pAttr;

        public sGraphicListbox(sAttributeListbox attr)
            : base(attr)
        {
            pAttr = attr;
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!pAttr.pTransparent)
            {
                //Background
                Int32 tx = (Int32)pAttr.pAbsolutX;
                Int32 ty = (Int32)pAttr.pAbsolutY;
                Int32 tw = (Int32)pAttr.pWidth;
                Int32 th = (Int32)pAttr.pHeight;

                if (((sAttributeListbox)pAttr).pBackgroundPixmap != null)
                    new sGraphicImage(pAttr, ((sAttributeListbox)pAttr).pBackgroundPixmapName).paint(sender, e);
                else
                    new sGraphicRectangel((UInt32)(tx > 0 ? tx : 0), (UInt32)(ty > 0 ? ty : 0), (UInt32)(tw > 0 ? tw : 0), (UInt32)(th > 0 ? th : 0), true, (float)1.0, ((sAttributeListbox)pAttr).pListboxBackgroundColor.pColor).paint(sender, e);
            }

            //BorderLayout
            UInt32 x = pAttr.pAbsolutX, xm = pAttr.pAbsolutX + pAttr.pWidth;

            if (((sAttributeListbox)pAttr).pbpTopLeftName != null)
            {
                new sGraphicImage(pAttr,
                    ((sAttributeListbox)pAttr).pbpTopLeftName,
                    x - (UInt32)(((sAttributeListbox)pAttr).pbpLeft != null ? ((sAttributeListbox)pAttr).pbpLeft.Width : 0),
                    pAttr.pAbsolutY - (UInt32)((sAttributeListbox)pAttr).pbpTopLeft.Height
                    ).paint(sender, e);
                //painter.blit(tl, ePoint(x, pos.top()));
                //x += (UInt32)pAttr.pbpTopLeft.Width;
            }

            if (((sAttributeListbox)pAttr).pbpTopRightName != null)
            {
                //xm -= (UInt32)pAttr.pbpTopRight.Width;
                new sGraphicImage(pAttr,
                    ((sAttributeListbox)pAttr).pbpTopRightName,
                    xm + (UInt32)(((sAttributeListbox)pAttr).pbpRight != null ? ((sAttributeListbox)pAttr).pbpRight.Width : 0) - (UInt32)((sAttributeListbox)pAttr).pbpTopRight.Width,
                    pAttr.pAbsolutY - (UInt32)((sAttributeListbox)pAttr).pbpTopRight.Height
                    ).paint(sender, e);
                //painter.blit(tr, ePoint(xm, pos.top()), pos);
            }

            if (((sAttributeListbox)pAttr).pbpTopName != null)
            {
                x += (UInt32)(((sAttributeListbox)pAttr).pbpTopLeft != null ? ((sAttributeListbox)pAttr).pbpTopLeft.Width : 0) - (UInt32)(((sAttributeListbox)pAttr).pbpLeft != null ? ((sAttributeListbox)pAttr).pbpLeft.Width : 0);
                int diff = (((sAttributeListbox)pAttr).pbpRight != null ? ((sAttributeListbox)pAttr).pbpRight.Width : 0) - (((sAttributeListbox)pAttr).pbpTopRight != null ? ((sAttributeListbox)pAttr).pbpTopRight.Width : 0);
                xm -= (UInt32)(diff > 0 ? diff : -diff);
                while (x < xm)
                {
                    new sGraphicImage(pAttr,
                        ((sAttributeListbox)pAttr).pbpTopName,
                        x,
                        pAttr.pAbsolutY - (UInt32)((sAttributeListbox)pAttr).pbpTop.Height,
                        xm - x,
                        (UInt32)((sAttributeListbox)pAttr).pbpTop.Height
                        ).paint(sender, e);
                    //painter.blit(t, ePoint(x, pos.top()), eRect(x, pos.top(), xm - x, pos.height()));
                    x += (UInt32)((sAttributeListbox)pAttr).pbpTop.Width;
                }
            }

            x = pAttr.pAbsolutX;
            xm = pAttr.pAbsolutX + pAttr.pWidth;

            if (((sAttributeListbox)pAttr).pbpBottomLeftName != null)
            {
                new sGraphicImage(pAttr,
                    ((sAttributeListbox)pAttr).pbpBottomLeftName,
                    x - (UInt32)(((sAttributeListbox)pAttr).pbpLeft != null ? ((sAttributeListbox)pAttr).pbpLeft.Width : 0),
                    pAttr.pAbsolutY + pAttr.pHeight
                    ).paint(sender, e);
                //painter.blit(bl, ePoint(pos.left(), pos.bottom()-bl->size().height()));
                //x += (UInt32)pAttr.pbpBottomLeft.Width;
            }

            if (((sAttributeListbox)pAttr).pbpBottomRightName != null)
            {
                //xm -= (UInt32)pAttr.pbpBottomRight.Width;
                new sGraphicImage(pAttr,
                    ((sAttributeListbox)pAttr).pbpBottomRightName,
                    xm + (UInt32)(((sAttributeListbox)pAttr).pbpRight != null ? ((sAttributeListbox)pAttr).pbpRight.Width : 0) - (UInt32)((sAttributeListbox)pAttr).pbpBottomRight.Width,
                    pAttr.pAbsolutY + pAttr.pHeight
                    ).paint(sender, e);
                //painter.blit(br, ePoint(xm, pos.bottom()-br->size().height()), eRect(x, pos.bottom()-br->size().height(), pos.width() - x, bl->size().height()));
            }

            if (((sAttributeListbox)pAttr).pbpBottomName != null)
            {
                x += (UInt32)(((sAttributeListbox)pAttr).pbpBottomLeft != null ? ((sAttributeListbox)pAttr).pbpBottomLeft.Width : 0) - (UInt32)(((sAttributeListbox)pAttr).pbpLeft != null ? ((sAttributeListbox)pAttr).pbpLeft.Width : 0);
                int diff = (((sAttributeListbox)pAttr).pbpRight != null ? ((sAttributeListbox)pAttr).pbpRight.Width : 0) - (((sAttributeListbox)pAttr).pbpBottomRight != null ? ((sAttributeListbox)pAttr).pbpBottomRight.Width : 0);
                xm -= (UInt32)(diff > 0 ? diff : -diff);
                while (x < xm)
                {
                    new sGraphicImage(pAttr,
                        ((sAttributeListbox)pAttr).pbpBottomName,
                        x,
                        pAttr.pAbsolutY + pAttr.pHeight,
                        xm - x,
                        (UInt32)((sAttributeListbox)pAttr).pbpBottom.Height
                        ).paint(sender, e);
                    //painter.blit(b, ePoint(x, pos.bottom()-b->size().height()), eRect(x, pos.bottom()-b->size().height(), xm - x, pos.height()));
                    x += (UInt32)((sAttributeListbox)pAttr).pbpBottom.Width;
                }
            }

            UInt32 y = 0;
            //if (pAttr.pbpTopLeft != null)
            //    y = (UInt32)pAttr.pbpTopLeft.Height;

            y += pAttr.pAbsolutY;

            UInt32 ym = pAttr.pAbsolutY + pAttr.pHeight;
            //if (pAttr.pbpBottomLeft != null)
            //    ym -= (UInt32)pAttr.pbpBottomLeft.Height;

            if (((sAttributeListbox)pAttr).pbpLeftName != null)
            {
                while (y < ym)
                {
                    new sGraphicImage(pAttr,
                        ((sAttributeListbox)pAttr).pbpLeftName,
                        pAttr.pAbsolutX - (UInt32)((sAttributeListbox)pAttr).pbpLeft.Width,
                        y,
                        (UInt32)((sAttributeListbox)pAttr).pbpLeft.Width,
                        ym - y
                        ).paint(sender, e);
                    //painter.blit(l, ePoint(pos.left(), y), eRect(pos.left(), y, pos.width(), ym - y));
                    y += (UInt32)((sAttributeListbox)pAttr).pbpLeft.Height;
                }
            }

            y = 0;

            //if (pAttr.pbpTopRight != null)
            //    y = (UInt32)pAttr.pbpTopRight.Height;

            y += pAttr.pAbsolutY;

            ym = pAttr.pAbsolutY + pAttr.pHeight;
            //if (pAttr.pbpBottomRight != null)
            //    ym -= (UInt32)pAttr.pbpBottomRight.Height;

            if (((sAttributeListbox)pAttr).pbpRightName != null)
            {
                while (y < ym)
                {
                    new sGraphicImage(pAttr,
                        ((sAttributeListbox)pAttr).pbpRightName,
                        pAttr.pAbsolutX + pAttr.pWidth,
                        y,
                        (UInt32)((sAttributeListbox)pAttr).pbpRight.Width,
                        ym - y
                        ).paint(sender, e);
                    //painter.blit(r, ePoint(pos.right() - r->size().width(), y), eRect(pos.right()-r->size().width(), y, r->size().width(), ym - y));
                    y += (UInt32)((sAttributeListbox)pAttr).pbpRight.Height;
                }
            }

            if (((sAttributeListbox)pAttr).pSelectionPixmapName != null)
                new sGraphicImage(pAttr, ((sAttributeListbox)pAttr).pSelectionPixmapName).paint(sender, e);
            else
                new sGraphicRectangel(pAttr.pAbsolutX, pAttr.pAbsolutY, pAttr.pWidth, 20, true, 1.0F, ((sAttributeListbox)pAttr).pListboxSelectedBackgroundColor.pColor).paint(sender, e);
        }
    }
}
