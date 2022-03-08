using System;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.Models
{
    public class GeceCalisma
    {
        public string Id { get; set; }
        public string eskiId { get; set; }
        public string KisiId { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool duzenlemede { get; set; }


        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }

        public DateTime BasSaat { get; set; }
        public DateTime BitSaat { get; set; }


        public bool PSal { get; set; }
        public bool SCar { get; set; }
        public bool CPer { get; set; }
        public bool PCum { get; set; }
        public bool CCmt { get; set; }
        public bool CPzr { get; set; }
        public bool PPzt { get; set; }


        public string TatilGunu { get; set; } = "Pazar";
        public bool TatilVardiyaiDus { get; set; }
        public bool TatilVardiyaEkle { get; set; }


        public double HCalismaSaatMiktari { get; set; }
        public double GeceFazlaSaat { get; set; }
        public double GeceFazlaSaatElle { get; set; }
        public double DinlenmeSure { get; set; }



        // --------AGİ
        public string EsCalisiyor { get; set; } = "ÇalışMIyor";
        public int CocukSayisi { get; set; } = 0;
        public string Bekar { get; set; } = "Evli";


        public decimal VergiMatrah { get; set; }
        public int VergiYili { get; set; }


        // ---------------

        public decimal Toplam { get; set; }
        public decimal SGK { get; set; }
        public decimal Vergi { get; set; }
        public decimal DamgaV { get; set; }
        public decimal Issizlik { get; set; }
        public decimal Net { get; set; }





        //izinler
        public ObservableCollection<GeceMesaiKisiIzinKayitlari> IzinKaytilariBilgi { get; set; }
        public ObservableCollection<MaasGeceMesai> MaasBilgi { get; set; }

        public ObservableCollection<GeceMesaiHaftalar> HaftalarBilgi { get; set; }

        public ObservableCollection<HesaptanDusecekTarihler> GeceDusecekTarihler { get; set; }

        public GeceCalisma()
        {


            MaasBilgi = new ObservableCollection<MaasGeceMesai>();

            IzinKaytilariBilgi = new ObservableCollection<GeceMesaiKisiIzinKayitlari>();

            HaftalarBilgi = new ObservableCollection<GeceMesaiHaftalar>();

            GeceDusecekTarihler = new ObservableCollection<HesaptanDusecekTarihler>(); 

        }




    }


    public class MaasGeceMesai
    {

        public string Id { get; set; }
        public string GeceMesaiId { get; set; }

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

        public GeceCalisma geceCalisma { get; set; }

    }

    public  class HesaptanDusecekTarihler
    {
        public string Id { get; set; }
        public string GeceMesaiId { get; set; }
        public string KisiId { get; set; }

        public bool Secili { get; set; }

        public DateTime Tarih { get; set; }

        public string Aciklama { get; set; }
        public string IzinTur { get; set; }

        public string EkBilgi1 { get; set; }
        public string EkBilgi2 { get; set; }

        public GeceCalisma geceCalisma { get; set; }

    }
    public class GeceMesaiKisiIzinKayitlari
    {
        public string Id { get; set; }

        public string GeceMesaiId { get; set; }
        public int Sira { get; set; }
        public string KisiId { get; set; }

        public DateTime BaslangicTar { set; get; }
        public DateTime BitisTar { set; get; }

        public string Aciklama { get; set; }
        public int IzinTur { get; set; }

        public string EkBilgi1 { get; set; }
        public string EkBilgi2 { get; set; }


        public string Izingun { get; set; }
        public bool HaftaSonuCalismayiCikar { get; set; }

        public bool AradakiResmiTatilleriCikar { get; set; }


        public GeceCalisma geceCalisma { get; set; }

    }

    public class GeceMesaiHaftalar
    {


        public string Id { get; set; }
        public int Sira { get; set; }

        public string GeceMesaiId { get; set; }

        public bool GeceMesaiVar { get; set; }


        public string GunIsimleri { get; set; }

        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }

        // Gün ve Saat İşlemleri
        public bool PSal { get; set; }
        public bool SCar { get; set; }
        public bool CPer { get; set; }
        public bool PCum { get; set; }
        public bool CCmt { get; set; }
        public bool CPzr { get; set; }
        public bool PPzt { get; set; }

        public DateTime TarPSal { get; set; }
        public DateTime TarSCar { get; set; }
        public DateTime TarCPer { get; set; }
        public DateTime TarPCum { get; set; }
        public DateTime TarCCmt { get; set; }
        public DateTime TarCPzr { get; set; }
        public DateTime TarPPzt { get; set; }

        public DateTime BitTarPSal { get; set; }
        public DateTime BitTarSCar { get; set; }
        public DateTime BitTarCPer { get; set; }
        public DateTime BitTarPCum { get; set; }
        public DateTime BitTarCCmt { get; set; }
        public DateTime BitTarCPzr { get; set; }
        public DateTime BitTarPPzt { get; set; }

        public DateTime BasSaatPSal { get; set; }
        public DateTime BasSaatSCar { get; set; }
        public DateTime BasSaatCPer { get; set; }
        public DateTime BasSaatPCum { get; set; }
        public DateTime BasSaatCCmt { get; set; }
        public DateTime BasSaatCPzr { get; set; }
        public DateTime BasSaatPPzt { get; set; }


        public DateTime BitSaatPSal { get; set; }
        public DateTime BitSaatSCar { get; set; }
        public DateTime BitSaatCPer { get; set; }
        public DateTime BitSaatPCum { get; set; }
        public DateTime BitSaatCCmt { get; set; }
        public DateTime BitSaatCPzr { get; set; }
        public DateTime BitSaatPPzt { get; set; }

        // Saatlik Ücret
        public decimal SaatlikPSal { get; set; }
        public decimal SaatlikSCar { get; set; }
        public decimal SaatlikCPer { get; set; }
        public decimal SaatlikPCum { get; set; }
        public decimal SaatlikCCmt { get; set; }
        public decimal SaatlikCPzr { get; set; }
        public decimal SaatlikPPzt { get; set; }


        // Giriş Çıkış saatlerinden saati hesapla
        // Saatlik Ucretle çarpıp net ücreti bul...
        public decimal UcretPSal { get; set; }
        public decimal UcretSCar { get; set; }
        public decimal UcretCPer { get; set; }
        public decimal UcretPCum { get; set; }
        public decimal UcretCCmt { get; set; }
        public decimal UcretCPzr { get; set; }
        public decimal UcretPPzt { get; set; }


        // Hesaplanan Saat
        public double NetSaatPSal { get; set; } = 0;
        public double NetSaatSCar { get; set; } = 0;
        public double NetSaatCPer { get; set; } = 0;
        public double NetSaatPCum { get; set; } = 0;
        public double NetSaatCCmt { get; set; } = 0;
        public double NetSaatCPzr { get; set; } = 0;
        public double NetSaatPPzt { get; set; } = 0;

        public double ToplamNetSaat { get; set; }
        // Hesaplanan Saat -ELLE
        public double NetSaatEllePSal { get; set; } = 0;
        public double NetSaatElleSCar { get; set; } = 0;
        public double NetSaatElleCPer { get; set; } = 0;
        public double NetSaatEllePCum { get; set; } = 0;
        public double NetSaatElleCCmt { get; set; } = 0;
        public double NetSaatelleCPzr { get; set; } = 0;
        public double NetSaatEllePPzt { get; set; } = 0;

        public double ToplamSaatElle { get; set; } = 0;

        // Hesaplanan Gece Calisma  Ücretleri..
        public decimal GeceUcret { get; set; }
        public decimal GeceMesaiUcret2 { get; set; }




        // Tarihler.....
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }

        public DateTime IsbasiSaati { get; set; }
        public DateTime IsbistisSaati { get; set; }


        // Maaş
        public decimal BrutUcret { get; set; }
        public decimal SaatlikUcret { get; set; }
        public decimal GunlukUcret { get; set; }

        // HaftaIcerisindekiSayı

        public double HesaplananFazlaMesaiSaati { get; set; }
        public double GerceklesenFazlaMesaiSaati { get; set; }
        public double GerceklesenFazlaMesaiSaati2 { get; set; }

        public GeceCalisma geceCalisma { get; set; }

    }


    public class GCGunBilgi
    {
        public string Id { get; set; }
        public DateTime BasTar { get; set; }
        public DateTime BitTar { get; set; }
        public string GunIsim { get; set; }
        
        public decimal GunlukUcret { get; set; }
        public decimal SaatSayi { get; set; }
        public decimal GunToplam { get; set; }

        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }


    }
}
