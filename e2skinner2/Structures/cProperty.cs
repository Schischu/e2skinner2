using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using e2skinner2.Logic;
using System.Drawing;
using System.Collections;

namespace e2skinner2.Structures
{
    class cProperty
    {
        public class sColorConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                sColor[] colors = (sColor[])cDataBase.pColors.getArray();
                ArrayList list = new ArrayList();
                foreach (sColor color in colors)
                    list.Add(color.pName);
                return new StandardValuesCollection(list.ToArray());
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }

        public class GradeEditor : UITypeEditor
        {
            public override bool GetPaintValueSupported(
                  ITypeDescriptorContext context)
            {
                // let the property browser know we'd like
                // to do custom painting.
                return true;
            }

            public override void PaintValue(PaintValueEventArgs pe)
            {
                // choose the right bitmap based on the value
                String g = (String)pe.Value;
                sColor color = (sColor)cDataBase.pColors.get(g);

                // draw that bitmap onto the surface provided.
                if(color != null)
                    pe.Graphics.FillRectangle(new SolidBrush(color.pColor), pe.Bounds);
                else
                    pe.Graphics.DrawRectangle(new Pen(Color.Black, 1.0F), pe.Bounds);
            }
        }

        public enum eAlphatest { on, off, blend };
        public class AlphatestConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eAlphatest.off.ToString(), 
                                                     eAlphatest.on.ToString(), 
                                                     eAlphatest.blend.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }

        public enum eVAlign { Top, Center, Bottom };
        public enum eHAlign { Left, Center, Right };
        public class VAlignConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eVAlign.Top.ToString(), 
                                                     eVAlign.Center.ToString(), 
                                                     eVAlign.Bottom.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }
        public class HAlignConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eHAlign.Left.ToString(), 
                                                     eHAlign.Center.ToString(), 
                                                     eHAlign.Right.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }

        public enum eScrollbarMode { showOnDemand, showAlways, showNever };
        public class ScrollbarModeConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(
                           ITypeDescriptorContext context)
            {
                return true;
            }

            public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[]{eScrollbarMode.showNever.ToString(), 
                                                     eScrollbarMode.showOnDemand.ToString(), 
                                                     eScrollbarMode.showAlways.ToString()});
            }

            public override bool GetStandardValuesExclusive(
                           ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }
}
