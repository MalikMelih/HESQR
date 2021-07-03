using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing.QrCode;
using ZXing;
using System.Drawing.Printing;

namespace HesCodeQr
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private QrCodeEncodingOptions options;
        public static int loading = 0;
        public string data = "";
        public string hesqr = "";
        int with = 200;
        int height = 100;
        int qrsize = 120;
        int ltimer = 0;
        SplashScreen ssc = new SplashScreen();

        public Form1()
        {
            InitializeComponent();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = qrsize,
                Height = qrsize,
            };
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            sayacdt.Text = Properties.Settings.Default.count.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loading++;
            if (loading == 90)
            {
                timer1.Stop();
                ssc.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            version.Text = Program.version;
            timer1.Interval = 90;
            timer1.Start();
            ssc.ShowDialog();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            data = txt_hes.Text + textBox1.Text + textBox2.Text;
            hesqr = txt_hes.Text + "-" + textBox1.Text + "-" + textBox2.Text;
            if (String.IsNullOrWhiteSpace(data) || String.IsNullOrEmpty(data))  //Veri yok ise hata döndür
            {
                pict.Image = null;
                MessageBox.Show("Veri Girilmedi", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else //Veri ile Qr Kod oluştur
            {
                Properties.Settings.Default.count++;
                sayacdt.Text = Properties.Settings.Default.count.ToString();
                var qr = new BarcodeWriter();
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;
                var result = new Bitmap(qr.Write("c64dc337b87b41d08a165ffb020126ac|"+data.Trim()));
                pict.Image = result;
                printDocument1.PrinterSettings.PrinterName = "qrcode";
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("100 x 300 mm", with, height);
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
                //printDocument1.Print();   //Önizleme Olmadan Direk Çıkart
                //Temizlik
                txt_name.Clear();
                txt_hes.Clear();
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)  //Yazdırma Sayfası Tasarımı
        {
            //int width = (e.PageBounds.Width / 8);
            Rectangle hname = new Rectangle(new Point(5, 50), new Size((with / 2 - 10), 20));
            Rectangle hcode = new Rectangle(new Point(5, 80), new Size((with / 2 - 10), 20));
            StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
            format1.LineAlignment = StringAlignment.Center;
            format1.Alignment = StringAlignment.Center;
            e.Graphics.DrawImage(pict.Image,-8,90, qrsize, qrsize);
            e.Graphics.DrawString(txt_name.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, (RectangleF)hname, format1);
            e.Graphics.DrawString(hesqr, new Font("Arial", 9, FontStyle.Regular), Brushes.Black, (RectangleF)hcode, format1);
        }

        private void txt_hes_TextChanged(object sender, EventArgs e)
        {
            if (txt_hes.Text.Length == 4)   //4 Rakam girildi ise geç
            {
                SendKeys.Send("{TAB}");
            }
            else if (txt_hes.Text.Length > 4)
            {
                if (txt_hes.Text.Length == 10)   //10 Rakamdan az ise dağıt
                {
                    string str = txt_hes.Text.Substring(0, 4);
                    string str2 = txt_hes.Text.Substring(4, 4);
                    string str3 = txt_hes.Text.Substring(8, 2);
                    txt_hes.Text = str;
                    textBox1.Text = str2;
                    textBox2.Text = str3;
                }
                else if (txt_hes.Text.Length > 10 && txt_hes.Text.Length <12)  //10 Rakamdan fazla ise ayır ve dağıt
                {
                    String[] spearator = { " ", "-", "," };
                    Int32 count = 3;
                    String[] strlist = txt_hes.Text.Split(spearator, count,
                    StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < count; i++)
                    {
                        if (i < 1)
                        {
                            txt_hes.Text = strlist[i];
                        }
                        else if (i < 2)
                        {
                            textBox1.Text = strlist[i];
                        }
                        else
                        {
                            textBox2.Text = strlist[i];
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Girilen Veri Türü Hatalı ! \n \n Destek Kaydında Lütfen Hata Kodunu Belirtiniz", "Hata F1DTERR");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 4)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txt_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Program.OpenWeb();
        }

        private void about_Click(object sender, EventArgs e)
        {
            Detail dt = new Detail();
            dt.ShowDialog();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            Program.AlpemixCM();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.count = 0;
            Properties.Settings.Default.Save();
            sayacdt.Text = Properties.Settings.Default.count.ToString();
        }

        private void btn_ok_MouseEnter(object sender, EventArgs e)
        {
            btn_ok.BackColor = Color.FromArgb(192,0,0);
            btn_ok.ForeColor = Color.White;
        }

        private void btn_ok_MouseLeave(object sender, EventArgs e)
        {
            btn_ok.BackColor = Color.White;
            btn_ok.ForeColor = Color.Black;
        }
    }
}
