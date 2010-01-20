using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using e2skinner2.Logic;
using e2skinner2.Structures;
using System.Reflection;

namespace e2skinner2.Frames
{
    public partial class fMain : Form
    {
        private cXMLHandler pXmlHandler = null;
        private cDesigner pDesigner = null;

        public fMain()
        {
            InitializeComponent();

            textBoxEditor2.ConfigurationManager.Language = "xml";

            this.Text = String.Format("{0} v{1}", ((AssemblyProductAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product, Assembly.GetExecutingAssembly().GetName().Version.ToString());

            pDesigner = new cDesigner(pictureBox1.CreateGraphics());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                cProperties.setProperty("path_skin_xml", openFileDialog1.FileName);
                System.Console.Write("{0}\n", openFileDialog1.FileName);

                String path = openFileDialog1.FileName;
                
                open(path);
             }
        }

        public void open(String path)
        {
            if (path.LastIndexOf("/") > 0) //linux
                path = path.Substring(0, path.LastIndexOf("/"));
            else
                path = path.Substring(0, path.LastIndexOf("\\"));

            cProperties.setProperty("path_skin", path);

            if (path.LastIndexOf("/") > 0) //linux
                path = path.Substring(0, path.LastIndexOf("/"));
            else
                path = path.Substring(0, path.LastIndexOf("\\"));

            cProperties.setProperty("path", path);
            cProperties.setProperty("path_fonts", path + "/fonts");

            pXmlHandler = new cXMLHandler();
            //treeview TO Xml
            pXmlHandler.XmlToTreeView(openFileDialog1.FileName, treeView1);
            cDataBase.init(pXmlHandler);

            treeView1.GetNodeAt(0, 0).Expand();

            pDesigner.drawFrame();
            pictureBox1.Invalidate();

            panelDesignerInner.AutoScrollMinSize = new Size((int)cDataBase.pResolution.getResolution().Xres + 100, (int)cDataBase.pResolution.getResolution().Yres + 100);

            MiOpen.Enabled = false;

            MiSave.Enabled = true;
            MiSaveAs.Enabled = true;
            MiClose.Enabled = true;
            MiResolution.Enabled = true;
            MiColors.Enabled = true;
            MiFonts.Enabled = true;
            MiWindowStyles.Enabled = true;
            btnSave.Enabled = true;
            btnSaveEditor.Enabled = true;

            MiAddLabel.Enabled = true;
            MiAddPixmap.Enabled = true;
            MiAddWidget.Enabled = true;
        }

        public void close()
        {
            pXmlHandler = null;
            pDesigner.clear();
            cDataBase.clear();
            treeView1.Nodes.Clear();
            treeView1.Invalidate();
            //textBoxEditor.Clear();
            textBoxEditor2.Text = "";
            propertyGrid1.SelectedObject = null;

            pictureBox1.Invalidate();

            MiOpen.Enabled = true;

            MiSave.Enabled = false;
            MiSaveAs.Enabled = false;
            MiClose.Enabled = false;
            MiResolution.Enabled = false;
            MiColors.Enabled = false;
            MiFonts.Enabled = false;
            MiWindowStyles.Enabled = false;
            btnSave.Enabled = false;
            btnSaveEditor.Enabled = false;

            MiAddLabel.Enabled = false;
            MiAddPixmap.Enabled = false;
            MiAddWidget.Enabled = false;
        }

        public void reload()
        {
            save();
            close();
            open(cProperties.getProperty("path_skin_xml"));
        }

        public void save()
        {
            pXmlHandler.XmlToFile(cProperties.getProperty("path_skin_xml"));
        }

        public void saveAs(String name)
        {
            pXmlHandler.XmlToFile(name);
            cProperties.setProperty("path_skin_xml", name);
        }

        private void refreshEditor()
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                int hash = selectedNode.GetHashCode();
                XmlNode node = pXmlHandler.XmlGetNode(hash);
                {
                    //textBoxEditor.Clear();
                    String text = node.OuterXml;
                    text = FormatXml(node);
                    //textBoxEditor.AppendText(text);

                    //textBoxEditor.SelectionStart = 0;
                    //textBoxEditor.ScrollToCaret();

                    //textBoxEditor2.Clear();
                    textBoxEditor2.Text = text;

                    //textBoxEditor2.SelectionStart = 0;
                    //textBoxEditor2.ScrollToCaret();
                }
            }
        }

