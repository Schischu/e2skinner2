using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using e2skinner2.Logic;
using e2skinner2.Structures;
using System.Drawing.Text;
using System.IO;

namespace e2skinner2.Frames
{
    public partial class fFonts : Form
    {
        private cXMLHandler pXmlHandler = null;

        public fFonts()
        {
            InitializeComponent();
        }

        public void setup(cXMLHandler xmlhandler)
        {
            pXmlHandler = xmlhandler;

            sFont[] fonts = cDataBase.getFonts();

            //listView1.Clear();
            foreach (sFont font in fonts)
            {
                System.Windows.Forms.ListViewItem.ListViewSubItem[] subtitems = new System.Windows.Forms.ListViewItem.ListViewSubItem[6];

                subtitems[0] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[0].Text = font.Name;
                subtitems[1] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[1].Text = font.Filename;
                subtitems[2] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[2].Text = font.Path;
                subtitems[3] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[3].Text = Convert.ToString(font.Scale);
                subtitems[4] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[4].Text = Convert.ToString(font.Replacement);
                subtitems[5] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[5].Text = "0";
                ListViewItem item = new ListViewItem(subtitems, 0);
                listView1.Items.Add(item);
            }
            listView1.RedrawItems(0, listView1.Items.Count - 1, false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBoxName.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBoxPath.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBoxScale.Text = listView1.SelectedItems[0].SubItems[3].Text;
                checkBoxReplacement.Checked = Convert.ToBoolean(listView1.SelectedItems[0].SubItems[4].Text);

                PrivateFontCollection pfc = new PrivateFontCollection();
                try
                {
                    pfc.AddFontFile(cProperties.getProperty("path_fonts") + "/" + textBoxPath.Text);
                }
                catch (FileNotFoundException error)
                {
                    String errormessage = error.Message + ":\n\n";
                    errormessage += cProperties.getProperty("path_fonts") + "/" + textBoxPath.Text + "\n";
                    errormessage += error.Message;

                    MessageBox.Show(errormessage,
                        error.Message,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);

                    return;
                }
                //pfc.Families.GetValue(0);

                FontFamily pff = (FontFamily)pfc.Families[0];
                System.Drawing.Font font = new System.Drawing.Font(pff, 20.25F, FontStyle.Bold);

                textBoxPreview.Font = font;
            }
        }
    }
}
