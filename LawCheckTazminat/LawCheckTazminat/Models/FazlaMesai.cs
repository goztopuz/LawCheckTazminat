using System;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.Models
{

    public class FazlaMesai
    {

        public FazlaMesai()
        {
            FMHaftalarBilgi = new ObservableCollection<FazlaMesaiHaftalar>();
            MaasBilgi = new ObservableCollection<MaasFazlaMesai>();

            ResmiTatilBilgi= new ObservableCollection<FazlaMesaiResmiTatil>();
          //  ResmiTatilHaftalarBilgi = new ObservableCollection<FazlaMesaiResmiTatilHaftalar>();

            IzinKaytilariBilgi = new ObservableCollection<FazlaMesaiKisiIzinKayitlari>();
            IzinGunleriBilgi = new ObservableCollection<FazlaMesaiKisiIzinGunleri>();


            HaftalikIzinHaftalarBilgi = new ObservableCollection<FazlaMesaiHaftalikIzinHaftalar>();
          
        }

        public bool duzenlemede { get; set; }

        //Listeler

        public ObservableCollection<FazlaMesaiHaftalar> FMHaftalarBilgi { get; set; }
        public ObservableCollection<MaasFazlaMesai> MaasBilgi { get; set; }

        //ResmiTatil
        public ObservableCollection<FazlaMesaiResmiTatil> ResmiTatilBilgi { get; set; }
     //   public ObservableCollection<FazlaMesaiResmiTatilHaftalar> ResmiTatilHaftalarBilgi { get; set; }


        // Haftalık İzin Bilgi..

        public ObservableCollection<FazlaMesaiHaftalikIzinHaftalar> HaftalikIzinHaftalarBilgi { get; set; }



        //izinler

        public ObservableCollection<FazlaMesaiKisiIzinKayitlari> IzinKaytilariBilgi { get; set; }
        public ObservableCollection<FazlaMesaiKisiIzinGunleri> IzinGunleriBilgi { get; set; }
        

        //------------------------------------
        // Alanlar

        public string Id { get; set; }

        public string EskiId { get; set; }

        public string Aciklama { get; set; }

        public string KisiId { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public DateTime HesapTarihi { get; set; }

        public DateTime BasTarih { get; set; }

        public DateTime BitTarih { get; set; }

        // Fazla Mesai
        public DateTime BasTarihMesai { get; set; }
        public DateTime BitTarihMesai { get; set; }

        //ResmiTatil
        public DateTime BasTarihResmiTatil { get; set; }
        public DateTime BitTarihResmiTatil { get; set; }
        public bool TumResmiTatllerdeCalisti { get; set; } = false;
        public bool TumDiniBayramlardaCalisti { get; set; } = false;

        //HaftaSonu
        public DateTime BasTarihHaftaSonu { get; set; }
        public DateTime BitTarihHaftaSonu { get; set; }

        public bool TumHaftaSonlarindaCalisti { get; set; } = true;



        public double HaftaCalismaSaat { get; set; }

        public double HaftaCalismaSaat2 { get; set;}

        public double SozlesmeCalismaSaat { get; set; } = 45;

        public double FazlaCalismaMiktar { get; set; }

        public double FazlaMesaiMiktar { get; set; }

        public double FazlaMesaiMiktar2 { get; set; }

        //-----------
        public decimal Toplam { get; set; }

        public decimal ToplamSGK { get; set; }

        public decimal ToplamVergi { get; set; }

        public decimal ToplamDamgaVergi { get; set; }

        public decimal ToplamIssizlik { get; set; }

        public decimal ToplamNet { get; set; }

        
        // HaftasonuToplamı
        public decimal ToplamHsonu { get; set; }
        public decimal ToplamHsonuSGK { get; set; }
        public decimal ToplamHsonuVergi { get; set; }
        public decimal ToplamHsonuDamgaVergi { get; set; }
        public decimal ToplamHsonuIssizlik { get; set; }
        public decimal ToplamHsonuNet { get; set; }


        // Resmi Tatil Toplamları
        public decimal ToplamResmiTatil { get; set; }
        public decimal ToplamResmiTatilSGK { get; set; }
        public decimal ToplamResmiTatilVergi { get; set; }
        public decimal ToplamResmiTatilDamgaVergi { get; set; }
        public decimal ToplamResmiTatilIssizlik { get; set; }
        public decimal ToplamResmiTatilNet { get; set; }

        //---------
        public decimal BrutUcret { get; set; }
        public decimal SaatUcret { get; set; }

        //------
        public string TatilGunu { get; set; } = "Pazar";
        public string TatilGunu2 { get; set; }


        //---- Vergi Yıl
        public double Vergiyil { get; set; }
        public double VergiMatrah { get; set; }


        //----------
        public string FM_Gunler { get; set; }
        public string FM_GunlerGece { get; set; }
        public string Ek_Gece3 { get; set; }


        //-------------------------
        public int HaftasonuVar { get; set; }
        public bool HaftasonuDus { get; set; } = false;
        public int  FazlaMesaiVar { get; set; }


        // --------AGİ
        public string EsCalisiyor { get; set; } = "ÇalışMIyor";
        public int CocukSayisi { get; set; } = 0;
        public string Bekar { get; set; } = "Evli";



        public string Ek1 { get; set; }
        public string Ek2 { get; set; }
        public decimal Ek3 { get; set; }
           
    }


    // MAasFazlaMesai
    public class MaasFazlaMesai
    {
        public string Id { get; set; }
        public string mesaiId { get; set; }

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

        public FazlaMesai fazlaMesai { get; set; }

    }

    public class FazlaMesaiHaftalar
    {
        public string Id { get; set; }
        public int Sira { get; set; }
        public string FazlaMesaiId { get; set; }

        public bool FazlaMesaiVar { get; set; }

        public string HaftaSonuCalismaVar { get; set; }
        public double HaftaSonuCalismaGunSayisi { get; set; }

        public string ResmiTatilCalismaVar { get; set; }
        public double ResmiTatilCalismaGunSayisi { get; set; }

        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }



        // Tarihler.....
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }

        // Maaş
        public decimal BrutUcret { get; set; }
        public decimal SaatlikUcret { get; set; }
        public decimal GunlukUcret { get; set; }


        // HaftaIcerisindekiSayı

        public double FazlaMesaiSozlesme { get; set; }
        public double FazlaMesaiOHaftadaki { get; set; }
        public double FazlaCalismaOHaftadaki { get; set; }



        // Hesaplanan FazlaMesai Tazminatlar..
        public decimal MesaiUcret { get; set; }
        public decimal MesaiUcret2 { get; set; }


        //Hesaplanan Resmi Tatil Günü Ücreti
        public decimal ResmiTatilUcret { get; set; }
        public decimal ResmiTatilUcret2 { get; set; }

        // Hesaplanan İzin Günü Ücreti
        public decimal HaftaSonuUcret { get; set; }
        public decimal HaftaSonuUcret2 { get; set; }



        public FazlaMesai fazlaMesai { get; set; }




    }



    //Resmi Tatil
    public class FazlaMesaiResmiTatilHaftalar
    {
        public string Id { get; set; }
        public int Sira { get; set; }
        public string FazlaMesaiId { get; set; }

        // Tarihler
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }

        public bool ResmiTatilVar { get; set; }
        public string ResmiTatilVar2 { get; set; }

        public double ResmiTatilGunSayisi { get; set; }
        public string ResmiTatilGunler { get; set; }

        // Maaş
        public decimal BrutUcret { get; set; }
        public decimal SaatlikUcret { get; set; }
        public decimal GunlukUcret { get; set; }


        //Hesaplanan Resmi Tatil Günü Ücreti
        public decimal ResmiTatilUcret { get; set; }
        public decimal ResmiTatilUcret2 { get; set; }

        public FazlaMesai fazlaMesai { get; set; }

    }
    public class FazlaMesaiResmiTatil
    {
        public string Id { get; set; }
        public string mesaiId { get; set; }
        public string Aciklama { get; set; }
        public DateTime tarih { get; set; }
        public decimal Miktar { get; set; }



        public string ekbilgi { get; set; }
        public string ekBilgi1 { get; set; }
        public string ekBilgi2 { get; set; }
        public Nullable<double> ekBilgi3 { get; set; }
        public Nullable<long> ekBilgi4 { get; set; }

        public int AktifDurumu { get; set; }

        public FazlaMesai fazlaMesai { get; set; }
    }


    // Haftalık İzin
    public class FazlaMesaiHaftalikIzinHaftalar
    {
        public string Id { get; set; }

        public int Sira { get; set; }

        public bool Aktif { get; set; }

        public string FazlaMesaiId { get; set; }

        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }

        public  bool HaftaSonuIzinVar { get; set; }
        public string HaftaSonucIzinVar2 { get; set; }

        public string IzinGunu { get; set; }

        public DateTime HaftalikIzinTarihi{ get; set; }

        public int HaftaSonuIzinGunSayisi { get; set; }
        public string HaftaSonuKullanilanIzinGunleri { get; set; }

        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }


        // Maaş
        public decimal BrutUcret { get; set; }
        public decimal SaatlikUcret { get; set; }
        public decimal GunlukUcret { get; set; }


        //Hesaplanan Resmi Tatil Günü Ücreti
        public decimal HaftaSonuUcret { get; set; }
        public decimal HaftaSonuUcret2 { get; set; }

        public FazlaMesai fazlaMesai { get; set; }


    }



    //İzinler
    public class FazlaMesaiKisiIzinGunleri
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

        public FazlaMesai fazlaMesai { get; set; }


    }

    public class FazlaMesaiKisiIzinKayitlari
    {
        public string Id { get; set; }

        public string FazlaMesaiId { get; set; }
        public int Sira { get; set; }
        public string KisiId { get; set; }

        public DateTime BaslangicTar { set; get; }
        public DateTime BitisTar { set; get; }

        public string Aciklama { get; set; }
        public int IzinTur { get; set; }

        public string EkBilgi1 {get;set;}
        public string EkBilgi2 { get; set; }

        public FazlaMesai fazlaMesai { get; set; }

        public string Izingun { get; set; }
        public bool HaftaSonuCalismayiCikar { get; set; }

        public bool AradakiResmiTatilleriCikar { get; set; }


    }



}
