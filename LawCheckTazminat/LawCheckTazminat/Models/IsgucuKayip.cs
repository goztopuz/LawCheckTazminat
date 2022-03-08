using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LawCheckTazminat.Models
{
  public  class IsgucuKayip
    {

        public IsgucuKayip()
        {
            this.IsGucuKayipOranlar = new ObservableCollection<KayipOran>();
            this.IsGucuKayipYakinlar = new ObservableCollection<YakinIsgucu>();
            this.IsGucuKayipMasraflar = new ObservableCollection<MasrafIsgucu>();
            this.IsGucuKayipMaaslar = new ObservableCollection<MaasIsGucu>();
        }


        public string Id { get; set; }
        public bool duzenlemede { get; set; } = false;

        public string EskiId { get; set; }


        public string aciklama { get; set; }


        public string trafikKazasi { get; set; } = "Hayır";
        public bool trafikPMF { get; set; } = true;
        public bool trafikTRh { get; set; } = false;

        public string ad { get; set; }
        public string soyad { get; set; }
        public string musteriId { get; set; }
        public DateTime dogumTarihi { get; set; } = DateTime.Now.AddYears(-30);
        public DateTime kazaTarihi { get; set; }= DateTime.Now;
        public string cinsiyet { get; set; } = "Erkek";


        public DateTime raporTarihi { get; set; } = DateTime.Now;
        public string paylastirmaTur { get; set; } = "Progresif Rant";
        public double kusurOrani { get; set; } = 100;
        public string yasamTablosu { get; set; } = "PMF-1931";
        public int yasiYuvarla { get; set; } = 0;


        public decimal isleyecekMaas { get; set; }
        public decimal emekliMaas { get; set; }

        public bool AgiDus { get; set; } = false;


        public int emeklilikYasi { get; set; } = 60;
        public string calismaDurumu { get; set; } = "Çalışıyor";
        public string meslek { get; set; }
        public string meslekAciklama { get; set; }

        public string askerlikYapti { get; set; } = "Yaptı";
        public int askerlikSuresi { get; set; } = 12;
        public int  askereGidisYil { get; set; }
       public int askereGidisAy { get; set; }


        public double kayipOrani { get; set; }
        public string hastaneYatisi { get; set; } = "Yok";
        public DateTime hastaneYatisTarihi { get; set; }
        public DateTime hastaneCikisTarihi { get; set; }
        public DateTime hastaneAciklama { get; set; }

        

        public ObservableCollection<KayipOran> IsGucuKayipOranlar { get; set; }
        public ObservableCollection<YakinIsgucu> IsGucuKayipYakinlar { get; set; }
        public ObservableCollection<MasrafIsgucu> IsGucuKayipMasraflar { get; set; }
        public ObservableCollection<MaasIsGucu> IsGucuKayipMaaslar { get; set; }

    }


    public class KayipOran
    {
        public string id { get; set; }

        public string dosyaId { get; set; }

        public DateTime  baslangicTarihi { get; set; }
        public DateTime cikisTarihi { get; set; }
        public double kayipOrani { get; set; }
        public string aciklama { get; set; }
        public string aciklama2 { get; set; }

        public IsgucuKayip IsgucuKayip { get; set; }

    }

    public class YakinIsgucu
    {
        public string Id { get; set; }
        public string dosyaId { get; set; }
        public string yakinlik { get; set; }

        public string ad { get; set; }
        public string soyad { get; set; }
        public string cinsiyet { get; set; } = "Kadın";
        public DateTime dogumTarihi { get; set; }

        public DateTime cikisTar{get;set;}

        public string meslek { get; set; }
        public string meslekAciklama { get; set; }

        public string okumaDurum { get; set; } = "Üniversite";
        public int okulBitisYas { get; set; } = 25;

        public bool esEvlenmeDurumAyim { get; set; } = true;
        public bool esEvlenmeDurumMoser { get; set; } = false;
        public bool esEvlenmeDurumStaauffer { get; set; } = false;
        public string esCalisiyorMu { get; set; } = "ÇalışMIyor";

        public string anneBabaMaasDurumu { get; set; } = "Yok";

        public IsgucuKayip IsgucuKayip { get; set; }
    }

    public class MasrafIsgucu
    {
        public string Id { get; set; }
        public string dosyaId { get; set; }

        public int masrafTur1 { get; set; }
        public string masraftur2 { get; set; }

        public decimal miktar { get; set; }

        public DateTime tarihBas { get; set; } = DateTime.Now;
        public DateTime tarihBit { get; set; }

        public string odemeTur { get; set; }

        public string ekBilgi1 { get; set; }
        public string ekBilgi2 { get; set; }


        public IsgucuKayip IsgucuKayip { get; set; }
    }

    public class MaasIsGucu
    {
        public string Id { get; set; }
        public string dosyaId { get; set; }

        public decimal brutMaas { get; set; }
        public decimal netMaas { get; set; }

        public DateTime yilBas { get; set; }
        public DateTime yilBit { get; set; }

        public string yil { get; set; }
        public int yilBolum { get; set; }

        public string ekBilgi1 { get; set; }
        public string ekBilgi2 { get; set; }
        public Nullable<double> ekBilgi3 { get; set; }
        public decimal ekBilgi4 { get; set; }

        public IsgucuKayip IsgucuKayip { get; set; }

    }


}