        private void refresh()
        {
            refreshEditor();

            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                int hash = selectedNode.GetHashCode();
                XmlNode node = pXmlHandler.XmlGetNode(hash);
                {
                    //Get Screen Node
                    XmlNode screenNode = node;
                    //As we could be selecting a sub Element, walk up
                    while (screenNode != null && screenNode.Name != "screen")
                    {
                        hash = pXmlHandler.XmlGetParentHandle(hash);
                        screenNode = pXmlHandler.XmlGetNode(hash);
                    }

                    pDesigner.clear();

                    if (screenNode != null)
                    {
                        //Draw Screen and its Elements
                        sAttribute subattr = null;
                        {
                            sAttribute attr = new sAttributeScreen(screenNode);
                            if (screenNode.Name == "screen")
                            {
                                //sAttribute subattr = new sAttributeScreen(node);
                                propertyGrid1.SelectedObject = attr;
                            }
                            pDesigner.draw(attr);

                            XmlNode[] nodes = pXmlHandler.XmlGetChildNode(hash);
                            foreach (XmlNode tmpnode in nodes)
                            {
                                 if (tmpnode.Name == "eLabel")
                                {
                                    subattr = new sAttributeLabel(attr, tmpnode);
                                    pDesigner.draw(subattr);
                                }
                                else if (tmpnode.Name == "ePixmap")
                                {
                                    subattr = new sAttributePixmap(attr, tmpnode);
                                    pDesigner.draw(subattr);
                                }
                                else if (tmpnode.Name == "widget")
                                {
                                    subattr = new sAttributeWidget(attr, tmpnode);
                                    pDesigner.draw(subattr);
                                }

                                if (tmpnode == node)
                                {
                                    propertyGrid1.SelectedObject = subattr;
                                    if(cProperties.getPropertyBool("fading"))
                                        pDesigner.drawFog((int)subattr.pAbsolutX, (int)subattr.pAbsolutY, (int)subattr.pWidth, (int)subattr.pHeight);
                                }
                            }
                        }
                          
                        pDesigner.drawFrame();
                    }

                    pDesigner.sort();
                    pictureBox1.Invalidate();
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            refresh();
        }

        private void resolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reload();
            fResolution ftmp = new fResolution();
            ftmp.setup(pXmlHandler);
            ftmp.ShowDialog();
            reload();
        }

        private string FormatXml(XmlNode sUnformattedXml)
        {
            //will hold formatted xml
            StringBuilder sb = new StringBuilder();

            //pumps the formatted xml into the StringBuilder above
            StringWriter sw = new StringWriter(sb);

            //does the formatting
            XmlTextWriter xtw = null;

            try
            {
                //point the xtw at the StringWriter
                xtw = new XmlTextWriter(sw);

                //we want the output formatted
                xtw.Formatting = Formatting.Indented;

                //get the dom to dump its contents into the xtw 
                //xd.WriteTo(xtw);
                sUnformattedXml.WriteTo(xtw);
            }
            finally
            {
                //clean up even if error
                if (xtw != null)
                    xtw.Close();
            }

            //return the formatted xml
            return sb.ToString();
        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fColors ftmp = new fColors();
            ftmp.setup(pXmlHandler);
            ftmp.ShowDialog();

            refresh();
        }

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fFonts ftmp = new fFonts();
            ftmp.setup(pXmlHandler);
            ftmp.ShowDialog();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pDesigner.paint(sender, e);
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //Property changed, so save it to xml, and repaint screen

            pXmlHandler.XmlSyncTreeChilds(treeView1.SelectedNode.GetHashCode(), treeView1.SelectedNode);
            //treeView1.Invalidate();

