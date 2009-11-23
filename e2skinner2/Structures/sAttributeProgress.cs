using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using e2skinner2.Logic;

namespace e2skinner2.Structures
{
    class sAttributeProgress : sAttribute
    {
        private const String entryName = "Progress";

        public sColor pBackgroundColor;

        [Editor(typeof(e2skinner2.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(e2skinner2.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryName)]
        public String BackgroundColor
        {
            get { return pBackgroundColor.pName; }
            set { 
                pBackgroundColor = (sColor)cDataBase.pColors.get(value);

                if (pBackgroundColor != (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["Background"])
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

        public sAttributeProgress(sAttribute parent, XmlNode node)
            : base(parent, node)
        {

            if (node.Attributes["backgroundColor"] != null)
                pBackgroundColor = (sColor)cDataBase.pColors.get(node.Attributes["backgroundColor"].Value);
            else
                pBackgroundColor = (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["Background"];
        }
    }
}
