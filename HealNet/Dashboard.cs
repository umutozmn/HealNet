using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealNet
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {
            InitializeComponent();
        }


        private void AnaEkran_Load(object sender, EventArgs e)
        {
        }

       

        private void btnHastaKayit_Click(object sender, EventArgs e)
        {
            btnTemizle hastaKayit = new btnTemizle();
            hastaKayit.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();

        }
    }
}
