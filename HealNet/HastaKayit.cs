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
    public partial class HastaKayit : Form
    {
        public HastaKayit()
        {
            InitializeComponent();
        }

        private void HastaKayit_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(0, 0, 0),   // koyu mavi
                Color.FromArgb(0, 0, 140), // açık mavi
                LinearGradientMode.Vertical
            );

            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
    }
}
