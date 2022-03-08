using System;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.Models
{
    public class YillikIzin
    {

        public YillikIzin()
        {

            IzinKaytilariBilgi = new ObservableCollection<YillikIzinIzinKayitlari>();
            IzinGunleriBilgi = new ObservableCollection<YillikIzinIzinGunleri>();

            IzindekiResmiTatillerBilgi = new ObservableCollection<YYResmiTatil>();
            IzindekiHaftaIzniBilgi = new ObservableCollection<YYHaftaIzni>();
            CalisilanYillarBilgi = new ObservableCollection<YYYilBilgi>();
            HesapYillariBilgi = new ObservableCollection<YYYilBilgi2>();

        }

        public string Id { get; set; }
        public string EskiId { get; set; }
        public string kisiId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string Aciklama { get; set; }

        public bool duzenlemede { get; set; }


        public DateTime IseGirisTarihi { get; set; }
        public DateTime HesapBaslangicTar{ get; set; }
        public DateTime HesapBitisTar { get; set; }
        public DateTime DogumTarihi { get; set; } = new DateTime(2000, 1, 1);


        public bool YerAltiCalisani { get; set; } = false;

        public DateTime HesapTarihi { get; set; }

        public double HakEdilen { get; set; }
        public double Kullanilan { get; set; }

        public double Gun { get; set; }
        public double Gun2 { get; set; }

        public decimal BrutUcret { get; set; }
        public decimal NetUcret { get; set; }
        public decimal GunlukUcret { get; set; }

        public decimal VergiMatrah { get; set; }
        public int VergiYil { get; set; }

        public double ResmiTatilSayi { get; set; }
        public double HaftalikIzinSayi { get; set; }

        //Hesap Bilgi
        public string izinGunu { get; set; }
        public string TatilGunu { get; set; } = "Pazar";
        public double izindeAlinanRaporSayisi { get; set; } = 0;
        public bool haftalikIizinYillikIzindenDusme { get; set; } = true;
        public bool resmiTatilYillikIzindenDusme { get; set; } = true;
        public bool raporlarinYillikIzindenDusme { get; set; } = true;

        // ---------------

        public decimal Toplam { get; set; }
        public decimal SGK { get; set; }
        public decimal Vergi { get; set; }
        public decimal DamgaV { get; set; }
        public decimal Issizlik { get; set; }
        public decimal Net { get; set; }
        


        //izinler

        public ObservableCollection<YillikIzinIzinKayitlari> IzinKaytilariBilgi { get; set; }
        public ObservableCollection<YillikIzinIzinGunleri> IzinGunleriBilgi { get; set; }

        public ObservableCollection<YYResmiTatil> IzindekiResmiTatillerBilgi { get; set; }
        public ObservableCollection<YYHaftaIzni> IzindekiHaftaIzniBilgi { get; set; }
        public ObservableCollection<YYYilBilgi> CalisilanYillarBilgi { get; set; }
        public ObservableCollection<YYYilBilgi2> HesapYillariBilgi { get; set; }


    }


    //İzinler
    public class YillikIzinIzinGunleri
    {
        public string Id { get; set; }
        public string FazlaMesaiId { get; set; }

        public string KayitId { get; set; }
        public string KisiId { get; set; }

        public DateTime Tarih { get; set; }

        public string GunAdi { get; set; }

        public string IzinTur { get; set; }

        public string Aciklama { get; set; }

        public string Ek1 { get; set; }

        public double Ek2 { get; set; }

        public YillikIzin yillikIzin { get; set; }


    }

    public class YillikIzinIzinKayitlari
    {
        public string Id { get; set; }

        public string FazlaMesaiId { get; set; }
        public int Sira { get; set; }
        public string KisiId { get; set; }

        public DateTime BaslangicTar { set; get; }
        public DateTime BitisTar { set; get; }

        public string Aciklama { get; set; }
        public int IzinTur { get; set; }

        public string EkBilgi1 { get; set; }
        public string EkBilgi2 { get; set; }

        public YillikIzin yillikIzin { get; set; }

        public string Izingun { get; set; }
        public bool HaftaSonuCalismayiCikar { get; set; }

        public bool AradakiResmiTatilleriCikar { get; set; }


    }

    public class YYResmiTatil
    {
        public string Id { get; set; }
        public string YYId { get; set; }
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; }
        public string Ek { get; set; }

        public YillikIzin yillikIzin { get; set; }


    }

    public class YYHaftaIzni
    {
        public string Id { get; set; }
        public string YYId { get; set; }
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; }
        public string Ek { get; set; }

        public YillikIzin yillikIzin { get; set; }



    }

    public class YYYilBilgi
    {
        public string Id { get; set; }
        public string YYId { get; set; }
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }
        public int YilSay { get; set; }
        public int GunSay { get; set; }

        public YillikIzin yillikIzin { get; set; }

    }

    public class YYYilBilgi2
    {
        public string Id { get; set; }
        public string YYId { get; set; }
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }
        public int YilSay { get; set; }
        public int GunSay { get; set; }

        public YillikIzin yillikIzin { get; set; }


    }

}
