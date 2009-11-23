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

namespace e2skinner2.Frames
{
    public partial class fResolution : Form
    {
        private cXMLHandler pXmlHandler = null;

        public fResolution()
        {
            InitializeComponent();
        }

        public void setup(cXMLHandler xmlhandler)
        {
            pXmlHandler = xmlhandler;

            sResolution resolution = cDataBase.pResolution.getResolution();

            if (resolution.Yres == 576)
                radioButton576.Checked = true;
            else if (resolution.Yres == 720)
                radioButton720.Checked = true;
            else if (resolution.Yres == 1080)
                radioButton1080.Checked = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sResolution res = cDataBase.pResolution.getResolution();
            Size nRes = new Size(); ;
            if (radioButton576.Checked)
            {
                nRes.Width = 720;
                nRes.Height = 576;
            }
            else if (radioButton720.Checked)
            {
                nRes.Width = 1280;
                nRes.Height = 720;
            }
            else if (radioButton1080.Checked)
            {
                nRes.Width = 1920;
                nRes.Height = 1080;
            }

            cDataBase.pImage.rescale((int)res.Xres, (int)res.Yres, (int)nRes.Width, (int)nRes.Height);
            cDataBase.rescaleLocations(pXmlHandler, (int)res.Xres, (int)res.Yres, (int)nRes.Width, (int)nRes.Height);
            cDataBase.pResolution.setResolution((uint)nRes.Width, (uint)nRes.Height);

            this.Close();
        }
    }
}
