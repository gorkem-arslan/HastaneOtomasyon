using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Hastane_Otomasyon_Sistemi.HastaBilgi;
using Hastane_Otomasyon_Sistemi.SqlBaglanti;

namespace Hastane_Otomasyon_Sistemi
{
    public partial class RandevuKayit : Form
    {
        public RandevuKayit()
        {
            InitializeComponent();
        }
        HastaBilgileri hasta = new HastaBilgileri();
        public void RandevuKayit_Load(object sender, EventArgs e)
        {
            CboKlinik();

        }
        public void CboKlinik()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select KlinikAdi from Klinik_tbl", sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow item in dataTable.Rows)
            {
                cmbKlinik.Items.Add(item["KlinikAdi"]);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa();
        }
        public void AnaSayfa()
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            this.Hide();
            anaSayfa.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MevcutBilgileriGetirVeGüncelle();
            //GuncellemeIslemi(); //updatede bi sıkıntı var ona bak
            /*
             UPDATE HastaBilgileri_tbl SET TCNo=@tcno,Ad=@ad,Soyad=@soyad,Cinsiyet=@cinsiyet,
EPosta=@eposta,CepTel=@ceptel,DogumYeri=@dogumyeri,DogumTarihi=@dogumtarihi,AnneAdi=@anneadi,
BabaAdi=@babaadi,YakinTelNo=@yakintelno
            */
            /*
             INSERT INTO HastaBilgileri_tbl(TCNo,Ad,Soyad,Cinsiyet,EPosta,CepTel,DogumYeri,DogumTarihi,AnneAdi,BabaAdi,YakinTelNo)
VALUES(@tcno,@ad,@soyad,@cinsiyet,@eposta,@ceptel,@dogumyeri,@dogumtarihi,@anneadi,@babaadi,@yakintelno)

             */
        }

