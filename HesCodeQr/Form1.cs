using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.QrCode;
using ZXing;
using ZXing.Common;
using System.Drawing.Printing;

namespace HesCodeQr
{
    public partial class Form1 : Form
    {
        private QrCodeEncodingOptions options;

        public Form1()
        {
            InitializeComponent();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250,
            };
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txt_hes.Text) || String.IsNullOrEmpty(txt_hes.Text))
            {
                pict.Image = null;
                MessageBox.Show("", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var qr = new ZXing.BarcodeWriter();
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;
                var result = new Bitmap(qr.Write("c64dc337b87b41d08a165ffb020126ac|"+txt_hes.Text.Trim()));
                pict.Image = result;
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                //printDocument1.Print();
                txt_name.Clear();
                txt_hes.Clear();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int width = (e.PageBounds.Width / 4);
            e.Graphics.DrawString(txt_name.Text, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new Point(45, 20));
            e.Graphics.DrawImage(pict.Image,0,60, width, width);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
