using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Hastane_Otomasyon_Sistemi.SqlBaglanti;
using Hastane_Otomasyon_Sistemi.HastaBilgi;



namespace Hastane_Otomasyon_Sistemi
{
    public partial class HastaListesi : Form
    {
        public HastaListesi()
        {
            InitializeComponent();
        }

        HastaBilgileri hasta = new HastaBilgileri();


        private void btnGeri2_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            this.Hide();
            anaSayfa.ShowDialog();
        }

        private void HastaListesi_Load(object sender, EventArgs e)
        {
            GuncelGridiVer();
        }

        
        public void btnSil_Click(object sender, EventArgs e)
        {
            SatirSil();
            GuncelGridiVer();
        }

       
        public void GuncelGridiVer()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            string komut = @"select TCNo,Ad,Soyad,Cinsiyet,EPosta,CepTel,DogumYeri,DogumTarihi,AnneAdi,BabaAdi,YakinTelNo from HastaBilgileri_tbl";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(komut, sqlConnection);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = dt;
        }
        public void SatirSil()
        {

            DialogResult kayıtctrl = new DialogResult();
            kayıtctrl = MessageBox.Show(@" Seçtiğiniz satırı silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (kayıtctrl == DialogResult.Yes)
            {
                SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                try
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select HastaBilgileriID from HastaBilgileri_tbl", sqlConnection);
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    hasta.hastaBilgileriID = (int)dt.Rows[dataGridView1.SelectedRows[0].Index][0];

                    string komut = @"delete from HastaBilgileri_tbl where HastaBilgileriID = @hastaid";
                    SqlCommand sqlCommand = new SqlCommand(komut, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@hastaid", hasta.hastaBilgileriID);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme işlemi gerçekleşirken bir hata oluştu.", "Uyarı");
                }
               
            }
        }
    }
}
