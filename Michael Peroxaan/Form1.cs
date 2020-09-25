using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace Michael_Peroxaan
{
    public partial class Form1 : Form
    {
        Thread t;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Thread(MoveFormThread);
            t.IsBackground = true;
            t.Start();
        }

        private void MoveFormThread()
        {
            while (true)
            {
                Invoke(new Action(() =>
                {
                    this.Location = new Point(Cursor.Position.X - (this.Width / 2), Cursor.Position.Y - (this.Height / 2));
                    this.BringToFront();
                    this.Activate();
                }));
                Thread.Sleep(50);
            }
        }

        private const uint SPI_SETDESKWALLPAPER = 0x14;

        private const uint SPIF_UPDATEINIFILE = 0x1;

        private const uint SPIF_SENDWININICHANGE = 0x2;

        [DllImport("user32.dll", SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Michael Peroxaan", "Michael Peroxaan");
            Properties.Resources.MichaelPeroxaan.Save("C:\\MichaelPeroxaan.bmp");
            try
            {
                uint flags = 0;
                if (true)
                    flags = SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE;

                if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, "C:\\MichaelPeroxaan.bmp", flags))
                {
                    MessageBox.Show("SystemParametersInfo failed.",
                        "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.ToString());
            }
        }
    }
}
