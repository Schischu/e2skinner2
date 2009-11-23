using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using e2skinner2.Logic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Collections;

namespace e2skinner2.Structures
{
    class sAttributeLabel : sAttribute
    {
        private const String entryName = "Label";

        private sWindowStyle pWindowStyle;

        public String pText = null;
        public String pPreviewText = null;
        public sFont pFont;
        public float pFontSize;

        public sColor pBackgroundColor;
        public sColor pForegroundColor;

        public cProperty.eVAlign pValign = cProperty.eVAlign.Center;
        public cProperty.eHAlign pHalign = cProperty.eHAlign.Left;

        public bool pNoWrap = true;

        [CategoryAttribute(entryName),
        DefaultValueAttribute("")]
        public String Text
        {
            get { return pText; }
            set
            {
                pText = value;
                if (pText.Length > 0)
                {
                    if (myNode.Attributes["text"] != null)
                        myNode.Attributes["text"].Value = pText;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("text"));
                        myNode.Attributes["text"].Value = pText;
                    }
                }
                else
                    if (myNode.Attributes["text"] != null)
                        myNode.Attributes.RemoveNamedItem("text");
            }
        }

        [CategoryAttribute(entryName),
        ReadOnlyAttribute(true)]
        public String Font
        {
            get
            {
                if (pFont != null) return pFont.Name;
                else return "(none)";
            }
        }

        [CategoryAttribute(entryName)]
        public float FontSize
        {
            get { return pFontSize; }
            set { 
                pFontSize = value;

                if (myNode.Attributes["font"] != null)
                    myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
                else
                {
                    myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
                    myNode.Attributes["font"].Value = pFont.Name + "; " + pFontSize;
                }
            }
        }

        [Editor(typeof(e2skinner2.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(e2skinner2.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryName)]
        public String ForegroundColor
        {
            get { return pForegroundColor.pName; }
            set
            {
                pForegroundColor = (sColor)cDataBase.pColors.get(value);

                if (pForegroundColor != (sColor)pWindowStyle.pColors["LabelForeground"])
                {
                    if (myNode.Attributes["foregroundColor"] != null)
                        myNode.Attributes["foregroundColor"].Value = pForegroundColor.pName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("foregroundColor"));
                        myNode.Attributes["foregroundColor"].Value = pForegroundColor.pName;
                    }
                }
                else
                    if (myNode.Attributes["foregroundColor"] != null)
                        myNode.Attributes.RemoveNamedItem("foregroundColor");
            }
        }

        [Editor(typeof(e2skinner2.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(e2skinner2.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryName)]
        public String BackgroundColor
        {
            get { return pBackgroundColor.pName; }
            set { 
                pBackgroundColor = (sColor)cDataBase.pColors.get(value);
                
                if (pBackgroundColor != (sColor)pWindowStyle.pColors["LabelBackground"])
                {
                    if (myNode.Attributes["backgroundColor"] != null)
                        myNode.Attributes["backgroundColor"].Value = pBackgroundColor.pName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("backgroundColor"));
                        myNode.Attributes["backgroundColor"].Value = pBackgroundColor.pName;
                    }
                }
                else
                    if (myNode.Attributes["backgroundColor"] != null)
                        myNode.Attributes.RemoveNamedItem("backgroundColor");
            }
        }


        [TypeConverter(typeof(cProperty.VAlignConverter)),
        CategoryAttribute(entryName)]
        public String Valign
        {
            get { return pValign.ToString(); }
            set
            {
                if (value == cProperty.eVAlign.Top.ToString()) pValign = cProperty.eVAlign.Top;
                else if (value == cProperty.eVAlign.Center.ToString()) pValign = cProperty.eVAlign.Center;
                else pValign = cProperty.eVAlign.Bottom;

                if (pHalign == cProperty.eHAlign.Left) myNode.Attributes["valign"].Value = "top";
                else if (pHalign == cProperty.eHAlign.Center) myNode.Attributes["valign"].Value = "center";
                else myNode.Attributes["valign"].Value = "bottom";
            }
        }

        [TypeConverter(typeof(cProperty.HAlignConverter)),
        CategoryAttribute(entryName)]
        public String Halign
        {
            get { return pHalign.ToString(); }
            set
            {
                if (value == cProperty.eHAlign.Left.ToString()) pHalign = cProperty.eHAlign.Left;
                else if (value == cProperty.eHAlign.Center.ToString()) pHalign = cProperty.eHAlign.Center;
                else pHalign = cProperty.eHAlign.Right;

                if (pHalign == cProperty.eHAlign.Left) myNode.Attributes["halign"].Value = "left";
                else if (pHalign == cProperty.eHAlign.Center) myNode.Attributes["halign"].Value = "center";
                else myNode.Attributes["halign"].Value = "right";
            }
        }

        [CategoryAttribute(entryName)]
        public bool noWrap
        {
            get { return pNoWrap; }
            set { 
                pNoWrap = value;

                if (!pNoWrap)
                {
                    if (myNode.Attributes["noWrap"] != null)
                        myNode.Attributes["noWrap"].Value = pNoWrap ? "1" : "0";
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("noWrap"));
                        myNode.Attributes["noWrap"].Value = pNoWrap ? "1" : "0";
                    }
                }
                else
                    if (myNode.Attributes["noWrap"] != null)
                        myNode.Attributes.RemoveNamedItem("noWrap");
            }
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>

        public sAttributeLabel(sAttribute parent, XmlNode node)
            : base(parent, node)
        {
 
            pWindowStyle = (sWindowStyle)cDataBase.pWindowstyles.get();

            if (myNode.Attributes["text"] != null)
                pText = myNode.Attributes["text"].Value;

            if (myNode.Attributes["font"] != null)
            {
                String tmpfont = myNode.Attributes["font"].Value;
                float size = Convert.ToSingle(tmpfont.Substring(tmpfont.IndexOf(';') + 1));
                String fontname = tmpfont.Substring(0, tmpfont.IndexOf(';'));
                pFont = cDataBase.getFont(fontname);
                pFontSize = size;
            }
            else
            {
                pFont = cDataBase.getFont("Regular");
                pFontSize = 16;
            }

            if (myNode.Attributes["backgroundColor"] != null)
                pBackgroundColor = (sColor)cDataBase.pColors.get(myNode.Attributes["backgroundColor"].Value);
            else if ((sColor)pWindowStyle.pColors["LabelBackground"] != null)
                pBackgroundColor = (sColor)pWindowStyle.pColors["LabelBackground"];
            else
                pBackgroundColor = (sColor)pWindowStyle.pColors["Background"];

            if (myNode.Attributes["foregroundColor"] != null)
                pForegroundColor = (sColor)cDataBase.pColors.get(myNode.Attributes["foregroundColor"].Value);
            else
                pForegroundColor = (sColor)pWindowStyle.pColors["LabelForeground"];

            if (myNode.Attributes["valign"] != null)
                pValign = myNode.Attributes["valign"].Value.ToLower() == "top" ? cProperty.eVAlign.Top :
                    myNode.Attributes["valign"].Value.ToLower() == "center" ? cProperty.eVAlign.Center :
                    cProperty.eVAlign.Bottom;

            if (myNode.Attributes["halign"] != null)
                pHalign = myNode.Attributes["halign"].Value.ToLower() == "left" ? cProperty.eHAlign.Left :
                    myNode.Attributes["halign"].Value.ToLower() == "center" ? cProperty.eHAlign.Center :
                    cProperty.eHAlign.Right;

            if (myNode.Attributes["noWrap"] != null)
                pNoWrap = Convert.ToUInt32(myNode.Attributes["noWrap"].Value.ToLower()) != 0 ? true : false;
        }
    }
}
