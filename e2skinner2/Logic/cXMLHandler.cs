using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections;

using e2skinner2.Structures;
using System.IO;

namespace e2skinner2.Logic
{
    public class cXMLHandler
    {
        public XmlDocument xmlDocument;
        ArrayList ElementList = null;
        int rootNode = 0;

        /// <summary>
        /// Initialisiert eine neue Instanz der MultiClipboard Klasse.
        /// </summary>
        public cXMLHandler()
        {
        }

        /// <summary>
        /// Eine vorher Exportierte Xml Datei wieder in ein TreeView importieren
        /// </summary>
        /// <param name="path">Der Quellpfad der Xml Datei</param>
        /// <param name="treeView">Ein TreeView in dem der Inhalt der Xml Datei wieder angezeigt werden soll</param>
        /// <exception cref="FileNotFoundException">gibt an das die Datei nicht gefunden werden konnte</exception>
        public void XmlToTreeView(String path, TreeView treeView) {
            xmlDocument = new XmlDocument();
            
            xmlDocument.Load(path);
            treeView.Nodes.Clear();
            ElementList = new ArrayList();

            TreeNode treeNode;
            treeNode = new TreeNode("/");
            treeView.Nodes.Add(treeNode);

            rootNode = treeNode.GetHashCode();
            sElementList element = new sElementList(rootNode, 0, null, xmlDocument.DocumentElement.ParentNode);
            ElementList.Add(element);
            

            XmlRekursivImport(treeNode.Nodes, xmlDocument.DocumentElement.ChildNodes);
        }

        public void XmlToFile(String path)
        {
            xmlDocument.Save(path);
        }

        private String XmlElementStringLookup(String element)
        {
            String stmp = "";
            switch (element)
            {
                case "#comment":
                    stmp = "Comment";
                    break;
                //case "screen":
                //    stmp = "name";
                //    break;
                case "eLabel":
                    stmp = "text";
                    break;
                case "ePixmap":
                    stmp = "pixmap";
                    break;
                //case "widget":
                //    stmp = "name";
                //    break;
                default:
                    break;
            }

            return stmp;
        }

        private void XmlRekursivImport(TreeNodeCollection elem, XmlNodeList xmlNodeList) {
            TreeNode treeNode;
            foreach (XmlNode myXmlNode in xmlNodeList) {
                //if (myXmlNode.Attributes != null)
                {
                    if (myXmlNode.Name == "output" || myXmlNode.Name == "colors" || myXmlNode.Name == "fonts" || myXmlNode.Name == "windowstyle")
                        continue;

                    String name = myXmlNode.Name;

                    if (myXmlNode.Attributes != null && myXmlNode.Attributes["name"] != null)
                        name += " - " + myXmlNode.Attributes["name"].Value;

                    String ext = XmlElementStringLookup(myXmlNode.Name);
                    if (ext.Length > 0)
                    {
                        if (myXmlNode.Attributes != null && myXmlNode.Attributes[ext] != null)
                            name += " : " + myXmlNode.Attributes[ext].Value;
                        else
                            name += " " + myXmlNode.Value;
                    }
                    treeNode = new TreeNode(name/*Attributes["value"].Value*/);

                    if (myXmlNode.ChildNodes.Count > 0)
                    {
                        XmlRekursivImport(treeNode.Nodes, myXmlNode.ChildNodes);
                    }
                    elem.Add(treeNode);
                    sElementList element = new sElementList(treeNode.GetHashCode(), treeNode.Parent.GetHashCode(), treeNode, myXmlNode);
                    ElementList.Add(element);
                }
            }
        }

