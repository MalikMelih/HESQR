using HesCodeQr.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace HesCodeQr
{
    public partial class SplashScreen : MetroFramework.Forms.MetroForm
    {
        int loading = 0;
        public SplashScreen()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(26, 26, 26);
            //this.BackColor = Color.Black; //Arkaplan Saydamlaştırma
            //TransparencyKey = Color.Black;
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            Program.OpenWeb();
        }

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form1.loading != 90)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (loading <= 100 && loading >= 52 && (Settings.Default.SerialKey == ""))
            {
                timer1.Stop();
                MessageBox.Show("Lisans Anahtarı Eksik, Lütfen Lisans Anahtarınızı Giriniz !", "Lisans Sorunu");
            }
            if (loading <= 100)
            {
                if (loading <= 10)
                {
                    openlbl.Text = "Sürüm Kontrol Ediliyor . . . ";
                }
                else if (loading <= 50 && loading > 10)
                {
                    openlbl.Text = "Lisans Kontol Ediliyor . . . ";
                }
                else if (loading <= 60 && loading > 50 && (Settings.Default.SerialKey != ""))
                {
                    openlbl.Text = "Kullanıcı Kontol Ediliyor . . . ";
                }
                else
                {
                    openlbl.Text = "Başlatılıyor";
                }
                loading++;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void SplashScreen_Load(object sender, System.EventArgs e)
        {
            timer1.Interval = 70;
            timer1.Start();
        }
    }
}
