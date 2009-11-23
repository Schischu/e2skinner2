using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using e2skinner2.Structures;
using System.Collections;

namespace e2skinner2.Logic
{
    public class cDesigner
    {
        protected ArrayList pDrawList = null;
        protected Graphics pGraph = null;

        public cDesigner(Graphics graph)
        {
            pDrawList = new ArrayList();

            pGraph = graph;
        }

        public void sort()
        {
            pDrawList.Sort();
        }

        public void clear()
        {
            pDrawList.Clear();
        }

        public void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //the array should be sorrted regarding zposition !!!
            foreach (sGraphicElement ele in pDrawList)
            {
                ele.paint(sender, e);
            }
        }

        public sGraphicElement getElement(uint x, uint y)
        {
            //the array should be sorrted regarding zposition !!!
            for (int i = 0; i < pDrawList.Count; i++)
            {
                sGraphicElement ele = (sGraphicElement)pDrawList[pDrawList.Count - 1 - i];

                if (ele.pX <= x && x < ele.pX + ele.pWidth &&
                    ele.pY <= y && y < ele.pY + ele.pHeight &&
                    ele.pZPosition != 1000)
                    return ele;
            }
            return null;
        }

        public void drawFog(int x, int y, int w, int h)
        {
            sResolution res = cDataBase.pResolution.getResolution();

            int Xres = (int)res.Xres;
            int Yres = (int)res.Yres;

            pDrawList.Add(new sGraphicRectangel(0,              0, (uint)Xres, (uint)y, true, (float)1.0, Color.FromArgb(200, Color.LightGray)));
            pDrawList.Add(new sGraphicRectangel(0, (uint)y, (uint)x, (uint)h, true, (float)1.0, Color.FromArgb(200, Color.LightGray)));
            pDrawList.Add(new sGraphicRectangel((uint)(x + w), (uint)y, (uint)((Xres - x - w) > 0 ? (Xres - x - w) : 0), (uint)h, true, (float)1.0, Color.FromArgb(200, Color.LightGray)));
            pDrawList.Add(new sGraphicRectangel(0, (uint)(y + h), (uint)Xres, (uint)((Yres - y - h) > 0 ? (Yres - y - h) : 0), true, (float)1.0, Color.FromArgb(200, Color.LightGray)));
        }

        public void drawFrame()
        {
            sResolution res = cDataBase.pResolution.getResolution();
            pDrawList.Add(new sGraphicRectangel(0, 0, res.Xres, res.Yres, false, (float)1.0, Color.Black));
            pDrawList.Add(new sGraphicRectangel(0, 0, res.Xres + 2, res.Yres + 2, false, (float)1.0, Color.Gray));
            pDrawList.Add(new sGraphicRectangel(0, 0, res.Xres + 4, res.Yres + 4, false, (float)1.0, Color.Black));
        }

        public void draw(sAttribute attr)
        {
            sGraphicElement ele = null;

            Type type = attr.GetType();
            if (type == typeof(sAttributeScreen))
            {
                ele = new sGraphicScreen((sAttributeScreen)attr);
            }
            else if (type == typeof(sAttributeLabel))
            {
                ele = new sGraphicLabel((sAttributeLabel)attr);
            }
            else if (type == typeof(sAttributePixmap))
            {
                ele = new sGraphicPixmap((sAttributePixmap)attr);
            }
            else if (type == typeof(sAttributeWidget))
            {
                //ele = new sGraphicRectangel((sAttributeWidget)attr, false, 1.0F, Color.GreenYellow);
                ele = new sGraphicWidget((sAttributeWidget)attr);
            }

            pDrawList.Add(ele);
        }
    }
}
