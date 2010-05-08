using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Drawing;
using e2skinner2.Logic;

namespace e2skinner2.Structures
{
    class sAttributeWidget : sAttribute
    {
        private const String entryName = "2 Widget";

        public String pSource;
        public String pRender;

        public sAttributeLabel pLabel;
        public sAttributePixmap pPixmap;
        public sAttributeProgress pProgress;
        public sAttributeListbox pListbox;

        /*//Pixmap
        public String pPixmapPath;

        //Label
        public String pText;
        public sFont pFont;
        public float pFontSize;
        public sColor pBackgroundColor; //background is used for the shadow if wanted, or posily if not transparent is selected !!!!
        public sColor pForegroundColor;
        public enum eVAlign { Top, Center, Bottom };
        public enum eHAlign { Left, Center, Right };
        public eVAlign pValign = eVAlign.Center;
        public eHAlign pHalign = eHAlign.Left;
        public bool pNoWrap = true;*/

        [CategoryAttribute(entryName),
        DefaultValue("(none)")]
        public String Source
        {
            get { return pSource; }
            set { 
                pSource = value;
                if (pSource.Length > 0)
                {
                    if (myNode.Attributes["source"] != null)
                        myNode.Attributes["source"].Value = pSource;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("source"));
                        myNode.Attributes["source"].Value = pSource;
                    }
                }
                else
                    if (myNode.Attributes["source"] != null)
                        myNode.Attributes.RemoveNamedItem("source");
            }
        }

        [TypeConverter(typeof(RenderConverter)),
        CategoryAttribute(entryName)]
        public String Render
        {
            get { return pRender; }
            set { 
                pRender = value;
                if (pRender.Length > 0)
                {
                    if (myNode.Attributes["render"] != null)
                        myNode.Attributes["render"].Value = pRender;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("render"));
                        myNode.Attributes["render"].Value = pRender;
                    }
                }
                else
                    if (myNode.Attributes["render"] != null)
                        myNode.Attributes.RemoveNamedItem("render");
            }
        }

        //#####################################################################
        //################# LABEL #############################################
        private const String entryNameLabel = "3 Label";
        [CategoryAttribute(entryNameLabel)]
        public String Text
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.Text;
                else return "(none)";
            }
            set
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel")
                {
                    pLabel.Text = value;

                    /*if (pLabel.pText.Length > 0)
                    {
                        if (myNode.Attributes["text"] != null)
                            myNode.Attributes["text"].Value = pLabel.pText;
                        else
                        {
                            myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("text"));
                            myNode.Attributes["text"].Value = pLabel.pText;
                        }
                    }
                    else
                        if (myNode.Attributes["text"] != null)
                            myNode.Attributes.RemoveNamedItem("text");*/
                }
            }
        }

        [CategoryAttribute(entryNameLabel),
        ReadOnlyAttribute(true)]
        public String PreviewText
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.pPreviewText;
                else return "(none)";
            }
        }

        [CategoryAttribute(entryNameLabel),
        ReadOnlyAttribute(true)]
        public String Font
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.Font;
                else return "(none)";
            }
        }

        [CategoryAttribute(entryNameLabel)]
        public float FontSize
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.FontSize;
                else return 0;
            }
            set
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel")
                {
                    pLabel.FontSize = value;

                    /*if (myNode.Attributes["font"] != null)
                        myNode.Attributes["font"].Value = pLabel.pFont.Name + ", " + pLabel.pFontSize;
                    else
                    {
                        myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("font"));
                        myNode.Attributes["font"].Value = pLabel.pFont.Name + ", " + pLabel.pFontSize;
                    }*/
                }
            }
        }

        [Editor(typeof(e2skinner2.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(e2skinner2.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryNameLabel)]
        public String ForegroundColor
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.ForegroundColor;
                else return "(none)";
            }
            set
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel")
                {
                    pLabel.ForegroundColor = value;

                    /*if (pLabel.pForegroundColor != (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["LabelForeground"])
                    {
                        if (myNode.Attributes["foregroundColor"] != null)
                            myNode.Attributes["foregroundColor"].Value = pLabel.pForegroundColor.pName;
                        else
                        {
                            myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("foregroundColor"));
                            myNode.Attributes["foregroundColor"].Value = pLabel.pForegroundColor.pName;
                        }
                    }
                    else
                        if (myNode.Attributes["foregroundColor"] != null)
                            myNode.Attributes.RemoveNamedItem("foregroundColor");*/
                }
            }
        }

        [Editor(typeof(e2skinner2.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(e2skinner2.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryNameLabel)]
        public String BackgroundColor
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.BackgroundColor;
                else return "(none)";
            }
            set
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel")
                {
                    pLabel.BackgroundColor = value;

                    /*if (pLabel.BackgroundColor != (sColor)((sWindowStyle)cDataBase.pWindowstyles.get()).pColors["LabelBackground"])
                    {
                        if (myNode.Attributes["backgroundColor"] != null)
                            myNode.Attributes["backgroundColor"].Value = pLabel.BackgroundColor.pName;
                        else
                        {
                            myNode.Attributes.Append(myNode.OwnerDocument.CreateAttribute("backgroundColor"));
                            myNode.Attributes["backgroundColor"].Value = pLabel.BackgroundColor.pName;
                        }
                    }
                    else
                        if (myNode.Attributes["backgroundColor"] != null)
                            myNode.Attributes.RemoveNamedItem("backgroundColor");*/
                }
            }
        }


        [TypeConverter(typeof(cProperty.VAlignConverter)),
        CategoryAttribute(entryNameLabel)]
        public String Valign
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.Valign;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") pLabel.Valign = value; }
        }

        [TypeConverter(typeof(cProperty.HAlignConverter)),
        CategoryAttribute(entryNameLabel)]
        public String Halign
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.Halign;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") pLabel.Halign = value; }
        }

        [CategoryAttribute(entryNameLabel)]
        public bool noWrap
        {
            get
            {
                if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") return pLabel.pNoWrap;
                else return false;
            }
            set { if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel") pLabel.noWrap = value; }
        }

        //######################################################################
        //################# PIXMAP #############################################
        private const String entryNamePixmap = "4 Pixmap";
        [CategoryAttribute(entryNamePixmap)]
        public String Path
        {
            get
            {
                if (pRender.ToLower() == "pixmap") return pPixmap.Path;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "pixmap") pPixmap.Path = value; }
        }

        [CategoryAttribute(entryNamePixmap),
        ReadOnlyAttribute(true)]
        public Size Resolution
        {
            get
            {
                if (pRender.ToLower() == "pixmap") return pPixmap.Resolution;
                else return new Size();
            }
        }

        [TypeConverter(typeof(cProperty.AlphatestConverter)),
        CategoryAttribute(entryNamePixmap)]
        public String Alphatest
        {
            get
            {
                if (pRender.ToLower() == "pixmap") return pPixmap.Alphatest;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "pixmap") pPixmap.Alphatest = value; }
        }

        //######################################################################
        //################# PIXMAP #############################################
        private const String entryNameProgress = "6 Progress";

        [Editor(typeof(e2skinner2.Structures.cProperty.GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(e2skinner2.Structures.cProperty.sColorConverter)),
        CategoryAttribute(entryNameProgress)]
        public String ProgressColor
        {
            get
            {
                if (pRender.ToLower() == "progress") return pProgress.BackgroundColor;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "progress") pProgress.BackgroundColor = value; }
        }

        //######################################################################
        //################# LISTBOX ############################################
        private const String entryNameListbox = "7 Listbox";

        [TypeConverter(typeof(cProperty.ScrollbarModeConverter)),
        CategoryAttribute(entryNameListbox)]
        public String ScrollbarMode
        {
            get
            {
                if (pRender.ToLower() == "listbox") return pListbox.ScrollbarMode;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "listbox") pListbox.ScrollbarMode = value; }
        }

        [CategoryAttribute(entryNameListbox)]
        public String BackgroundPixmap
        {
            get
            {
                if (pRender.ToLower() == "listbox") return pListbox.BackgroundPixmap;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "listbox") pListbox.BackgroundPixmap = value; }
        }

        [CategoryAttribute(entryNameListbox)]
        public String SelectionPixmap
        {
            get
            {
                if (pRender.ToLower() == "listbox") return pListbox.SelectionPixmap;
                else return "(none)";
            }
            set { if (pRender.ToLower() == "listbox") pListbox.SelectionPixmap = value; }
        }


        //######################################################################
        //################# WIDGET #############################################

        public class RenderConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{   "Canvas",
                                                                    "FixedLabel",
                                                                    "FrontpanelLed",
                                                                    "Label",
                                                                    "Listbox",
                                                                    "Picon",
                                                                    "Pig",
                                                                    "Pixmap",
                                                                    "PositionGauge",
                                                                    "Progress"});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }

        public sAttributeWidget(sAttribute parent, XmlNode node)
            : base(parent, node)
        {
            if (node.Attributes["source"] != null)
                pSource = node.Attributes["source"].Value;

            if (node.Attributes["render"] != null)
                pRender = node.Attributes["render"].Value;

            if (pRender == null)
            {
                if (node.Attributes["pixmap"] != null)
                    pRender = "Pixmap";
                else if (pName == "menu" || pName == "list" || pName.EndsWith("list")) //depreceated
                    pRender = "Listbox";
                else if (pName == "PositionGauge") //depreceated
                    pRender = "PositionGauge";
                else if (node.Attributes["pointer"] != null)
                    pRender = "PositionGauge";
                else
                    pRender = "Label";
            }

            if (pRender.ToLower() == "label" || pRender.ToLower() == "fixedlabel")
            {
                pLabel = new sAttributeLabel(parent, node);
            }
            else if (pRender.ToLower() == "pixmap")
            {
                pPixmap = new sAttributePixmap(parent, node);
            }
            else if (pRender.ToLower() == "progress")
            {
                pProgress = new sAttributeProgress(parent, node);
            }
            else if (pRender.ToLower() == "listbox")
            {
                pListbox = new sAttributeListbox(parent, node);
            }

            if (pLabel != null)
            {
                //if (pLabel.pText == null || pLabel.pText.Length > 0)
                //    pLabel.pPreviewText = cPreviewText.getText(parent.Name, Name);
            }

            if (node.HasChildNodes)
            {
                foreach (XmlNode nodeConverter in node.ChildNodes)
                {
                   // XmlNode nodeConverter = node.ChildNodes[0];

                    if (nodeConverter.Attributes != null)
                    {
                        String type = nodeConverter.Attributes["type"].Value;
                        String parameter = nodeConverter.InnerText;

                        String text = cConverter.getText(pSource, type, parameter);

                        if (pLabel != null)
                        {
                            if (pLabel.pText == null || pLabel.pText.Length > 0)
                                pLabel.pPreviewText = text;

                            if (text == "MAGIC#TRUE") 
                            {
                                //pLabel.pText = "";
                            }
                            else if (text == "MAGIC#FALSE") 
                            {
                                pLabel.pPreviewText = "";
                            }
                        }
                        else if (pPixmap != null)
                        {
                            if (text == "MAGIC#TRUE") 
                            {
                            //pLabel.pText = "";
                            }
                            else if (text == "MAGIC#FALSE")
                            {
                                pPixmap.pPixmap = new Size(0, 0);
                                pPixmap.pHide = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