            pDesigner.sort();
            pictureBox1.Invalidate();
            refreshEditor();
        }

        private void btnSkinned_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("skinned_screen", btnSkinned.Checked);
            cProperties.setProperty("skinned_label", btnSkinned.Checked);
            cProperties.setProperty("skinned_pixmap", btnSkinned.Checked);
            cProperties.setProperty("skinned_widget", btnSkinned.Checked);

            btnSkinnedScreen.Checked = btnSkinned.Checked;
            btnSkinnedLabel.Checked = btnSkinned.Checked;
            btnSkinnedPixmap.Checked = btnSkinned.Checked;
            btnSkinnedWidget.Checked = btnSkinned.Checked;

            pictureBox1.Invalidate();
        }

        private void btnSkinnedScreen_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("skinned_screen", btnSkinnedScreen.Checked);

            if ((btnSkinnedScreen.Checked && btnSkinnedLabel.Checked && btnSkinnedPixmap.Checked && btnSkinnedWidget.Checked)
                || (!btnSkinnedScreen.Checked && !btnSkinnedLabel.Checked && !btnSkinnedPixmap.Checked && !btnSkinnedWidget.Checked))
                btnSkinned.Checked = btnSkinnedScreen.Checked;

            pictureBox1.Invalidate();
        }

        private void btnSkinnedLabel_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("skinned_label", btnSkinnedLabel.Checked);

            if ((btnSkinnedScreen.Checked && btnSkinnedLabel.Checked && btnSkinnedPixmap.Checked && btnSkinnedWidget.Checked)
                || (!btnSkinnedScreen.Checked && !btnSkinnedLabel.Checked && !btnSkinnedPixmap.Checked && !btnSkinnedWidget.Checked))
                btnSkinned.Checked = btnSkinnedScreen.Checked;

            pictureBox1.Invalidate();
        }

        private void btnSkinnedPixmap_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("skinned_pixmap", btnSkinnedPixmap.Checked);

            if ((btnSkinnedScreen.Checked && btnSkinnedLabel.Checked && btnSkinnedPixmap.Checked && btnSkinnedWidget.Checked)
                || (!btnSkinnedScreen.Checked && !btnSkinnedLabel.Checked && !btnSkinnedPixmap.Checked && !btnSkinnedWidget.Checked))
                btnSkinned.Checked = btnSkinnedScreen.Checked;

            pictureBox1.Invalidate();
        }

        private void btnSkinnedWidget_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("skinned_widget", btnSkinnedWidget.Checked);

            if ((btnSkinnedScreen.Checked && btnSkinnedLabel.Checked && btnSkinnedPixmap.Checked && btnSkinnedWidget.Checked)
                || (!btnSkinnedScreen.Checked && !btnSkinnedLabel.Checked && !btnSkinnedPixmap.Checked && !btnSkinnedWidget.Checked))
                btnSkinned.Checked = btnSkinnedScreen.Checked;

            pictureBox1.Invalidate();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void windowStylesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fWindowstyle ftmp = new fWindowstyle();
            ftmp.setup(pXmlHandler);
            ftmp.ShowDialog();
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            cProperties.saveFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveAs(saveFileDialog1.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        private void MiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            new fAbout().ShowDialog();
        }

        private void addLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addElement("eLabel");
        }

        private void addPixmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addElement("ePixmap");
        }

        private void widgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addElement("widget");
        }

        private void addElement(String type)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                int hash = selectedNode.GetHashCode();
                XmlNode node = pXmlHandler.XmlGetNode(hash);
                {
                    //Get Screen Node
                    XmlNode screenNode = node;
                    //As we could be selecting a sub Element, walk up
                    while (screenNode != null && screenNode.Name != "screen")
                    {
                        hash = pXmlHandler.XmlGetParentHandle(hash);
                        screenNode = pXmlHandler.XmlGetNode(hash);
                    }

                    String[] attributes = { type,
                                            "name",  "new " + type,
                                            "position",  "0, 0",
                                            "size",  "10, 10" };
                    TreeNode treenode = selectedNode;
                    if (!treenode.Text.StartsWith("screen"))
                        treenode = treenode.Parent;

                    treeView1.SelectedNode = pXmlHandler.XmlSyncAddTreeChild(treenode.GetHashCode(), treenode, pXmlHandler.XmlCreateNode(screenNode, attributes));
                    pXmlHandler.XmlSyncTreeChilds(treeView1.SelectedNode.GetHashCode(), treeView1.SelectedNode);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MiPreferences_Click(object sender, EventArgs e)
        {
            new fPreferences().Show();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveEditor_Click(object sender, EventArgs e)
        {
            int hash = treeView1.SelectedNode.GetHashCode();
            try
            {
                //pXmlHandler.XmlReplaceNode(hash,textBoxEditor.Text);

                pXmlHandler.XmlReplaceNode(hash, textBoxEditor2.Text); 

                refresh();
                propertyGrid1_PropertyValueChanged(null, null);
                toolStripLabel1.Text = "No Errors.";
            }
            catch (Exception ex)
            {
                toolStripLabel1.Text = ex.Message;
            }
        }



        private void btnFading_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("fading", btnFading.Checked);

            pictureBox1.Invalidate();
        }

        private void MiClose_Click(object sender, EventArgs e)
        {
            close();
        }

        int x = 0;
        int y = 0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("pictureBox1_Click");
            sGraphicElement pele = pDesigner.getElement((uint)x, (uint)y);
            if (pele != null)
            {
                pele.pX += (uint)(((MouseEventArgs)e).X - x);
                pele.pY += (uint)(((MouseEventArgs)e).Y - y);
                refresh();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("pictureBox1_MouseDown");

            x = ((MouseEventArgs)e).X;
            y = ((MouseEventArgs)e).Y;

            sGraphicElement ele = pDesigner.getElement((uint)((MouseEventArgs)e).X, (uint)((MouseEventArgs)e).Y);
            if (ele != null)
            {
                //toolStripLabel2.Text = ele.ToString();
                if (ele.pAttr != null)
                    treeView1.SelectedNode = pXmlHandler.XmlGetTreeNode(ele.pAttr.myNode);
            }


        }
    }
}