        public void XmlSyncTreeChilds(int hash, TreeNode elem)
        {
            foreach (sElementList temp in ElementList)
            {
                if (temp.Handle == hash)
                {
                    String name = temp.Node.Name;

                    if (temp.Node.Attributes != null && temp.Node.Attributes["name"] != null)
                        name += " - " + temp.Node.Attributes["name"].Value;

                    String ext = XmlElementStringLookup(temp.Node.Name);
                    if (ext.Length > 0)
                    {
                        if (temp.Node.Attributes != null && temp.Node.Attributes[ext] != null)
                            name += " : " + temp.Node.Attributes[ext].Value;
                        else
                            name += " " + temp.Node.Value;
                    }
                    temp.TreeNode.Name = name;

                    //if (treenode.GetHashCode() == temp.Handle)
                            elem.Text = name;
                }
            }
        }

        public TreeNode XmlSyncAddTreeChild(int hash, TreeNode elem, XmlNode node)
        {
            TreeNode treeNode = new TreeNode("new"/*Attributes["value"].Value*/);
            elem.Nodes.Add(treeNode);

            sElementList element = new sElementList(treeNode.GetHashCode(), treeNode.Parent.GetHashCode(), treeNode, node);
            ElementList.Add(element);

            return treeNode;
        }

        public XmlNode XmlCreateNode(XmlNode node, String[] attributes)
        {
            XmlNode xmlNode = null;
            xmlNode = xmlDocument.CreateElement(attributes[0]);

            for (int i = 1; i < attributes.Length; i += 2)
            {
                xmlNode.Attributes.Append(xmlDocument.CreateAttribute(attributes[i]));
                xmlNode.Attributes[attributes[i]].Value = attributes[i+1];
            }

            if (node != null)
                node.AppendChild(xmlNode);

            return xmlNode;
        }

        public XmlNode XmlGetNode(int hash)
        {
            foreach (sElementList temp in ElementList)
            {
                if (temp.Handle == hash)
                {
                    return temp.Node;
                }
            }
            return null;
        }

        public int XmlGetHash(XmlNode node )
        {
            foreach (sElementList temp in ElementList)
            {
                if (temp.Node == node)
                {
                    return temp.Handle;
                }
            }
            return 0;
        }

        public TreeNode XmlGetTreeNode(XmlNode node)
        {
            foreach (sElementList temp in ElementList)
            {
                if (temp.Node == node)
                {
                    return temp.TreeNode;
                }
            }
            return null;
        }

        public void XmlReplaceNode(int hash, String node)
        {
            XmlTextReader xmlReader = new XmlTextReader(new StringReader(node));
            foreach (sElementList temp in ElementList)
            {
                if (temp.Handle == hash)
                {
                    temp.Node = xmlDocument.ReadNode(xmlReader);
                    break;
                }
            }
        }

        public XmlNode[] XmlGetChildNode(int hash)
        {
            ArrayList list = new ArrayList();
            foreach (sElementList temp in ElementList)
            {
                if (temp.ParentHandle == hash)
                {
                    list.Add(temp.Node);
                }
            }
            return (XmlNode[])list.ToArray(typeof(XmlNode));
        }

        public int XmlGetParentHandle(int hash)
        {
            foreach (sElementList temp in ElementList)
            {
                if (temp.Handle == hash)
                {
                    return temp.ParentHandle;
                }
            }
            return 0;
        }

        public XmlNode XmlGetRootNodeElement(String[] name)
        {
            foreach (sElementList temp in ElementList)
            {
                if (temp.Handle == rootNode)
                {
                    if (temp.Node.ChildNodes.Count > 0)
                    {
                        return XmlGetSearchChilds(temp.Node.ChildNodes, name);
                    }
                }
            }
            return null;
        }

        private XmlNode XmlGetSearchChilds(XmlNodeList node, String[] name)
        {
            foreach (XmlNode myXmlNode in node)
            {
                if (myXmlNode.Name == name[0])
                {
                    if (name.Length > 1)
                    {
                        String[] path = new String[name.Length - 1];
                        for (int i = 0; i < name.Length - 1; i++)
                            path[i] = name[i + 1];

                        return XmlGetSearchChilds(myXmlNode.ChildNodes, path);
                    }
                    else
                    {
                        return myXmlNode;
                    }
                }
            }
            return null;
        }
    }
}
