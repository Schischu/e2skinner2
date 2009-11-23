using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Collections;

namespace e2skinner2.Logic
{
    static class cProperties
    {
        static private Hashtable pDefaultTable = new Hashtable();
        static private Hashtable pTable = new Hashtable();

        static cProperties()
        {
            pDefaultTable.Add("fading", (bool)true);
            pDefaultTable.Add("label_test", (bool)true);
            pDefaultTable.Add("skinned", (bool)true);
            pDefaultTable.Add("skinned_screen", (bool)true);
            pDefaultTable.Add("skinned_label", (bool)true);
            pDefaultTable.Add("skinned_pixmap", (bool)true);
            pDefaultTable.Add("skinned_widget", (bool)true);
            pDefaultTable.Add("path_skin_xml", (string)"C:\\skin.xml");
            pDefaultTable.Add("path_skin", (string)"C:\\");
            pDefaultTable.Add("path", (string)"C:\\");
            pDefaultTable.Add("enable_alpha", (bool)false);
            pDefaultTable.Add("path_fonts", (string)"E:\\Visual Studio 2008\\e2skinner2\\fonts");
        }

        static public void saveFile()
        {
        }



        static public void setProperty(String name, String value)
        {
            if (pTable[name] == null)
                pTable.Add(name, value);
            else
                pTable[name] = value;
        }

        static public void setProperty(String name, bool value)
        {
            if (pTable[name] == null)
                pTable.Add(name, value);
            else
                pTable[name] = value;
        }


        static public String getProperty(String name)
        {
            String value = "";
            if (pTable[name] == null)
            {
                if (pDefaultTable[name] != null)
                {
                    value = pDefaultTable[name].ToString();
                }
                else
                    value = "0";
                setProperty(name, value);
            }
            else
            {
                value = pTable[name].ToString();
            }
            return value;
        }

        static public bool getPropertyBool(String name)
        {
            String value = "";
            if (pTable[name] == null)
            {
                if (pDefaultTable[name] != null)
                {
                    value = pDefaultTable[name].ToString();
                } else
                    value = true.ToString();
                setProperty(name, value);
            }
            else
            {
                value = pTable[name].ToString();
            }
            return Convert.ToBoolean(value);
        }

        static public Hashtable getProperties()
        {
            return pTable;
        }

    }
}
