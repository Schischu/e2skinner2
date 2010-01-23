using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.ComponentModel;

namespace e2skinner2.Structures
{
    public class sGraphicElement : IComparable
    {
        private UInt32 _pX;
        private UInt32 _pY;

        [BrowsableAttribute(false)]
        public UInt32 pX
        {
            get { return pAttr != null?pAttr.pAbsolutX:_pX; }
            set { _pX = value; }
        }

        [BrowsableAttribute(false)]
        public UInt32 pY
        {
            get { return pAttr != null ? pAttr.pAbsolutY : _pY; }
            set { _pY = value; }
        }

        //public UInt32 pX;
        //public UInt32 pY;
        public UInt32 pWidth;
        public UInt32 pHeight;

        public sAttribute pAttr = null;

        public Int32 pZPosition;

        public sGraphicElement(sAttribute attr)
        {
            pAttr = attr;

            pX = attr.pAbsolutX;
            pY = attr.pAbsolutY;
            pWidth = attr.pWidth;
            pHeight = attr.pHeight;

            pZPosition = attr.pZPosition;
        }

        public sGraphicElement(UInt32 x, UInt32 y, UInt32 width, UInt32 height)
        {
            pX = x;
            pY = y;
            pWidth = width;
            pHeight = height;

            pZPosition = 0;
        }

        public virtual void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        }

        public int CompareTo(object obj)
        {
            sGraphicElement val = (sGraphicElement)obj;
            if (val.pZPosition < this.pZPosition)
                return 1;
            else if (val.pZPosition == this.pZPosition)
                return 0;
            else
                return -1;
        }
    }
}
