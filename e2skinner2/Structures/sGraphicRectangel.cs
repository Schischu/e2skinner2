using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;

namespace e2skinner2.Structures
{
    class sGraphicRectangel : sGraphicElement
    {
        protected bool pFilled;
        protected float pLineWidth;
        protected Color pColor;

        public sGraphicRectangel(sAttribute attr, bool filled, float linewidth, Color color)
            : base(attr)
        {
            pFilled = filled;
            pLineWidth = linewidth;
            pColor = color;
        }

        public sGraphicRectangel(UInt32 x, UInt32 y, UInt32 width, UInt32 height, bool filled, float linewidth, Color color)
            : base(x, y, width, height)
        {
            pFilled = filled;
            pLineWidth = linewidth;
            pColor = color;

            pZPosition = 1000;
        }

        public override void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (pFilled)
                g.FillRectangle(new SolidBrush(pColor), pX, pY, pWidth, pHeight);
            else
                g.DrawRectangle(new Pen(pColor, pLineWidth), pX, pY, pWidth, pHeight);
        }
    }
}
