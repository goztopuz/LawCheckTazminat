using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Transactions;

namespace LawCheckTazminat.Models
{
  public  class DestektenYoksunluk
    {

        public DestektenYoksunluk()
        {
            this.DestekYoksunlukYakinlar = new ObservableCollection<Yakin>();
            this.DosyaDestektenYoksunlukMasraf = new ObservableCollection<Masraf>();
            this.DestektekYoksunlukMaaslar = new ObservableCollection<Maas>();

        }

        public bool Duzenlemede { get; set; } = false;
        public string Id { get; set; }
        public string musteriId { get; set; }
        public string musteriIsim { get; set; }
        public string vefatEden { get; set; }

        public string aciklama { get; set; }
      
        public string trafikKazasi { get; set; } = "Hayır";
        public bool trafikPMF { get; set; } = true;
        public bool trafikTRh { get; set; } = false;

        public DateTime vefatTarihi { get; set; } = DateTime.Now;
        public string ad { get; set; }
        public string soyad { get; set; }
        public string cinsiyet { get; set; } = "Erkek";
        public DateTime dogumTarihi { get; set; } = DateTime.Now.AddYears(-30);

        public int emeklilikYasi { get; set; } = 60;
        public string calismaDurumu { get; set; } = "Çalışıyor";
        public string meslek { get; set; }
        public string meslekAciklama { get; set; }

        public string askerlikYapti { get; set; } = "Yaptı";
        public int askerlikSuresi { get; set; } = 12;
        public int askereGidisAy { get; set; }
        public int askereGidisYil { get; set; }

        public decimal esEvlenmeYuzdesi { get; set; }
        public bool esEvlenmeElleHesapla { get; set; } = false;
        public decimal esEvlenmeElle { get; set; } = 0;


        public DateTime raporTarihi { get; set; } = DateTime.Now;
        public string paylastirmaTur { get; set; } = "Progresif Rant";
        public double kusurOrani { get; set; } = 100;
        public string yasamTablosu { get; set; } = "PMF-1931";
        public int yasiYuvarla { get; set; } = 0;


        public decimal isleyecekMaas { get; set; }
        public decimal emekliMaas { get; set; }

        public bool AgiDus { get; set; } = false;





        public bool BekarCocukDurum { get; set; } = false;
        public int BekarCocukEvlenmeYas { get; set; } = 27;
        public int BekarCocukCocukSayisi { get; set; } = 2;
        public int BekarCouckCocukIlkSene { get; set; } = 2;
        public int BekarCocukCocuklarArasiYil { get; set; } = 2;
        public int BearCocukCocukDestekCikisYasi { get; set; } = 25;


        public DateTime BekarCocuk_EvlenmeTarihi { get; set; }
        public decimal BekarCocuk_18OncesiMaas { get; set; }

        public int BekarCocuk_CalismaBasYasi { get; set; } = 23;
        public DateTime BekarCocuk_CalismaBasTarihi { get; set; }

        public decimal BekarCocuk_GelecekCalismaUcreti { get; set; }

        public bool BekarCocuk_SuAnCalisiyor { get; set; } = false;
        public decimal BekarCocuk_SuAnkiUcret { get; set; }



        public ObservableCollection<Yakin>DestekYoksunlukYakinlar { get; set; }
        public ObservableCollection<Masraf> DosyaDestektenYoksunlukMasraf { get; set; }
        public ObservableCollection<Maas> DestektekYoksunlukMaaslar { get; set; }
   
    }

    public class Masraf
    {
        public string Id { get; set; }
        public string dosyaId { get; set; }

        public int masrafTur1 { get; set; }
        public string masraftur2 { get; set; }

        public decimal miktar { get; set; }

        public DateTime tarihBas { get; set; }
        public DateTime tarihBit { get; set; }

        public string odemeTur { get; set; }

        public string ekBilgi1 { get; set; }
        public string ekBilgi2 { get; set; }
        
        
      public DestektenYoksunluk DestektenYoksunluk { get; set; }
    }

    public class Maas
    {
        public string Id { get; set; }
        public string dosyaId { get; set; }

        public decimal brutMaas { get; set; }
        public decimal netMaas { get; set; }

        public DateTime  yilBas { get; set; }
        public DateTime yilBit { get; set; }

        public string yil { get; set; }
        public int yilBolum { get; set; }

        public string ekBilgi1 { get; set; }
        public string ekBilgi2 { get; set; }
        public Nullable<double> ekBilgi3 { get; set; }
        public decimal ekBilgi4 { get; set; }
       
      public DestektenYoksunluk DestektenYoksunluk { get; set; }

    }

    public class Yakin
    {
        public string Id { get; set; }
        public string dosyaId { get; set; }
        public string yakinlik { get; set; }

        public string ad { get; set; }
        public string soyad { get; set; }
        public string cinsiyet { get; set; } = "Kadın";
        public DateTime dogumTarihi { get; set; } = DateTime.Now.AddYears(-20);
        
        public string meslek { get; set; }
        public string meslekAciklama { get; set; }

        public string okumaDurum { get; set; } = "Üniversite";
        public int okulBitisYas { get; set; } = 25;

        public bool esEvlenmeDurumAyim { get; set; } = true;
        public bool esEvlenmeDurumMoser { get; set; } = false;
        public bool esEvlenmeDurumStaauffer { get; set; } = false;
        public string esCalisiyorMu { get; set; } = "ÇalışMIyor";

        public string anneBabaMaasDurumu { get; set; } = "Yok";

        public DestektenYoksunluk DestektenYoksunluk { get; set; }
    }
        
}
