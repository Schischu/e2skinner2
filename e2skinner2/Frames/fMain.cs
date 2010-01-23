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

                        pDesigner.drawBackground();

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
            if (treeView1.SelectedNode != null)
            {
                //Property changed, so save it to xml, and repaint screen

                //Actually, this only syncs the name of the element with treeview, no saveing is done here
                pXmlHandler.XmlSyncTreeChilds(treeView1.SelectedNode.GetHashCode(), treeView1.SelectedNode);

                pDesigner.sort();
                pictureBox1.Invalidate();

                refreshEditor();
            }
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

        private Int32 _StartX = 0;
        private Int32 _StartY = 0;
        private Boolean mouseDown = false;
        //sAttribute _Attr = null;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("pictureBox1_MouseDown");

            _StartX = ((MouseEventArgs)e).X;
            _StartY = ((MouseEventArgs)e).Y;

            sGraphicElement elem = pDesigner.getElement((uint)_StartX, (uint)_StartY);
            if (elem != null)
            {
                sAttribute _Attr = elem.pAttr;
                if (_Attr != null)
                {
                    treeView1.SelectedNode = pXmlHandler.XmlGetTreeNode(_Attr.myNode);
                }
            }

            tabControl1.Focus();
            

            mouseDown = true;
            this.Cursor = Cursors.SizeAll;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //System.Console.WriteLine("pictureBox1_MouseMove");
            Int32 curX = ((MouseEventArgs)e).X;
            Int32 curY = ((MouseEventArgs)e).Y;
            if (mouseDown)
            {
                if (propertyGrid1.SelectedObject != null)
                {
                    //Console.WriteLine(_Attr.pRelativX + "+(" + curX + "-" + _StartX + ")");
                    if (curX != _StartX || curY != _StartY)
                    {
                        sAttribute _Attr = (sAttribute)propertyGrid1.SelectedObject;
                        Int32 posX = (Int32)(_Attr.pRelativX + (curX - _StartX));
                        Int32 posY = (Int32)(_Attr.pRelativY + (curY - _StartY));
                        if (posX < 0) posX = 0;
                        if (posY < 0) posY = 0;
                        sAttribute.Position pos = new sAttribute.Position();

                        pos.X = ((UInt32)posX).ToString();
                        pos.Y = ((UInt32)posY).ToString();

                        _Attr.Relativ = pos;

                        propertyGrid1.Refresh();
                        pictureBox1.Invalidate();
                    }
                }
            }
            _StartX = curX;
            _StartY = curY;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("pictureBox1_MouseUp");
            if (mouseDown)
            {
                //propertyGrid1_PropertyValueChanged(null, null);
            }
            mouseDown = false;
            this.Cursor = Cursors.Default;
        }

        private bool isCTRL(KeyEventArgs e)
        {
            return e.Control;
        }

        private bool isUP(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Up;
        }

        private bool isDOWN(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Down;
        }

        private bool isLEFT(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Left;
        }

        private bool isRIGHT(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Right;
        }

        private bool isCURSOR(KeyEventArgs e)
        {
            return (isUP(e) || isDOWN(e) || isLEFT(e) || isRIGHT(e));
        }

        private bool isPLUS(KeyEventArgs e)
        {
            return ((int)e.KeyCode) == 107;
        }

        private bool isMINUS(KeyEventArgs e)
        {
            return ((int)e.KeyCode) == 109;
        }

        private bool isF11(KeyEventArgs e)
        {
            return e.KeyCode == Keys.F11;
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            // If CTRL pressed, use margin 1, else margin 5
            //Console.WriteLine(e.Control.ToString());
            if (isCURSOR(e))
            {
                int marging = 5;

                if (isCTRL(e))
                    marging = 1;

                if (propertyGrid1.SelectedObject != null)
                {
                    int x = isLEFT(e) ? -marging : (isRIGHT(e) ? +marging : 0);
                    int y = isUP(e)   ? -marging : (isDOWN(e)  ? +marging : 0);

                    sAttribute _Attr = (sAttribute)propertyGrid1.SelectedObject;
                    Int32 posX = (Int32)(_Attr.pRelativX + x);
                    Int32 posY = (Int32)(_Attr.pRelativY + y);
                    if (posX < 0) posX = 0;
                    if (posY < 0) posY = 0;

                    sAttribute.Position pos = new sAttribute.Position();

                    pos.X = ((UInt32)posX).ToString();
                    pos.Y = ((UInt32)posY).ToString();

                    _Attr.Relativ = pos;

                    propertyGrid1.Refresh();
                    pictureBox1.Invalidate();
                }


                e.Handled = true;
            }
            else if (isPLUS(e))
            {
                //pDesigner.zoomIn();
                //pictureBox1.Scale(new SizeF((float)0.5, (float)0.5));
                //
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Invalidate();
            }
            else if (isMINUS(e))
            {
                pDesigner.zoomOut();
                pictureBox1.Invalidate();
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            this.keyCaptureNotifyButton.Image = global::e2skinner2.Properties.Resources.Lock_icon;
        }

        private void tabControl1_Leave(object sender, EventArgs e)
        {
            this.keyCaptureNotifyButton.Image = global::e2skinner2.Properties.Resources.UnLock_icon;
        }

        private bool _bFullScreenMode = false;
        private int x, y, w, h, h_ms1;

        private void toggleFullscreen()
        {
            if (_bFullScreenMode == false)
            {
                x = this.Left;
                y = this.Top;
                w = this.Width;
                h = this.Height;
                h_ms1 = menuStrip1.Height;

                this.FormBorderStyle = FormBorderStyle.None;
                //menuStrip1.Hide();
                //toolStripMain.Hide();

                menuStrip1.Visible = false;
                toolStripMain.Visible = false;

                //menuStrip1.Height = 0;
                //menuStrip1.Size = new Size(menuStrip1.Width, 0);
                this.Left = 0;
                this.Top = 0;
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height;
                
                _bFullScreenMode = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                
                toolStripMain.Visible = true;
                menuStrip1.Visible = true;

                //menuStrip1.Height = h_ms1;

                this.Left = x;
                this.Top = y;
                this.Width = w;
                this.Height = h;

                _bFullScreenMode = false;
            }
        }

        private void fMain_KeyUp(object sender, KeyEventArgs e)
        {
            if(isF11(e))
                toggleFullscreen();
        }

        private void fMain_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("enable_alpha", btnFading.Checked);

            pictureBox1.Invalidate();
        }
    }
}
