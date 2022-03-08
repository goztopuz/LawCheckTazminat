using System;
namespace LawCheckTazminat.Models
{
    public class KidemIhbar
    {
        public string Id { get; set; }
        public string kisiId {get;set;}
        public string eskId { get; set; }
        public bool duzenlemede { get; set; }
        public string Aciklama { get; set; }

        public decimal BrutUcret { get; set; }
        public decimal EkUcretler { get; set; }
        public decimal GiydirilmisBrutUcret { get; set; }
       
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public int IhbarHaftaSayisi { get; set; }
        public string HespSuresi { get; set; }

        public int VergiYili { get; set; }
        public decimal VergiMatrah { get; set; }
        public decimal EkGelir { get; set; }

        public string ekBilgi1 { get; set; }
        public string ekBilgi2 { get; set; }
        public decimal ekBilgi3 { get; set; }
        public double ekBilgi4 { get; set; }

        public decimal IhbarSonucToplam { get; set; }
        public decimal IhbarSonucDV{ get; set; }
        public decimal IhbarSonucGV{ get; set; }
        public decimal IhbarSonucNet { get; set; }

        public decimal KidemSonucToplam { get; set; }
        public decimal KidemSonucDV { get; set; }
        public decimal KidemSonucNet { get; set; }

    }
}
