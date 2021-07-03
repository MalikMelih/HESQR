using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

using ZXing.Common;
using ZXing;
using ZXing.QrCode;

namespace HESQR
{
    public partial class Form1 : Form
    {
        private QrCodeEncodingOptions options;

        public string hes = "";
        public string name = "";
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
                MessageBox.Show("Hes Kodu Girilmedi", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var qr = new ZXing.BarcodeWriter();
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;
                var result = new Bitmap(qr.Write(txt_hes.Text.Trim()));
                pict.Image = result;
                txt_name.Clear();
                txt_hes.Clear();
            }

            /*hes = txt_hes.Text.ToString();
            name = txt_name.Text.ToString();
            Random rndm = new Random();
            string num = rndm.Next(50).ToString();

            //Temizle
            txt_hes.Text = null;
            txt_name.Text = null;

            //İşlem
            Create(hes,num);
            pict.Image = System.Drawing.Image.FromFile(num+".png");
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
            //printDocument1.Print();*/
        }

        public static void Create(string data, string num)
        {
           QRCodeWriter.CreateQrCode("c64dc337b87b41d08a165ffb020126ac|"+data, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng(num+".png");           
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(name, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(45, 20)); //(e.PageBounds.Width / 2)
            e.Graphics.DrawImage(pict.Image, 50,50,64,64);
        }
    }
}
