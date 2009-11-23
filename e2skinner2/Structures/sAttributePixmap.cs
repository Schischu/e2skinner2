using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using e2skinner2.Logic;
using System.Xml;
using System.IO;
using System.ComponentModel;



namespace e2skinner2.Structures
{
    class sAttributePixmap : sAttribute
    {
        private const String entryName = "Pixmap";

        public String pPixmapName;
        public Size pPixmap;
        //public Image pPixmap = null;


        public cProperty.eAlphatest pAlphatest = cProperty.eAlphatest.off;

        [CategoryAttribute(entryName)]
        public String Path
        {
            get { return pPixmapName; }
            set {
                pPixmapName = value;
                if (pPixmapName.Length > 0)
                {
                    if (myNode.Attributes["pixmap"] != null)
                        myNode.Attributes["pixmap"].Value = pPixmapName;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("pixmap"));
                        myNode.Attributes["pixmap"].Value = pPixmapName;
                    }
                }
                else
                    if (myNode.Attributes["pixmap"] != null)
                        myNode.Attributes.RemoveNamedItem("pixmap");
            }
        }

        [CategoryAttribute(entryName),
        ReadOnlyAttribute(true)]
        public Size Resolution
        {
            get { return new Size(pPixmap.Width, pPixmap.Height); }
        }

        [TypeConverter(typeof(cProperty.AlphatestConverter)),
        CategoryAttribute(entryName)]
        public String Alphatest
        {
            get { return pAlphatest.ToString(); }
            set
            {
                if (value == cProperty.eAlphatest.on.ToString()) pAlphatest = cProperty.eAlphatest.on;
                else if (value == cProperty.eAlphatest.blend.ToString()) pAlphatest = cProperty.eAlphatest.blend;
                else pAlphatest = cProperty.eAlphatest.off;

                if (pAlphatest == cProperty.eAlphatest.on) myNode.Attributes["alphatest"].Value = "on";
                else if (pAlphatest == cProperty.eAlphatest.blend) myNode.Attributes["alphatest"].Value = "blend";
                else myNode.Attributes["alphatest"].Value = "off";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>

        ~sAttributePixmap()
        {
            //if (pPixmap != null) pPixmap.Dispose();
        }

        public sAttributePixmap(sAttribute parent, XmlNode node)
            : base(parent, node)
        {

            if (node.Attributes["pixmap"] != null)
            {
                pPixmapName = node.Attributes["pixmap"].Value;
                try
                {
                    Image pixmap = Image.FromFile(cDataBase.getPath(pPixmapName));
                    pPixmap = pixmap.Size;
                    pixmap.Dispose();
                }
                catch (FileNotFoundException e)
                {
                    pPixmap = new Size(0,0);
                }
            }

            if (node.Attributes["alphatest"] != null)
                pAlphatest = node.Attributes["alphatest"].Value.ToLower() == "on" ? cProperty.eAlphatest.on :
                    node.Attributes["alphatest"].Value.ToLower() == "blend" ? cProperty.eAlphatest.blend :
                    cProperty.eAlphatest.off;
        }
    }
}
