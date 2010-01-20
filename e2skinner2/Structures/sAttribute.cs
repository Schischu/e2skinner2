using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using e2skinner2.Logic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace e2skinner2.Structures
{
    public class sAttribute
    {
        public class PositionConverter : ExpandableObjectConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
            {
                if (destinationType == typeof(Position))
                    return true;
                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context,
                CultureInfo culture,
                object value,
                System.Type destinationType)
            {
                if (destinationType == typeof(System.String) && value is Position)
                {
                    return value.ToString();
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context,
                System.Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context,
                              CultureInfo culture, object value) 
            {
                if (value is string) {
                    try {
                        string s = (string) value;
                        int comma = s.IndexOf(',');
                        if (comma != -1) {
                            string X = s.Substring(0, comma);
                            string Y = s.Substring(comma + 1).Trim();

                            //Test if valid input
                            if (!X.Equals("center"))
                                Int32.Parse(X);
                            if (!Y.Equals("center"))
                                Int32.Parse(Y);

                            Position po = new Position();
                            po.X = X;
                            po.Y = Y;
                            return po;
                        }
                    }
                    catch {
                        throw new ArgumentException(
                            " '" + (string)value + " is not valid input!");
                    }
                }  
                return base.ConvertFrom(context, culture, value);
            }
        }

        [TypeConverterAttribute(typeof(PositionConverter/*ExpandableObjectConverter*/))]
        public class Position
        {
            public Position() { X = "0"; Y = "0"; }
            public Position(Size sz) { X = sz.Width.ToString(); Y = sz.Height.ToString(); }
            public Position(int x, int y) { X = x.ToString(); Y = y.ToString(); }
            public Position(String x, String y) { X = x; Y = y; }

            public String X { get; set; }
            public String Y { get; set; }

            public Int32 iX() { return Int32.Parse(X); }
            public Int32 iY() { return Int32.Parse(Y); }

            public override string ToString() { return X + ", " + Y; }
        }

        private const String entryName = "1 Global";

        public UInt32 pAbsolutX;
        public UInt32 pAbsolutY;
        public UInt32 pRelativX;
        public UInt32 pRelativY;
        public UInt32 pWidth;
        public UInt32 pHeight;

        public String pName;

        public Int32 pZPosition;
        public bool pTransparent;

        //Border around Element, is this allowed for every element ?
        public bool pBorder;
        public UInt32 pBorderWidth;
        public sColor pBorderColor;

        [CategoryAttribute(entryName),
        DefaultValueAttribute("")]
        public String Name
        {
            get { return pName; }
            set { 
                pName = value;
                if (pName.Length > 0)
                {
                    if (myNode.Attributes["name"] != null)
                        myNode.Attributes["name"].Value = pName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("name"));
                        myNode.Attributes["name"].Value = pName;
                    }
                }
                else
                    if (myNode.Attributes["name"] != null)
                        myNode.Attributes.RemoveNamedItem("name");
            }
        }

        [CategoryAttribute(entryName)]
        public Position Relativ
        {
            get
            {
                String x = pRelativX.ToString(), y = pRelativY.ToString();
                if (pRelativX == (cDataBase.pResolution.getResolution().Xres - pWidth) >> 1 /*1/2*/)
                    x = "center";
                if (pRelativY == (cDataBase.pResolution.getResolution().Yres - pHeight) >> 1 /*1/2*/)
                    y = "center";
                return new Position(x, y); }
            set {

                UInt32 vX = 0;
                UInt32 vY = 0;
                if (value.X.Equals("center"))
                    vX = (cDataBase.pResolution.getResolution().Xres - pWidth) >> 1 /*1/2*/;
                else
                    vX = (UInt32)value.iX();
                if (value.Y.Equals("center"))
                    vY = (cDataBase.pResolution.getResolution().Yres - pHeight) >> 1 /*1/2*/;
                else
                    vY = (UInt32)value.iY();

                pAbsolutX = pAbsolutX + ((UInt32)vX - pRelativX); 
                pRelativX = (UInt32)vX;
                pAbsolutY = pAbsolutY + ((UInt32)vY - pRelativY); 
                pRelativY = (UInt32)vY;



                if (myNode.Attributes["position"] != null)
                    myNode.Attributes["position"].Value = value.X + ", " + value.Y;
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("position"));
                    myNode.Attributes["position"].Value = pRelativX + ", " + pRelativY;
                }
            }
        }

        [CategoryAttribute(entryName),
        ReadOnlyAttribute(true)]
        public Point Absolut
        {
            get { return new Point((int)pAbsolutX, (int)pAbsolutY); }
            //set { pAbsolutX = (UInt32)value.X; pAbsolutY = (UInt32)value.Y; }
        }

        [CategoryAttribute(entryName)]
        public Size Size
        {
            get { return new Size((int)pWidth, (int)pHeight); }
            set { 
                pWidth = (UInt32)value.Width; 
                pHeight = (UInt32)value.Height;

                if (myNode.Attributes["size"] != null)
                    myNode.Attributes["size"].Value = pWidth + ", " + pHeight;
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("size"));
                    myNode.Attributes["size"].Value = pWidth + ", " + pHeight;
                }
            }
        }

        [CategoryAttribute(entryName)]
        public Int32 zPosition
        {
            get { return pZPosition; }
            set { 
                pZPosition = value;

                if (myNode.Attributes["zPosition"] != null)
                    myNode.Attributes["zPosition"].Value = pZPosition.ToString();
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("zPosition"));
                    myNode.Attributes["zPosition"].Value = pZPosition.ToString();
                }
            }
        }

        [CategoryAttribute(entryName)]
        public bool Transparent
        {
            get { return pTransparent; }
            set { 
                pTransparent = value;

                if (myNode.Attributes["transparent"] != null)
                    myNode.Attributes["transparent"].Value = pTransparent?"1":"0";
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("transparent"));
                    myNode.Attributes["transparent"].Value = pTransparent ? "1" : "0";
                }
            }
        }

        [CategoryAttribute(entryName)]
        public UInt32 BorderWidth
        {
            get { return pBorderWidth; }
            set {   
                pBorderWidth = value;
                if (pBorderWidth > 0 && pBorderColor != null)
                    pBorder = true;
                else
                    pBorder = false;

                if (pBorder)
                {
                    if (myNode.Attributes["borderWidth"] != null)
                        myNode.Attributes["borderWidth"].Value = pBorderWidth.ToString();
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("borderWidth"));
                        myNode.Attributes["borderWidth"].Value = pBorderWidth.ToString();
                    }

                    if (myNode.Attributes["borderColor"] != null)
                        myNode.Attributes["borderColor"].Value = pBorderColor.pName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("borderColor"));
                        myNode.Attributes["borderColor"].Value = pBorderColor.pName;
                    }
                }
                else
                {
                    if (myNode.Attributes["borderWidth"] != null)
                        myNode.Attributes.RemoveNamedItem("borderWidth");
                    if (myNode.Attributes["borderColor"] != null)
                        myNode.Attributes.RemoveNamedItem("borderColor");
                }
            }
        }

        [Editor(typeof(e2skinner2.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(e2skinner2.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryName)]
        public String BorderColor
        {
            get
            {
                if (pBorderColor != null) return pBorderColor.pName;
                else return "(none)";
            }
            set {
                if (value != "(none)")
                {
                    pBorderColor = (sColor)cDataBase.pColors.get(value);
                    if (pBorderWidth > 0 && pBorderColor != null)
                        pBorder = true;
                    else
                        pBorder = false;
                }
                else
                {
                    pBorderColor = null;
                    pBorder = false;
                }

                if (pBorder)
                {
                    if (myNode.Attributes["borderWidth"] != null)
                        myNode.Attributes["borderWidth"].Value = pBorderWidth.ToString();
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("borderWidth"));
                        myNode.Attributes["borderWidth"].Value = pBorderWidth.ToString();
                    }

                    if (myNode.Attributes["borderColor"] != null)
                        myNode.Attributes["borderColor"].Value = pBorderColor.pName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("borderColor"));
                        myNode.Attributes["borderColor"].Value = pBorderColor.pName;
                    }
                }
                else
                {
                    if (myNode.Attributes["borderWidth"] != null)
                        myNode.Attributes.RemoveNamedItem("borderWidth");
                    if (myNode.Attributes["borderColor"] != null)
                        myNode.Attributes.RemoveNamedItem("borderColor");
                }
            }
        }

        public XmlNode myNode;

        public sAttribute( sAttribute parent, XmlNode node)
        {
            if (node == null)
                return;

            myNode = node;

            pWidth = Convert.ToUInt32(node.Attributes["size"].Value.Substring(0, node.Attributes["size"].Value.IndexOf(',')).Trim());
            pHeight = Convert.ToUInt32(node.Attributes["size"].Value.Substring(node.Attributes["size"].Value.IndexOf(',') + 1).Trim());

            try
            {
                pRelativX = Convert.ToUInt32(node.Attributes["position"].Value.Substring(0, node.Attributes["position"].Value.IndexOf(',')).Trim());
            } catch(OverflowException e)
            {
                pRelativX = 0;
            }
            pAbsolutX = parent.pAbsolutX+ pRelativX;

            try
            {
                pRelativY = Convert.ToUInt32(node.Attributes["position"].Value.Substring(node.Attributes["position"].Value.IndexOf(',') + 1).Trim());
            }
            catch (OverflowException e)
            {
                pRelativY = 0;
            }
            pAbsolutY = parent.pAbsolutY + pRelativY;


            if (node.Attributes["name"] != null)
                pName = node.Attributes["name"].Value.Trim();
            else
                pName = "";

            if (node.Attributes["zPosition"] != null)
                pZPosition = Convert.ToInt32(node.Attributes["zPosition"].Value.Trim());
            else
                pZPosition = 0;

            if (node.Attributes["transparent"] != null)
                pTransparent = Convert.ToUInt32(node.Attributes["transparent"].Value.Trim()) != 0;
            else
                pTransparent = false;

            if (node.Attributes["borderWidth"] != null)
                pBorderWidth = Convert.ToUInt32(node.Attributes["borderWidth"].Value.Trim());
            else
                pBorderWidth = 0;

            if (node.Attributes["borderColor"] != null)
                pBorderColor = (sColor)cDataBase.pColors.get(node.Attributes["borderColor"].Value.Trim());
            else
                pBorderColor = null;

            if (pBorderWidth > 0 && pBorderColor != null)
                pBorder = true;
            else
                pBorder = false;
        }

        public sAttribute(XmlNode node)
        {
            if (node == null)
                return;

            myNode = node;

            pWidth = Convert.ToUInt32(node.Attributes["size"].Value.Substring(0, node.Attributes["size"].Value.IndexOf(',')).Trim());
            pHeight = Convert.ToUInt32(node.Attributes["size"].Value.Substring(node.Attributes["size"].Value.IndexOf(',') + 1).Trim());

            try
            {
                String sRelativeX = node.Attributes["position"].Value.Substring(0, node.Attributes["position"].Value.IndexOf(',')).Trim();
                if (sRelativeX.Equals("center"))
                    pRelativX = (cDataBase.pResolution.getResolution().Xres - pWidth) >> 1 /*1/2*/;
                else
                    pRelativX = Convert.ToUInt32(sRelativeX);
            }
            catch (OverflowException e)
            {
                pRelativX = 0;
            }
            pAbsolutX = pRelativX;

            try
            {
                String sRelativeY = node.Attributes["position"].Value.Substring(node.Attributes["position"].Value.IndexOf(',') + 1).Trim();
                if (sRelativeY.Equals("center"))
                    pRelativY = (cDataBase.pResolution.getResolution().Yres - pHeight) >> 1 /*1/2*/;
                else
                    pRelativY = Convert.ToUInt32(sRelativeY);
            }
            catch (OverflowException e)
            {
                pRelativY = 0;
            }
            pAbsolutY = pRelativY;
                      

            if (node.Attributes["name"] != null)
                pName = node.Attributes["name"].Value.Trim();

            if (node.Attributes["zPosition"] != null)
                pZPosition = Convert.ToInt32(node.Attributes["zPosition"].Value.Trim());
            else
                pZPosition = 0;

            if (node.Attributes["transparent"] != null)
                pTransparent = Convert.ToUInt32(node.Attributes["transparent"].Value.Trim()) != 0;
            else
                pTransparent = false;

            if (node.Attributes["borderWidth"] != null)
                pBorderWidth = Convert.ToUInt32(node.Attributes["borderWidth"].Value.Trim());
            else
                pBorderWidth = 0;

            if (node.Attributes["borderColor"] != null)
                pBorderColor = (sColor)cDataBase.pColors.get(node.Attributes["borderColor"].Value.Trim());
            else
                pBorderColor = null;

            if (pBorderWidth > 0 && pBorderColor != null)
                pBorder = true;
            else
                pBorder = false;
        }

        public sAttribute(UInt32 x, UInt32 y, UInt32 width, UInt32 height, String name)
        {
            pAbsolutX = x;
            pAbsolutY = y;
            pRelativX = x;
            pRelativY = y;
            pWidth = width;
            pHeight = height;

            pName = name;
        }
    }
}
