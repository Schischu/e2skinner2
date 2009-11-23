using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using e2skinner2.Logic;
using e2skinner2.Structures;

namespace e2skinner2.Frames
{
    public partial class fColors : Form
    {
        private cXMLHandler pXmlHandler = null;

        public fColors()
        {
            InitializeComponent();
        }

        private void refresh()
        {
            sColor[] colors = (sColor[])cDataBase.pColors.getArray();

            listView1.Items.Clear();

            foreach (sColor color in colors)
            {
                System.Windows.Forms.ListViewItem.ListViewSubItem[] subtitems = new System.Windows.Forms.ListViewItem.ListViewSubItem[3];
                subtitems[0] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[0].Text = color.pName;
                subtitems[1] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[1].Text = Convert.ToString(color.pValue, 16);
                while (subtitems[1].Text.Length < 8)
                    subtitems[1].Text = "0" + subtitems[1].Text;
                subtitems[2] = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subtitems[2].Text = "0";
                ListViewItem item = new ListViewItem(subtitems, 0);
                listView1.Items.Add(item);
            }
            listView1.RedrawItems(0, listView1.Items.Count - 1, false);
        }

        public void setup(cXMLHandler xmlhandler)
        {
            pXmlHandler = xmlhandler;

            refresh();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBoxName.Text = listView1.SelectedItems[0].SubItems[0].Text;

                String colorString = listView1.SelectedItems[0].SubItems[1].Text;
                if (colorString.Length == 0)
                    colorString = "0";
                UInt32 color = Convert.ToUInt32(colorString, 16);
                textBoxValue.Text = Convert.ToString(color, 16);

                int alpha = (int)(color >> 24) & 0xff;
                textBoxAlpha.Text = alpha.ToString();

                int red = (int)(color >> 16) & 0xff;
                textBoxRed.Text = red.ToString();

                int green = (int)(color >> 8) & 0xff;
                textBoxGreen.Text = green.ToString();

                int blue = (int)color & 0xff;
                textBoxBlue.Text = blue.ToString();

                pictureBoxColor.BackColor = Color.FromArgb(/*alpha, */(int)red, (int)green, (int)blue);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text.Length == 0)
            {
            }
            else if (textBoxValue.Text.Length == 0)
            {
                cDataBase.pColors.remove((Object)new sColor(textBoxName.Text, 0));
            }
            else if(textBoxName.Text != listView1.SelectedItems[0].SubItems[1].Text)
            {
                cDataBase.pColors.rename(pXmlHandler, listView1.SelectedItems[0].SubItems[0].Text, textBoxName.Text);
            }
            else
            {
                //cDataBase.pColors.add((Object)new sColor(textBoxName.Text, Convert.ToUInt32(maskedTextBoxColor.Text, 16)));
            }

            refresh();
        }

        private void fColors_FormClosing(object sender, FormClosingEventArgs e)
        {
            cDataBase.pColors.sync(pXmlHandler);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length > 0 && textBoxValue.Text.Length > 0)
            {
                cDataBase.pColors.add((Object)new sColor(textBoxName.Text, Convert.ToUInt32(textBoxValue.Text, 16)));
            }
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            String colorString = "0";
            UInt32 color = 0;
            try
            {
                colorString = textBoxValue.Text.Length > 0 ? textBoxValue.Text.Replace(" ", "") : "0";
                if (colorString.Length > 8)
                    colorString = colorString.Substring(0, 8);
                color = Convert.ToUInt32(colorString, 16);
            }
            catch (Exception error)
            {
                String errormessage = error.Message + ":\n\n";
                errormessage += "Invalid format for value hexadecimal value!" + "\n";
                errormessage += error.Message;

                MessageBox.Show(errormessage,
                    error.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                return;
            }
            int alpha = (int)(color >> 24) & 0xff;
            textBoxAlpha.Text = alpha.ToString();
            int red = (int)(color >> 16) & 0xff;
            textBoxRed.Text = red.ToString();
            int green = (int)(color >> 8) & 0xff;
            textBoxGreen.Text = green.ToString();
            int blue = (int)color & 0xff;
            textBoxBlue.Text = blue.ToString();
            pictureBoxColor.BackColor = Color.FromArgb(/*alpha, */(int)red, (int)green, (int)blue);
        }

        private void textBoxAlpha_TextChanged(object sender, EventArgs e)
        {
            UInt32 alpha = 0;
            UInt32 red = 0;
            UInt32 green = 0;
            UInt32 blue = 0;

            try
            {
                String alphaString = textBoxAlpha.Text.Length > 0 ? textBoxAlpha.Text.Trim() : "0";
                alpha = Convert.ToUInt32(alphaString);
                alpha &= 0xFF;
            }
            catch (Exception error)
            {
                String errormessage = error.Message + ":\n\n";
                errormessage += "Invalid format for value alpha!" + "\n";
                errormessage += error.Message;

                MessageBox.Show(errormessage,
                    error.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                return;
            }

            try
            {
                String redString = textBoxRed.Text.Length > 0 ? textBoxRed.Text.Trim() : "0";
                red = Convert.ToUInt32(redString);
                red &= 0xFF;
            }
            catch (Exception error)
            {
                String errormessage = error.Message + ":\n\n";
                errormessage += "Invalid format for value red!" + "\n";
                errormessage += error.Message;

                MessageBox.Show(errormessage,
                    error.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                return;
            }

            try
            {
                String greenString = textBoxGreen.Text.Length > 0 ? textBoxGreen.Text.Trim() : "0";
                green = Convert.ToUInt32(greenString);
                green &= 0xFF;
            }
            catch (Exception error)
            {
                String errormessage = error.Message + ":\n\n";
                errormessage += "Invalid format for value green!" + "\n";
                errormessage += error.Message;

                MessageBox.Show(errormessage,
                    error.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                return;
            }

            try
            {
                String blueString = textBoxBlue.Text.Length > 0 ? textBoxBlue.Text.Trim() : "0";
                blue = Convert.ToUInt32(blueString);
                blue &= 0xFF;
            }
            catch (Exception error)
            {
                String errormessage = error.Message + ":\n\n";
                errormessage += "Invalid format for value blue!" + "\n";
                errormessage += error.Message;

                MessageBox.Show(errormessage,
                    error.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

                return;
            }

            UInt32 value = /*Convert.ToUInt32*/((alpha * 0x01000000) + (red * 0x00010000) + (green * 0x00000100) + blue);
            textBoxValue.Text = Convert.ToString(value, 16);

            pictureBoxColor.BackColor = Color.FromArgb(/*alpha, */(int)red, (int)green, (int)blue);
        }

    }
}
