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

            toolStripButton2.Checked = cProperties.getPropertyBool("enable_alpha"); ;

            if (Platform.sysPlatform != Platform.ePlatform.MONO)
                textBoxEditor2.ConfigurationManager.Language = "xml";

            this.Text = String.Format("{0} v{1}", ((AssemblyProductAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product, Assembly.GetExecutingAssembly().GetName().Version.ToString());

            pDesigner = new cDesigner(pictureBox1.CreateGraphics());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fOpen ftmp = new fOpen();
            //ftmp.setup(pXmlHandler);
            ftmp.ShowDialog();
            if (ftmp.Status == fOpen.eStatus.OK)
            {
                cProperties.setProperty("path_skin_xml", ftmp.SkinName + "/skin.xml");
                open(ftmp.SkinName);
            }
            /*
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                cProperties.setProperty("path_skin_xml", openFileDialog1.FileName);
                System.Console.Write("{0}\n", openFileDialog1.FileName);

                String path = openFileDialog1.FileName;
                
                open(path);
             }*/
        }

        public void open(String path)
        {
            cProperties.setProperty("path_skin", path);
            cProperties.setProperty("path", "./skins");
            cProperties.setProperty("path_fonts", "./fonts");

            pXmlHandler = new cXMLHandler();
            //treeview TO Xml
            pXmlHandler.XmlToTreeView(cProperties.getProperty("path") + "/" + cProperties.getProperty("path_skin_xml"), treeView1);
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

            if (Platform.sysPlatform == Platform.ePlatform.MONO)
                textBoxEditor.Clear();
            else
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
                    String text = node.OuterXml;
                    text = FormatXml(node);

                    if (Platform.sysPlatform == Platform.ePlatform.MONO)
                    {
                        textBoxEditor.Clear();
                        textBoxEditor.AppendText(text);

                        textBoxEditor.SelectionStart = 0;
                        textBoxEditor.ScrollToCaret();
                    } else
                        textBoxEditor2.Text = text;
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
                if (Platform.sysPlatform == Platform.ePlatform.MONO)
                    pXmlHandler.XmlReplaceNode(hash, textBoxEditor.Text); 
                else
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
        private Boolean isResize = false;
        private Boolean isResizeW = false;
        private Boolean isResizeE = false;
        private Boolean isResizeN = false;
        private Boolean isResizeS = false;
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
                    if (isResize)
                    {
                        mouseDown = true;
                    }
                    else if (inBounds(((MouseEventArgs)e).Location, _Attr.Rectangle, -2))
                    {
                        treeView1.SelectedNode = pXmlHandler.XmlGetTreeNode(_Attr.myNode);

                        mouseDown = true;
                        this.Cursor = Cursors.SizeAll;
                    }
                    else if (inBounds(((MouseEventArgs)e).Location, _Attr.Rectangle, +2))
                    {
                        mouseDown = true;
                        //this.Cursor = Cursors.SizeAll;
                    }                       
                }
            }

            tabControl1.Focus();
        }

        private bool inRange(int myx, int targetX, int margin)
        {
            int targetXMax = targetX + margin;
            int targetXMin = targetX - margin;
            if (myx > targetXMin && myx < targetXMax)
                return true;
            return false;
        }

        private bool inBounds(Point myx, Rectangle target, int margin)
        {
            Rectangle targetMax = new Rectangle(target.X - margin, target.Y - margin, target.Width + 2*margin, target.Height + 2*margin);
            //targetMin = new Rectangle(target.X - margin, target.Y - margin, target.Width - margin, target.Height - margin);

            return targetMax.Contains(myx);
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
                        if (isResize)
                        {
                            sAttribute _Attr = (sAttribute)propertyGrid1.SelectedObject;
                            Size size = new Size();
                            size.Width = _Attr.pWidth;
                            size.Height = _Attr.pHeight;

                            if (isResizeW || isResizeE)
                            {
                                Int32 sizeX = (Int32)(_Attr.pWidth + (curX - _StartX));
                                if (isResizeE)
                                    size.Width = (Int32)sizeX;
                                else
                                {
                                    Int32 posX = (Int32)(_Attr.pRelativX + (curX - _StartX));
                                    Int32 posY = (Int32)_Attr.pRelativY;
                                    sAttribute.Position pos = new sAttribute.Position();
                                    pos.X = ((Int32)posX).ToString();
                                    pos.Y = ((Int32)posY).ToString();
                                    _Attr.Relativ = pos;

                                    sizeX = (Int32)(_Attr.pWidth + (_StartX - curX));
                                    size.Width = (Int32)sizeX;
                                }
                            }
                            if (isResizeN || isResizeS)
                            {
                                Int32 sizeY = (Int32)(_Attr.pHeight + (curY - _StartY));
                                if (isResizeS)
                                    size.Height = (Int32)sizeY;
                                else
                                {
                                    Int32 posX = (Int32)_Attr.pRelativX;
                                    Int32 posY = (Int32)(_Attr.pRelativY + (curY - _StartY));
                                    sAttribute.Position pos = new sAttribute.Position();
                                    pos.X = ((Int32)posX).ToString();
                                    pos.Y = ((Int32)posY).ToString();
                                    _Attr.Relativ = pos;

                                    sizeY = (Int32)(_Attr.pHeight + (_StartY - curY));
                                    size.Height = (Int32)sizeY;
                                }
                                
                            }


                            /* TODO: If we would not set it here directly to Relative but to X and Y the value would only be
                             * temprarly saved, and so we could set it finaly in mouse up, this has an advantage cause we 
                             * could easier implement a UNDO functionality.
                             */
                            _Attr.Size = size;
                        }
                        else
                        {
                            sAttribute _Attr = (sAttribute)propertyGrid1.SelectedObject;
                            Int32 posX = (Int32)(_Attr.pRelativX + (curX - _StartX));
                            Int32 posY = (Int32)(_Attr.pRelativY + (curY - _StartY));
                            sAttribute.Position pos = new sAttribute.Position();

                            pos.X = ((Int32)posX).ToString();
                            pos.Y = ((Int32)posY).ToString();


                            /* TODO: If we would not set it here directly to Relative but to X and Y the value would only be
                             * temprarly saved, and so we could set it finaly in mouse up, this has an advantage cause we 
                             * could easier implement a UNDO functionality.
                             */
                            _Attr.Relativ = pos;
                        }

                        propertyGrid1.Refresh();
                        pictureBox1.Invalidate();
                    }
                }
            }
            else
            {
                sAttribute _Attr = (sAttribute)propertyGrid1.SelectedObject;
                if (_Attr != null)
                {
                    if (inBounds(((MouseEventArgs)e).Location, _Attr.Rectangle, 2))
                    {
                        isResize = true;
                        if (inRange(curX, _Attr.Absolut.X, 2))
                        {
                            this.Cursor = Cursors.SizeWE;
                            isResizeW = true;
                        }
                        else if (inRange(curX, _Attr.Absolut.X + _Attr.Size.Width, 2))
                        {
                            this.Cursor = Cursors.SizeWE;
                            isResizeE = true;
                        }
                        else if (inRange(curY, _Attr.Absolut.Y, 2))
                        {
                            this.Cursor = Cursors.SizeNS;
                            isResizeN = true;
                        }
                        else if (inRange(curY, _Attr.Absolut.Y + _Attr.Size.Height, 2))
                        {
                            this.Cursor = Cursors.SizeNS;
                            isResizeS = true;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            isResize = false;
                            isResizeW = false;
                            isResizeE = false;
                            isResizeN = false;
                            isResizeS = false;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        isResize = false;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    isResize = false;
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
            isResize = false;
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
        private bool isF10(KeyEventArgs e)
        {
            return e.KeyCode == Keys.F10;
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
            this.Cursor = Cursors.Default;
        }

        private bool _bFullScreenMode = false, _bPreviewFullScreenMode = false;
        private Form previewForm = null;
        private int x, y, w, h;

        private void toggleFullscreen()
        {
            if (_bFullScreenMode == false)
            {
                x = this.Left;
                y = this.Top;
                w = this.Width;
                h = this.Height;
                this.FormBorderStyle = FormBorderStyle.None;
                menuStrip1.Visible = false;
                toolStripMain.Visible = false;
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
                this.Left = x;
                this.Top = y;
                this.Width = w;
                this.Height = h;
                _bFullScreenMode = false;
            }
        }

        private void togglePreviewFullscreen()
        {
            if (previewForm == null)
            {
                previewForm = new Form();
                previewForm.FormBorderStyle = FormBorderStyle.None;
                previewForm.TopMost = true;
                previewForm.Left = 0;
                previewForm.Top = 0;
                previewForm.Width = Screen.PrimaryScreen.Bounds.Width;
                previewForm.Height = Screen.PrimaryScreen.Bounds.Height;
                previewForm.BackColor = Color.Black;
                previewForm.Controls.Add(pictureBox1);
                pictureBox1.Size = new Size(1280, 720);
                previewForm.KeyUp += fMain_KeyUp;
                previewForm.Visible = true;
            }
            else
            {
                panelDesignerInner.Controls.Add(pictureBox1);
                previewForm.Visible = false;
                previewForm = null;
            }
        }

        private void fMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (isF11(e))
                toggleFullscreen();
            else if (isF10(e))
                togglePreviewFullscreen();
        }

        private void fMain_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            cProperties.setProperty("enable_alpha", toolStripButton2.Checked);

            pictureBox1.Invalidate();
        }
    }
}
