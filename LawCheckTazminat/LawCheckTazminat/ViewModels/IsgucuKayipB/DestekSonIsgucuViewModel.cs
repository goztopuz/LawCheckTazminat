using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using System.Linq;
using LawCheckTazminat.Services.DestektenYoksunlukHesapService;
using Xamarin.Forms;
using System.Windows.Input;
using LawCheckTazminat.Views.IsgucuKayipV;
using System.Reflection;
using Syncfusion.DocIO.DLS;
using System.Globalization;
using System.IO;
using LawCheckTazminat.Dependency;
using Syncfusion.DocIO;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class DestekSonIsgucuViewModel : ViewModelBase
    {

        YasamSuresiService yasamSureIslem = new YasamSuresiService();
        MaasHesaplaService _maasHesaplaIslem = new MaasHesaplaService();

        Decimal SonMaas = 0;

        List<TRH_Trafik> trhListe = new List<TRH_Trafik>();
        List<KayipOran> oranListesi = new List<KayipOran>();


        public DestekSonIsgucuViewModel(IsgucuKayip _kayip)
        {
            this.IsGucu = new IsgucuKayip();
            this.IsGucu = _kayip;
            TrafikMi = false;

            if (IsGucu.trafikKazasi == "Evet")
            {

                TrafikMi = true;

            }


            SonMaas =_kayip.isleyecekMaas;

            decimal isMik = _kayip.isleyecekMaas;

            Hesaplamalar2();



        }

        System.Decimal islenmisToplam = 0;
        System.Decimal islenecekAktifToplam = 0;
        System.Decimal pasifToplam = 0;
        System.Decimal genelToplam = 0;
        System.Decimal masrafToplam = 0;

        private IsgucuKayip _isgucuKayip;

        public IsgucuKayip IsGucu
        {
            get => _isgucuKayip;
            set
            {
                _isgucuKayip = value;
            }
        }


        List<MaasAy> maasListesi = new List<MaasAy>();

        List<MaasAy2> maasListesi2 = new List<MaasAy2>();

        List<bolum> bolumListe = new List<bolum>();
        List<bolum> bolumListeSirali = new List<bolum>();


        private void OranListeOlustur()
        {
            oranListesi.Clear();

            var oranListesi2 = IsGucu.IsGucuKayipOranlar.ToList();

            var o1 = new KayipOran();
            o1.baslangicTarihi = IsGucu.kazaTarihi;
            o1.kayipOrani = IsGucu.kayipOrani;
            oranListesi2.Add(o1);

            int ii2 = 0;

            var oranListesi3 = oranListesi2.OrderBy(o => o.baslangicTarihi).ToList();

            foreach (var oo in oranListesi3)
            {
                if (oranListesi2.Count == 1)
                {
                    oo.cikisTarihi = new DateTime(9999, 1, 1);
                    oranListesi.Add(oo);
                }
                else
                {
                    if (ii2 + 1 == oranListesi2.Count)
                    {
                        oo.cikisTarihi = new DateTime(9999, 1, 1);
                        oranListesi.Add(oo);
                    }
                    else
                    {
                        oo.cikisTarihi = oranListesi3[ii2 + 1].baslangicTarihi.AddDays(-1);
                        oranListesi.Add(oo);
                    }
                }
                ii2 = ii2 + 1;
            }


        }


        private double OranOgren(DateTime _tarih1)
        {
            double orandeger = 0;
    
           
                var bolum = oranListesi.Find(o => o.baslangicTarihi <= _tarih1 && _tarih1 <= o.cikisTarihi);
            if(bolum != null)
            {
                orandeger = bolum.kayipOrani;
            }

            if (IsGucu.hastaneYatisi == "Var")
            {
                if (IsGucu.hastaneYatisTarihi.Date <= _tarih1 && _tarih1.Date <= IsGucu.hastaneCikisTarihi.Date)
                {
                    orandeger = 100;
                }
              }
            return orandeger;

        }

        private bool _trafikMi;

        public bool TrafikMi
        {
            get => _trafikMi;
            set
            {
                _trafikMi = value;
                OnPropertyChanged();
            }
        }

        private decimal _genelToplam;

        public decimal GenelToplam
        {
            get => _genelToplam;
            set
            {
                _genelToplam = value;
                OnPropertyChanged();


            }
        }

        private decimal OgunkuMaas(DateTime _tarih)
        {

            decimal maas = 0;
            decimal gunlukMaas = 0;

            bool agiKontrol = false;

            if (IsGucu.kazaTarihi <= _tarih && IsGucu.raporTarihi > _tarih)
            {
                // İşlemiş Dönem Maaşlarından seçilecek

                var x1 = IsGucu.IsGucuKayipMaaslar.Where(o => o.yilBas <= _tarih && o.yilBit >= _tarih).FirstOrDefault();

                if (x1 != null)
                {
                    maas = x1.netMaas;
                }
            }
            else if (IsGucu.raporTarihi <= _tarih && _tarih < emeklilikTarihi)
            {
                if (IsGucu.trafikKazasi == "Evet" && IsGucu.trafikTRh==true)
                {
                    maas = IsGucu.isleyecekMaas;

                }
                else
                {
                    maas = IsGucu.isleyecekMaas * Convert.ToDecimal((1.1 * 0.9091));

                }


                if (IsGucu.AgiDus == true)
                {
                    agiKontrol = true;
                }
            }
            else if (emeklilikTarihi <= _tarih)
            {
                if (IsGucu.trafikKazasi == "Evet" && IsGucu.trafikTRh== true)
                {
                    maas = IsGucu.isleyecekMaas;

                }
                else
                {
                    maas = IsGucu.isleyecekMaas * Convert.ToDecimal((1.1 * 0.9091));

                }


            }



            // AGİ Durumunu Kontrol et ve Gereklise
            // Destekteki çocucuk sayısına göre Düş
            if (maas != 0)
            {

                if (agiKontrol == true)
                {
                    // Eş -Çalışma-Çocuk Sayısını  bul. -O günkü asgari Ücreti de bul...

                    // NET MAAŞ'dan düşülecek olan AGİ Miktarı

                    int a2e = IsGucu.raporTarihi.Year;
                    int b2e = 1;
                    if (IsGucu.raporTarihi.Month > 6)
                    {
                        b2e = 2;
                    }

                    string c2e = a2e.ToString() + "-" + b2e.ToString();

                    decimal asgariUcretAGie = 0;

                    AsgariUcretTablosu g103e = asgariMaasListesi.ToList().Find(o => o.yil == c2e);

                    if (g103e != null)
                    {
                        asgariUcretAGie = Convert.ToDecimal(g103e.brut);
                    }


                    var esVar2 = IsGucu.IsGucuKayipYakinlar.ToList().Find(o => o.yakinlik == "Eş");

                    int esDurum2 = -1;

                    if (esVar2 != null)
                    {
                        if (esVar2.esCalisiyorMu == "Hayır")
                        {
                            esDurum2 = 0;
                        }
                        else if (esVar2.esCalisiyorMu == "Evet")
                        {
                            esDurum2 = 1;
                        }
                    }

                    int hesabaKatilacakCocukSayisi2 = 0;

                    foreach (var t in IsGucu.IsGucuKayipYakinlar)
                    {
                        if (t.yakinlik == "Çocuk")
                        {

                            hesabaKatilacakCocukSayisi2 = hesabaKatilacakCocukSayisi2 + 1;

                        }

                    }


                    Decimal agi2e = _maasHesaplaIslem.AsgariGecimHespla(asgariUcretAGie, esDurum2, hesabaKatilacakCocukSayisi2);

                    maas = maas - agi2e;

                    // Mevcut Destekteki çocuk Sayısı
                    int destekkiCocukSay = 0;

                    foreach (var t2 in IsGucu.IsGucuKayipYakinlar)
                    {
                        if (t2.yakinlik == "Çocuk")
                        {
                            if (_tarih < t2.cikisTar)
                            {
                                destekkiCocukSay = destekkiCocukSay + 1;
                            }

                        }
                    }

                    decimal agi3 = _maasHesaplaIslem.AsgariGecimHespla(asgariUcretAGie, esDurum2, destekkiCocukSay);


                    maas = maas + agi3;
                }

            }



            int gunsay = DateTime.DaysInMonth(_tarih.Year, _tarih.Month);

            gunlukMaas = maas / gunsay;


            return gunlukMaas;

        }

        private string DonemiBul(DateTime _tarih)
        {

            string donem = "";
            if (IsGucu.kazaTarihi <= _tarih && IsGucu.raporTarihi > _tarih)
            {
                // İşlemiş Dönem Maaşlarından seçilecek

                donem = "İşlemiş";

            }
            else if (IsGucu.raporTarihi <= _tarih && emeklilikTarihi > _tarih)
            {
                donem = "Aktif";

            }
            else if (_tarih >= emeklilikTarihi)
            {
                donem = "Pasif";
            }

            return donem;

        }



        DateTime emeklilikTarihi;


        List<AsgariUcretTablosu> asgariMaasListesi = new List<AsgariUcretTablosu>();

        List<GunlukBilgi> GunlukListe = new List<GunlukBilgi>();

        async private void Hesaplamalar2()
        {

            AsgariUcretService islem2 = new AsgariUcretService();
             asgariMaasListesi = (await islem2.GetItemsAsync()).ToList();
            asgariMaasListesi.OrderByDescending(o => o.brut);

            OranListeOlustur();

            var yakinListe = IsGucu.IsGucuKayipYakinlar.ToList();

            var gecmisMaasDegerleri = IsGucu.IsGucuKayipMaaslar.ToList();
            maasListesi.Clear();


            // Mevcut Yaş Vefat Tarihi Bilgileri....
            int mevcutYas = 0;

            // Sadece YIL alınacak küsür Yok
            if (IsGucu.yasiYuvarla == 0)
            {

                DateTime tmpDogum = IsGucu.dogumTarihi;
                int yil = 0;
                while (IsGucu.kazaTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                mevcutYas = yil;

            }
            else if (IsGucu.yasiYuvarla == 1)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = IsGucu.dogumTarihi;
                int yil = 0;
                while (IsGucu.kazaTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = IsGucu.dogumTarihi.AddYears(yil);

                while (tmpDogum2 <= IsGucu.kazaTarihi)
                {

                    ayy = ayy + 1;
                    tmpDogum2 = tmpDogum2.AddMonths(1);

                }

                ayy = ayy - 1;
                if (ayy >= 6)
                {
                    yil = yil + 1;
                }

                mevcutYas = yil;
            }



            decimal kalanOmur = 0;


            if(IsGucu.trafikKazasi=="Evet" && IsGucu.trafikTRh==true)
            {

                kalanOmur = Convert.ToDecimal(yasamSureIslem.TrhSureHesapla(mevcutYas,IsGucu.cinsiyet));

            }
            else
            {
                kalanOmur = Convert.ToDecimal(yasamSureIslem.PmfSureHesapla(mevcutYas));


            }

            decimal kalanYil = Math.Truncate(kalanOmur);
            decimal kusurat = kalanOmur - kalanYil;

            decimal kalanAy1 = kusurat * 12;

            decimal kalanAy = Math.Truncate(kalanAy1);

            decimal gun = (kalanAy1 - kalanAy) * 30;

            decimal kalanGun = Convert.ToInt32(gun);


            //gelen kalan ömür  küsüratları olay tarihine eklenir.
            DateTime vefatYas = IsGucu.kazaTarihi;

            vefatYas = vefatYas.AddYears(Convert.ToInt32(kalanYil));
            vefatYas = vefatYas.AddMonths(Convert.ToInt32(kalanAy));
            vefatYas = vefatYas.AddDays(Convert.ToInt32(kalanGun));



            VefatTar = vefatYas;

            BaslangicTar = IsGucu.kazaTarihi;

            int emeklilikYas = 60;

            if (IsGucu.emeklilikYasi != null)
            {

                if (IsGucu.emeklilikYasi != 0)
                {

                    emeklilikYas = Convert.ToInt32(IsGucu.emeklilikYasi);

                }
            }

             emeklilikTarihi = IsGucu.dogumTarihi.AddYears(emeklilikYas);


            // ORAN LİSTESİİ......

            //var oranListesi2 = IsGucu.IsGucuKayipOranlar.ToList();

    

            decimal kusurrr = Convert.ToDecimal(IsGucu.kusurOrani);


            //Günlük Döngü

            var tmpBas = IsGucu.kazaTarihi;
            int gunSira = 0;
            decimal katsayi = 1;
            while (tmpBas <= vefatYas)
            {
                gunSira = gunSira + 1;
                GunlukBilgi gunn = new GunlukBilgi();


                //  1 - O Günlük Maaş Geliri
                //    AGi Ekle veya Çıkar..
                DateTime _tarih = tmpBas;


               

                
                decimal maas = OgunkuMaas(_tarih);


                // 2 - O Günlük Kusur Oranıyla Çarpılacak..

                decimal kusurCarpimi = (decimal)(kusurrr / 100);
                decimal gunlukTazminat = maas * kusurCarpimi;


                // 3- Kayıp Oranı.....


                var orann = OranOgren(_tarih);

                var orann2 = orann / 100;
                gunlukTazminat = gunlukTazminat * Convert.ToDecimal(orann2);



                //  -  Günlük Bölüm Bilgisi Alınacak (işlemiş işleyecek......)

                string _donemi = "";
                _donemi = DonemiBul(_tarih);

                gunn.gunlukTazminat = gunlukTazminat;
                gunn.tarih = _tarih;
                gunn.donemi = _donemi;

                //3--  Yaşama İhtimali Çarpımı Katsayısı...


                if (_donemi != "İşlemiş" && IsGucu.trafikKazasi == "Evet" && IsGucu.trafikTRh==true)
                {
                    katsayi = YasamaIhtimali(IsGucu.cinsiyet, IsGucu.dogumTarihi,
                        Convert.ToDateTime(IsGucu.raporTarihi), Convert.ToDateTime(gunn.tarih)
                        , vefatYas);
                }

                gunlukTazminat = gunlukTazminat * katsayi;

                gunn.miktar = gunlukTazminat; ;



                // 4-ExtraConditions...(Erkekse-Askerlik yapmamışsa Tazminat düşülecek)

                if (IsGucu.cinsiyet == "Erkek")
                {
                    if (IsGucu.askerlikYapti == "YapMAdı")
                    {
                        DateTime askereGidis = new DateTime(IsGucu.askereGidisYil, IsGucu.askereGidisAy, 1);
                        if (askereGidis != null)
                        {

                            DateTime askerlikBasTar = askereGidis;
                            DateTime askelikBitisTar = askerlikBasTar.AddMonths(Convert.ToInt32(IsGucu.askerlikSuresi));


                            if (_tarih >= askelikBitisTar && _tarih <= askelikBitisTar)
                            {
                                gunn.gunlukTazminat = 0;
                                foreach (var t2 in gunn.kisiListe)
                                {
                                    t2.alinanMiktar = 0;
                                }
                            }

                        }
                    }
                }



                // Bir Gün Arttırılarak Devam Edilecek..
                GunlukListe.Add(gunn);
                tmpBas = tmpBas.AddDays(1);

            }



            decimal _masrafToplam;

            // Genel Toplamlar

            decimal _islemisToplam = 0;
            decimal _aktifToplam = 0;
            decimal _pasifToplam = 0;
            decimal _genelToplam = 0;

            foreach (var t in GunlukListe)
            {

               
                    if (t.donemi == "İşlemiş")
                    {
                        _islemisToplam = _islemisToplam + t.miktar;
                    }
                    else if (t.donemi == "Aktif")
                    {
                        _aktifToplam = _aktifToplam + t.miktar;
                    }
                    else if (t.donemi == "Pasif")
                    {
                        _pasifToplam = _pasifToplam + t.miktar;
                    }

            }


            foreach(var t3 in IsGucu.IsGucuKayipMasraflar)
            {
                MasrafToplam = MasrafToplam + t3.miktar;
            }


            AktifToplam = _aktifToplam;

            IslemisToplam = _islemisToplam;

            PasifToplam = _pasifToplam;

            _masrafToplam = MasrafToplam;
            _genelToplam = _aktifToplam + _islemisToplam + _pasifToplam + _masrafToplam;

            GenelToplam = _genelToplam;


        }

        private decimal YasamaIhtimali(string cinsiyet, DateTime dogumTar, DateTime raporTarihi, DateTime hesapTarihi, DateTime cikisTarihi)
        {


            int raporTarihindekiYas = kisininOTarihtekiYasi(dogumTar, raporTarihi, false);


            int cikisTarihindekiYas = kisininOTarihtekiYasi(dogumTar, cikisTarihi.AddDays(1), false);

            int _hesapTarihindekiYas = kisininOTarihtekiYasi(dogumTar, hesapTarihi, false);



            List<AktuerYasamaSans> listeYasamaSansi = new List<AktuerYasamaSans>();

            int yasAktifBas1 = Convert.ToInt32(raporTarihindekiYas);
            int yasAktifYasBitis1 = Convert.ToInt32(cikisTarihindekiYas + 2);


            int tmpAktifBas1 = yasAktifBas1;
            int tmpAktifBit1 = yasAktifYasBitis1;

            int sayacc = 0;
            listeYasamaSansi.Clear();
            for (int i = 0; tmpAktifBas1 < tmpAktifBit1; tmpAktifBas1++)
            {

                sayacc = sayacc + 1;
                if (cinsiyet == "Kadın")
                {


                    AktuerYasamaSans aa = new AktuerYasamaSans();
                    var kayit1A = trhListe.Find(o => o.Id == yasAktifBas1);
                    var kayit2A = trhListe.Find(o => o.Id == tmpAktifBas1);

                    decimal nx1A = Convert.ToDecimal(kayit1A.NxKadin);
                    decimal dx1A = Convert.ToDecimal(kayit1A.DxKadin);
                    decimal nx2A = Convert.ToDecimal(kayit2A.NxKadin);

                    decimal hes1A = ((nx1A - nx2A) / dx1A);

                    AktuerYasamaSans aa2 = new AktuerYasamaSans();

                    aa2.oran = hes1A;
                    aa2.sira = sayacc;
                    aa2.yas = tmpAktifBas1;
                    listeYasamaSansi.Add(aa2);





                }

                else
                {

                    var kayit1A = trhListe.Find(o => o.Id == yasAktifBas1);
                    var kayit2A = trhListe.Find(o => o.Id == tmpAktifBas1);
                    decimal nx1A = Convert.ToDecimal(kayit1A.NxErkek);
                    decimal dx1A = Convert.ToDecimal(kayit1A.DXErkek);
                    decimal nx2A = Convert.ToDecimal(kayit2A.NxErkek);

                    decimal hes1A = ((nx1A - nx2A) / dx1A);

                    AktuerYasamaSans aa = new AktuerYasamaSans();

                    aa.oran = hes1A;
                    aa.sira = sayacc;
                    aa.yas = tmpAktifBas1;
                    listeYasamaSansi.Add(aa);
                }





            }


            decimal deger1 = 0;
            decimal deger2 = 0;

            var ilkYas = listeYasamaSansi.Find(o => o.yas == _hesapTarihindekiYas);
            if (ilkYas != null)
            {
                deger1 = ilkYas.oran;
            }

            var ilkYas2 = listeYasamaSansi.Find(o => o.yas == _hesapTarihindekiYas + 1);
            if (ilkYas2 != null)
            {
                deger2 = ilkYas2.oran;
            }

            return deger2 - deger1;

        }


        private int kisininOTarihtekiYasi(DateTime dogumTarihi, DateTime tarih, bool yuvarla)
        {
            int yass1 = 0;



            if (yuvarla == false)
            {

                DateTime tmpDogum = dogumTarihi;
                int yil = 0;
                while (tarih > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                yass1 = yil;

            }
            else if (yuvarla == true)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = dogumTarihi;
                int yil = 0;
                while (tarih > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = dogumTarihi.AddYears(yil);

                while (tmpDogum2 <= tarih)
                {

                    ayy = ayy + 1;
                    tmpDogum2 = tmpDogum2.AddMonths(1);

                }

                ayy = ayy - 1;
                if (ayy >= 6)
                {
                    yil = yil + 1;
                }

                yass1 = yil;
            }

            return yass1;


        }

        private decimal _islemisToplam;
        public decimal IslemisToplam
        {
            get => _islemisToplam;
            set
            {

                _islemisToplam = value;
                OnPropertyChanged();
            }
        }

        private decimal _aktifToplam;
        public decimal AktifToplam
        {
            get => _aktifToplam;
            set
            {
                _aktifToplam = value;
                OnPropertyChanged();
            }
        }

        private decimal _pasifToplam;
        public decimal PasifToplam
        {
            get => _pasifToplam;
            set
            {
                _pasifToplam = value;
                OnPropertyChanged();
            }
        }


        private decimal _masrafToplam;
        public decimal MasrafToplam
        {
            get => _masrafToplam;
            set
            {
                _masrafToplam = value;
                OnPropertyChanged();
            }
        }

        private DateTime _baslangicTar;
        public DateTime BaslangicTar
        {
            get => _baslangicTar;
            set
            {
                _baslangicTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _vefatTar;
        public DateTime VefatTar
        {
            get => _vefatTar;
            set
            {
                _vefatTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _destekBitis;
        public DateTime DestekBitis
        {
            get => _destekBitis;
            set
            {
                _destekBitis = value;
                OnPropertyChanged();
            }
        }




        private class AktuerYasamaSans
        {
            public int sira { get; set; }
            public int yas { get; set; }
            public decimal oran { get; set; }

        }
        async private void Hesaplamalar()
        {


            AsgariUcretService islem2 = new AsgariUcretService();
            var asgariMaasListesi = (await islem2.GetItemsAsync()).ToList();
            asgariMaasListesi.OrderByDescending(o => o.brut);

            var yakinListe = IsGucu.IsGucuKayipYakinlar.ToList();

            var gecmisMaasDegerleri = IsGucu.IsGucuKayipMaaslar.ToList();
            maasListesi.Clear();
            int mevcutYas = 0;

            //


            // Sadece YIL alınacak küsür Yok
            if (IsGucu.yasiYuvarla == 0)
            {

                DateTime tmpDogum = IsGucu.dogumTarihi;
                int yil = 0;
                while (IsGucu.kazaTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;
                mevcutYas = yil;

            }
            else if (IsGucu.yasiYuvarla == 1)
            {

                // YIL + AY Küsürlü üste 

                DateTime tmpDogum = IsGucu.dogumTarihi;
                int yil = 0;
                while (IsGucu.kazaTarihi > tmpDogum)
                {
                    yil = yil + 1;

                    tmpDogum = tmpDogum.AddYears(1);

                }

                yil = yil - 1;

                int ayy = 0;

                DateTime tmpDogum2 = IsGucu.dogumTarihi.AddYears(yil);

                while (tmpDogum2 <= IsGucu.kazaTarihi)
                {

                    ayy = ayy + 1;
                    tmpDogum2 = tmpDogum2.AddMonths(1);

                }

                ayy = ayy - 1;
                if (ayy >= 6)
                {
                    yil = yil + 1;
                }

                mevcutYas = yil;
            }




            decimal kalanOmur = 0;





            kalanOmur = Convert.ToDecimal(yasamSureIslem.PmfSureHesapla(mevcutYas));


            decimal kalanYil = Math.Truncate(kalanOmur);
            decimal kusurat = kalanOmur - kalanYil;

            decimal kalanAy1 = kusurat * 12;

            decimal kalanAy = Math.Truncate(kalanAy1);

            decimal gun = (kalanAy1 - kalanAy) * 30;

            decimal kalanGun = Convert.ToInt32(gun);




            //gelen kalan ömür  küsüratları olay tarihine eklenir.
            DateTime vefatYas = IsGucu.kazaTarihi;

            vefatYas = vefatYas.AddYears(Convert.ToInt32(kalanYil));
            vefatYas = vefatYas.AddMonths(Convert.ToInt32(kalanAy));
            vefatYas = vefatYas.AddDays(Convert.ToInt32(kalanGun));

            //lblBakiyeOmur.Text = kalanYil.ToString() + " Yıl " + kalanAy.ToString() + " Ay " + kalanGun.ToString() + " Gün";
            //lblTahVEfatTarihi.Text = vefatYas.ToShortDateString();

            VefatTar = vefatYas;

            int emeklilikYas = 60;

            if (IsGucu.emeklilikYasi != null)
            {

                if (IsGucu.emeklilikYasi != 0)
                {

                    emeklilikYas = Convert.ToInt32(IsGucu.emeklilikYasi);

                }
            }


            DateTime emeklilikTarihi = IsGucu.dogumTarihi.AddYears(emeklilikYas);

         //   lblEmeklilikTarihi.Text = emeklilikTarihi.ToShortDateString();




            //bolum Listesi Oluşturma

            bolumListe.Clear();

            bolum bolumOlayTarihi = new bolum();
            bolumOlayTarihi.tur = 1;
            bolumOlayTarihi.baslangic = Convert.ToDateTime(IsGucu.kazaTarihi);
            bolumOlayTarihi.aciklama = "Olay Tarihi";
            bolumListe.Add(bolumOlayTarihi);

            bolum bolumRaporTarihi = new bolum();
            bolumRaporTarihi.tur = 2;
            bolumRaporTarihi.baslangic = Convert.ToDateTime(IsGucu.raporTarihi);
            bolumRaporTarihi.aciklama = "Rapor Tarihi";
            bolumListe.Add(bolumRaporTarihi);

            bolum bolumEmeklilikTarihi = new bolum();
            bolumEmeklilikTarihi.tur = 3;
            bolumEmeklilikTarihi.baslangic = emeklilikTarihi;
            bolumEmeklilikTarihi.aciklama = "Emeklilik Tarihi";
            bolumListe.Add(bolumEmeklilikTarihi);

            bolum bolumYasamTarihi = new bolum();
            bolumYasamTarihi.tur = 4;
            bolumYasamTarihi.baslangic = vefatYas;
            bolumYasamTarihi.aciklama = "Yasam Tarihi";
            bolumListe.Add(bolumYasamTarihi);

            var oranListesi2 = IsGucu.IsGucuKayipOranlar.ToList();

            List<KayipOran> oranListesi = new List<KayipOran>();
            int ii2 = 0;
            foreach(var oo in oranListesi2)
            {
                if(oranListesi.Count==1)
                {
                    oo.cikisTarihi = new DateTime(9999, 1, 1);
                    oranListesi.Add(oo);
                }
                else
                {
                    if(ii2 +1 == oranListesi.Count)
                    {
                        oo.cikisTarihi = new DateTime(9999, 1, 1);
                        oranListesi.Add(oo);
                    }
                    else
                    {
                       oo.cikisTarihi = oranListesi[ii2 + 1].baslangicTarihi.AddDays(-1);
                       oranListesi.Add(oo);
                    }
                }
                ii2 = ii2 + 1;
            }


            //--------------------------------------------------------------------------------------
            // NET MAAŞ'dan düşülecek olan AGİ Miktarı

            int a2e = IsGucu.raporTarihi.Year;
            int b2e = 1;
            if (IsGucu.raporTarihi.Month > 6)
            {
                b2e = 2;
            }

            string c2e = a2e.ToString() + "-" + b2e.ToString();

            decimal asgariUcretAGie = 0;

            AsgariUcretTablosu g103e = asgariMaasListesi.Find(o => o.yil == c2e);

            if (g103e != null)
            {
                asgariUcretAGie = Convert.ToDecimal(g103e.brut);
            }


            var esVar2 = yakinListe.Find(o => o.yakinlik == "Eş");

            int esDurum2 = -1;

            if (esVar2 != null)
            {
                if (esVar2.esCalisiyorMu == "Hayır")
                {
                    esDurum2 = 0;
                }
                else if (esVar2.esCalisiyorMu == "Evet")
                {
                    esDurum2 = 1;
                }
            }

            int hesabaKatilacakCocukSayisi2 = 0;

            foreach (var t in yakinListe)
            {
                if (t.yakinlik == "Çocuk")
                {

                    hesabaKatilacakCocukSayisi2 = hesabaKatilacakCocukSayisi2 + 1;

                }

            }


            Decimal agi2e = _maasHesaplaIslem.AsgariGecimHespla(asgariUcretAGie, esDurum2, hesabaKatilacakCocukSayisi2);

            Decimal isleyecekMaas = IsGucu.isleyecekMaas;


            isleyecekMaas = isleyecekMaas - agi2e;

            //Kişi Maaşından AGİ'nin temizlenme kodu sonu...



            int siraa = 0;

        

            //if (oranListesi.Count > 1)
            //{
            //    for (int i = 1; i < oranListesi.Count; i++)
            //    {
            //        bolum bolumSakatlilikTarihi = new bolum();
            //        bolumSakatlilikTarihi.tur = 5;
            //        bolumSakatlilikTarihi.baslangic = Convert.ToDateTime(oranListesi[i].baslangicTarihi);
            //        bolumSakatlilikTarihi.aciklama = "Kayıp Değişim Tarihi";
            //        bolumListe.Add(bolumSakatlilikTarihi);
            //    }
            //}



            for (int jj = 0; jj < bolumListe.Count; jj++)
            {


                if (jj > 0)
                {

                    DateTime tar1 = bolumListe[jj - 1].baslangic;
                    DateTime tar2 = bolumListe[jj].baslangic;

                    if (tar1.Year == tar2.Year && tar1.Month == tar2.Month)
                    {

                        //DateTime tar3 = new DateTime(tar2.Year, tar2.Month + 1, 1);

                        //DateTime tmpTr = tar3.AddMonths(1);

                        //tmpTr = tmpTr.AddDays(-1);

                        //bolumListe[jj].baslangic = tar3;

                    }

                }



            }



            for (int jj = 0; jj < bolumListe.Count; jj++)
            {


                if (jj > 0)
                {

                    DateTime tar1 = bolumListe[jj - 1].baslangic;


                    int yyy = 0;
                    ////MessageBox.Show(tar1.ToString());

                }



            }

            var bolumListe2 = bolumListe.OrderBy(o => o.baslangic).ToList();
            bolumListeSirali.Clear();
            bolumListeSirali = bolumListe2;

            //lblUyari.Text = "";

            if (oranListesi.Count == 0)
            {
           //     lblUyari.Text = "Kayıp Oranı Girilmemiş";


            }
            else if (IsGucu.kazaTarihi < oranListesi[0].baslangicTarihi)
            {
                //lblUyari.Text = "Kayıp Oranı Tarihi- Olay Tarihi Uyumsuzluğu";

            }

            int tt = 1;

            int j = bolumListe2.Count();

            int emekliDurum = 0;

            int islenmisDurum = 0;

            for (int i = 0; i < j; i++)
            {

                if (i + 1 == j)
                {

                    break;
                }
                string bolumAciklama = "";

                int ayniAy = 0;
                DateTime bolBas = bolumListe2[i].baslangic;

                DateTime bolBit = bolumListe2[i + 1].baslangic.AddDays(-1);

                if (bolBas.Year == bolBit.Year && bolBas.Month == bolBit.Month)
                {
                    ayniAy = 1;

                }



                bolumAciklama = bolumListe2[i].tur.ToString();

                if (bolumListe2[i].tur == 2)
                {
                    islenmisDurum = 1;
                }

                if (bolumListe2[i].tur == 3)
                {
                    emekliDurum = 1;
                }



                if (gecmisMaasDegerleri.Count == 0)
                {
                    return;
                }

                DateTime tar1A = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);

                int gunnX = 0;

                if (ayniAy == 0)
                {
                    gunnX = (tar1A - bolBas).Days + 1;

                }
                else
                {
                    gunnX = (bolBit - bolBas).Days + 1;
                }


                DateTime tmpTarX = tar1A;

                int ayyX = 0;


                while (tmpTarX < bolBit)
                {

                    ayyX = ayyX + 1;

                    tmpTarX = tmpTarX.AddMonths(1);

                }

                if (bolBas.Day > bolBit.Day)
                {
                    ayyX = ayyX - 2;
                }

                else if (bolBas.Day <= bolBit.Day)
                {

                    ayyX = ayyX - 2;
                }
                int gun2X = bolBit.Day;
                // 

                DateTime tazminatBas1X = Convert.ToDateTime(bolBas);

                decimal gunToplamX1 = 0;

                if (ayniAy == 0)
                {
                    //Gün Bölüm1
                    if (bolBas.Day == 1)
                    {






                        //-------------------------------------------------------------------------------------

                        decimal oAykiKisiMaasi = 0;
                        MaasAy m = new MaasAy();



                        m.tarih = bolBas;

                        DateTime tmpT = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);
                        DateTime tmpT2 = tmpT.AddDays(-1);
                        m.tarih2 = tmpT;



                        var s7 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);

                        Decimal agi2f = 0;

                        Decimal agi = 0;
                        Decimal oAykiAsgariUcret = 0;
                        int a = bolBas.Year;
                        int b = 1;
                        if (bolBas.Month > 6)
                        {
                            b = 2;
                        }


                        string c = a.ToString() + "-" + b.ToString();
                        AsgariUcretTablosu g100 = asgariMaasListesi.Find(o => o.yil == c);

                        if (g100 != null)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(g100.brut);
                        }
                        else
                        {
                            if (a >= DateTime.Now.Year)
                            {
                                oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                            }


                        }



                        //YAKIN LİSTE

                        var esVar = yakinListe.Find(o => o.yakinlik == "Eş");

                        int esDurum = -1;

                        if (esVar != null)
                        {
                            if (esVar.cikisTar > bolBas)

                                if (esVar.esCalisiyorMu == "Hayır")
                                {
                                    esDurum = 0;
                                }
                                else if (esVar.esCalisiyorMu == "Evet")
                                {
                                    esDurum = 1;
                                }
                        }
                        int hesabaKatilacakCocukSayisi = 0;

                        foreach (var t in yakinListe)
                        {
                            if (t.yakinlik == "Çocuk")
                            {
                                if (t.cikisTar >= m.tarih2)
                                {

                                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                                }
                            }

                        }


                        agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);





                        if (s7 != null)
                        {
                            oAykiKisiMaasi = Convert.ToDecimal(s7.netMaas);






                            agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);



                        }




                        if (islenmisDurum == 0)
                        {

                            oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;
                            m.donemi = "İşlenmiş";

                        }
                        else if (islenmisDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(isleyecekMaas);
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;
                            m.donemi = "İşlenecek Aktif";
                        }
                        if (emekliDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(IsGucu.emekliMaas);
                            m.donemi = "Pasif";
                        }

                        m.aciklama = bolumAciklama;

                        m.maas = oAykiKisiMaasi;


                        m.maas2 = m.maas;



                        // BolBaştaki Maaş Bulunacak 



                        if (islenmisDurum != 0)
                        {


                            m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                        }
                        else
                        {
                            m.maas3 = m.maas2;
                        }

                        decimal kusur =  Convert.ToDecimal(IsGucu.kusurOrani);

                        decimal kayipOran = 0;

                        bool AradaKalmis = false;
                        int aradaKalanSayi = 0;
                        for (int xx = 0; xx < oranListesi.Count; xx++)
                        { 
                            DateTime basTar = (DateTime)oranListesi[xx].baslangicTarihi;

                            DateTime bitTar;

                            if (xx + 1 != oranListesi.Count)
                            {
                                bitTar = (DateTime)oranListesi[xx + 1].baslangicTarihi;

                            }
                            else
                            {
                                bitTar = new DateTime(9999, 12, 31);

                            }
                            //try
                            //{
                            //    bitTar = (DateTime)oranListesi[xx + 1].tarihbas;
                            //}
                            //catch
                            //{
                            //    bitTar = new DateTime(9999, 12, 31);
                            //}

                            DateTime basTar2 = new DateTime(basTar.Year, basTar.Month, basTar.Day);
                            DateTime bitTar2 = new DateTime(bitTar.Year, bitTar.Month, bitTar.Day);

                            DateTime kontrolTar = new DateTime(m.tarih.Year, m.tarih.Month, m.tarih.Day);

                            DateTime kontrolTar2 = new DateTime(m.tarih2.Year, m.tarih2.Month, m.tarih2.Day);


                            if (basTar2 <= kontrolTar2 && kontrolTar2 < bitTar2)
                            {

                                kayipOran = (Decimal)oranListesi[xx].kayipOrani;
                            }

                            //if (kayipOran==0)
                            //{
                            //    if (basTar2 <= kontrolTar2 && kontrolTar2 < bitTar2)
                            //    {

                            //        kayipOran = (Decimal)oranListesi[xx].oran;
                            //        AradaKalmis = true;
                            //    }

                            //}



                        }



                        m.maas4 = Convert.ToDecimal((m.maas3) * (kayipOran / 100) * (kusur / 100));


                        m.kayipOran = kayipOran;
                        m.sira = siraa + 1;
                        maasListesi.Add(m);
                        siraa = siraa + 1;
                        tazminatBas1X = tazminatBas1X.AddMonths(1);
                        //AY ALINACAK
                        //  gunToplamX1 = maasIlk;

                    }
                    else
                    {
                        MaasAy m = new MaasAy();

                        m.tarih = bolBas;

                        DateTime tmpT = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);
                        DateTime tmpT2 = tmpT.AddDays(-1);
                        m.tarih2 = tmpT2;

                        // AY MAAŞ Güne çevrilip Gün olarak bulunacak

                        decimal oAykiKisiMaasi = 0;


                        var s8 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);



                        Decimal agi2f = 0;

                        Decimal agi = 0;
                        Decimal oAykiAsgariUcret = 0;
                        int a = bolBas.Year;
                        int b = 1;
                        if (bolBas.Month > 6)
                        {
                            b = 2;
                        }


                        string c = a.ToString() + "-" + b.ToString();
                        AsgariUcretTablosu g100 = asgariMaasListesi.Find(o => o.yil == c);

                        if (g100 != null)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(g100.brut);
                        }
                        else
                        {
                            if (a >= DateTime.Now.Year)
                            {
                                oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                            }


                        }



                        //YAKIN LİSTE

                        var esVar = yakinListe.Find(o => o.yakinlik == "Eş");

                        int esDurum = -1;

                        if (esVar != null)
                        {
                            if (esVar.cikisTar > bolBas)

                                if (esVar.esCalisiyorMu == "Hayır")
                                {
                                    esDurum = 0;
                                }
                                else if (esVar.esCalisiyorMu == "Evet")
                                {
                                    esDurum = 1;
                                }
                        }
                        int hesabaKatilacakCocukSayisi = 0;

                        foreach (var t in yakinListe)
                        {
                            if (t.yakinlik == "Çocuk")
                            {
                                if (t.cikisTar >= m.tarih2)
                                {

                                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                                }
                            }

                        }


                        agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);






                        if (s8 != null)
                        {
                            oAykiKisiMaasi = Convert.ToDecimal(s8.netMaas);


                            agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);


                        }






                        if (islenmisDurum == 0)
                        {
                            m.donemi = "İşlenmiş";
                            oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;


                        }
                        else if (islenmisDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(isleyecekMaas);
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;
                            m.donemi = "İşlenecek Aktif";
                        }
                        if (emekliDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(IsGucu.emekliMaas);
                            m.donemi = "Pasif";
                        }

                        m.aciklama = bolumAciklama;
                        gunnX = gunnX - 1;
                        System.Decimal gunMaas = oAykiKisiMaasi / 30;

                        //if(m.tarih2.Day==31)
                        //{
                        //    gunnX = gunnX - 1;
                        //}
                        gunToplamX1 = gunMaas * gunnX;


                        m.maas = gunToplamX1;
                        m.maas2 = m.maas;


                        if (islenmisDurum != 0)
                        {


                            m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                        }
                        else
                        {
                            m.maas3 = m.maas2;
                        }


                        decimal kusur =  Convert.ToDecimal(IsGucu.kusurOrani);

                        decimal kayipOran = 0;

                        for (int xx = 0; xx < oranListesi.Count; xx++)
                        {
                            DateTime basTar = (DateTime)oranListesi[xx].baslangicTarihi;

                            DateTime bitTar;

                            if (xx + 1 != oranListesi.Count)
                            {
                                bitTar = (DateTime)oranListesi[xx + 1].baslangicTarihi;

                            }
                            else
                            {
                                bitTar = new DateTime(9999, 12, 31);

                            }
                            //try
                            //{
                            //    bitTar = (DateTime)oranListesi[xx + 1].tarihbas;
                            //}
                            //catch
                            //{
                            //    bitTar = new DateTime(9999, 12, 31);
                            //}

                            DateTime basTar2 = new DateTime(basTar.Year, basTar.Month, basTar.Day);
                            DateTime bitTar2 = new DateTime(bitTar.Year, bitTar.Month, bitTar.Day);

                            DateTime kontrolTar = new DateTime(m.tarih.Year, m.tarih.Month, m.tarih.Day);
                            DateTime kontrolTar2 = new DateTime(m.tarih2.Year, m.tarih2.Month, m.tarih2.Day);

                            if (basTar2 <= kontrolTar2 && kontrolTar2 < bitTar2)
                            {

                                kayipOran = (Decimal)oranListesi[xx].kayipOrani;
                            }


                        }


                        m.maas4 = Convert.ToDecimal((m.maas3) * (kayipOran / 100) * (kusur / 100));


                        m.kayipOran = kayipOran;
                        m.sira = siraa + 1;
                        maasListesi.Add(m);
                        siraa = siraa + 1;
                        tazminatBas1X = tazminatBas1X.AddMonths(1);

                    }




                    /// AYLARIN BAŞLANGICI


                    for (int j2 = 0; j2 < ayyX + 1; j2++)
                    {


                        MaasAy m = new MaasAy();

                        m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);
                        DateTime tmpT = m.tarih.AddMonths(1);
                        DateTime tmpT2 = tmpT.AddDays(-1);
                        m.tarih2 = tmpT2;
                        decimal oAykiKisiMaasi = 0;


                        var s1 = gecmisMaasDegerleri.Find(o => o.yilBas <= tazminatBas1X && o.yilBit >= tazminatBas1X);




                        Decimal agi2f = 0;

                        Decimal agi = 0;
                        Decimal oAykiAsgariUcret = 0;


                        int a = tazminatBas1X.Year;
                        int b = 1;
                        if (tazminatBas1X.Month > 6)
                        {
                            b = 2;
                        }

                        string c = a.ToString() + "-" + b.ToString();


                        AsgariUcretTablosu g105 = asgariMaasListesi.Find(o => o.yil == c);


                        if (g105 != null)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(g105.brut);
                        }
                        else
                        {
                            if (a >= DateTime.Now.Year)
                            {
                                oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                            }


                        }



                        //YAKIN LİSTE

                        var esVar = yakinListe.Find(o => o.yakinlik == "Eş");

                        int esDurum = -1;

                        if (esVar != null)
                        {
                            if (esVar.cikisTar > bolBas)

                                if (esVar.esCalisiyorMu == "Hayır")
                                {
                                    esDurum = 0;
                                }
                                else if (esVar.esCalisiyorMu == "Evet")
                                {
                                    esDurum = 1;
                                }
                        }
                        int hesabaKatilacakCocukSayisi = 0;

                        foreach (var t in yakinListe)
                        {
                            if (t.yakinlik == "Çocuk")
                            {
                                if (t.cikisTar >= m.tarih2)
                                {

                                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                                }
                            }

                        }



                        agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                        if (s1 != null)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(s1.netMaas);



                            agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);


                        }






                        if (islenmisDurum == 0)
                        {
                            m.donemi = "İşlenmiş";

                            oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                            m.maas = oAykiKisiMaasi;
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;
                            m.maas2 = oAykiKisiMaasi;


                        }
                        else if (islenmisDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(isleyecekMaas);
                            m.maas = oAykiKisiMaasi;
                            m.donemi = "İşlenecek Aktif";
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;
                            m.maas2 = oAykiKisiMaasi;

                        }
                        if (emekliDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(IsGucu.emekliMaas);
                            m.donemi = "Pasif";
                            m.maas = oAykiKisiMaasi;
                            m.maas2 = m.maas;
                        }



                        m.aciklama = bolumAciklama;



                        if (islenmisDurum != 0)
                        {


                            m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                        }
                        else
                        {
                            m.maas3 = m.maas2;
                        }



                        decimal kusur = Convert.ToDecimal(IsGucu.kusurOrani);

                        decimal kayipOran = 0;

                        for (int xx = 0; xx < oranListesi.Count; xx++)
                        {
                            DateTime basTar = (DateTime)oranListesi[xx].baslangicTarihi;

                            DateTime bitTar;

                            if (xx + 1 != oranListesi.Count)
                            {
                                bitTar = (DateTime)oranListesi[xx + 1].baslangicTarihi;

                            }
                            else
                            {
                                bitTar = new DateTime(9999, 12, 31);

                            }


                            DateTime basTar2 = new DateTime(basTar.Year, basTar.Month, basTar.Day);
                            DateTime bitTar2 = new DateTime(bitTar.Year, bitTar.Month, bitTar.Day);

                            DateTime kontrolTar = new DateTime(m.tarih.Year, m.tarih.Month, m.tarih.Day);


                            DateTime kontrolTar2 = new DateTime(m.tarih2.Year, m.tarih2.Month, m.tarih2.Day);

                            if (basTar2 <= kontrolTar2 && kontrolTar2 < bitTar2)
                            {

                                kayipOran = (Decimal)oranListesi[xx].kayipOrani;
                            }
                        }


                        m.maas4 = Convert.ToDecimal((m.maas3) * (kayipOran / 100) * (kusur / 100));
                        m.kayipOran = kayipOran;
                        m.sira = siraa + 1;
                        maasListesi.Add(m);
                        siraa = siraa + 1;
                        tazminatBas1X = tazminatBas1X.AddMonths(1);
                        //ToplamX1 = ToplamX1 + maas;

                    }


                    //decimal maasSon = Convert.ToDecimal(gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas).maas);


                    // GÜN bölüm2

                    DateTime sonAyKontrol = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);
                    if (bolBit.AddDays(1).Month != sonAyKontrol.Month)
                    {

                        MaasAy m = new MaasAy();

                        m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1).AddMonths(1);

                        m.tarih2 = bolBit;
                        decimal oAykiKisiMaasi = 0;
                        m.aciklama = bolumAciklama;

                        var s14 = gecmisMaasDegerleri.Find(o => o.yilBas <= sonAyKontrol && o.yilBit >= sonAyKontrol);


                        Decimal oAykiAsgariUcret = 0;

                        Decimal agi2f = 0;

                        Decimal agi = 0;


                        int a = tazminatBas1X.Year;
                        int b = 1;
                        if (tazminatBas1X.Month > 6)
                        {
                            b = 2;
                        }

                        string c = a.ToString() + "-" + b.ToString();

                        AsgariUcretTablosu g107 = asgariMaasListesi.Find(o => o.yil == c);

                        if (g107 != null)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(g107.brut);
                        }
                        else
                        {
                            if (a >= DateTime.Now.Year)
                            {
                                oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                            }


                        }



                        //YAKIN LİSTE

                        var esVar = yakinListe.Find(o => o.yakinlik == "Eş");

                        int esDurum = -1;
                        if (esVar != null)
                        {
                            if (esVar.esCalisiyorMu == "Hayır")
                            {
                                esDurum = 0;
                            }
                            else if (esVar.esCalisiyorMu == "Evet")
                            {
                                esDurum = 1;
                            }
                        }
                        int hesabaKatilacakCocukSayisi = 0;

                        foreach (var t in yakinListe)
                        {
                            if (t.yakinlik == "Çocuk")
                            {
                                if (t.cikisTar >= m.tarih2)
                                {

                                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                                }
                            }

                        }


                        agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);

                        if (s14 != null)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(s14.netMaas);



                            agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);


                        }






                        m.maas = oAykiKisiMaasi;




                        if (islenmisDurum == 0)
                        {
                            m.donemi = "İşlenmiş";

                            m.maas = oAykiKisiMaasi - agi2f;
                            m.maas2 = oAykiKisiMaasi + agi;

                        }
                        else if (islenmisDurum == 1)
                        {


                            oAykiKisiMaasi = Convert.ToDecimal(isleyecekMaas);
                            m.maas = oAykiKisiMaasi;
                            m.maas2 = oAykiKisiMaasi + agi;
                            m.donemi = "İşlenecek Aktif";

                        }
                        if (emekliDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(IsGucu.emekliMaas);

                            m.donemi = "Pasif";


                            m.maas = oAykiKisiMaasi;
                            m.maas2 = m.maas;

                        }


                        if (islenmisDurum != 0)
                        {


                            m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                        }
                        else
                        {
                            m.maas3 = m.maas2;
                        }





                        decimal kusur =  Convert.ToDecimal(IsGucu.kusurOrani);

                        decimal kayipOran = 0;

                        int durumm = 1;




                      for (int xx = 0; xx < oranListesi.Count; xx++)
                        {
                            DateTime basTar = (DateTime)oranListesi[xx].baslangicTarihi;

                            DateTime bitTar;

                            if (xx + 1 != oranListesi.Count)
                            {
                                bitTar = (DateTime)oranListesi[xx + 1].baslangicTarihi;

                            }
                            else
                            {
                                bitTar = new DateTime(9999, 12, 31);

                            }
                            //try
                            //{
                            //    bitTar = (DateTime)oranListesi[xx + 1].tarihbas;
                            //}
                            //catch
                            //{
                            //    bitTar = new DateTime(9999, 12, 31);
                            //}

                            DateTime basTar2 = new DateTime(basTar.Year, basTar.Month, basTar.Day);
                            DateTime bitTar2 = new DateTime(bitTar.Year, bitTar.Month, bitTar.Day);

                            DateTime kontrolTar = new DateTime(m.tarih.Year, m.tarih.Month, m.tarih.Day);
                            DateTime kontrolTar2 = new DateTime(m.tarih2.Year, m.tarih2.Month, m.tarih2.Day);
                            if (basTar2 <= kontrolTar2 && kontrolTar2 < bitTar2)
                            {

                                kayipOran = (Decimal)oranListesi[xx].kayipOrani;
                            }


                        }


                         foreach(var tt3 in oranListesi)
                        {
                            if(tt3.baslangicTarihi<= m.tarih && tt3.cikisTarihi>= m.tarih)
                            {
                                kayipOran = Convert.ToDecimal(tt3.kayipOrani);
                            }

                            if(tt3.baslangicTarihi<=m.tarih && tt3.cikisTarihi<m.tarih2)
                            {
                                durumm = 2;
                            }


                        }


                       // kayipOran= oranListesi.Find(o)

                     
                         if(durumm==1)
                        {
                            m.maas4 = Convert.ToDecimal((m.maas3) * (kayipOran / 100) * (kusur / 100));
                        }
                        else
                        {
                            

                        }



                        m.kayipOran = kayipOran;
                        m.sira = siraa + 1;
                        //m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas) * 1.1) * (0.9));
                        maasListesi.Add(m);
                        siraa = siraa + 1;


                    }
                    else
                    {
                        // AY MAAŞ Güne çevrilip Gün olarak bulunacak

                        // AY MAAŞ Güne çevrilip Gün olarak bulunacak


                        MaasAy m = new MaasAy();

                        m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1).AddMonths(1);

                        m.tarih2 = bolBit;
                        decimal oAykiKisiMaasi = 0;
                        m.aciklama = bolumAciklama;

                        var s14 = gecmisMaasDegerleri.Find(o => o.yilBas <= sonAyKontrol && o.yilBit >= sonAyKontrol);


                        Decimal oAykiAsgariUcret = 0;

                        Decimal agi2f = 0;

                        Decimal agi = 0;


                        int a = tazminatBas1X.Year;
                        int b = 1;
                        if (tazminatBas1X.Month > 6)
                        {
                            b = 2;
                        }

                        string c = a.ToString() + "-" + b.ToString();

                        AsgariUcretTablosu g107 = asgariMaasListesi.Find(o => o.yil == c);

                        if (g107 != null)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(g107.brut);
                        }
                        else
                        {
                            if (a >= DateTime.Now.Year)
                            {
                                oAykiAsgariUcret = Convert.ToDecimal(asgariMaasListesi[0].brut);

                            }


                        }


                        //YAKIN LİSTE

                        var esVar = yakinListe.Find(o => o.yakinlik == "Eş");

                        int esDurum = -1;
                        if (esVar != null)
                        {
                            if (esVar.esCalisiyorMu == "Hayır")
                            {
                                esDurum = 0;
                            }
                            else if (esVar.esCalisiyorMu == "Evet")
                            {
                                esDurum = 1;
                            }
                        }
                        int hesabaKatilacakCocukSayisi = 0;

                        foreach (var t in yakinListe)
                        {
                            if (t.yakinlik == "Çocuk")
                            {
                                if (t.cikisTar >= m.tarih2)
                                {

                                    hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                                }
                            }

                        }


                        agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);


                        if (s14 != null)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(s14.netMaas);

                            agi2f = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum2, hesabaKatilacakCocukSayisi2);

                        }
















                        if (islenmisDurum == 0)
                        {


                            oAykiKisiMaasi = oAykiKisiMaasi - agi2f;
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;

                            m.donemi = "İşlenmiş";


                        }
                        else if (islenmisDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(isleyecekMaas);
                            oAykiKisiMaasi = oAykiKisiMaasi + agi;
                            m.donemi = "İşlenecek Aktif";
                        }
                        if (emekliDurum == 1)
                        {

                            oAykiKisiMaasi = Convert.ToDecimal(IsGucu.emekliMaas);
                            m.donemi = "Pasif";
                        }

                        m.aciklama = bolumAciklama;
                        System.Decimal gunMaas = oAykiKisiMaasi / 30;





                        m.tarih = new DateTime(tazminatBas1X.Year, tazminatBas1X.Month, 1);

                        m.tarih2 = bolBit;








                        int gunSay = (m.tarih2 - m.tarih).Days;
                        gunSay = gunSay + 1;








                        if (m.tarih2.Day == 31)
                        {
                            gunSay = gunSay - 1;
                        }

                        m.maas = gunMaas * gunSay;

                        m.maas2 = m.maas;

                        if (islenmisDurum != 0)
                        {


                            m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                        }
                        else
                        {
                            m.maas3 = m.maas2;
                        }




                        decimal kusur =  Convert.ToDecimal(IsGucu.kusurOrani);

                        decimal kayipOran = 0;

                        for (int xx = 0; xx < oranListesi.Count; xx++)
                        {
                            DateTime basTar = (DateTime)oranListesi[xx].baslangicTarihi;

                            DateTime bitTar;

                            if (xx + 1 != oranListesi.Count)
                            {
                                bitTar = (DateTime)oranListesi[xx + 1].baslangicTarihi;

                            }
                            else
                            {
                                bitTar = new DateTime(9999, 12, 31);

                            }
                            //try
                            //{
                            //    bitTar = (DateTime)oranListesi[xx + 1].tarihbas;
                            //}
                            //catch
                            //{
                            //    bitTar = new DateTime(9999, 12, 31);
                            //}

                            DateTime basTar2 = new DateTime(basTar.Year, basTar.Month, basTar.Day);
                            DateTime bitTar2 = new DateTime(bitTar.Year, bitTar.Month, bitTar.Day);

                            DateTime kontrolTar = new DateTime(m.tarih.Year, m.tarih.Month, m.tarih.Day);
                            DateTime kontrolTar2 = new DateTime(m.tarih2.Year, m.tarih2.Month, m.tarih2.Day);
                            if (basTar2 <= kontrolTar2 && kontrolTar2 < bitTar2)
                            {

                                kayipOran = (Decimal)oranListesi[xx].kayipOrani;
                            }


                        }


                        m.maas4 = Convert.ToDecimal((m.maas3) * (kayipOran / 100) * (kusur / 100));

                        m.kayipOran = kayipOran;
                        m.sira = siraa + 1;

                        maasListesi.Add(m);
                        siraa = siraa + 1;




                    }

                }
                else
                {

                    //AYNI AY...
                    MaasAy m = new MaasAy();

                    m.tarih = bolBas;

                    DateTime tmpT = new DateTime(bolBas.Year, bolBas.Month, 1).AddMonths(1);
                    //DateTime tmpT2 =
                    m.tarih2 = bolBit;

                    // AY MAAŞ Güne çevrilip Gün olarak bulunacak

                    decimal oAykiKisiMaasi = 0;
                    if (islenmisDurum == 0)
                    {

                        var s8 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);

                        if (s8 != null)
                        {
                            oAykiKisiMaasi = Convert.ToDecimal(s8.netMaas);



                        }

                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(isleyecekMaas);
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(IsGucu.emekliMaas);
                    }




                    Decimal oAykiAsgariUcret = 0;

                    int a = bolBas.Year;
                    int b = 1;
                    if (bolBas.Month > 6)
                    {
                        b = 2;
                    }

                    string c = a.ToString() + "-" + b.ToString();
                    AsgariUcretTablosu g102 = asgariMaasListesi.Find(o => o.yil == c);
                    if (g102 != null)
                    {
                        oAykiAsgariUcret = Convert.ToDecimal(g102.brut);
                    }


                    if (m.donemi == "İşlenecek Aktif" || m.donemi == "Pasif")
                    {
                        int a2 = IsGucu.raporTarihi.Year;
                        int b2 = 1;
                        if (IsGucu.raporTarihi.Month > 6)
                        {
                            b2 = 2;
                        }

                        string c2 = a2.ToString() + "-" + b2.ToString();

                        AsgariUcretTablosu g103 = asgariMaasListesi.Find(o => o.yil == c2);

                        if (g103 != null)
                        {
                            oAykiAsgariUcret = Convert.ToDecimal(g103.brut);
                        }

                    }


                    //YAKIN LİSTE

                    var esVar = yakinListe.Find(o => o.yakinlik == "Eş");

                    int esDurum = -1;
                    if (esVar != null)
                    {
                        if (esVar.esCalisiyorMu == "Hayır")
                        {
                            esDurum = 0;
                        }
                        else if (esVar.esCalisiyorMu == "Evet")
                        {
                            esDurum = 1;
                        }
                    }
                    int hesabaKatilacakCocukSayisi = 0;

                    foreach (var t in yakinListe)
                    {
                        if (t.yakinlik == "Çocuk")
                        {
                            if (t.cikisTar >= bolBit)
                            {

                                hesabaKatilacakCocukSayisi = hesabaKatilacakCocukSayisi + 1;
                            }
                        }

                    }


                    Decimal agi = _maasHesaplaIslem.AsgariGecimHespla(oAykiAsgariUcret, esDurum, hesabaKatilacakCocukSayisi);



                    if (islenmisDurum == 0)
                    {
                        m.donemi = "İşlenmiş";

                        var s9 = gecmisMaasDegerleri.Find(o => o.yilBas <= bolBas && o.yilBit >= bolBas);
                        if (s9 != null)
                        {
                            oAykiKisiMaasi = Convert.ToDecimal(s9.netMaas);


                        }

                    }
                    else if (islenmisDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(isleyecekMaas);
                        m.donemi = "İşlenecek Aktif";
                    }
                    if (emekliDurum == 1)
                    {

                        oAykiKisiMaasi = Convert.ToDecimal(IsGucu.emekliMaas);
                        m.donemi = "Pasif";
                    }

                    m.aciklama = bolumAciklama;
                    //gunnX = gunnX - 1;
                    System.Decimal gunMaas = oAykiKisiMaasi / 30;

                    if (m.tarih2.Day == 31)
                    {
                        gunnX = gunnX - 1;
                    }
                    gunToplamX1 = gunMaas * gunnX;

                    Decimal agi2 = (agi / 30) / gunnX;
                    m.maas = gunToplamX1;

                    if (emekliDurum == 1)

                    {
                        m.maas2 = m.maas;
                    }
                    else
                    {
                        m.maas2 = m.maas + agi2;
                    }


                    if (islenmisDurum != 0)
                    {


                        m.maas3 = Convert.ToDecimal((Convert.ToDouble(m.maas2) * 1.1) * (0.9091));

                }
                    else
                    {
                        m.maas3 = m.maas2;
                    }


                    decimal kusur =  Convert.ToDecimal(IsGucu.kusurOrani);

                    decimal kayipOran = 0;

                    for (int xx = 0; xx < oranListesi.Count; xx++)
                    {
                        DateTime basTar = (DateTime)oranListesi[xx].baslangicTarihi;

                        DateTime bitTar;

                        if (xx + 1 != oranListesi.Count)
                        {
                            bitTar = (DateTime)oranListesi[xx + 1].baslangicTarihi;

                        }
                        else
                        {
                            bitTar = new DateTime(9999, 12, 31);

                        }
                        //try
                        //{
                        //    bitTar = (DateTime)oranListesi[xx + 1].tarihbas;
                        //}
                        //catch
                        //{
                        //    bitTar = new DateTime(9999, 12, 31);
                        //}

                        DateTime basTar2 = new DateTime(basTar.Year, basTar.Month, basTar.Day);
                        DateTime bitTar2 = new DateTime(bitTar.Year, bitTar.Month, bitTar.Day);

                        DateTime kontrolTar = new DateTime(m.tarih.Year, m.tarih.Month, m.tarih.Day);
                        DateTime kontrolTar2 = new DateTime(m.tarih2.Year, m.tarih2.Month, m.tarih2.Day);

                        if (basTar2 <= kontrolTar2 && kontrolTar2 < bitTar2)
                        {

                            kayipOran = (Decimal)oranListesi[xx].kayipOrani;
                        }


                    }


                    m.maas4 = Convert.ToDecimal((m.maas3) * (kayipOran / 100) * (kusur / 100));


                    m.kayipOran = kayipOran;
                    m.sira = siraa + 1;
                    maasListesi.Add(m);
                    siraa = siraa + 1;
                    tazminatBas1X = tazminatBas1X.AddMonths(1);


                }





            }




            maasListesi[maasListesi.Count - 1].tarih2 = bolumYasamTarihi.baslangic;


           
            if (IsGucu.cinsiyet == "Erkek")
            {
                if (IsGucu.askerlikYapti == "Hayır")
                {
                    DateTime _askereGidisTar = new DateTime(IsGucu.askereGidisYil, IsGucu.askereGidisAy, 1);
                    if (_askereGidisTar != null)
                    {



                        DateTime askerlikBasTar = _askereGidisTar;
                        DateTime askelikBitisTar = askerlikBasTar.AddMonths(Convert.ToInt32(IsGucu.askerlikSuresi));

                        //askelikBitisTar = askelikBitisTar.AddDays(-1);



                        foreach (var t2 in maasListesi)
                        {

                            bool askerde = false;

                            if (t2.tarih >= askerlikBasTar && t2.tarih < askelikBitisTar)
                            {

                                askerde = true;

                            }

                            if (askerde == true)
                            {
                                t2.donemi = "Askerde";
                                t2.maas = 0;
                                t2.maas2 = 0;
                                t2.maas3 = 0;
                                t2.maas4 = 0;
                            }



                        }

                    }

                }

            }


            int x = 0;
            int y = 0;
            int z = 0; ;

            islenmisToplam = 0;
            islenecekAktifToplam = 0;
            pasifToplam = 0;
            masrafToplam = 0;
            genelToplam = 0;

            //genelIslemisToplam = 0;

            foreach (var t in maasListesi)
            {



                if (t.donemi == "İşlenmiş")

                {
                    islenmisToplam = islenmisToplam + t.maas4;
                    t.aciklama = "İşlenmiş";
                    x = x + 1;
                }
                else if (t.donemi == "İşlenecek Aktif")
                {

                    islenecekAktifToplam = islenecekAktifToplam + t.maas4;
                    t.aciklama = "İşlenecek Aktif";
                    y = y + 1;
                }
                else if (t.donemi == "Pasif")
                {

                    pasifToplam = pasifToplam + t.maas4;

                    t.aciklama = "Pasif";
                    z = z + 1;
                }

                genelToplam = genelToplam + t.maas4;

            }

            foreach (var t in IsGucu.IsGucuKayipMasraflar)

            {
                masrafToplam = masrafToplam + Convert.ToDecimal(t.miktar);

            }

            genelToplam = genelToplam + masrafToplam;
            GenelToplam = genelToplam;

          //  genelIslemisToplam = islenmisToplam;


            int bbb = 1;


        }

        
        // Yeniden Hesapla
        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        async private void OnYenidenHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            IsGucu.duzenlemede = true;

            var sayfa = new Basamak1IGView(IsGucu);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }


        // Rapor Bitiş
        public ICommand RaporBitisCommand => new Command(OnRaporBitis);
        async private void OnRaporBitis(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            var sayfa = new Views.AnaSayfaV.Anasayfa();
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            IsBusy = false;
        }


        // WORD RAPORU OLUŞTUR...

        public ICommand WordRaporCommand => new Command(OnWordRapor);
        async private void OnWordRapor()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            WordRapor2();
            IsBusy = false;

        }

        async private void WordRapor2()
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            WordDocument document = new WordDocument();

            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);


            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;


            //Appends paragraph
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("İŞGÜCÜ KAYIP BİLİRKİŞİ RAPORU") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("______________ MAHKEMESİ'NE") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("\t\t\t______________ ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            // Dosya Bilgileri......

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DOSYA NO \t\t\t:") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph.ParagraphFormat.LineSpacing = 18f;

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVACI \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVALI \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";


            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVA KONUSU\t\t\t: İşgücü Kaybı".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TALEP") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("SAVUNMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TESPİT") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("DEĞERLENDİRME VE HESAPLAMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Kaza Tarihi \t\t : ".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string olayTarihi = IsGucu.kazaTarihi.ToShortDateString();
            textRange = paragraph.AppendText(olayTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";


            //RaporTarihi
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Rapor Tarihi \t\t: ".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string raporTarihi = IsGucu.raporTarihi.ToShortDateString();
            textRange = paragraph.AppendText(raporTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            //Yaşam Tablosu	
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Yaşam Tablosu\t\t:".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string yasamTablosu = IsGucu.yasamTablosu;
            textRange = paragraph.AppendText(yasamTablosu.ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";

            //Davalı Kusur Oranı
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Davalı Kusur Oranı \t:".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            string kusurOrani = "%" + IsGucu.kusurOrani;
            textRange = paragraph.AppendText(kusurOrani) as WTextRange;
            textRange.CharacterFormat.FontName = "Times New Roman";


            // Kişi Bilgileri.
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Kişi Bilgileri".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();


            IWTable tablooo2 = section.AddTable();

            tablooo2.TableFormat.Borders.LineWidth = 0.4f;
            tablooo2.TableFormat.CellSpacing = 0;

            tablooo2.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooo2.ResetCells(7, 2);

            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";

            WCharacterFormat cf2 = new WCharacterFormat(document);
            cf2.FontSize = 12;
            cf2.FontName = "Times New Roman";


            tablooo2.Rows[0].Height = 45;
            tablooo2[0, 0].AddParagraph().AppendText(" Kaza Geçiren \t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo2[0, 0].Width = 200;
            tablooo2.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string kazaKisi = IsGucu.ad + " " + IsGucu.soyad;
            tablooo2[0, 1].AddParagraph().AppendText("   " + kazaKisi .ToUpper()+ "").ApplyCharacterFormat(cf1);
            tablooo2[0, 1].Width = 200;
            tablooo2.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooo2.Rows[1].Height = 45;
            tablooo2[1, 0].AddParagraph().AppendText(" Kaza Tarihi \t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo2[1, 0].Width = 200;
            tablooo2.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            string kazaTarihi = " " +IsGucu.kazaTarihi.ToShortDateString();
            tablooo2[1, 1].AddParagraph().AppendText( kazaTarihi ).ApplyCharacterFormat(cf1);
            tablooo2[1, 1].Width = 200;
            tablooo2.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooo2.Rows[2].Height = 45;
            tablooo2[2, 0].AddParagraph().AppendText(" Doğum Tarihi \t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo2[2, 0].Width = 200;
            tablooo2.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string dogumTarihi = IsGucu.dogumTarihi.ToShortDateString();
            tablooo2[2, 1].AddParagraph().AppendText("  " + dogumTarihi + "   ").ApplyCharacterFormat(cf1);
            tablooo2[2, 1].Width = 200;
            tablooo2.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooo2.Rows[3].Height = 45;
            tablooo2[3, 0].AddParagraph().AppendText(" Bitiş Tarihi \t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo2[3, 0].Width = 200;
            tablooo2.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string bakiyeOmur = "";// Bakiye Ömür Eklenecek.
            tablooo2[3, 1].AddParagraph().AppendText("  " + VefatTar.ToShortDateString() + "   ").ApplyCharacterFormat(cf1);
            tablooo2[3, 1].Width = 200;
            tablooo2.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            tablooo2.Rows[4].Height = 45;
            tablooo2[4, 0].AddParagraph().AppendText(" Net Maaş \t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo2[4, 0].Width = 200;
            tablooo2.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string gelir = IsGucu.isleyecekMaas.ToString("C", CultureInfo.CurrentCulture);
            tablooo2[4, 1].AddParagraph().AppendText("  " + gelir + "  ").ApplyCharacterFormat(cf1);
            tablooo2[4, 1].Width = 200;
            tablooo2.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooo2.Rows[5].Height = 45;
            tablooo2[5, 0].AddParagraph().AppendText(" Emekli Maaş \t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo2[5, 0].Width = 200;
            tablooo2.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string emekliMaas = IsGucu.emekliMaas.ToString("C", CultureInfo.CurrentCulture);
            tablooo2[5, 1].AddParagraph().AppendText("   " + emekliMaas + "   ").ApplyCharacterFormat(cf1);
            tablooo2[5, 1].Width = 200;
            tablooo2.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooo2.Rows[6].Height = 45;
            tablooo2[6, 0].AddParagraph().AppendText(" Askerlik \t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo2[6, 0].Width = 200;
            tablooo2.Rows[6].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string askerlikDurum = IsGucu.askerlikYapti;
            tablooo2[6, 1].AddParagraph().AppendText("  " + askerlikDurum.ToUpper() + " ").ApplyCharacterFormat(cf1);
            tablooo2[6, 1].Width = 200;
            tablooo2.Rows[6].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            paragraph = section.AddParagraph();

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Kayıp Oranları".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();


            // Kayıp Oranları  Sayısını Bulma ve Üzerinde Yeni Bir Liste Oluşturma..
            List<KayipOran> listeKayipOran = new List<KayipOran>();

            var liste2 = IsGucu.IsGucuKayipOranlar;

            var liste3 = liste2.OrderBy(o => o.baslangicTarihi);

            
            foreach(var tt1 in IsGucu.IsGucuKayipOranlar)
            {
                if(tt1.aciklama=="Hastane")
                {
                    listeKayipOran.Add(tt1);
                }

            }

            foreach(var tt2 in liste3)
            {
                if(tt2.aciklama != "Hastane")
                {
                    listeKayipOran.Add(tt2);
                }
            }

            

            int say1 = IsGucu.IsGucuKayipOranlar.Count + 2;


            IWTable tablooo3 = section.AddTable();

            tablooo3.TableFormat.Borders.LineWidth = 0.3f;
            tablooo3.TableFormat.CellSpacing = 0;

            tablooo3.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooo3.ResetCells(say1, 2);

            tablooo3.Rows[0].Height = 40;
            tablooo3[0, 0].AddParagraph().AppendText(" Tarih :".ToUpper()).ApplyCharacterFormat(cf1);
            tablooo3[0, 0].Width = 150;
            tablooo3.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



            tablooo3[0, 1].AddParagraph().AppendText(" " + "Oran".ToUpper() + " ").ApplyCharacterFormat(cf1);
            tablooo3[0, 1].Width = 150;
            tablooo3.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



            int sayac1 = 1;
            foreach(var tt4 in oranListesi)
            {
                string yazi1 = "";

            

                yazi1 = tt4.baslangicTarihi.ToShortDateString();

                tablooo3.Rows[sayac1].Height = 38;
                
                tablooo3[sayac1, 0].AddParagraph().AppendText("  " + yazi1.ToUpper() + " " ).ApplyCharacterFormat(cf1);
                tablooo3[sayac1, 0].Width = 150;
                tablooo3.Rows[sayac1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tablooo3[sayac1, 1].AddParagraph().AppendText("   % "+  tt4.kayipOrani  ).ApplyCharacterFormat(cf1);
                tablooo3[sayac1, 1].Width = 150;
                tablooo3.Rows[sayac1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                sayac1 = sayac1 + 1;

            }


            paragraph = section.AddParagraph();

            // MASRAFLAR............

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Masraflar".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            if (IsGucu.IsGucuKayipMasraflar.Count == 0)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.LineSpacing = 18f;
                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
                textRange = paragraph.AppendText("....") as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
                textRange.CharacterFormat.FontName = "Times New Roman";
            }
            else
            {
                IWTable tablooo4 = section.AddTable();

                tablooo4.TableFormat.Borders.LineWidth = 0.3f;
                tablooo4.TableFormat.CellSpacing = 0;

                tablooo4.ApplyStyle(BuiltinTableStyle.LightGrid);
                int satirSay = IsGucu.IsGucuKayipMasraflar.Count + 2;

                tablooo4.ResetCells(satirSay, 2);


                tablooo4.Rows[0].Height = 40;
                tablooo4[0, 0].AddParagraph().AppendText("   Masraf :  ").ApplyCharacterFormat(cf1);
                tablooo4[0, 0].Width = 150;
                tablooo4.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tablooo4[0, 1].AddParagraph().AppendText("   Miktar : ").ApplyCharacterFormat(cf1);
                tablooo4[0, 1].Width = 150;
                tablooo4.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                int ii4 = 1;
                decimal toplamMasrafMiktar = 0;

                foreach (var t2 in IsGucu.IsGucuKayipMasraflar)
                {

                    tablooo4.Rows[ii4].Height = 38;


                    tablooo4[ii4, 0].AddParagraph().AppendText("  " + t2.masraftur2.ToUpper() + " ").ApplyCharacterFormat(cf2);
                    tablooo4[ii4, 0].Width = 150;
                    tablooo4.Rows[ii4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    string masrafMiktar = String.Format("{0:C2}", t2.miktar);

                    tablooo4[ii4, 1].AddParagraph().AppendText(" " + masrafMiktar + "  ").ApplyCharacterFormat(cf1);
                    tablooo4[ii4, 1].Width = 150;
                    tablooo4.Rows[ii4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                    toplamMasrafMiktar = toplamMasrafMiktar + t2.miktar;
                    ii4 = ii4 + 1;

                }

                tablooo4.Rows[ii4].Height = 40;
                tablooo4[ii4, 0].AddParagraph().AppendText(" Toplam :  ".ToUpper()).ApplyCharacterFormat(cf1);
                tablooo4[ii4, 0].Width = 150;
                tablooo4.Rows[ii4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tablooo4[ii4, 1].AddParagraph().AppendText("   " + String.Format("{0:C2}", toplamMasrafMiktar)).ApplyCharacterFormat(cf1);
                tablooo4[ii4, 1].Width = 150;
                tablooo4.Rows[ii4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            }




            //İŞLEMİŞ DÖNEM YILLARA GÖRE TAZMİNAT TOPLAMLARI:



            List<GunlukBilgi> listeMaasIslemis = new List<GunlukBilgi>();
            List<GunlukBilgi> lsteMaasIsleyeecekAktif = new List<GunlukBilgi>();
            List<GunlukBilgi> listeMaasIsleyecekPasif = new List<GunlukBilgi>();

            List<YilSayiYapi> yillarIslemis = new List<YilSayiYapi>();
            List<YilSayiYapi> yillarIsleyecekAktif = new List<YilSayiYapi>();
            List<YilSayiYapi> yillarIsleyecekPasif = new List<YilSayiYapi>();


            foreach (var t in GunlukListe)
            {

                if (t.donemi == "İşlemiş")
                {

                    listeMaasIslemis.Add(t);

                    int yil1 = t.tarih.Year;
                    var yilVar = yillarIslemis.Find(o => o.yill == yil1);

                    if (yilVar == null)
                    {
                        YilSayiYapi ys = new YilSayiYapi();
                        ys.yill = yil1;
                        yillarIslemis.Add(ys);
                    }

                }
                else if (t.donemi == "Aktif")
                {
                    lsteMaasIsleyeecekAktif.Add(t);

                    int yil1 = t.tarih.Year;
                    var yilVar = yillarIsleyecekAktif.Find(o => o.yill == yil1);

                    if (yilVar == null)
                    {
                        YilSayiYapi ys = new YilSayiYapi();
                        ys.yill = yil1;
                        yillarIsleyecekAktif.Add(ys);
                    }
                }
                else if (t.donemi == "Pasif")
                {

                    listeMaasIsleyecekPasif.Add(t);

                    int yil1 = t.tarih.Year;
                    var yilVar = yillarIsleyecekPasif.Find(o => o.yill == yil1);

                    if (yilVar == null)
                    {
                        YilSayiYapi ys = new YilSayiYapi();
                        ys.yill = yil1;
                        yillarIsleyecekPasif.Add(ys);
                    }


                }


            }


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            section.AddParagraph().AppendText(" ");
            section.AddParagraph().AppendText("İşlemiş Dönem".ToUpper()).ApplyCharacterFormat(cf1);
            paragraph = section.AddParagraph();

            IWTable tabloIslemis = section.AddTable();

            tabloIslemis.TableFormat.Borders.LineWidth = 0.3f;
            tabloIslemis.TableFormat.CellSpacing = 0;

            tabloIslemis.ApplyStyle(BuiltinTableStyle.LightGrid);
            int satirSay2 = yillarIslemis.Count +1;

            tabloIslemis.ResetCells(satirSay2, 5);

            decimal genelToplam1 = 0;


            tabloIslemis[0, 0].AddParagraph().AppendText("Başlangıç  -\nBitiş".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis[0, 0].Width = 105;
            tabloIslemis.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloIslemis[0, 1].AddParagraph().AppendText("Süre  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis[0, 1].Width = 105;
            tabloIslemis.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloIslemis[0, 2].AddParagraph().AppendText("Maaş  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis[0, 2].Width = 105;
            tabloIslemis.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloIslemis[0, 3].AddParagraph().AppendText("Dönem \nTazminatı  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis[0, 3].Width = 105;
            tabloIslemis.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloIslemis[0, 4].AddParagraph().AppendText("Genel \nToplam  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIslemis[0, 4].Width = 105;
            tabloIslemis.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            var gecmisMaasDegerleri = IsGucu.IsGucuKayipMaaslar.ToList();


            if (yillarIslemis.Count == 1)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                donemBaslangic = listeMaasIslemis[0].tarih.ToShortDateString();
                int bitisIndex = listeMaasIslemis.Count - 1;
                donemBitis = listeMaasIslemis[bitisIndex].tarih.ToShortDateString();

                DateTime donemBaslangicTarihi = listeMaasIslemis[0].tarih;
                DateTime donemBitisTarihi = listeMaasIslemis[bitisIndex].tarih;

                int donemYil = yillarIslemis[0].yill;
                string donem1 = yillarIslemis[0].yill.ToString() + "-1";
                string donem2 = yillarIslemis[0].yill.ToString() + "-2";

                var yilMaas1 = gecmisMaasDegerleri.Find(o => o.yil == donem1);

                if (yilMaas1 != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", yilMaas1.netMaas);
                }
                var yilMaas2 = gecmisMaasDegerleri.Find(o => o.yil == donem2);

                if (yilMaas2 != null)
                {
                    donemYilMaas2 = String.Format("{0:C2}", yilMaas2.netMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }


                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIslemis)
                {
                    //foreach (var t3 in t2.kisiListe)
                    //{
                    //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                    //}
                    donemTazminatToplami = donemTazminatToplami + t2.miktar;

                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[0].Height = 38;
                tabloIslemis[1, 0].AddParagraph().AppendText(donemBaslangic + "- \n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 0].Width = 105;
                tabloIslemis.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 1].AddParagraph().AppendText("" + donemSure).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 1].Width = 105;
                tabloIslemis.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                if (donemBaslangicTarihi.Month > 6)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);
                }
                else if (donemBitisTarihi.Month < 7)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1)).ApplyCharacterFormat(cf2);
                }
                else if (donemBaslangicTarihi.Month < 7 && donemBitisTarihi.Month > 6)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1) +
              "-\n" + String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);
                }
                tabloIslemis[1, 2].Width = 105;
                tabloIslemis.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 3].Width = 105;
                tabloIslemis.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 4].Width = 105;
                tabloIslemis.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            }
            else if (yillarIslemis.Count == 2)
            {
                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIslemis[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIslemis[0].yill;
                string donem1 = yillarIslemis[0].yill.ToString() + "-1";
                string donem2 = yillarIslemis[0].yill.ToString() + "-2";

                var yilMaas1 = gecmisMaasDegerleri.Find(o => o.yil == donem1);

                if (yilMaas1 != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", yilMaas1.netMaas);
                }
                var yilMaas2 = gecmisMaasDegerleri.Find(o => o.yil == donem2);

                if (yilMaas2 != null)
                {
                    donemYilMaas2 = String.Format("{0:C2}", yilMaas2.netMaas);
                }


                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }


                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        //}

                        donemTazminatToplami = donemTazminatToplami + t2.miktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[1].Height = 38;
                tabloIslemis[1, 0].AddParagraph().AppendText(donemBaslangic + " - \n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 0].Width = 105;
                tabloIslemis.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 1].Width = 105;
                tabloIslemis.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                if (donemBitisTarihi.Month < 7)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText("\n" + String.Format("{0:C2}", donemYilMaas1)).ApplyCharacterFormat(cf2);

                }
                else
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1) +
                                     " - \n" + String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);
                }

                tabloIslemis[1, 2].Width = 105;
                tabloIslemis.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 3].Width = 105;
                tabloIslemis.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 4].Width = 105;
                tabloIslemis.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                //....... TABLO 2.SATIR...........
                //--------------------------------------------------------

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                DateTime donemBaslangicTarih2 = new DateTime(yillarIslemis[1].yill, 1, 1);
                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIslemis[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;

                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 - 1;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }


                //-------------------------
                int donemYilB = yillarIslemis[1].yill;
                string donem1B = yillarIslemis[1].yill.ToString() + "-1";
                string donem2B = yillarIslemis[1].yill.ToString() + "-2";

                var yilMaas1B = gecmisMaasDegerleri.Find(o => o.yil == donem1B);

                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", yilMaas1B.netMaas);
                }
                var yilMaas2B = gecmisMaasDegerleri.Find(o => o.yil == donem2B);

                if (yilMaas2B != null)
                {
                    donemYilMaas22 = String.Format("{0:C2}", yilMaas2B.netMaas);
                }



                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;

                        //}
                        donemTazminatToplami2 = donemTazminatToplami2 + t2.miktar;



                    }
                }

                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[2].Height = 38;
                tabloIslemis[2, 0].AddParagraph().AppendText(donemBaslangic2 + " - \n" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloIslemis[2, 0].Width = 105;
                tabloIslemis.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[2, 1].AddParagraph().AppendText(donemSure2);
                tabloIslemis[2, 1].Width = 105;
                tabloIslemis.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                if (donemYilMaas12 == donemYilMaas22)
                {
                    tabloIslemis[2, 2].AddParagraph().AppendText("\n" + String.Format("{0:C2}", donemYilMaas12) +
                        "").ApplyCharacterFormat(cf2);
                }
                else
                {
                    tabloIslemis[2, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas12) +
                     "\n - \n" + String.Format("{0:C2}", donemYilMaas22)).ApplyCharacterFormat(cf2);

                }
                tabloIslemis[2, 2].Width = 105;
                tabloIslemis.Rows[2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloIslemis[2, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloIslemis[2, 3].Width = 105;
                tabloIslemis.Rows[2].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[2, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis[2, 4].Width = 105;
                tabloIslemis.Rows[2].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            }
            else if (yillarIslemis.Count > 2)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIslemis[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIslemis[0].yill;
                string donem1 = yillarIslemis[0].yill.ToString() + "-1";
                string donem2 = yillarIslemis[0].yill.ToString() + "-2";

                var yilMaas1 = gecmisMaasDegerleri.Find(o => o.yil == donem1);

                if (yilMaas1 != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", yilMaas1.netMaas);
                }
                var yilMaas2 = gecmisMaasDegerleri.Find(o => o.yil == donem2);

                if (yilMaas2 != null)
                {
                    donemYilMaas2 = String.Format("{0:C2}", yilMaas2.netMaas);
                }


                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }





                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        //}

                        donemTazminatToplami = donemTazminatToplami + t2.miktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloIslemis.Rows[0].Height = 38;
                tabloIslemis[1, 0].AddParagraph().AppendText(donemBaslangic + " - \n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 0].Width = 105;
                tabloIslemis.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 1].Width = 105;
                tabloIslemis.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                if (donemBitisTarihi.Month < 7)
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText("\n" + String.Format("{0:C2}", donemYilMaas1)).ApplyCharacterFormat(cf2);

                }
                else
                {
                    tabloIslemis[1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1) +
                                     " - \n" + String.Format("{0:C2}", donemYilMaas2)).ApplyCharacterFormat(cf2);
                }

                tabloIslemis[1, 2].Width = 105;
                tabloIslemis.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 3].Width = 105;
                tabloIslemis.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis[1, 4].Width = 105;
                tabloIslemis.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                //-----------------------------------------------
                // YILLAR BÖLÜMÜ.............


                int yilSayac = 1;

                int yilSayisi = yillarIslemis.Count;


                for (int i = 0; yilSayac < yilSayisi - 1; i++)

                {
                    var donemX = yillarIslemis[yilSayac].yill;


                    yilSayac = yilSayac + 1;

                    string donemBaslangicX = "";
                    string donemBitisX = "";
                    string donemSureX = "";
                    string donemYilMaas1X = "";
                    string donemYilMaas2X = "";
                    string donemTazminatiX = "";
                    string genelTazminatToplamiX = "";

                    donemBaslangicX = "01.01." + donemX.ToString();
                    DateTime donemBaslangicTarihX = new DateTime(donemX, 1, 1);
                    DateTime donemBtisTarihiX = new DateTime(donemX, 12, 31);
                    donemBitisX = "31.12." + donemX.ToString();
                    donemSureX = "12 Ay";


                    string donem1X = donemX.ToString() + "-1";
                    string donem2X = donemX.ToString() + "-2";


                    var yilMaas1X = gecmisMaasDegerleri.Find(o => o.yil == donem1X);

                    if (yilMaas1X != null)
                    {
                        donemYilMaas1X = String.Format("{0:C2}", yilMaas1X.netMaas);
                    }
                    var yilMaas2X = gecmisMaasDegerleri.Find(o => o.yil == donem2X);

                    if (yilMaas2X != null)
                    {
                        donemYilMaas2X = String.Format("{0:C2}", yilMaas2X.netMaas);
                    }


                    decimal donemTazminatToplamiX = 0;

                    foreach (var t2 in listeMaasIslemis)
                    {

                        if (t2.tarih >= donemBaslangicTarihX && t2.tarih <= donemBtisTarihiX)
                        {
                            //foreach (var t3 in t2.kisiListe)
                            //{
                            //    donemTazminatToplamiX = donemTazminatToplamiX + t3.alinanMiktar;


                            //}

                            donemTazminatToplamiX = donemTazminatToplamiX + t2.miktar;


                        }
                    }

                    donemTazminatiX = String.Format("{0:C2}", donemTazminatToplamiX);


                    genelToplam1 = genelToplam1 + donemTazminatToplamiX;

                    genelTazminatToplamiX = String.Format("{0:C2}", genelToplam1);


                    tabloIslemis.Rows[yilSayac].Height = 38;
                    // tabloya yazma
                    tabloIslemis[yilSayac, 0].AddParagraph().AppendText(donemBaslangicX + "- \n" + donemBitisX).ApplyCharacterFormat(cf2);
                    tabloIslemis[yilSayac, 0].Width = 105;
                    tabloIslemis.Rows[yilSayac].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloIslemis[yilSayac, 1].AddParagraph().AppendText(donemSureX).ApplyCharacterFormat(cf2);
                    tabloIslemis[yilSayac, 1].Width = 105;
                    tabloIslemis.Rows[yilSayac].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    if (donemBitisTarihi.Month < 7)
                    {
                        tabloIslemis[yilSayac, 2].AddParagraph().AppendText("\n" + String.Format("{0:C2}", donemYilMaas1X)).ApplyCharacterFormat(cf2);

                    }
                    else
                    {
                        tabloIslemis[yilSayac, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas1X) +
                                         "\n - \n" + String.Format("{0:C2}", donemYilMaas2X)).ApplyCharacterFormat(cf2);
                    }
                    tabloIslemis[yilSayac, 2].Width = 105;
                    tabloIslemis.Rows[yilSayac].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloIslemis[yilSayac, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminatiX)).ApplyCharacterFormat(cf2);
                    tabloIslemis[yilSayac, 3].Width = 105;
                    tabloIslemis.Rows[yilSayac].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                    tabloIslemis[yilSayac, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplamiX)).ApplyCharacterFormat(cf2);
                    tabloIslemis[yilSayac, 4].Width = 105;
                    tabloIslemis.Rows[yilSayac].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                }





                //....... SON BÖLÜM............
                //--------------------------------------------------------

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIslemis[bitisIndex2].tarih;


                DateTime donemBaslangicTarih2 = new DateTime(donemBitisTarihi22.Year, 1, 1);

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;

                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 ;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }


                //-------------------------
                int donemYilB = yillarIslemis[yilSayac].yill;
                string donem1B = yillarIslemis[yilSayac].yill.ToString() + "-1";
                string donem2B = yillarIslemis[yilSayac].yill.ToString() + "-2";

                var yilMaas1B = gecmisMaasDegerleri.Find(o => o.yil == donem1B);

                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", yilMaas1B.netMaas);
                }
                var yilMaas2B = gecmisMaasDegerleri.Find(o => o.yil == donem2B);

                if (yilMaas2B != null)
                {
                    donemYilMaas22 = String.Format("{0:C2}", yilMaas2B.netMaas);
                }



                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in listeMaasIslemis)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        donemTazminatToplami2 = donemTazminatToplami2 + t2.miktar;

                    }
                }

                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloIslemis.Rows[yilSayac + 1].Height = 38;
                tabloIslemis[yilSayac + 1, 0].AddParagraph().AppendText(donemBaslangic2 + " - \n" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloIslemis[yilSayac+1, 0].Width = 105;
                tabloIslemis.Rows[yilSayac+1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[yilSayac + 1, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloIslemis[yilSayac + 1, 1].Width = 105;
                tabloIslemis.Rows[yilSayac + 1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

       

                if (donemYilMaas12 == donemYilMaas22)
                {
                    tabloIslemis[yilSayac + 1, 2].AddParagraph().AppendText("\n" + String.Format("{0:C2}", donemYilMaas12) +
                        "").ApplyCharacterFormat(cf2);
                }
                else
                {
                    tabloIslemis[yilSayac + 1, 2].AddParagraph().AppendText(String.Format("{0:C2}", donemYilMaas12) +
                     "- \n" + String.Format("{0:C2}", donemYilMaas22)).ApplyCharacterFormat(cf2);

                }
                tabloIslemis[yilSayac + 1, 2].Width = 105;
                tabloIslemis.Rows[yilSayac + 1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[yilSayac + 1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloIslemis[yilSayac + 1, 3].Width = 105;
                tabloIslemis.Rows[yilSayac + 1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloIslemis[yilSayac + 1, 4].AddParagraph().
                    AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloIslemis[yilSayac + 1, 4].Width = 105;
                tabloIslemis.Rows[yilSayac + 1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;





            }











            // İŞLEYECEK DÖNEM...

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            section.AddParagraph().AppendText(" ");
            section.AddParagraph().AppendText("İşleyecek Aktif Dönem ".ToUpper()).ApplyCharacterFormat(cf1);
            paragraph = section.AddParagraph();

            section.AddParagraph().AppendText("%10 Arttırım %10 Azaltım Uygulanmıştır. ".ToUpper()).ApplyCharacterFormat(cf2);

            IWTable tabloAktif = section.AddTable();

            tabloAktif.TableFormat.Borders.LineWidth = 0.3f;
            tabloAktif.TableFormat.CellSpacing = 0;

            tabloAktif.ApplyStyle(BuiltinTableStyle.LightGrid);
            int satirSay2A = yillarIsleyecekAktif.Count + 2;

            tabloAktif.ResetCells(satirSay2A, 5);

            decimal genelToplam1A = 0;

            tabloAktif.Rows[0].Height = 40;

            tabloAktif[0, 0].AddParagraph().AppendText("Başlangıç - \nBitiş".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif[0 , 0].Width = 105;
            tabloAktif.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloAktif[0, 1].AddParagraph().AppendText("Süre  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif[0, 1].Width = 105;
            tabloAktif.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloAktif[0, 2].AddParagraph().AppendText("Maaş ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif[0, 2].Width = 105;
            tabloAktif.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloAktif[0, 3].AddParagraph().AppendText("Dönem \nTazminatı  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif[0, 3].Width = 105;
            tabloAktif.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloAktif[0, 4].AddParagraph().AppendText("Genel \nToplam  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloAktif[0, 4].Width = 105;
            tabloAktif.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



            if (yillarIsleyecekAktif.Count == 1)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                donemBaslangic = lsteMaasIsleyeecekAktif[0].tarih.ToShortDateString();
                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;
                donemBitis = lsteMaasIsleyeecekAktif[bitisIndex].tarih.ToShortDateString();

                DateTime donemBaslangicTarihi = lsteMaasIsleyeecekAktif[0].tarih;
                DateTime donemBitisTarihi = lsteMaasIsleyeecekAktif[bitisIndex].tarih;

                int donemYil = yillarIsleyecekAktif[0].yill;



                var yilMaas1 = SonMaas;

                if (SonMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", SonMaas);
                }



                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }


                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {
                    //foreach (var t3 in t2.kisiListe)
                    //{
                    //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                    //}

                    donemTazminatToplami = donemTazminatToplami + t2.miktar;

                }


                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloAktif.Rows[1].Height = 38;

                tabloAktif[1, 0].AddParagraph().AppendText(donemBaslangic + "-\n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloAktif[1, 0].Width = 105;
                tabloAktif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloAktif[1, 1].Width = 105;
                tabloAktif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloAktif[1, 2].AddParagraph().AppendText(" " + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloAktif[1, 2].Width = 105;
                tabloAktif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloAktif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloAktif[1, 3].Width = 105;
                tabloAktif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif[1, 4].Width = 105;
                tabloAktif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;




            }
            else if (yillarIsleyecekAktif.Count == 2)
            {
                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = lsteMaasIsleyeecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = lsteMaasIsleyeecekAktif[0].tarih;
                //    DateTime donemBitisTarihi = listeMaasIslemis[bitisIndex].tarih2;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekAktif[0].yill;

                var yilMaas1 = SonMaas;

                if (SonMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", SonMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);


                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        //}

                        donemTazminatToplami = donemTazminatToplami + t2.miktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);

                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloAktif.Rows[1].Height = 38;

                tabloAktif[1, 0].AddParagraph().AppendText(donemBaslangic + " - \n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloAktif[1, 0].Width = 105;
                tabloAktif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2) ;
                tabloAktif[1, 1].Width = 105;
                tabloAktif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloAktif[1, 2].Width = 105;
                tabloAktif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloAktif[1, 3].Width = 105;
                tabloAktif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif[1, 4].Width = 105;
                tabloAktif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                // 2.Satır....

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                DateTime donemBaslangicTarih2 = new DateTime(yillarIslemis[1].yill, 1, 1);
                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIslemis[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();

                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;

                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 - 1;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }


                var yilMaas1B = SonMaas;

                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", SonMaas);
                }

                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;


                        //}

                        donemTazminatToplami2 = donemTazminatToplami2 + t2.miktar;


                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloAktif.Rows[2].Height = 38;

                tabloAktif[2, 0].AddParagraph().AppendText(donemBaslangic2 + "-\n" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloAktif[2, 0].Width = 105;
                tabloAktif.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[2, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloAktif[2, 1].Width = 105;
                tabloAktif.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[2, 2].AddParagraph().AppendText("\n" + donemYilMaas12).ApplyCharacterFormat(cf2);
                tabloAktif[2, 0].Width = 105;
                tabloAktif.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloAktif[2, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloAktif[2, 3].Width = 105;
                tabloAktif.Rows[2].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[2, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif[2, 4].Width = 105;
                tabloAktif.Rows[2].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



            }
            else if (yillarIsleyecekAktif.Count > 2)
            {
                // ----------------------------İLK BÖLÜM------------------------------------

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = yillarIsleyecekAktif.Count - 1;

                DateTime donemBaslangicTarihi = lsteMaasIsleyeecekAktif[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekAktif[0].yill;

                var yilMaas1 = SonMaas;

                if (SonMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", SonMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);


                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }


                decimal donemTazminatToplami = 0;
                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {
                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;
                        //}

                        donemTazminatToplami = donemTazminatToplami + t2.miktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloAktif.Rows[1].Height = 38;
                tabloAktif[1, 0].AddParagraph().AppendText(donemBaslangic + "-\n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloAktif[1, 0].Width = 105;
                tabloAktif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloAktif[1, 1].Width = 105;
                tabloAktif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloAktif[1, 2].AddParagraph().AppendText(" " + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloAktif[1, 2].Width = 105;
                tabloAktif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloAktif[1, 3].Width = 105;
                tabloAktif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif[1, 4].Width = 105;
                tabloAktif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                //Ara yıllar..
                int yilSayac = 1;

                int yilSayisi = yillarIsleyecekAktif.Count;


                for (int i = 0; yilSayac < yilSayisi - 1; i++)

                {
                    var donemX = yillarIsleyecekAktif[yilSayac].yill;


                    yilSayac = yilSayac + 1;

                    string donemBaslangicX = "";
                    string donemBitisX = "";
                    string donemSureX = "";
                    string donemYilMaas1X = "";
                    string donemYilMaas2X = "";
                    string donemTazminatiX = "";
                    string genelTazminatToplamiX = "";

                    donemBaslangicX = "01.01." + donemX.ToString();
                    DateTime donemBaslangicTarihX = new DateTime(donemX, 1, 1);
                    DateTime donemBtisTarihiX = new DateTime(donemX, 12, 31);
                    donemBitisX = "31.12." + donemX.ToString();
                    donemSureX = "12 Ay";


                    //string donem1X = donemX.ToString() + "-1";
                    //string donem2X = donemX.ToString() + "-2";


                    var yilMaas1X = SonMaas;
                    if (SonMaas != null)
                    {
                        donemYilMaas1X = String.Format("{0:C2}", SonMaas);
                    }



                    decimal donemTazminatToplamiX = 0;

                    foreach (var t2 in lsteMaasIsleyeecekAktif)
                    {

                        if (t2.tarih >= donemBaslangicTarihX && t2.tarih <= donemBtisTarihiX)
                        {
                            //foreach (var t3 in t2.kisiListe)
                            //{
                            //    donemTazminatToplamiX = donemTazminatToplamiX + t3.alinanMiktar;


                            //}
                            donemTazminatToplamiX = donemTazminatToplamiX + t2.miktar;



                        }
                    }

                    donemTazminatiX = String.Format("{0:C2}", donemTazminatToplamiX);

                    genelToplam1 = genelToplam1 + donemTazminatToplamiX;

                    genelTazminatToplamiX = String.Format("{0:C2}", genelToplam1);


                    tabloAktif.Rows[yilSayac].Height = 38;
                    tabloAktif[yilSayac, 0].AddParagraph().AppendText(donemBaslangicX + "-\n" + donemBitisX).ApplyCharacterFormat(cf2);
                    tabloAktif[yilSayac, 0].Width = 105;
                    tabloAktif.Rows[yilSayac].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloAktif[yilSayac, 1].AddParagraph().AppendText(donemSureX).ApplyCharacterFormat(cf2);
                    tabloAktif[yilSayac, 1].Width = 105;
                    tabloAktif.Rows[yilSayac].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloAktif[yilSayac, 2].AddParagraph().AppendText("" + donemYilMaas1X).ApplyCharacterFormat(cf2);
                    tabloAktif[yilSayac, 2].Width = 105;
                    tabloAktif.Rows[yilSayac].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                    tabloAktif[yilSayac, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminatiX)).ApplyCharacterFormat(cf2);
                    tabloAktif[yilSayac, 3].Width = 105;
                    tabloAktif.Rows[yilSayac].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloAktif[yilSayac, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplamiX));
                    tabloAktif[yilSayac, 4].Width = 105;
                    tabloAktif.Rows[yilSayac].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                }


                //---- Son Bölüm


                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";

                int sonIndex = yillarIsleyecekAktif.Count() - 1;

                int sonYil = yillarIsleyecekAktif[sonIndex].yill;
                DateTime donemBaslangicTarih2 = new DateTime(sonYil, 1, 1);
                int bitisIndex2 = lsteMaasIsleyeecekAktif.Count - 1;
                DateTime donemBitisTarihi22 = lsteMaasIsleyeecekAktif[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();

                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;


                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }


                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 ;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }



                var yilMaas1B = SonMaas;



                donemYilMaas12 = String.Format("{0:C2}", SonMaas);


                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;

                        //}
                        donemTazminatToplami2 = donemTazminatToplami2 + t2.miktar;


                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami2 = String.Format("{0:C2}", genelToplam1);




                tabloAktif.Rows[yilSayac + 1].Height = 38;

                tabloAktif[yilSayac + 1, 0].AddParagraph().AppendText(donemBaslangic2 + " -\n" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloAktif[yilSayac+1, 0].Width = 105;
                tabloAktif.Rows[yilSayac +1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[yilSayac + 1, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloAktif[yilSayac+1, 1].Width = 105;
                tabloAktif.Rows[yilSayac+1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[yilSayac + 1, 2].AddParagraph().AppendText(" " + donemYilMaas12).ApplyCharacterFormat(cf2);
                tabloAktif[yilSayac + 1, 2].Width = 105;
                tabloAktif.Rows[yilSayac + 1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[yilSayac + 1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloAktif[yilSayac + 1, 3].Width = 105;
                tabloAktif.Rows[yilSayac + 1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloAktif[yilSayac + 1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloAktif[yilSayac + 1, 4].Width = 105;
                tabloAktif.Rows[yilSayac + 1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            }







            //-------------------------------------------
            // PASİF DÖNEM...
            //---------------------------------------------------

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            section.AddParagraph().AppendText(" ");
            section.AddParagraph().AppendText("Pasif Dönem".ToUpper()).ApplyCharacterFormat(cf1);
            paragraph = section.AddParagraph();
            IWTable tabloPasif = section.AddTable();

            tabloPasif.TableFormat.Borders.LineWidth = 0.3f;
            tabloPasif.TableFormat.CellSpacing = 0;

            tabloPasif.ApplyStyle(BuiltinTableStyle.LightGrid);
            int satirSay3A = yillarIsleyecekPasif.Count + 2;

            tabloPasif.ResetCells(satirSay3A, 5);

            decimal genelToplam2A = 0;

            tabloPasif.Rows[0].Height = 40;
            tabloPasif[0, 0].AddParagraph().AppendText("Başlangıç -\nBitiş".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif[0, 0].Width = 105;
            tabloPasif.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloPasif[0, 1].AddParagraph().AppendText("Süre ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif[0, 1].Width = 105;
            tabloPasif.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloPasif[0, 2].AddParagraph().AppendText("Maaş  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif[0, 2].Width = 105;
            tabloPasif.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloPasif[0, 3].AddParagraph().AppendText("Dönem \nTazminatı  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif[0, 3].Width = 105;
            tabloPasif.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloPasif[0, 4].AddParagraph().AppendText("Genel \nToplam  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloPasif[0, 4].Width = 105;
            tabloPasif.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;




            if (yillarIsleyecekPasif.Count == 1)
            {

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                donemBaslangic = listeMaasIsleyecekPasif[0].tarih.ToShortDateString();
                int bitisIndex = listeMaasIsleyecekPasif.Count - 1;
                donemBitis = listeMaasIsleyecekPasif[bitisIndex].tarih.ToShortDateString();

                DateTime donemBaslangicTarihi = listeMaasIsleyecekPasif[0].tarih;
                DateTime donemBitisTarihi = listeMaasIsleyecekPasif[bitisIndex].tarih;

                int donemYil = yillarIsleyecekAktif[0].yill;


                if (IsGucu.emekliMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", IsGucu.emekliMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);


                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }


                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {
                    //foreach (var t3 in t2.kisiListe)
                    //{
                    //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                    //}

                    donemTazminatToplami = donemTazminatToplami + t2.miktar;

                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloPasif.Rows[1].Height = 38;
                tabloPasif[1, 0].AddParagraph().AppendText(donemBaslangic + " -\n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloPasif[1, 0].Width = 105;
                tabloPasif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloPasif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloPasif[1, 1].Width = 105;
                tabloPasif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloPasif[1, 2].Width = 105;
                tabloPasif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloPasif[1, 3].Width = 105;
                tabloPasif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloPasif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloPasif[1, 4].Width = 105;
                tabloPasif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



            }
            else if (yillarIsleyecekPasif.Count == 2)
            {
                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = listeMaasIsleyecekPasif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIsleyecekPasif[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekPasif[0].yill;

                var yilMaas1 = IsGucu.emekliMaas;

                if (IsGucu.emekliMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", IsGucu.emekliMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;


                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        //}

                        donemTazminatToplami = donemTazminatToplami + t2.miktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);
                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);


                tabloPasif.Rows[1].Height = 38;
                tabloPasif[1, 0].AddParagraph().AppendText(donemBaslangic + "-\n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloPasif[1, 0].Width = 105;
                tabloPasif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloPasif[1, 1].Width = 105;
                tabloPasif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 2].AddParagraph().AppendText("" + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloPasif[1, 2].Width = 105;
                tabloPasif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloPasif[1, 3].Width = 105;
                tabloPasif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloPasif[1, 4].Width = 105;
                tabloPasif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                // 2.bölüm.....

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";


                DateTime donemBaslangicTarih2 = new DateTime(yillarIsleyecekPasif[1].yill, 1, 1);
                int bitisIndex2 = listeMaasIslemis.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIsleyecekPasif[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;

                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;

                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 - 1;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }


                int donemYil2 = yillarIsleyecekPasif[1].yill;



                var yilMaas1B = IsGucu.emekliMaas;



                if (yilMaas1B != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", IsGucu.emekliMaas);
                }



                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in lsteMaasIsleyeecekAktif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;


                        //}

                        donemTazminatToplami2 = donemTazminatToplami2 + t2.miktar;


                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami2 = String.Format("{0:C2}", genelToplam1);


                tabloPasif.Rows[2].Height = 38;
                tabloPasif[2, 0].AddParagraph().AppendText(donemBaslangic2 + "-\n" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloPasif[2, 0].Width = 105;
                tabloPasif.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
              
                tabloPasif[2, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloPasif[2, 1].Width = 105;
                tabloPasif.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[2, 2].AddParagraph().AppendText(" " + donemYilMaas12).ApplyCharacterFormat(cf2);
                tabloPasif[2, 2].Width = 105;
                tabloPasif.Rows[2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[2, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloPasif[2, 3].Width = 105;
                tabloPasif.Rows[2].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[2, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami2)).ApplyCharacterFormat(cf2);
                tabloPasif[2, 4].Width = 105;
                tabloPasif.Rows[2].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            }
            else if (yillarIsleyecekPasif.Count > 2)
            {

                // ----------------------------İLK BÖLÜM------------------------------------

                string donemBaslangic = "";
                string donemBitis = "";
                string donemSure = "";
                string donemYilMaas1 = "";
                string donemYilMaas2 = "";
                string donemTazminati = "";
                string genelTazminatToplami = "";

                int bitisIndex = yillarIsleyecekPasif.Count - 1;

                DateTime donemBaslangicTarihi = listeMaasIsleyecekPasif[0].tarih;

                donemBaslangic = donemBaslangicTarihi.ToShortDateString();

                DateTime donemBitisTarihi = new DateTime(donemBaslangicTarihi.Year, 12, 31);
                donemBitis = donemBitisTarihi.ToShortDateString();

                int donemYil = yillarIsleyecekPasif[0].yill;

                var yilMaas1 = IsGucu.emekliMaas;

                if (IsGucu.emekliMaas != null)
                {
                    donemYilMaas1 = String.Format("{0:C2}", IsGucu.emekliMaas);
                }

                int aySay1 = 0;

                DateTime tmpdonemBaslangicTarihi = donemBaslangicTarihi;
                DateTime tmpdonemBaslangicTarihiB = donemBaslangicTarihi;
                while (tmpdonemBaslangicTarihi <= donemBitisTarihi)
                {
                    aySay1 = aySay1 + 1;
                    tmpdonemBaslangicTarihi = tmpdonemBaslangicTarihi.AddMonths(1);
                }

                aySay1 = aySay1 - 1;

                int gunSay1 = 0;

                DateTime tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihiB;
                tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddMonths(aySay1);

                while (tmpdonemBaslangicTarihi2 <= donemBitisTarihi)
                {

                    gunSay1 = gunSay1 + 1;

                    tmpdonemBaslangicTarihi2 = tmpdonemBaslangicTarihi2.AddDays(1);

                }
                gunSay1 = gunSay1 - 1;

                if (aySay1 != 0)
                {
                    donemSure = donemSure + aySay1.ToString() + " Ay ";
                }
                if (gunSay1 != 0)
                {
                    donemSure = donemSure + gunSay1 + " Gün";
                }

                decimal donemTazminatToplami = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {

                    if (t2.tarih >= donemBaslangicTarihi && t2.tarih <= donemBitisTarihi)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami = donemTazminatToplami + t3.alinanMiktar;


                        //}

                        donemTazminatToplami = donemTazminatToplami + t2.miktar;


                    }
                }
                donemTazminati = String.Format("{0:C2}", donemTazminatToplami);


                genelToplam1 = genelToplam1 + donemTazminatToplami;

                genelTazminatToplami = String.Format("{0:C2}", genelToplam1);

                tabloPasif.Rows[1].Height = 38;
                tabloPasif[1, 0].AddParagraph().AppendText(donemBaslangic + "-\n" + donemBitis).ApplyCharacterFormat(cf2);
                tabloPasif[1, 0].Width = 105;
                tabloPasif.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 1].AddParagraph().AppendText(donemSure).ApplyCharacterFormat(cf2);
                tabloPasif[1, 1].Width = 105;
                tabloPasif.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 2].AddParagraph().AppendText(" " + donemYilMaas1).ApplyCharacterFormat(cf2);
                tabloPasif[1, 2].Width = 105;
                tabloPasif.Rows[1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloPasif[1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati)).ApplyCharacterFormat(cf2);
                tabloPasif[1, 3].Width = 105;
                tabloPasif.Rows[1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami)).ApplyCharacterFormat(cf2);
                tabloPasif[1, 4].Width = 105;
                tabloPasif.Rows[1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;





                // ----------------------YILLAR-------------------------------------------

                int yilSayac = 1;

                int yilSayisi = yillarIsleyecekPasif.Count;


                for (int i = 0; yilSayac < yilSayisi - 1; i++)

                {
                    var donemX = yillarIsleyecekPasif[yilSayac].yill;


                    yilSayac = yilSayac + 1;

                    string donemBaslangicX = "";
                    string donemBitisX = "";
                    string donemSureX = "";
                    string donemYilMaas1X = "";
                    string donemYilMaas2X = "";
                    string donemTazminatiX = "";
                    string genelTazminatToplamiX = "";

                    donemBaslangicX = "01.01." + donemX.ToString();
                    DateTime donemBaslangicTarihX = new DateTime(donemX, 1, 1);
                    DateTime donemBtisTarihiX = new DateTime(donemX, 12, 31);
                    donemBitisX = "31.12." + donemX.ToString();
                    donemSureX = "12 Ay";


                    //string donem1X = donemX.ToString() + "-1";
                    //string donem2X = donemX.ToString() + "-2";


                    var yilMaas1X = IsGucu.emekliMaas;
                    if (IsGucu.emekliMaas != null)
                    {
                        donemYilMaas1X = String.Format("{0:C2}", IsGucu.emekliMaas);
                    }



                    decimal donemTazminatToplamiX = 0;

                    foreach (var t2 in listeMaasIsleyecekPasif)
                    {

                        if (t2.tarih >= donemBaslangicTarihX && t2.tarih <= donemBtisTarihiX)
                        {
                            //foreach (var t3 in t2.kisiListe)
                            //{
                            //    donemTazminatToplamiX = donemTazminatToplamiX + t3.alinanMiktar;


                            //}

                            donemTazminatToplamiX = donemTazminatToplamiX + t2.miktar;


                        }
                    }

                    donemTazminatiX = String.Format("{0:C2}", donemTazminatToplamiX);

                    genelToplam1 = genelToplam1 + donemTazminatToplamiX;

                    genelTazminatToplamiX = String.Format("{0:C2}", genelToplam1);


                    tabloPasif.Rows[yilSayac].Height = 38;

                    tabloPasif[yilSayac, 0].AddParagraph().AppendText(donemBaslangicX + "\n" + donemBitisX).ApplyCharacterFormat(cf2);
                    tabloPasif[yilSayac, 0].Width = 105;
                    tabloPasif.Rows[yilSayac].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloPasif[yilSayac, 1].AddParagraph().AppendText(donemSureX).ApplyCharacterFormat(cf2);
                    tabloPasif[yilSayac, 1].Width = 105;
                    tabloPasif.Rows[yilSayac].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloPasif[yilSayac, 2].AddParagraph().AppendText(" " + donemYilMaas1X).ApplyCharacterFormat(cf2);
                    tabloPasif[yilSayac, 2].Width = 105;
                    tabloPasif.Rows[yilSayac].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloPasif[yilSayac, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminatiX)).ApplyCharacterFormat(cf2);
                    tabloPasif[yilSayac, 3].Width = 105;
                    tabloPasif.Rows[yilSayac].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloPasif[yilSayac, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplamiX)).ApplyCharacterFormat(cf2);
                    tabloPasif[yilSayac, 4].Width = 105;
                    tabloPasif.Rows[yilSayac].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                }


                //****************************************************************

                //---------------------------- SON BÖLÜM--------------------------------------

                string donemBaslangic2 = "";
                string donemBitis2 = "";
                string donemSure2 = "";
                string donemYilMaas12 = "";
                string donemYilMaas22 = "";
                string donemTazminati2 = "";
                string genelTazminatToplami2 = "";

                int sonIndex = yillarIsleyecekPasif.Count() - 1;

                int sonYil = yillarIsleyecekPasif[sonIndex].yill;
                DateTime donemBaslangicTarih2 = new DateTime(sonYil, 1, 1);
                int bitisIndex2 = listeMaasIsleyecekPasif.Count - 1;
                DateTime donemBitisTarihi22 = listeMaasIsleyecekPasif[bitisIndex2].tarih;

                donemBaslangic2 = donemBaslangicTarih2.ToShortDateString();
                donemBitis2 = donemBitisTarihi22.ToShortDateString();


                int yilSay2 = 0;
                int aySay2 = 0;
                int gunSay2 = 0;


                DateTime tmpdonemBaslangicTarihi22 = donemBaslangicTarih2;
                DateTime tmpdonemBaslangicTarihi22B = donemBaslangicTarih2;

                while (tmpdonemBaslangicTarihi22 <= donemBitisTarihi22)
                {
                    yilSay2 = yilSay2 + 1;

                    tmpdonemBaslangicTarihi22 = tmpdonemBaslangicTarihi22.AddYears(1);
                }

                yilSay2 = yilSay2 - 1;


                DateTime tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi22B.AddYears(aySay2);

                while (tmpdonemBaslangicTarihi33a <= donemBitisTarihi22)
                {
                    aySay2 = aySay2 + 1;

                    tmpdonemBaslangicTarihi33a = tmpdonemBaslangicTarihi33a.AddMonths(1);
                }

                aySay2 = aySay2 - 1;



                DateTime tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi22B.AddYears(yilSay2);
                tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddMonths(aySay2);

                while (tmpdonemBaslangicTarihi33 <= donemBitisTarihi22)
                {
                    gunSay2 = gunSay2 + 1;

                    tmpdonemBaslangicTarihi33 = tmpdonemBaslangicTarihi33.AddDays(1);
                }

                gunSay2 = gunSay2 ;

                if (yilSay2 != 0)
                {
                    donemSure2 = donemSure2 + yilSay2.ToString() + " Yıl ";
                }

                if (aySay2 != 0)
                {
                    donemSure2 = donemSure2 + aySay2.ToString() + " Ay ";
                }

                if (gunSay2 != 0)
                {
                    donemSure2 = donemSure2 + gunSay2.ToString() + " Gün";
                }

                int donemYil2 = yillarIsleyecekPasif[1].yill;
                //string donem12 = yillarIslemis[1].yill.ToString() + "-1";
                //string donem22 = yillarIslemis[1].yill.ToString() + "-2";



                var yilMaas1B = IsGucu.emekliMaas;



                if (IsGucu.emekliMaas != null)
                {
                    donemYilMaas12 = String.Format("{0:C2}", IsGucu.emekliMaas);
                }

                decimal donemTazminatToplami2 = 0;

                foreach (var t2 in listeMaasIsleyecekPasif)
                {

                    if (t2.tarih >= donemBaslangicTarih2 && t2.tarih <= donemBitisTarihi22)
                    {
                        //foreach (var t3 in t2.kisiListe)
                        //{
                        //    donemTazminatToplami2 = donemTazminatToplami2 + t3.alinanMiktar;


                        //}

                        donemTazminatToplami2 = donemTazminatToplami2 + t2.miktar;


                    }
                }
                donemTazminati2 = String.Format("{0:C2}", donemTazminatToplami2);

                genelToplam1 = genelToplam1 + donemTazminatToplami2;

                genelTazminatToplami2 = String.Format("{0:C2}", genelToplam1);


                tabloPasif.Rows[yilSayac + 1].Height = 38;

                tabloPasif[yilSayac + 1, 0].AddParagraph().AppendText(donemBaslangic2 + "\n" + donemBitis2).ApplyCharacterFormat(cf2);
                tabloPasif[yilSayac+1, 0].Width = 105;
                tabloPasif.Rows[yilSayac+1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[yilSayac + 1, 1].AddParagraph().AppendText(donemSure2).ApplyCharacterFormat(cf2);
                tabloPasif[yilSayac + 1, 1].Width = 105;
                tabloPasif.Rows[yilSayac + 1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[yilSayac + 1, 2].AddParagraph().AppendText(" " + donemYilMaas12).ApplyCharacterFormat(cf2);
                tabloPasif[yilSayac + 1, 2].Width = 105;
                tabloPasif.Rows[yilSayac + 1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


                tabloPasif[yilSayac + 1, 3].AddParagraph().AppendText(String.Format("{0:C2}", donemTazminati2)).ApplyCharacterFormat(cf2);
                tabloPasif[yilSayac + 1, 3].Width = 105;
                tabloPasif.Rows[yilSayac + 1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloPasif[yilSayac + 1, 4].AddParagraph().AppendText(String.Format("{0:C2}", genelTazminatToplami2)).ApplyCharacterFormat(cf2);
                tabloPasif[yilSayac + 1, 4].Width = 105;
                tabloPasif.Rows[yilSayac + 1].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            }


            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);



            var sayfa = new Views.PdfV.PdfView(stream, "IsgucuKayipRapor.docx", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);



            //Save the stream as a file in the device and invoke it for viewing
            //var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("IsgucuKayipRapor.docx", "application/msword", stream);


        }


        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        private class YilSayiYapi
        {
            public int yill { get; set; }
        }
    }



}
