using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Hastane_Otomasyon_Sistemi
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void btnRandevuOlustur_Click(object sender, EventArgs e)
        {
            RandevuKayit randevuKayit = new RandevuKayit();
            this.Hide();
            randevuKayit.ShowDialog();
        }

        private void btnHastalariListele_Click(object sender, EventArgs e)
        {
            HastaListesi hastaListesi = new HastaListesi();
            this.Hide();
            hastaListesi.ShowDialog();
        }
    }
}
