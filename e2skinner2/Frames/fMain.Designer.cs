namespace e2skinner2.Frames
{
    partial class fMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.elementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MiAddLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.MiAddPixmap = new System.Windows.Forms.ToolStripMenuItem();
            this.MiAddWidget = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MiResolution = new System.Windows.Forms.ToolStripMenuItem();
            this.MiColors = new System.Windows.Forms.ToolStripMenuItem();
            this.MiFonts = new System.Windows.Forms.ToolStripMenuItem();
            this.MiWindowStyles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MiPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelDesigner = new System.Windows.Forms.Panel();
            this.panelDesignerInner = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripDesigner = new System.Windows.Forms.ToolStrip();
            this.btnSkinned = new System.Windows.Forms.ToolStripButton();
            this.btnSkinnedScreen = new System.Windows.Forms.ToolStripButton();
            this.btnSkinnedLabel = new System.Windows.Forms.ToolStripButton();
            this.btnSkinnedPixmap = new System.Windows.Forms.ToolStripButton();
            this.btnSkinnedWidget = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFading = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.toolStripEditor = new System.Windows.Forms.ToolStrip();
            this.btnSaveEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxEditor = new System.Windows.Forms.TextBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelDesigner.SuspendLayout();
            this.panelDesignerInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStripDesigner.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.toolStripEditor.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(275, 365);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.elementToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1283, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiNew,
            this.MiOpen,
            this.MiSave,
            this.MiSaveAs,
            this.MiClose,
            this.toolStripSeparator2,
            this.MiExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MiNew
            // 
            this.MiNew.Enabled = false;
            this.MiNew.Name = "MiNew";
            this.MiNew.Size = new System.Drawing.Size(152, 22);
            this.MiNew.Text = "New";
            // 
            // MiOpen
            // 
            this.MiOpen.Name = "MiOpen";
            this.MiOpen.Size = new System.Drawing.Size(152, 22);
            this.MiOpen.Text = "Open";
            this.MiOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // MiSave
            // 
            this.MiSave.Enabled = false;
            this.MiSave.Name = "MiSave";
            this.MiSave.Size = new System.Drawing.Size(152, 22);
            this.MiSave.Text = "Save";
            this.MiSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // MiSaveAs
            // 
            this.MiSaveAs.Enabled = false;
            this.MiSaveAs.Name = "MiSaveAs";
            this.MiSaveAs.Size = new System.Drawing.Size(152, 22);
            this.MiSaveAs.Text = "Save As";
            this.MiSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MiClose
            // 
            this.MiClose.Enabled = false;
            this.MiClose.Name = "MiClose";
            this.MiClose.Size = new System.Drawing.Size(152, 22);
            this.MiClose.Text = "Close";
            this.MiClose.Click += new System.EventHandler(this.MiClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // MiExit
            // 
            this.MiExit.Name = "MiExit";
            this.MiExit.Size = new System.Drawing.Size(152, 22);
            this.MiExit.Text = "Exit";
            this.MiExit.Click += new System.EventHandler(this.MiExit_Click);
            // 
            // elementToolStripMenuItem
            // 
            this.elementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiAddLabel,
            this.MiAddPixmap,
            this.MiAddWidget,
            this.toolStripSeparator3,
            this.MiDelete});
            this.elementToolStripMenuItem.Name = "elementToolStripMenuItem";
            this.elementToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.elementToolStripMenuItem.Text = "Element";
            // 
            // MiAddLabel
            // 
            this.MiAddLabel.Enabled = false;
            this.MiAddLabel.Name = "MiAddLabel";
            this.MiAddLabel.Size = new System.Drawing.Size(138, 22);
            this.MiAddLabel.Text = "Add Label";
            this.MiAddLabel.Click += new System.EventHandler(this.addLabelToolStripMenuItem_Click);
            // 
            // MiAddPixmap
            // 
            this.MiAddPixmap.Enabled = false;
            this.MiAddPixmap.Name = "MiAddPixmap";
            this.MiAddPixmap.Size = new System.Drawing.Size(138, 22);
            this.MiAddPixmap.Text = "Add Pixmap";
            this.MiAddPixmap.Click += new System.EventHandler(this.addPixmapToolStripMenuItem_Click);
            // 
            // MiAddWidget
            // 
            this.MiAddWidget.Enabled = false;
            this.MiAddWidget.Name = "MiAddWidget";
            this.MiAddWidget.Size = new System.Drawing.Size(138, 22);
            this.MiAddWidget.Text = "Add Widget";
            this.MiAddWidget.Click += new System.EventHandler(this.widgetToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(135, 6);
            // 
            // MiDelete
            // 
            this.MiDelete.Enabled = false;
            this.MiDelete.Name = "MiDelete";
            this.MiDelete.Size = new System.Drawing.Size(138, 22);
            this.MiDelete.Text = "Delete";
            this.MiDelete.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiResolution,
            this.MiColors,
            this.MiFonts,
            this.MiWindowStyles,
            this.toolStripSeparator1,
            this.MiPreferences});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // MiResolution
            // 
            this.MiResolution.Enabled = false;
            this.MiResolution.Name = "MiResolution";
            this.MiResolution.Size = new System.Drawing.Size(148, 22);
            this.MiResolution.Text = "Resolution";
            this.MiResolution.Click += new System.EventHandler(this.resolutionToolStripMenuItem_Click);
            // 
            // MiColors
            // 
            this.MiColors.Enabled = false;
            this.MiColors.Name = "MiColors";
            this.MiColors.Size = new System.Drawing.Size(148, 22);
            this.MiColors.Text = "Colors";
            this.MiColors.Click += new System.EventHandler(this.colorsToolStripMenuItem_Click);
            // 
            // MiFonts
            // 
            this.MiFonts.Enabled = false;
            this.MiFonts.Name = "MiFonts";
            this.MiFonts.Size = new System.Drawing.Size(148, 22);
            this.MiFonts.Text = "Fonts";
            this.MiFonts.Click += new System.EventHandler(this.fontsToolStripMenuItem_Click);
            // 
            // MiWindowStyles
            // 
            this.MiWindowStyles.Enabled = false;
            this.MiWindowStyles.Name = "MiWindowStyles";
            this.MiWindowStyles.Size = new System.Drawing.Size(148, 22);
            this.MiWindowStyles.Text = "WindowStyles";
            this.MiWindowStyles.Click += new System.EventHandler(this.windowStylesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // MiPreferences
            // 
            this.MiPreferences.Name = "MiPreferences";
            this.MiPreferences.Size = new System.Drawing.Size(148, 22);
            this.MiPreferences.Text = "Preferences";
            this.MiPreferences.Click += new System.EventHandler(this.MiPreferences_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItemHelp.Text = "?";
            this.toolStripMenuItemHelp.Click += new System.EventHandler(this.toolStripMenuItemHelp_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(299, 66);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(972, 712);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelDesigner);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(964, 686);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Designer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelDesigner
            // 
            this.panelDesigner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDesigner.AutoScroll = true;
            this.panelDesigner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDesigner.Controls.Add(this.panelDesignerInner);
            this.panelDesigner.Controls.Add(this.toolStripDesigner);
            this.panelDesigner.Location = new System.Drawing.Point(3, 3);
            this.panelDesigner.Name = "panelDesigner";
            this.panelDesigner.Size = new System.Drawing.Size(958, 680);
            this.panelDesigner.TabIndex = 1;
            // 
            // panelDesignerInner
            // 
            this.panelDesignerInner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDesignerInner.AutoScroll = true;
            this.panelDesignerInner.Controls.Add(this.pictureBox1);
            this.panelDesignerInner.Location = new System.Drawing.Point(1, 28);
            this.panelDesignerInner.Name = "panelDesignerInner";
            this.panelDesignerInner.Size = new System.Drawing.Size(952, 645);
            this.panelDesignerInner.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(950, 646);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // toolStripDesigner
            // 
            this.toolStripDesigner.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSkinned,
            this.btnSkinnedScreen,
            this.btnSkinnedLabel,
            this.btnSkinnedPixmap,
            this.btnSkinnedWidget,
            this.toolStripSeparator4,
            this.btnFading});
            this.toolStripDesigner.Location = new System.Drawing.Point(0, 0);
            this.toolStripDesigner.Name = "toolStripDesigner";
            this.toolStripDesigner.Size = new System.Drawing.Size(954, 25);
            this.toolStripDesigner.TabIndex = 1;
            this.toolStripDesigner.Text = "toolStrip3";
            // 
            // btnSkinned
            // 
            this.btnSkinned.Checked = true;
            this.btnSkinned.CheckOnClick = true;
            this.btnSkinned.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSkinned.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSkinned.Image = ((System.Drawing.Image)(resources.GetObject("btnSkinned.Image")));
            this.btnSkinned.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkinned.Name = "btnSkinned";
            this.btnSkinned.Size = new System.Drawing.Size(53, 22);
            this.btnSkinned.Text = "Skinned";
            this.btnSkinned.Click += new System.EventHandler(this.btnSkinned_Click);
            // 
            // btnSkinnedScreen
            // 
            this.btnSkinnedScreen.Checked = true;
            this.btnSkinnedScreen.CheckOnClick = true;
            this.btnSkinnedScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSkinnedScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSkinnedScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnSkinnedScreen.Image")));
            this.btnSkinnedScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkinnedScreen.Name = "btnSkinnedScreen";
            this.btnSkinnedScreen.Size = new System.Drawing.Size(69, 22);
            this.btnSkinnedScreen.Text = "Screen Red";
            this.btnSkinnedScreen.Click += new System.EventHandler(this.btnSkinnedScreen_Click);
            // 
            // btnSkinnedLabel
            // 
            this.btnSkinnedLabel.Checked = true;
            this.btnSkinnedLabel.CheckOnClick = true;
            this.btnSkinnedLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSkinnedLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSkinnedLabel.Image = ((System.Drawing.Image)(resources.GetObject("btnSkinnedLabel.Image")));
            this.btnSkinnedLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkinnedLabel.Name = "btnSkinnedLabel";
            this.btnSkinnedLabel.Size = new System.Drawing.Size(73, 22);
            this.btnSkinnedLabel.Text = "Label Green";
            this.btnSkinnedLabel.Click += new System.EventHandler(this.btnSkinnedLabel_Click);
            // 
            // btnSkinnedPixmap
            // 
            this.btnSkinnedPixmap.Checked = true;
            this.btnSkinnedPixmap.CheckOnClick = true;
            this.btnSkinnedPixmap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSkinnedPixmap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSkinnedPixmap.Image = ((System.Drawing.Image)(resources.GetObject("btnSkinnedPixmap.Image")));
            this.btnSkinnedPixmap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkinnedPixmap.Name = "btnSkinnedPixmap";
            this.btnSkinnedPixmap.Size = new System.Drawing.Size(76, 22);
            this.btnSkinnedPixmap.Text = "Pixmap Blue";
            this.btnSkinnedPixmap.Click += new System.EventHandler(this.btnSkinnedPixmap_Click);
            // 
            // btnSkinnedWidget
            // 
            this.btnSkinnedWidget.Checked = true;
            this.btnSkinnedWidget.CheckOnClick = true;
            this.btnSkinnedWidget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSkinnedWidget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSkinnedWidget.Image = ((System.Drawing.Image)(resources.GetObject("btnSkinnedWidget.Image")));
            this.btnSkinnedWidget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkinnedWidget.Name = "btnSkinnedWidget";
            this.btnSkinnedWidget.Size = new System.Drawing.Size(87, 22);
            this.btnSkinnedWidget.Text = "Widget Yellow";
            this.btnSkinnedWidget.Click += new System.EventHandler(this.btnSkinnedWidget_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFading
            // 
            this.btnFading.Checked = true;
            this.btnFading.CheckOnClick = true;
            this.btnFading.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnFading.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFading.Image = ((System.Drawing.Image)(resources.GetObject("btnFading.Image")));
            this.btnFading.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFading.Name = "btnFading";
            this.btnFading.Size = new System.Drawing.Size(47, 22);
            this.btnFading.Text = "Fading";
            this.btnFading.Click += new System.EventHandler(this.btnFading_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelEditor);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(964, 686);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelEditor
            // 
            this.panelEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEditor.Controls.Add(this.toolStripEditor);
            this.panelEditor.Controls.Add(this.textBoxEditor);
            this.panelEditor.Location = new System.Drawing.Point(3, 3);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(958, 680);
            this.panelEditor.TabIndex = 2;
            // 
            // toolStripEditor
            // 
            this.toolStripEditor.AutoSize = false;
            this.toolStripEditor.CanOverflow = false;
            this.toolStripEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveEditor,
            this.toolStripLabel1});
            this.toolStripEditor.Location = new System.Drawing.Point(0, 0);
            this.toolStripEditor.Name = "toolStripEditor";
            this.toolStripEditor.Size = new System.Drawing.Size(958, 25);
            this.toolStripEditor.TabIndex = 1;
            this.toolStripEditor.Text = "toolStrip2";
            // 
            // btnSaveEditor
            // 
            this.btnSaveEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveEditor.Enabled = false;
            this.btnSaveEditor.Image = global::e2skinner2.Properties.Resources.saveHS;
            this.btnSaveEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveEditor.Name = "btnSaveEditor";
            this.btnSaveEditor.Size = new System.Drawing.Size(23, 22);
            this.btnSaveEditor.Text = "Save";
            this.btnSaveEditor.Click += new System.EventHandler(this.btnSaveEditor_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel1.Text = "No Errors.";
            // 
            // textBoxEditor
            // 
            this.textBoxEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEditor.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEditor.Location = new System.Drawing.Point(3, 28);
            this.textBoxEditor.Multiline = true;
            this.textBoxEditor.Name = "textBoxEditor";
            this.textBoxEditor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxEditor.Size = new System.Drawing.Size(952, 649);
            this.textBoxEditor.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(275, 330);
            this.propertyGrid1.TabIndex = 3;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.FileName = "skin.xml";
            this.openFileDialog1.Filter = "Skin files|*.xml";
            this.openFileDialog1.Title = "Open skin.xml";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 66);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(281, 712);
            this.splitContainer1.SplitterDistance = 371;
            this.splitContainer1.TabIndex = 3;
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(1283, 39);
            this.toolStripMain.TabIndex = 4;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(36, 36);
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 36);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "*.xml|";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ForeColor = System.Drawing.Color.Transparent;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 790);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "e2skinner v2.0.0.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelDesigner.ResumeLayout(false);
            this.panelDesigner.PerformLayout();
            this.panelDesignerInner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStripDesigner.ResumeLayout(false);
            this.toolStripDesigner.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panelEditor.ResumeLayout(false);
            this.panelEditor.PerformLayout();
            this.toolStripEditor.ResumeLayout(false);
            this.toolStripEditor.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MiNew;
        private System.Windows.Forms.ToolStripMenuItem MiOpen;
        private System.Windows.Forms.ToolStripMenuItem MiSave;
        private System.Windows.Forms.ToolStripMenuItem MiSaveAs;
        private System.Windows.Forms.ToolStripMenuItem MiClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxEditor;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MiResolution;
        private System.Windows.Forms.ToolStripMenuItem MiFonts;
        private System.Windows.Forms.ToolStripMenuItem MiColors;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripMenuItem MiWindowStyles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MiPreferences;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.ToolStrip toolStripEditor;
        private System.Windows.Forms.ToolStripButton btnSaveEditor;
        private System.Windows.Forms.ToolStripMenuItem MiExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panelDesigner;
        private System.Windows.Forms.ToolStrip toolStripDesigner;
        private System.Windows.Forms.ToolStripButton btnSkinned;
        private System.Windows.Forms.ToolStripButton btnSkinnedScreen;
        private System.Windows.Forms.ToolStripButton btnSkinnedLabel;
        private System.Windows.Forms.ToolStripButton btnSkinnedPixmap;
        private System.Windows.Forms.ToolStripButton btnSkinnedWidget;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem elementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MiAddLabel;
        private System.Windows.Forms.ToolStripMenuItem MiAddPixmap;
        private System.Windows.Forms.ToolStripMenuItem MiAddWidget;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MiDelete;
        private System.Windows.Forms.Panel panelDesignerInner;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnFading;
    }
}

