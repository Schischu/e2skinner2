using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using e2skinner2.Logic;
using System.ComponentModel;
using System.Drawing;

namespace e2skinner2.Structures
{
    public class sAttribute
    {
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
        public Point Relativ
        {
            get { return new Point((int)pRelativX, (int)pRelativY); }
            set { 
                pAbsolutX = pAbsolutX + ((UInt32)value.X - pRelativX); 
                pRelativX = (UInt32)value.X;
                pAbsolutY = pAbsolutY + ((UInt32)value.Y - pRelativY); 
                pRelativY = (UInt32)value.Y;

                if (myNode.Attributes["position"] != null)
                    myNode.Attributes["position"].Value = pRelativX + ", " + pRelativY;
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

            try
            {
                pRelativX = Convert.ToUInt32(node.Attributes["position"].Value.Substring(0, node.Attributes["position"].Value.IndexOf(',')));
            } catch(OverflowException e)
            {
                pRelativX = 0;
            }
            pAbsolutX = parent.pAbsolutX+ pRelativX;

            try
            {
                pRelativY = Convert.ToUInt32(node.Attributes["position"].Value.Substring(node.Attributes["position"].Value.IndexOf(',') + 1));
            }
            catch (OverflowException e)
            {
                pRelativY = 0;
            }
            pAbsolutY = parent.pAbsolutY + pRelativY;

            pWidth = Convert.ToUInt32(node.Attributes["size"].Value.Substring(0, node.Attributes["size"].Value.IndexOf(',')));
            pHeight = Convert.ToUInt32(node.Attributes["size"].Value.Substring(node.Attributes["size"].Value.IndexOf(',') + 1));

            if (node.Attributes["name"] != null)
                pName = node.Attributes["name"].Value;
            else
                pName = "";

            if (node.Attributes["zPosition"] != null)
                pZPosition = Convert.ToInt32(node.Attributes["zPosition"].Value);
            else
                pZPosition = 0;

            if (node.Attributes["transparent"] != null)
                pTransparent = Convert.ToUInt32(node.Attributes["transparent"].Value) != 0;
            else
                pTransparent = false;

            if (node.Attributes["borderWidth"] != null)
                pBorderWidth = Convert.ToUInt32(node.Attributes["borderWidth"].Value);
            else
                pBorderWidth = 0;

            if (node.Attributes["borderColor"] != null)
                pBorderColor = (sColor)cDataBase.pColors.get(node.Attributes["borderColor"].Value);
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

            try
            {
                pRelativX = Convert.ToUInt32(node.Attributes["position"].Value.Substring(0, node.Attributes["position"].Value.IndexOf(',')));
            }
            catch (OverflowException e)
            {
                pRelativX = 0;
            }
            pAbsolutX = pRelativX;

            try
            {
                pRelativY = Convert.ToUInt32(node.Attributes["position"].Value.Substring(node.Attributes["position"].Value.IndexOf(',') + 1));
            }
            catch (OverflowException e)
            {
                pRelativY = 0;
            }
            pAbsolutY = pRelativY;

            pWidth = Convert.ToUInt32(node.Attributes["size"].Value.Substring(0, node.Attributes["size"].Value.IndexOf(',')));
            pHeight = Convert.ToUInt32(node.Attributes["size"].Value.Substring(node.Attributes["size"].Value.IndexOf(',') + 1));

            if (node.Attributes["name"] != null)
                pName = node.Attributes["name"].Value;

            if (node.Attributes["zPosition"] != null)
                pZPosition = Convert.ToInt32(node.Attributes["zPosition"].Value);
            else
                pZPosition = 0;

            if (node.Attributes["transparent"] != null)
                pTransparent = Convert.ToUInt32(node.Attributes["transparent"].Value) != 0;
            else
                pTransparent = false;

            if (node.Attributes["borderWidth"] != null)
                pBorderWidth = Convert.ToUInt32(node.Attributes["borderWidth"].Value);
            else
                pBorderWidth = 0;

            if (node.Attributes["borderColor"] != null)
                pBorderColor = (sColor)cDataBase.pColors.get(node.Attributes["borderColor"].Value);
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
