using System;
namespace LawCheckTazminat.Models
{

    public class DestekTalep
    {

        public string Id { get; set; }

        public string destekId { get; set; }

        public string musteriId { get; set; }

        public string eposta { get; set; }

        public string telefon { get; set; }

        public string iletisim { get; set; }

        public string isim { get; set; }

        public string konu { get; set; }

        public string soru { get; set; }

        public string durum { get; set; }

        public string resim1 {get;set;}

        public string resim2 { get; set; }

        public DateTime tarih { get; set; }

        public DateTime tarih2 { get; set; }

        public string yanitlayan { get; set; }

        public DateTime cevapTarihi { get; set; }

        public string anaSoruId { get; set; }

        public string ilgiliSoru { get; set; }

        public double ek1 { get; set; }

        public string ek2 { get; set; }

        public string ek3 { get; set; }

        public DateTime ek4 { get; set; }

        
    }

    public class IdBilgi
    {
        public string Id { get; set; }

        public string Id2 { get; set; }

    }

    public class DestekYanit
    {
        public string Id { get; set; }

        public string Id2 { get; set; }

        public string destekId { get; set; }

        public string musteriId { get; set; }

        public string yanitlayan { get; set; }

        public DateTime yanitTarihi { get; set; }

        public string yanitBaslik { get; set; }

        public string yanit { get; set; }

        public string soru { get; set; }

        public string durum { get; set; }

        public string durum2 { get; set; }

        public string resim1 { get; set; }

        public string resim2 { get; set; }

        public string anaSoruId { get; set; }

        public string ilgiliSoru { get; set; }

        public double ek1 { get; set; }

        public string ek2 { get; set; }

        public string ek3 { get; set; }

        public DateTime ek4 { get; set; }
      



    }
}