        public void MevcutBilgileriGetirVeGüncelle()
        {
            HastaBilgileri hasta = new HastaBilgileri();
            if (tcKontrol() == true)
            {
                string komut2 = @"INSERT INTO HastaBilgileri_tbl(TCNo,Ad,Soyad,Cinsiyet,EPosta,CepTel,DogumYeri,DogumTarihi,AnneAdi,BabaAdi,YakinTelNo) VALUES(@tcno,@ad,@soyad,@cinsiyet,@eposta,@ceptel,@dogumyeri,@dogumtarihi,@anneadi,@babaadi,@yakintelno)";

                SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
                SqlCommand ilkBilgiler = new SqlCommand(komut2,sqlConnection);

                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                ilkBilgiler.Parameters.AddWithValue("@tcno",txtTCNo.Text);
                ilkBilgiler.Parameters.AddWithValue("@ad", txtAd.Text);
                ilkBilgiler.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                ilkBilgiler.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
                ilkBilgiler.Parameters.AddWithValue("@eposta", txtEPosta.Text);
                ilkBilgiler.Parameters.AddWithValue("@ceptel", txtCepTel.Text);
                ilkBilgiler.Parameters.AddWithValue("@dogumyeri", txtDogumYeri.Text);
                ilkBilgiler.Parameters.AddWithValue("@dogumtarihi", txtDogumTarihi.Text);
                ilkBilgiler.Parameters.AddWithValue("@anneadi", txtAnneAdi.Text);
                ilkBilgiler.Parameters.AddWithValue("@babaadi", txtBabaAdi.Text);
                ilkBilgiler.Parameters.AddWithValue("@yakintelno", txtYakinTel.Text);
                int kayitDurumu2 = ilkBilgiler.ExecuteNonQuery();
                

                DialogResult kayıtctrl = new DialogResult();
                kayıtctrl = MessageBox.Show(@" Girmiş olduğunuz T.C. Kimlik Numarası sistemde kayıtlı. Bilgileri görmek ister misiniz ?", "Uyarı", MessageBoxButtons.YesNo);
                if (kayıtctrl == DialogResult.Yes)

                {
                    int kayitDurumu = 0;

                    #region Girilen TC numarasına ait bilgileri getirir.
                   
                    
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(komut2, sqlConnection);
                    DataTable mevcutBilgilerTablo = new DataTable();
                    sqlDataAdapter.Fill(mevcutBilgilerTablo);

                    txtTCNo.Text = mevcutBilgilerTablo.Rows[0][0].ToString();
                    txtAd.Text = mevcutBilgilerTablo.Rows[0][1].ToString();
                    txtSoyad.Text = mevcutBilgilerTablo.Rows[0][2].ToString();
                    cmbCinsiyet.Text = mevcutBilgilerTablo.Rows[0][3].ToString();
                    txtEPosta.Text = mevcutBilgilerTablo.Rows[0][4].ToString();
                    txtCepTel.Text = mevcutBilgilerTablo.Rows[0][5].ToString();
                    txtDogumYeri.Text = mevcutBilgilerTablo.Rows[0][6].ToString();
                    txtDogumTarihi.Text = mevcutBilgilerTablo.Rows[0][7].ToString();
                    txtAnneAdi.Text = mevcutBilgilerTablo.Rows[0][8].ToString();
                    txtBabaAdi.Text = mevcutBilgilerTablo.Rows[0][9].ToString();
                    txtYakinTel.Text = mevcutBilgilerTablo.Rows[0][10].ToString();
                    #endregion

                    DialogResult guncelleKontrol = new DialogResult();
                    guncelleKontrol = MessageBox.Show(@"Girmiş olduğunuz T.C. Kimlik Numarasına ait bilgileri güncellemek ister misiniz ?", "Uyarı",MessageBoxButtons.YesNo);
                    if (guncelleKontrol == DialogResult.Yes)
                    {
                        string komut1 = @"INSERT INTO HastaBilgileri_tbl(TCNo,Ad,Soyad,Cinsiyet,EPosta,CepTel,DogumYeri,DogumTarihi,AnneAdi,BabaAdi,YakinTelNo) VALUES(@tcno,@ad,@soyad,@cinsiyet,@eposta,@ceptel,@dogumyeri,@dogumtarihi,@anneadi,@babaadi,@yakintelno)";
                        try
                        {
                            if (sqlConnection.State == ConnectionState.Closed)
                            {
                                sqlConnection.Open();
                            }
                            SqlCommand sqlCommand = new SqlCommand(komut1, sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@tcno", txtTCNo.Text);
                            sqlCommand.Parameters.AddWithValue("@ad", txtAd.Text);
                            sqlCommand.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                            sqlCommand.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
                            sqlCommand.Parameters.AddWithValue("@eposta", txtEPosta.Text);
                            sqlCommand.Parameters.AddWithValue("@ceptel", txtCepTel.Text);
                            sqlCommand.Parameters.AddWithValue("@dogumyeri",txtDogumYeri.Text);
                            sqlCommand.Parameters.AddWithValue("@dogumtarihi", txtDogumTarihi.Text);
                            sqlCommand.Parameters.AddWithValue("@anneadi", txtAnneAdi.Text);
                            sqlCommand.Parameters.AddWithValue("@babaadi", txtBabaAdi.Text);
                            sqlCommand.Parameters.AddWithValue("@yakintelno", txtYakinTel.Text);
                            kayitDurumu = sqlCommand.ExecuteNonQuery();
                            MessageBox.Show("Kayıt tamamlanmıştır.");
                            Temizlik();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            sqlConnection.Dispose();
                            sqlConnection.Close();
                        }
                    }
                }
            }
        }

        //string komut1 = @"INSERT INTO HastaBilgileri_tbl(TCNo,Ad,Soyad,Cinsiyet,EPosta,CepTel,DogumYeri,DogumTarihi,AnneAdi,BabaAdi,YakinTelNo) VALUES(@tcno,@ad,@soyad,@cinsiyet,@eposta,@ceptel,@dogumyeri,@dogumtarihi,@anneadi,@babaadi,@yakintelno)";
        //SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
        //try
        //{
        //    if (sqlConnection.State == ConnectionState.Closed)
        //    {
        //        sqlConnection.Open();
        //    }
        //    SqlCommand sqlCommand = new SqlCommand(komut1, sqlConnection);
        //    sqlCommand.Parameters.AddWithValue("@tcno", txtTCNo.Text);
        //    sqlCommand.Parameters.AddWithValue("@ad", txtAd.Text);
        //    sqlCommand.Parameters.AddWithValue("@soyad", txtSoyad.Text);
        //    sqlCommand.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
        //    sqlCommand.Parameters.AddWithValue("@eposta", txtEPosta.Text);
        //    sqlCommand.Parameters.AddWithValue("@ceptel", txtCepTel.Text);
        //    sqlCommand.Parameters.AddWithValue("@dogumyeri", txtDogumYeri.Text);
        //    sqlCommand.Parameters.AddWithValue("@dogumtarihi", txtDogumTarihi.Text);
        //    sqlCommand.Parameters.AddWithValue("@anneadi", txtAnneAdi.Text);
        //    sqlCommand.Parameters.AddWithValue("@babaadi", txtBabaAdi.Text);
        //    sqlCommand.Parameters.AddWithValue("@yakintelno", txtYakinTel.Text);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.ToString());
        //}
        //finally
        //{
        //    sqlConnection.Dispose();
        //    sqlConnection.Close();
        //}

        public bool tcKontrol()
        {
            bool durum = false;
            
            List<string> tcListesi = new List<string>();
            SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlDataAdapter sqlDataAdapterTC = new SqlDataAdapter("select TCNo from HastaBilgileri_tbl",sqlConnection);
            DataTable dataTableTC = new DataTable();
            sqlDataAdapterTC.Fill(dataTableTC);
            for (int i = 0; i < dataTableTC.Rows.Count; i++)
            {
                tcListesi.Add(dataTableTC.Rows[i][0].ToString());
            }
            
            if (tcListesi.Contains(txtTCNo.Text))
            {
                durum = true;
            }
            return durum;
        }
        public void Temizlik()
        {
            txtTCNo.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbCinsiyet.Text = "";
            txtDogumYeri.Text = "";
            txtDogumTarihi.Text = "";
            txtAnneAdi.Text = "";
            txtBabaAdi.Text = "";
            txtCepTel.Text = "";
            txtYakinTel.Text = "";
            txtEPosta.Text = "";
            cmbKlinik.Text = "";
        }

        public void GuncellemeIslemi()
        {
            DialogResult kayıtctrl = new DialogResult();
            kayıtctrl = MessageBox.Show(@" İlgili kayıt işlemini onaylıyor musunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (kayıtctrl == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(Connection.sqlCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int kayitDurumu = 0;
                string komut1 = @"INSERT INTO HastaBilgileri_tbl (TCNo,Ad,Soyad,Cinsiyet,EPosta,CepTel,DogumYeri,DogumTarihi,AnneAdi,BabaAdi,YakinTelNo) VALUES (@tcno,@ad,@soyad,@cinsiyet,@eposta,@ceptel,@dogumyeri,@dogumtarihi,@anneadi,@babaadi,@yakintelno)";
                SqlConnection sqlConnection = new SqlConnection(Connection.sqlCon);
                try
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    SqlCommand sqlCommand = new SqlCommand(komut1, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@tcno", txtTCNo.Text);
                    sqlCommand.Parameters.AddWithValue("@ad", txtAd.Text);
                    sqlCommand.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                    sqlCommand.Parameters.AddWithValue("@cinsiyet", cmbCinsiyet.Text);
                    sqlCommand.Parameters.AddWithValue("@eposta", txtEPosta.Text);
                    sqlCommand.Parameters.AddWithValue("@ceptel", txtCepTel.Text);
                    sqlCommand.Parameters.AddWithValue("@dogumyeri", txtDogumYeri.Text);
                    sqlCommand.Parameters.AddWithValue("@dogumtarihi", txtDogumTarihi.Text);
                    sqlCommand.Parameters.AddWithValue("@anneadi", txtAnneAdi.Text);
                    sqlCommand.Parameters.AddWithValue("@babaadi", txtBabaAdi.Text);
                    sqlCommand.Parameters.AddWithValue("@yakintelno", txtYakinTel.Text);
                    kayitDurumu = sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Kayıt tamamlanmıştır.");
                    Temizlik();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlConnection.Dispose();
                    sqlConnection.Close();
                }
            }
        }


        private void txtCepTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtYakinTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
