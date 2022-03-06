using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hastane_Otomasyon_Sistemi.SqlBaglanti;
using Hastane_Otomasyon_Sistemi.HastaBilgi;
using Hastane_Otomasyon_Sistemi.Adminler;
using System.Data.SqlClient;

namespace Hastane_Otomasyon_Sistemi
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }
        Admin admin = new Admin();

        public bool girisKontrol(string kullanici, string sifre)
        {
            bool isValid = false;
            SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Admin_tbl",sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            admin.kullaniciAdi = dataTable.Rows[0][1].ToString();
            admin.sifre = dataTable.Rows[0][2].ToString();

            if (kullanici == admin.kullaniciAdi && sifre == admin.sifre)
            {
                isValid = true;
                MessageBox.Show("Giriş başarılı!");
            }
            return isValid;
        }
        private void btnGiris_Click(object sender, EventArgs e)
        {
            bool cntrl = girisKontrol(txtKullaniciAdi.Text, txtSifre.Text);
            try
            {
                if (cntrl == false)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "UYARI");
                    txtKullaniciAdi.Text = "";
                    txtSifre.Text = "";
                    txtKullaniciAdi.Focus();
                }
                else
                {
                    AnaSayfa anaSayfa = new AnaSayfa();
                    this.Hide();
                    anaSayfa.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void txtKullaniciAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                bool cntrl = girisKontrol(txtKullaniciAdi.Text, txtSifre.Text);
                if (cntrl == false)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "UYARI");
                    txtKullaniciAdi.Text = "";
                    txtSifre.Text = "";
                    txtKullaniciAdi.Focus();
                }
                else
                {
                    AnaSayfa anaSayfa = new AnaSayfa();
                    this.Hide();
                    anaSayfa.ShowDialog();
                }
            }
        }

        private void txtSifre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                bool cntrl = girisKontrol(txtKullaniciAdi.Text, txtSifre.Text);
                if (cntrl == false)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "UYARI");
                    txtKullaniciAdi.Text = "";
                    txtSifre.Text = "";
                    txtKullaniciAdi.Focus();
                }
                else
                {
                    AnaSayfa anaSayfa = new AnaSayfa();
                    this.Hide();
                    anaSayfa.ShowDialog();
                }
            }
        }
    }
}
