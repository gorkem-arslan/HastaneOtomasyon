using System;
using System.Collections.Generic;
using System.Text;
namespace Hastane_Otomasyon_Sistemi.HastaBilgi
{
    public class HastaBilgileri
    {
        public string tcNo { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string cinsiyet { get; set; }
        public string dogumYeri { get; set; }
        public string dogumTarihi { get; set; }
        public string babaAdi { get; set; }
        public string anneAdi { get; set; }
        public string cepTel { get; set; }
        public string yakinTel { get; set; }
        public string ePostaAdresi { get; set; }
        public string klinik { get; set; }
        public int hastaBilgileriID { get; set; }
    }
}
