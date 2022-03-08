using System;
namespace LawCheckTazminat.Models
{
    public class Gelir
    {

        public string Id { get; set; }
        public DateTime Tarih { get; set; }

        public string TurId { get; set; }
        public string TurAciklama { get; set; }
        public string TurDiger { get; set; }

        public string DosyaNo { get; set; }
        public string DosyaAciklama { get; set; }

        public string Aciklama { get; set; }

        public decimal Miktar { get; set; }

        public string GelecekDonem { get; set; }
        public string KismiOdemeVar { get; set; }

        public int Alindi { get; set; }


        public string MuvekkilId { get; set; }
        public string MuvekkilAdi { get; set; }

        public string PersonelId { get; set; }
        public string PersonelAdi { get; set; }

        public string Ek1 { get; set; }
        public string Ek2 { get; set; }
        public double Ek3 { get; set; }
        public decimal Ek4 { get; set; }

        public DateTime Ek5 { get; set; }
        public DateTime Ek6 { get; set; }

    }

    public class KismiGelir
    {
        public string Id { get; set; }
        public string GelirId { get; set; }

        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }

        public string Miktar { get; set; }

        public string Ek1 { get; set; }
        public string Ek2 { get; set; }
        public double Ek3 { get; set; }
        public decimal Ek4 { get; set; }
        public DateTime Ek5 { get; set; }

    }

    public class GelirTur
    {
        public string Id { get; set; }
        public string Aciklama { get; set; }
        public string EkTur { get; set; }
        public string Ek1 { get; set; }
    }


}
