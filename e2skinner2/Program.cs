using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;

using e2skinner2.Frames;

namespace e2skinner2
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Platform.detectPlatform();
            //For Debugging
            //Platform.sysPlatform = Platform.ePlatform.MONO;

            Logic.cProperties.init();
            Structures.cPreviewText.init();
            Structures.cConverter.init();

            Application.Run(new fMain());
        }
    }
}
