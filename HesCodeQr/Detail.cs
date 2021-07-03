using HesCodeQr.Properties;
using System;
using System.Windows.Forms;

namespace HesCodeQr
{
    public partial class Detail : MetroFramework.Forms.MetroForm
    {
        public Detail()
        {
            InitializeComponent();
            licanse();
        }

        public void licanse()
        {
            if (Settings.Default.SerialKey != "")
            {
                string substr = Settings.Default.SerialKey;
                serialdt.Text = substr.Substring(0, 2) + "******-********-********";
                serialdt.Enabled = false;
                metroButton1.Enabled = false;
            }
            else
            {
                serialdt.Enabled = true;
                metroButton1.Enabled = true;
            }
            versiondt.Text = Settings.Default.Version;
            user.Text = Settings.Default.user;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Program.OpenWeb();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Program.OpenWeb();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Settings.Default.SerialKey = serialdt.Text;
            Settings.Default.Save();
            licanse();
        }
    }
}
